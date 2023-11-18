using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class MainFrm : Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SaveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.SaveFileDialog3 = new System.Windows.Forms.SaveFileDialog();
            this.NotifyUpdates = new System.Windows.Forms.NotifyIcon(this.components);
            this.OpenFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.Button28 = new WinPaletter.UI.WP.Button();
            this.previewContainer = new WinPaletter.UI.WP.GroupBox();
            this.Select_WXP = new WinPaletter.UI.WP.RadioImage();
            this.Select_WVista = new WinPaletter.UI.WP.RadioImage();
            this.tabs_preview = new WinPaletter.UI.WP.TablessControl();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.pnl_preview = new System.Windows.Forms.Panel();
            this.WXP_Alert2 = new WinPaletter.UI.WP.AlertBox();
            this.ActionCenter = new WinPaletter.UI.Simulation.WinElement();
            this.start = new WinPaletter.UI.Simulation.WinElement();
            this.taskbar = new WinPaletter.UI.Simulation.WinElement();
            this.Window2 = new WinPaletter.UI.Simulation.Window();
            this.Window1 = new WinPaletter.UI.Simulation.Window();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Label8 = new WinPaletter.UI.WP.LabelAlt();
            this.setting_icon_preview = new WinPaletter.UI.WP.LabelAlt();
            this.lnk_preview = new WinPaletter.UI.WP.LabelAlt();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.pnl_preview_classic = new System.Windows.Forms.Panel();
            this.ClassicWindow1 = new WinPaletter.UI.Retro.WindowR();
            this.ClassicWindow2 = new WinPaletter.UI.Retro.WindowR();
            this.ClassicTaskbar = new WinPaletter.UI.Retro.PanelRaisedR();
            this.ButtonR4 = new WinPaletter.UI.Retro.ButtonR();
            this.ButtonR3 = new WinPaletter.UI.Retro.ButtonR();
            this.ButtonR2 = new WinPaletter.UI.Retro.ButtonR();
            this.Select_W7 = new WinPaletter.UI.WP.RadioImage();
            this.Button23 = new WinPaletter.UI.WP.Button();
            this.Select_W81 = new WinPaletter.UI.WP.RadioImage();
            this.Button1 = new WinPaletter.UI.WP.Button();
            this.Select_W10 = new WinPaletter.UI.WP.RadioImage();
            this.Button15 = new WinPaletter.UI.WP.Button();
            this.Select_W11 = new WinPaletter.UI.WP.RadioImage();
            this.PictureBox21 = new System.Windows.Forms.PictureBox();
            this.themename_lbl = new System.Windows.Forms.Label();
            this.author_lbl = new System.Windows.Forms.Label();
            this.TablessControl1 = new WinPaletter.UI.WP.TablessControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.PaletteContainer_W11 = new System.Windows.Forms.Panel();
            this.GroupBox13 = new WinPaletter.UI.WP.GroupBox();
            this.Button37 = new WinPaletter.UI.WP.Button();
            this.Button30 = new WinPaletter.UI.WP.Button();
            this.GroupBox14 = new WinPaletter.UI.WP.GroupBox();
            this.Label42 = new System.Windows.Forms.Label();
            this.W11_pic9 = new System.Windows.Forms.PictureBox();
            this.W11_lbl9 = new System.Windows.Forms.Label();
            this.W11_Color_Index7 = new WinPaletter.UI.Controllers.ColorItem();
            this.pnl8 = new WinPaletter.UI.WP.GroupBox();
            this.Label37 = new System.Windows.Forms.Label();
            this.W11_pic8 = new System.Windows.Forms.PictureBox();
            this.W11_lbl8 = new System.Windows.Forms.Label();
            this.W11_Color_Index6 = new WinPaletter.UI.Controllers.ColorItem();
            this.pnl7 = new WinPaletter.UI.WP.GroupBox();
            this.Label36 = new System.Windows.Forms.Label();
            this.W11_pic7 = new System.Windows.Forms.PictureBox();
            this.W11_lbl7 = new System.Windows.Forms.Label();
            this.W11_Color_Index5 = new WinPaletter.UI.Controllers.ColorItem();
            this.pnl4 = new WinPaletter.UI.WP.GroupBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.W11_pic4 = new System.Windows.Forms.PictureBox();
            this.W11_lbl4 = new System.Windows.Forms.Label();
            this.W11_Color_Index2 = new WinPaletter.UI.Controllers.ColorItem();
            this.PictureBox10 = new System.Windows.Forms.PictureBox();
            this.pnl6 = new WinPaletter.UI.WP.GroupBox();
            this.Label35 = new System.Windows.Forms.Label();
            this.W11_pic6 = new System.Windows.Forms.PictureBox();
            this.W11_lbl6 = new System.Windows.Forms.Label();
            this.W11_Color_Index4 = new WinPaletter.UI.Controllers.ColorItem();
            this.pnl1 = new WinPaletter.UI.WP.GroupBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.W11_pic1 = new System.Windows.Forms.PictureBox();
            this.W11_TaskbarFrontAndFoldersOnStart_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.W11_lbl1 = new System.Windows.Forms.Label();
            this.pnl3 = new WinPaletter.UI.WP.GroupBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.W11_pic3 = new System.Windows.Forms.PictureBox();
            this.W11_Color_Index1 = new WinPaletter.UI.Controllers.ColorItem();
            this.W11_lbl3 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.pnl2 = new WinPaletter.UI.WP.GroupBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.W11_Color_Index0 = new WinPaletter.UI.Controllers.ColorItem();
            this.W11_pic2 = new System.Windows.Forms.PictureBox();
            this.W11_lbl2 = new System.Windows.Forms.Label();
            this.pnl5 = new WinPaletter.UI.WP.GroupBox();
            this.Label34 = new System.Windows.Forms.Label();
            this.W11_pic5 = new System.Windows.Forms.PictureBox();
            this.W11_lbl5 = new System.Windows.Forms.Label();
            this.W11_Color_Index3 = new WinPaletter.UI.Controllers.ColorItem();
            this.GroupBox5 = new WinPaletter.UI.WP.GroupBox();
            this.GroupBox6 = new WinPaletter.UI.WP.GroupBox();
            this.W11_Accent_Taskbar = new WinPaletter.UI.WP.RadioImage();
            this.W11_Accent_StartTaskbar = new WinPaletter.UI.WP.RadioImage();
            this.W11_Accent_None = new WinPaletter.UI.WP.RadioImage();
            this.PictureBox19 = new System.Windows.Forms.PictureBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.GroupBox4 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.W11_WinMode_Toggle = new WinPaletter.UI.WP.Toggle();
            this.GroupBox18 = new WinPaletter.UI.WP.GroupBox();
            this.W11_Transparency_Toggle = new WinPaletter.UI.WP.Toggle();
            this.PictureBox18 = new System.Windows.Forms.PictureBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.GroupBox24 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox20 = new System.Windows.Forms.PictureBox();
            this.W11_AppMode_Toggle = new WinPaletter.UI.WP.Toggle();
            this.Label7 = new System.Windows.Forms.Label();
            this.PictureBox17 = new System.Windows.Forms.PictureBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.GroupBox1 = new WinPaletter.UI.WP.GroupBox();
            this.W11_Button8 = new WinPaletter.UI.WP.Button();
            this.W11_ShowAccentOnTitlebarAndBorders_Toggle = new WinPaletter.UI.WP.Toggle();
            this.GroupBox20 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox11 = new System.Windows.Forms.PictureBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.W11_InactiveTitlebar_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.GroupBox9 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.W11_ActiveTitlebar_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.GroupBox2 = new WinPaletter.UI.WP.GroupBox();
            this.Button38 = new WinPaletter.UI.WP.Button();
            this.GroupBox8 = new WinPaletter.UI.WP.GroupBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.W10_pic9 = new System.Windows.Forms.PictureBox();
            this.W10_lbl9 = new System.Windows.Forms.Label();
            this.W10_Color_Index7 = new WinPaletter.UI.Controllers.ColorItem();
            this.GroupBox16 = new WinPaletter.UI.WP.GroupBox();
            this.Label43 = new System.Windows.Forms.Label();
            this.W10_pic8 = new System.Windows.Forms.PictureBox();
            this.W10_lbl8 = new System.Windows.Forms.Label();
            this.W10_Color_Index6 = new WinPaletter.UI.Controllers.ColorItem();
            this.GroupBox25 = new WinPaletter.UI.WP.GroupBox();
            this.Label44 = new System.Windows.Forms.Label();
            this.W10_pic7 = new System.Windows.Forms.PictureBox();
            this.W10_lbl7 = new System.Windows.Forms.Label();
            this.W10_Color_Index5 = new WinPaletter.UI.Controllers.ColorItem();
            this.GroupBox27 = new WinPaletter.UI.WP.GroupBox();
            this.Label45 = new System.Windows.Forms.Label();
            this.W10_pic4 = new System.Windows.Forms.PictureBox();
            this.W10_lbl4 = new System.Windows.Forms.Label();
            this.W10_Color_Index2 = new WinPaletter.UI.Controllers.ColorItem();
            this.PictureBox7 = new System.Windows.Forms.PictureBox();
            this.GroupBox28 = new WinPaletter.UI.WP.GroupBox();
            this.Label46 = new System.Windows.Forms.Label();
            this.W10_pic6 = new System.Windows.Forms.PictureBox();
            this.W10_lbl6 = new System.Windows.Forms.Label();
            this.W10_Color_Index4 = new WinPaletter.UI.Controllers.ColorItem();
            this.GroupBox31 = new WinPaletter.UI.WP.GroupBox();
            this.Label47 = new System.Windows.Forms.Label();
            this.W10_pic1 = new System.Windows.Forms.PictureBox();
            this.W10_TaskbarFrontAndFoldersOnStart_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.W10_lbl1 = new System.Windows.Forms.Label();
            this.GroupBox34 = new WinPaletter.UI.WP.GroupBox();
            this.Label48 = new System.Windows.Forms.Label();
            this.W10_pic3 = new System.Windows.Forms.PictureBox();
            this.W10_Color_Index1 = new WinPaletter.UI.Controllers.ColorItem();
            this.W10_lbl3 = new System.Windows.Forms.Label();
            this.Label49 = new System.Windows.Forms.Label();
            this.GroupBox35 = new WinPaletter.UI.WP.GroupBox();
            this.Label50 = new System.Windows.Forms.Label();
            this.W10_Color_Index0 = new WinPaletter.UI.Controllers.ColorItem();
            this.W10_pic2 = new System.Windows.Forms.PictureBox();
            this.W10_lbl2 = new System.Windows.Forms.Label();
            this.GroupBox36 = new WinPaletter.UI.WP.GroupBox();
            this.Label51 = new System.Windows.Forms.Label();
            this.W10_pic5 = new System.Windows.Forms.PictureBox();
            this.W10_lbl5 = new System.Windows.Forms.Label();
            this.W10_Color_Index3 = new WinPaletter.UI.Controllers.ColorItem();
            this.GroupBox37 = new WinPaletter.UI.WP.GroupBox();
            this.GroupBox23 = new WinPaletter.UI.WP.GroupBox();
            this.W10_TBTransparency_Toggle = new WinPaletter.UI.WP.Toggle();
            this.PictureBox15 = new System.Windows.Forms.PictureBox();
            this.Label22 = new System.Windows.Forms.Label();
            this.GroupBox38 = new WinPaletter.UI.WP.GroupBox();
            this.W10_Button25 = new WinPaletter.UI.WP.Button();
            this.W10_Accent_Taskbar = new WinPaletter.UI.WP.RadioImage();
            this.W10_Accent_StartTaskbar = new WinPaletter.UI.WP.RadioImage();
            this.W10_Accent_None = new WinPaletter.UI.WP.RadioImage();
            this.PictureBox16 = new System.Windows.Forms.PictureBox();
            this.Label52 = new System.Windows.Forms.Label();
            this.GroupBox40 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox22 = new System.Windows.Forms.PictureBox();
            this.Label53 = new System.Windows.Forms.Label();
            this.W10_WinMode_Toggle = new WinPaletter.UI.WP.Toggle();
            this.GroupBox42 = new WinPaletter.UI.WP.GroupBox();
            this.W10_TB_Blur = new WinPaletter.UI.WP.Toggle();
            this.W10_Transparency_Toggle = new WinPaletter.UI.WP.Toggle();
            this.PictureBox26 = new System.Windows.Forms.PictureBox();
            this.Label54 = new System.Windows.Forms.Label();
            this.GroupBox43 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox27 = new System.Windows.Forms.PictureBox();
            this.W10_AppMode_Toggle = new WinPaletter.UI.WP.Toggle();
            this.Label55 = new System.Windows.Forms.Label();
            this.PictureBox31 = new System.Windows.Forms.PictureBox();
            this.Label56 = new System.Windows.Forms.Label();
            this.GroupBox44 = new WinPaletter.UI.WP.GroupBox();
            this.W10_Button8 = new WinPaletter.UI.WP.Button();
            this.W10_ShowAccentOnTitlebarAndBorders_Toggle = new WinPaletter.UI.WP.Toggle();
            this.GroupBox45 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox35 = new System.Windows.Forms.PictureBox();
            this.Label57 = new System.Windows.Forms.Label();
            this.W10_InactiveTitlebar_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.PictureBox34 = new System.Windows.Forms.PictureBox();
            this.Label58 = new System.Windows.Forms.Label();
            this.GroupBox46 = new WinPaletter.UI.WP.GroupBox();
            this.pictureBox33 = new System.Windows.Forms.PictureBox();
            this.Label59 = new System.Windows.Forms.Label();
            this.W10_ActiveTitlebar_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.PaletteContainer_W81 = new System.Windows.Forms.Panel();
            this.GroupBox17 = new WinPaletter.UI.WP.GroupBox();
            this.SeparatorVertical2 = new WinPaletter.UI.WP.SeparatorV();
            this.Label32 = new System.Windows.Forms.Label();
            this.Label31 = new System.Windows.Forms.Label();
            this.W81_start = new WinPaletter.UI.WP.Button();
            this.Label30 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.W81_theme_aerolite = new WinPaletter.UI.WP.RadioImage();
            this.PictureBox37 = new System.Windows.Forms.PictureBox();
            this.Label40 = new System.Windows.Forms.Label();
            this.W81_theme_aero = new WinPaletter.UI.WP.RadioImage();
            this.W81_logonui = new WinPaletter.UI.WP.Button();
            this.GroupBox32 = new WinPaletter.UI.WP.GroupBox();
            this.GroupBox39 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox28 = new System.Windows.Forms.PictureBox();
            this.W81_personalcolor_accent_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.Foregrounds = new System.Windows.Forms.Label();
            this.GroupBox41 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox29 = new System.Windows.Forms.PictureBox();
            this.W81_personalcls_background_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.Label33 = new System.Windows.Forms.Label();
            this.GroupBox15 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox9 = new System.Windows.Forms.PictureBox();
            this.W81_start_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.Label20 = new System.Windows.Forms.Label();
            this.GroupBox33 = new WinPaletter.UI.WP.GroupBox();
            this.W81_ColorizationBalance_val = new WinPaletter.UI.WP.Button();
            this.W81_ColorizationBalance_bar = new WinPaletter.UI.WP.Trackbar();
            this.PictureBox30 = new System.Windows.Forms.PictureBox();
            this.W81_ColorizationColor_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.Label39 = new System.Windows.Forms.Label();
            this.GroupBox29 = new WinPaletter.UI.WP.GroupBox();
            this.PictureBox23 = new System.Windows.Forms.PictureBox();
            this.W81_accent_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.Label29 = new System.Windows.Forms.Label();
            this.PictureBox32 = new System.Windows.Forms.PictureBox();
            this.Label41 = new System.Windows.Forms.Label();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.PaletteContainer_W7 = new System.Windows.Forms.Panel();
            this.GroupBox11 = new WinPaletter.UI.WP.GroupBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.PictureBox13 = new System.Windows.Forms.PictureBox();
            this.W7_theme_aero = new WinPaletter.UI.WP.RadioImage();
            this.Label28 = new System.Windows.Forms.Label();
            this.W7_theme_classic = new WinPaletter.UI.WP.RadioImage();
            this.Label25 = new System.Windows.Forms.Label();
            this.W7_theme_basic = new WinPaletter.UI.WP.RadioImage();
            this.Label14 = new System.Windows.Forms.Label();
            this.W7_theme_aeroopaque = new WinPaletter.UI.WP.RadioImage();
            this.Label6 = new System.Windows.Forms.Label();
            this.GroupBox22 = new WinPaletter.UI.WP.GroupBox();
            this.GroupBox19 = new WinPaletter.UI.WP.GroupBox();
            this.W7_ColorizationGlassReflectionIntensity_val = new WinPaletter.UI.WP.Button();
            this.W7_ColorizationGlassReflectionIntensity_bar = new WinPaletter.UI.WP.Trackbar();
            this.PictureBox24 = new System.Windows.Forms.PictureBox();
            this.Label26 = new System.Windows.Forms.Label();
            this.PictureBox39 = new System.Windows.Forms.PictureBox();
            this.GroupBox12 = new WinPaletter.UI.WP.GroupBox();
            this.W7_ColorizationBlurBalance_val = new WinPaletter.UI.WP.Button();
            this.W7_ColorizationBlurBalance_bar = new WinPaletter.UI.WP.Trackbar();
            this.PictureBox8 = new System.Windows.Forms.PictureBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label38 = new System.Windows.Forms.Label();
            this.GroupBox10 = new WinPaletter.UI.WP.GroupBox();
            this.W7_EnableAeroPeek_toggle = new WinPaletter.UI.WP.Toggle();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.Aero_EnableAeroPeek_lbl = new System.Windows.Forms.Label();
            this.GroupBox7 = new WinPaletter.UI.WP.GroupBox();
            this.W7_AlwaysHibernateThumbnails_Toggle = new WinPaletter.UI.WP.Toggle();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.Aero_AlwaysHibernateThumbnails_lbl = new System.Windows.Forms.Label();
            this.GroupBox30 = new WinPaletter.UI.WP.GroupBox();
            this.GroupBox21 = new WinPaletter.UI.WP.GroupBox();
            this.W7_ColorizationColorBalance_val = new WinPaletter.UI.WP.Button();
            this.W7_ColorizationColorBalance_bar = new WinPaletter.UI.WP.Trackbar();
            this.PictureBox12 = new System.Windows.Forms.PictureBox();
            this.W7_ColorizationColor_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.Label16 = new System.Windows.Forms.Label();
            this.GroupBox26 = new WinPaletter.UI.WP.GroupBox();
            this.W7_ColorizationAfterglowBalance_val = new WinPaletter.UI.WP.Button();
            this.W7_ColorizationAfterglowBalance_bar = new WinPaletter.UI.WP.Trackbar();
            this.W7_ColorizationAfterglow_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.PictureBox14 = new System.Windows.Forms.PictureBox();
            this.Label21 = new System.Windows.Forms.Label();
            this.PictureBox25 = new System.Windows.Forms.PictureBox();
            this.Label27 = new System.Windows.Forms.Label();
            this.TabPage8 = new System.Windows.Forms.TabPage();
            this.GroupBox50 = new WinPaletter.UI.WP.GroupBox();
            this.WVista_ColorizationColorBalance_val = new WinPaletter.UI.WP.Button();
            this.PictureBox45 = new System.Windows.Forms.PictureBox();
            this.WVista_ColorizationColorBalance_bar = new WinPaletter.UI.WP.Trackbar();
            this.WVista_ColorizationColor_pick = new WinPaletter.UI.Controllers.ColorItem();
            this.Label80 = new System.Windows.Forms.Label();
            this.GroupBox49 = new WinPaletter.UI.WP.GroupBox();
            this.Label70 = new System.Windows.Forms.Label();
            this.PictureBox42 = new System.Windows.Forms.PictureBox();
            this.WVista_theme_aero = new WinPaletter.UI.WP.RadioImage();
            this.Label72 = new System.Windows.Forms.Label();
            this.WVista_theme_classic = new WinPaletter.UI.WP.RadioImage();
            this.Label73 = new System.Windows.Forms.Label();
            this.WVista_theme_basic = new WinPaletter.UI.WP.RadioImage();
            this.Label74 = new System.Windows.Forms.Label();
            this.WVista_theme_aeroopaque = new WinPaletter.UI.WP.RadioImage();
            this.Label75 = new System.Windows.Forms.Label();
            this.TabPage9 = new System.Windows.Forms.TabPage();
            this.groupBox51 = new WinPaletter.UI.WP.GroupBox();
            this.Label76 = new System.Windows.Forms.Label();
            this.pictureBox36 = new System.Windows.Forms.PictureBox();
            this.WXP_VS_ReplaceColors = new WinPaletter.UI.WP.CheckBox();
            this.WXP_VS_ReplaceMetrics = new WinPaletter.UI.WP.CheckBox();
            this.WXP_VS_ReplaceFonts = new WinPaletter.UI.WP.CheckBox();
            this.WXP_Alert1 = new WinPaletter.UI.WP.AlertBox();
            this.GroupBox48 = new WinPaletter.UI.WP.GroupBox();
            this.WXP_VS_ColorsList = new WinPaletter.UI.WP.ComboBox();
            this.PictureBox38 = new System.Windows.Forms.PictureBox();
            this.WXP_VS_Browse = new WinPaletter.UI.WP.Button();
            this.Label71 = new System.Windows.Forms.Label();
            this.PictureBox41 = new System.Windows.Forms.PictureBox();
            this.PictureBox40 = new System.Windows.Forms.PictureBox();
            this.Label69 = new System.Windows.Forms.Label();
            this.Label67 = new System.Windows.Forms.Label();
            this.WXP_VS_textbox = new WinPaletter.UI.WP.TextBox();
            this.GroupBox47 = new WinPaletter.UI.WP.GroupBox();
            this.SeparatorVertical3 = new WinPaletter.UI.WP.SeparatorV();
            this.Label68 = new System.Windows.Forms.Label();
            this.WXP_CustomTheme = new WinPaletter.UI.WP.RadioImage();
            this.WXP_Classic = new WinPaletter.UI.WP.RadioImage();
            this.Label66 = new System.Windows.Forms.Label();
            this.Label62 = new System.Windows.Forms.Label();
            this.PictureBox6 = new System.Windows.Forms.PictureBox();
            this.WXP_Luna_Blue = new WinPaletter.UI.WP.RadioImage();
            this.Label63 = new System.Windows.Forms.Label();
            this.Label64 = new System.Windows.Forms.Label();
            this.WXP_Luna_Silver = new WinPaletter.UI.WP.RadioImage();
            this.Label65 = new System.Windows.Forms.Label();
            this.WXP_Luna_OliveGreen = new WinPaletter.UI.WP.RadioImage();
            this.Button19 = new WinPaletter.UI.WP.Button();
            this.apply_btn = new WinPaletter.UI.WP.Button();
            this.BetaBadge = new WinPaletter.UI.WP.AlertBox();
            this.Button13 = new WinPaletter.UI.WP.Button();
            this.MainToolbar = new WinPaletter.UI.WP.GroupBox();
            this.userButton = new WinPaletter.UI.WP.Button();
            this.button8 = new WinPaletter.UI.WP.Button();
            this.Button40 = new WinPaletter.UI.WP.Button();
            this.Button39 = new WinPaletter.UI.WP.Button();
            this.Button36 = new WinPaletter.UI.WP.Button();
            this.Button31 = new WinPaletter.UI.WP.Button();
            this.Button20 = new WinPaletter.UI.WP.Button();
            this.Button18 = new WinPaletter.UI.WP.Button();
            this.Button17 = new WinPaletter.UI.WP.Button();
            this.Button12 = new WinPaletter.UI.WP.Button();
            this.Button5 = new WinPaletter.UI.WP.Button();
            this.Button6 = new WinPaletter.UI.WP.Button();
            this.Button10 = new WinPaletter.UI.WP.Button();
            this.Button11 = new WinPaletter.UI.WP.Button();
            this.SeparatorVertical1 = new WinPaletter.UI.WP.SeparatorV();
            this.status_lbl = new System.Windows.Forms.Label();
            this.Button7 = new WinPaletter.UI.WP.Button();
            this.Button9 = new WinPaletter.UI.WP.Button();
            this.Button3 = new WinPaletter.UI.WP.Button();
            this.Button2 = new WinPaletter.UI.WP.Button();
            this.GroupBox3 = new WinPaletter.UI.WP.GroupBox();
            this.Button26 = new WinPaletter.UI.WP.Button();
            this.Button35 = new WinPaletter.UI.WP.Button();
            this.Button34 = new WinPaletter.UI.WP.Button();
            this.Button33 = new WinPaletter.UI.WP.Button();
            this.Button32 = new WinPaletter.UI.WP.Button();
            this.Button29 = new WinPaletter.UI.WP.Button();
            this.Button27 = new WinPaletter.UI.WP.Button();
            this.Button24 = new WinPaletter.UI.WP.Button();
            this.Button21 = new WinPaletter.UI.WP.Button();
            this.Button16 = new WinPaletter.UI.WP.Button();
            this.Button4 = new WinPaletter.UI.WP.Button();
            this.button41 = new WinPaletter.UI.WP.Button();
            this.previewContainer.SuspendLayout();
            this.tabs_preview.SuspendLayout();
            this.TabPage6.SuspendLayout();
            this.pnl_preview.SuspendLayout();
            this.Window1.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.TabPage7.SuspendLayout();
            this.pnl_preview_classic.SuspendLayout();
            this.ClassicTaskbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox21)).BeginInit();
            this.TablessControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.PaletteContainer_W11.SuspendLayout();
            this.GroupBox13.SuspendLayout();
            this.GroupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic9)).BeginInit();
            this.pnl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic8)).BeginInit();
            this.pnl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic7)).BeginInit();
            this.pnl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).BeginInit();
            this.pnl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic6)).BeginInit();
            this.pnl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic1)).BeginInit();
            this.pnl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic3)).BeginInit();
            this.pnl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic2)).BeginInit();
            this.pnl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic5)).BeginInit();
            this.GroupBox5.SuspendLayout();
            this.GroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox19)).BeginInit();
            this.GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.GroupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox18)).BeginInit();
            this.GroupBox24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox17)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.GroupBox20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.GroupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.TabPage2.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic9)).BeginInit();
            this.GroupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic8)).BeginInit();
            this.GroupBox25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic7)).BeginInit();
            this.GroupBox27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).BeginInit();
            this.GroupBox28.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic6)).BeginInit();
            this.GroupBox31.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic1)).BeginInit();
            this.GroupBox34.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic3)).BeginInit();
            this.GroupBox35.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic2)).BeginInit();
            this.GroupBox36.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic5)).BeginInit();
            this.GroupBox37.SuspendLayout();
            this.GroupBox23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox15)).BeginInit();
            this.GroupBox38.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).BeginInit();
            this.GroupBox40.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox22)).BeginInit();
            this.GroupBox42.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox26)).BeginInit();
            this.GroupBox43.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox31)).BeginInit();
            this.GroupBox44.SuspendLayout();
            this.GroupBox45.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox35)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox34)).BeginInit();
            this.GroupBox46.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox33)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.PaletteContainer_W81.SuspendLayout();
            this.GroupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox37)).BeginInit();
            this.GroupBox32.SuspendLayout();
            this.GroupBox39.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox28)).BeginInit();
            this.GroupBox41.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox29)).BeginInit();
            this.GroupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).BeginInit();
            this.GroupBox33.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox30)).BeginInit();
            this.GroupBox29.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox32)).BeginInit();
            this.TabPage4.SuspendLayout();
            this.PaletteContainer_W7.SuspendLayout();
            this.GroupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox13)).BeginInit();
            this.GroupBox22.SuspendLayout();
            this.GroupBox19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox39)).BeginInit();
            this.GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).BeginInit();
            this.GroupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            this.GroupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            this.GroupBox30.SuspendLayout();
            this.GroupBox21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox12)).BeginInit();
            this.GroupBox26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox25)).BeginInit();
            this.TabPage8.SuspendLayout();
            this.GroupBox50.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox45)).BeginInit();
            this.GroupBox49.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox42)).BeginInit();
            this.TabPage9.SuspendLayout();
            this.groupBox51.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox36)).BeginInit();
            this.GroupBox48.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox38)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox40)).BeginInit();
            this.GroupBox47.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            this.MainToolbar.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.DefaultExt = "wpt";
            this.OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // SaveFileDialog1
            // 
            this.SaveFileDialog1.DefaultExt = "wpt";
            this.SaveFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth";
            // 
            // BackgroundWorker1
            // 
            this.BackgroundWorker1.WorkerReportsProgress = true;
            this.BackgroundWorker1.WorkerSupportsCancellation = true;
            this.BackgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.BackgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // SaveFileDialog2
            // 
            this.SaveFileDialog2.DefaultExt = "wpt";
            this.SaveFileDialog2.Filter = "PNG File|*.png";
            // 
            // SaveFileDialog3
            // 
            this.SaveFileDialog3.DefaultExt = "wpt";
            this.SaveFileDialog3.Filter = "Test File (*.txt)|*.txt";
            // 
            // NotifyUpdates
            // 
            this.NotifyUpdates.Text = "WinPaletter";
            this.NotifyUpdates.BalloonTipClicked += new System.EventHandler(this.NotifyIcon1_BalloonTipClicked);
            // 
            // OpenFileDialog2
            // 
            this.OpenFileDialog2.DefaultExt = "wpt";
            this.OpenFileDialog2.Filter = "Visual Styles File (*.msstyles)|*.msstyles|Theme File (*.theme)|*.theme";
            // 
            // Button28
            // 
            this.Button28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(35)))));
            this.Button28.DrawOnGlass = false;
            this.Button28.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button28.ForeColor = System.Drawing.Color.White;
            this.Button28.Image = ((System.Drawing.Image)(resources.GetObject("Button28.Image")));
            this.Button28.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button28.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(99)))), ((int)(((byte)(149)))));
            this.Button28.Location = new System.Drawing.Point(730, 655);
            this.Button28.Name = "Button28";
            this.Button28.Size = new System.Drawing.Size(100, 34);
            this.Button28.TabIndex = 34;
            this.Button28.Text = "Logoff";
            this.Button28.UseVisualStyleBackColor = false;
            this.Button28.Click += new System.EventHandler(this.Button28_Click);
            this.Button28.MouseEnter += new System.EventHandler(this.Button28_MouseEnter);
            this.Button28.MouseLeave += new System.EventHandler(this.Button28_MouseLeave);
            // 
            // previewContainer
            // 
            this.previewContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.previewContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(35)))));
            this.previewContainer.Controls.Add(this.Select_WXP);
            this.previewContainer.Controls.Add(this.Select_WVista);
            this.previewContainer.Controls.Add(this.tabs_preview);
            this.previewContainer.Controls.Add(this.Select_W7);
            this.previewContainer.Controls.Add(this.Button23);
            this.previewContainer.Controls.Add(this.Select_W81);
            this.previewContainer.Controls.Add(this.Button1);
            this.previewContainer.Controls.Add(this.Select_W10);
            this.previewContainer.Controls.Add(this.Button15);
            this.previewContainer.Controls.Add(this.Select_W11);
            this.previewContainer.Controls.Add(this.PictureBox21);
            this.previewContainer.Controls.Add(this.themename_lbl);
            this.previewContainer.Controls.Add(this.author_lbl);
            this.previewContainer.Location = new System.Drawing.Point(547, 77);
            this.previewContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.previewContainer.Name = "previewContainer";
            this.previewContainer.Padding = new System.Windows.Forms.Padding(1);
            this.previewContainer.Size = new System.Drawing.Size(536, 406);
            this.previewContainer.TabIndex = 14;
            // 
            // Select_WXP
            // 
            this.Select_WXP.AllowDrop = true;
            this.Select_WXP.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Select_WXP.Checked = false;
            this.Select_WXP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Select_WXP.ForeColor = System.Drawing.Color.White;
            this.Select_WXP.Image = null;
            this.Select_WXP.ImageWithText = false;
            this.Select_WXP.Location = new System.Drawing.Point(133, 359);
            this.Select_WXP.Name = "Select_WXP";
            this.Select_WXP.ShowText = false;
            this.Select_WXP.Size = new System.Drawing.Size(40, 40);
            this.Select_WXP.TabIndex = 37;
            this.Select_WXP.Tag = "Change the preview to Windows XP";
            this.Select_WXP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Select_WXP.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Select_WXP_CheckedChanged);
            // 
            // Select_WVista
            // 
            this.Select_WVista.AllowDrop = true;
            this.Select_WVista.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Select_WVista.Checked = false;
            this.Select_WVista.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Select_WVista.ForeColor = System.Drawing.Color.White;
            this.Select_WVista.Image = null;
            this.Select_WVista.ImageWithText = false;
            this.Select_WVista.Location = new System.Drawing.Point(179, 359);
            this.Select_WVista.Name = "Select_WVista";
            this.Select_WVista.ShowText = false;
            this.Select_WVista.Size = new System.Drawing.Size(40, 40);
            this.Select_WVista.TabIndex = 36;
            this.Select_WVista.Tag = "Change the preview to Windows Vista";
            this.Select_WVista.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Select_WVista.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Select_WVista_CheckedChanged);
            // 
            // tabs_preview
            // 
            this.tabs_preview.Controls.Add(this.TabPage6);
            this.tabs_preview.Controls.Add(this.TabPage7);
            this.tabs_preview.Location = new System.Drawing.Point(3, 52);
            this.tabs_preview.Name = "tabs_preview";
            this.tabs_preview.SelectedIndex = 0;
            this.tabs_preview.Size = new System.Drawing.Size(528, 297);
            this.tabs_preview.TabIndex = 35;
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage6.Controls.Add(this.pnl_preview);
            this.TabPage6.Location = new System.Drawing.Point(4, 24);
            this.TabPage6.Margin = new System.Windows.Forms.Padding(0);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(520, 269);
            this.TabPage6.TabIndex = 0;
            this.TabPage6.Text = "0";
            // 
            // pnl_preview
            // 
            this.pnl_preview.BackColor = System.Drawing.Color.Black;
            this.pnl_preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_preview.Controls.Add(this.WXP_Alert2);
            this.pnl_preview.Controls.Add(this.ActionCenter);
            this.pnl_preview.Controls.Add(this.start);
            this.pnl_preview.Controls.Add(this.taskbar);
            this.pnl_preview.Controls.Add(this.Window2);
            this.pnl_preview.Controls.Add(this.Window1);
            this.pnl_preview.Location = new System.Drawing.Point(0, 0);
            this.pnl_preview.Name = "pnl_preview";
            this.pnl_preview.Size = new System.Drawing.Size(528, 297);
            this.pnl_preview.TabIndex = 2;
            // 
            // WXP_Alert2
            // 
            this.WXP_Alert2.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Warning;
            this.WXP_Alert2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WXP_Alert2.BackColor = System.Drawing.Color.Transparent;
            this.WXP_Alert2.CenterText = true;
            this.WXP_Alert2.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.WXP_Alert2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_Alert2.Image = null;
            this.WXP_Alert2.Location = new System.Drawing.Point(7, 8);
            this.WXP_Alert2.Name = "WXP_Alert2";
            this.WXP_Alert2.Size = new System.Drawing.Size(135, 36);
            this.WXP_Alert2.TabIndex = 54;
            this.WXP_Alert2.TabStop = false;
            this.WXP_Alert2.Text = "Classic theme is enabled. The preview won\'t work for other themes due to some lim" +
    "itations in visual styles Previewer. Apply another theme first then reopen WinPa" +
    "letter.";
            this.WXP_Alert2.Visible = false;
            // 
            // ActionCenter
            // 
            this.ActionCenter.ActionCenterButton_Hover = System.Drawing.Color.Empty;
            this.ActionCenter.ActionCenterButton_Normal = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.ActionCenter.ActionCenterButton_Pressed = System.Drawing.Color.Empty;
            this.ActionCenter.AppBackground = System.Drawing.Color.Empty;
            this.ActionCenter.AppUnderline = System.Drawing.Color.Empty;
            this.ActionCenter.BackColor = System.Drawing.Color.Transparent;
            this.ActionCenter.BackColorAlpha = 50;
            this.ActionCenter.Background = System.Drawing.Color.Empty;
            this.ActionCenter.Background2 = System.Drawing.Color.Empty;
            this.ActionCenter.BlurPower = 8;
            this.ActionCenter.DarkMode = true;
            this.ActionCenter.LinkColor = System.Drawing.Color.Empty;
            this.ActionCenter.Location = new System.Drawing.Point(400, 165);
            this.ActionCenter.Name = "ActionCenter";
            this.ActionCenter.NoisePower = 0.2F;
            this.ActionCenter.Padding = new System.Windows.Forms.Padding(2);
            this.ActionCenter.Shadow = true;
            this.ActionCenter.Size = new System.Drawing.Size(120, 85);
            this.ActionCenter.StartColor = System.Drawing.Color.Empty;
            this.ActionCenter.Style = WinPaletter.UI.Simulation.WinElement.Styles.ActionCenter11;
            this.ActionCenter.SuspendRefresh = false;
            this.ActionCenter.TabIndex = 5;
            this.ActionCenter.Transparency = true;
            this.ActionCenter.UseWin11ORB_WithWin10 = false;
            this.ActionCenter.UseWin11RoundedCorners_WithWin10_Level1 = false;
            this.ActionCenter.UseWin11RoundedCorners_WithWin10_Level2 = false;
            this.ActionCenter.Win7ColorBal = 100;
            this.ActionCenter.Win7GlowBal = 100;
            // 
            // start
            // 
            this.start.ActionCenterButton_Hover = System.Drawing.Color.Empty;
            this.start.ActionCenterButton_Normal = System.Drawing.Color.Empty;
            this.start.ActionCenterButton_Pressed = System.Drawing.Color.Empty;
            this.start.AppBackground = System.Drawing.Color.Empty;
            this.start.AppUnderline = System.Drawing.Color.Empty;
            this.start.BackColor = System.Drawing.Color.Transparent;
            this.start.BackColorAlpha = 150;
            this.start.Background = System.Drawing.Color.Empty;
            this.start.Background2 = System.Drawing.Color.Empty;
            this.start.BlurPower = 7;
            this.start.DarkMode = true;
            this.start.LinkColor = System.Drawing.Color.Empty;
            this.start.Location = new System.Drawing.Point(7, 50);
            this.start.Name = "start";
            this.start.NoisePower = 0.2F;
            this.start.Padding = new System.Windows.Forms.Padding(2);
            this.start.Shadow = true;
            this.start.Size = new System.Drawing.Size(135, 200);
            this.start.StartColor = System.Drawing.Color.Empty;
            this.start.Style = WinPaletter.UI.Simulation.WinElement.Styles.Start11;
            this.start.SuspendRefresh = false;
            this.start.TabIndex = 1;
            this.start.Transparency = true;
            this.start.UseWin11ORB_WithWin10 = false;
            this.start.UseWin11RoundedCorners_WithWin10_Level1 = false;
            this.start.UseWin11RoundedCorners_WithWin10_Level2 = false;
            this.start.Win7ColorBal = 100;
            this.start.Win7GlowBal = 100;
            // 
            // taskbar
            // 
            this.taskbar.ActionCenterButton_Hover = System.Drawing.Color.Empty;
            this.taskbar.ActionCenterButton_Normal = System.Drawing.Color.Empty;
            this.taskbar.ActionCenterButton_Pressed = System.Drawing.Color.Empty;
            this.taskbar.AppBackground = System.Drawing.Color.Empty;
            this.taskbar.AppUnderline = System.Drawing.Color.Empty;
            this.taskbar.BackColor = System.Drawing.Color.Transparent;
            this.taskbar.BackColorAlpha = 130;
            this.taskbar.Background = System.Drawing.Color.Empty;
            this.taskbar.Background2 = System.Drawing.Color.Empty;
            this.taskbar.BlurPower = 12;
            this.taskbar.DarkMode = true;
            this.taskbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.taskbar.LinkColor = System.Drawing.Color.Empty;
            this.taskbar.Location = new System.Drawing.Point(0, 255);
            this.taskbar.Name = "taskbar";
            this.taskbar.NoisePower = 0.2F;
            this.taskbar.Shadow = true;
            this.taskbar.Size = new System.Drawing.Size(528, 42);
            this.taskbar.StartColor = System.Drawing.Color.Empty;
            this.taskbar.Style = WinPaletter.UI.Simulation.WinElement.Styles.Taskbar11;
            this.taskbar.SuspendRefresh = false;
            this.taskbar.TabIndex = 0;
            this.taskbar.Transparency = true;
            this.taskbar.UseWin11ORB_WithWin10 = false;
            this.taskbar.UseWin11RoundedCorners_WithWin10_Level1 = false;
            this.taskbar.UseWin11RoundedCorners_WithWin10_Level2 = false;
            this.taskbar.Win7ColorBal = 100;
            this.taskbar.Win7GlowBal = 100;
            // 
            // Window2
            // 
            this.Window2.AccentColor_Active = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(100)))));
            this.Window2.AccentColor_Enabled = true;
            this.Window2.AccentColor_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Window2.AccentColor2_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Window2.AccentColor2_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Window2.Active = false;
            this.Window2.BackColor = System.Drawing.Color.Transparent;
            this.Window2.DarkMode = true;
            this.Window2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Window2.Location = new System.Drawing.Point(172, 160);
            this.Window2.Metrics_BorderWidth = 1;
            this.Window2.Metrics_CaptionHeight = 22;
            this.Window2.Metrics_PaddedBorderWidth = 4;
            this.Window2.Name = "Window2";
            this.Window2.Padding = new System.Windows.Forms.Padding(4, 40, 4, 4);
            this.Window2.Preview = WinPaletter.UI.Simulation.Window.Preview_Enum.W11;
            this.Window2.Radius = 5;
            this.Window2.Shadow = true;
            this.Window2.Size = new System.Drawing.Size(189, 85);
            this.Window2.SuspendRefresh = false;
            this.Window2.TabIndex = 3;
            this.Window2.Text = "Inactive app";
            this.Window2.ToolWindow = false;
            this.Window2.Win7Alpha = 100;
            this.Window2.Win7ColorBal = 100;
            this.Window2.Win7GlowBal = 100;
            this.Window2.Win7Noise = 1F;
            this.Window2.WinVista = false;
            // 
            // Window1
            // 
            this.Window1.AccentColor_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Window1.AccentColor_Enabled = true;
            this.Window1.AccentColor_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Window1.AccentColor2_Active = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Window1.AccentColor2_Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Window1.Active = true;
            this.Window1.BackColor = System.Drawing.Color.Transparent;
            this.Window1.Controls.Add(this.Panel3);
            this.Window1.Controls.Add(this.lnk_preview);
            this.Window1.DarkMode = true;
            this.Window1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Window1.Location = new System.Drawing.Point(172, 13);
            this.Window1.Metrics_BorderWidth = 1;
            this.Window1.Metrics_CaptionHeight = 22;
            this.Window1.Metrics_PaddedBorderWidth = 4;
            this.Window1.Name = "Window1";
            this.Window1.Padding = new System.Windows.Forms.Padding(4, 40, 4, 4);
            this.Window1.Preview = WinPaletter.UI.Simulation.Window.Preview_Enum.W11;
            this.Window1.Radius = 5;
            this.Window1.Shadow = true;
            this.Window1.Size = new System.Drawing.Size(189, 147);
            this.Window1.SuspendRefresh = false;
            this.Window1.TabIndex = 2;
            this.Window1.Text = "App preview";
            this.Window1.ToolWindow = false;
            this.Window1.Win7Alpha = 100;
            this.Window1.Win7ColorBal = 100;
            this.Window1.Win7GlowBal = 100;
            this.Window1.Win7Noise = 1F;
            this.Window1.WinVista = false;
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.Transparent;
            this.Panel3.Controls.Add(this.Label8);
            this.Panel3.Controls.Add(this.setting_icon_preview);
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel3.Location = new System.Drawing.Point(4, 40);
            this.Panel3.Name = "Panel3";
            this.Panel3.Padding = new System.Windows.Forms.Padding(1);
            this.Panel3.Size = new System.Drawing.Size(181, 78);
            this.Panel3.TabIndex = 0;
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label8.DrawOnGlass = false;
            this.Label8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label8.Location = new System.Drawing.Point(1, 46);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(179, 31);
            this.Label8.TabIndex = 15;
            this.Label8.Text = "This is a setting icon";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // setting_icon_preview
            // 
            this.setting_icon_preview.BackColor = System.Drawing.Color.Transparent;
            this.setting_icon_preview.Dock = System.Windows.Forms.DockStyle.Top;
            this.setting_icon_preview.DrawOnGlass = false;
            this.setting_icon_preview.Font = new System.Drawing.Font("Segoe MDL2 Assets", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setting_icon_preview.Location = new System.Drawing.Point(1, 1);
            this.setting_icon_preview.Name = "setting_icon_preview";
            this.setting_icon_preview.Size = new System.Drawing.Size(179, 45);
            this.setting_icon_preview.TabIndex = 14;
            this.setting_icon_preview.Text = "";
            this.setting_icon_preview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lnk_preview
            // 
            this.lnk_preview.BackColor = System.Drawing.Color.Transparent;
            this.lnk_preview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lnk_preview.DrawOnGlass = false;
            this.lnk_preview.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lnk_preview.ForeColor = System.Drawing.Color.Brown;
            this.lnk_preview.Location = new System.Drawing.Point(4, 118);
            this.lnk_preview.Name = "lnk_preview";
            this.lnk_preview.Size = new System.Drawing.Size(181, 25);
            this.lnk_preview.TabIndex = 16;
            this.lnk_preview.Text = "Settings link preview";
            this.lnk_preview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TabPage7
            // 
            this.TabPage7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage7.Controls.Add(this.pnl_preview_classic);
            this.TabPage7.Location = new System.Drawing.Point(4, 24);
            this.TabPage7.Margin = new System.Windows.Forms.Padding(0);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Size = new System.Drawing.Size(520, 269);
            this.TabPage7.TabIndex = 1;
            this.TabPage7.Text = "1";
            // 
            // pnl_preview_classic
            // 
            this.pnl_preview_classic.BackColor = System.Drawing.Color.Black;
            this.pnl_preview_classic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_preview_classic.Controls.Add(this.ClassicWindow1);
            this.pnl_preview_classic.Controls.Add(this.ClassicWindow2);
            this.pnl_preview_classic.Controls.Add(this.ClassicTaskbar);
            this.pnl_preview_classic.Location = new System.Drawing.Point(0, 0);
            this.pnl_preview_classic.Name = "pnl_preview_classic";
            this.pnl_preview_classic.Size = new System.Drawing.Size(528, 297);
            this.pnl_preview_classic.TabIndex = 34;
            // 
            // ClassicWindow1
            // 
            this.ClassicWindow1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow1.ButtonDkShadow = System.Drawing.Color.Black;
            this.ClassicWindow1.ButtonFace = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow1.ButtonHilight = System.Drawing.Color.White;
            this.ClassicWindow1.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow1.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClassicWindow1.ButtonText = System.Drawing.Color.Black;
            this.ClassicWindow1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.ClassicWindow1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(132)))), ((int)(((byte)(208)))));
            this.ClassicWindow1.ColorBorder = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow1.ColorGradient = true;
            this.ClassicWindow1.ControlBox = true;
            this.ClassicWindow1.Flat = false;
            this.ClassicWindow1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.ClassicWindow1.ForeColor = System.Drawing.Color.White;
            this.ClassicWindow1.Location = new System.Drawing.Point(174, 20);
            this.ClassicWindow1.MaximizeBox = false;
            this.ClassicWindow1.Metrics_BorderWidth = 0;
            this.ClassicWindow1.Metrics_CaptionHeight = 18;
            this.ClassicWindow1.Metrics_CaptionWidth = 0;
            this.ClassicWindow1.Metrics_PaddedBorderWidth = 0;
            this.ClassicWindow1.MinimizeBox = false;
            this.ClassicWindow1.Name = "ClassicWindow1";
            this.ClassicWindow1.Padding = new System.Windows.Forms.Padding(4, 22, 4, 4);
            this.ClassicWindow1.Size = new System.Drawing.Size(181, 146);
            this.ClassicWindow1.TabIndex = 4;
            this.ClassicWindow1.Text = "App preview";
            this.ClassicWindow1.UseItAsMenu = false;
            // 
            // ClassicWindow2
            // 
            this.ClassicWindow2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow2.ButtonDkShadow = System.Drawing.Color.Black;
            this.ClassicWindow2.ButtonFace = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow2.ButtonHilight = System.Drawing.Color.White;
            this.ClassicWindow2.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow2.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClassicWindow2.ButtonText = System.Drawing.Color.Black;
            this.ClassicWindow2.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClassicWindow2.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(181)))), ((int)(((byte)(181)))));
            this.ClassicWindow2.ColorBorder = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicWindow2.ColorGradient = true;
            this.ClassicWindow2.ControlBox = true;
            this.ClassicWindow2.Flat = false;
            this.ClassicWindow2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.ClassicWindow2.ForeColor = System.Drawing.Color.White;
            this.ClassicWindow2.Location = new System.Drawing.Point(174, 172);
            this.ClassicWindow2.MaximizeBox = false;
            this.ClassicWindow2.Metrics_BorderWidth = 0;
            this.ClassicWindow2.Metrics_CaptionHeight = 18;
            this.ClassicWindow2.Metrics_CaptionWidth = 0;
            this.ClassicWindow2.Metrics_PaddedBorderWidth = 0;
            this.ClassicWindow2.MinimizeBox = false;
            this.ClassicWindow2.Name = "ClassicWindow2";
            this.ClassicWindow2.Padding = new System.Windows.Forms.Padding(4, 22, 4, 4);
            this.ClassicWindow2.Size = new System.Drawing.Size(181, 60);
            this.ClassicWindow2.TabIndex = 5;
            this.ClassicWindow2.Text = "Inactive app";
            this.ClassicWindow2.UseItAsMenu = false;
            // 
            // ClassicTaskbar
            // 
            this.ClassicTaskbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClassicTaskbar.ButtonDkShadow = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.ClassicTaskbar.ButtonHilight = System.Drawing.Color.White;
            this.ClassicTaskbar.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.ClassicTaskbar.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClassicTaskbar.Controls.Add(this.ButtonR4);
            this.ClassicTaskbar.Controls.Add(this.ButtonR3);
            this.ClassicTaskbar.Controls.Add(this.ButtonR2);
            this.ClassicTaskbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ClassicTaskbar.Flat = false;
            this.ClassicTaskbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.ClassicTaskbar.ForeColor = System.Drawing.Color.Black;
            this.ClassicTaskbar.Location = new System.Drawing.Point(0, 253);
            this.ClassicTaskbar.Name = "ClassicTaskbar";
            this.ClassicTaskbar.Size = new System.Drawing.Size(528, 44);
            this.ClassicTaskbar.Style2 = false;
            this.ClassicTaskbar.TabIndex = 0;
            this.ClassicTaskbar.UseItAsWin7Taskbar = true;
            // 
            // ButtonR4
            // 
            this.ButtonR4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonR4.AppearsAsPressed = false;
            this.ButtonR4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR4.ButtonDkShadow = System.Drawing.Color.Black;
            this.ButtonR4.ButtonHilight = System.Drawing.Color.White;
            this.ButtonR4.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR4.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ButtonR4.FocusRectHeight = 1;
            this.ButtonR4.FocusRectWidth = 1;
            this.ButtonR4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonR4.ForeColor = System.Drawing.Color.Black;
            this.ButtonR4.HatchBrush = false;
            this.ButtonR4.Image = null;
            this.ButtonR4.Location = new System.Drawing.Point(113, 4);
            this.ButtonR4.Name = "ButtonR4";
            this.ButtonR4.Size = new System.Drawing.Size(48, 38);
            this.ButtonR4.TabIndex = 2;
            this.ButtonR4.UseItAsScrollbar = false;
            this.ButtonR4.UseVisualStyleBackColor = false;
            this.ButtonR4.WindowFrame = System.Drawing.Color.Black;
            // 
            // ButtonR3
            // 
            this.ButtonR3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonR3.AppearsAsPressed = true;
            this.ButtonR3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR3.ButtonDkShadow = System.Drawing.Color.Black;
            this.ButtonR3.ButtonHilight = System.Drawing.Color.White;
            this.ButtonR3.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR3.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ButtonR3.FocusRectHeight = 1;
            this.ButtonR3.FocusRectWidth = 1;
            this.ButtonR3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonR3.ForeColor = System.Drawing.Color.Black;
            this.ButtonR3.HatchBrush = false;
            this.ButtonR3.Image = null;
            this.ButtonR3.Location = new System.Drawing.Point(63, 4);
            this.ButtonR3.Name = "ButtonR3";
            this.ButtonR3.Size = new System.Drawing.Size(48, 38);
            this.ButtonR3.TabIndex = 1;
            this.ButtonR3.UseItAsScrollbar = false;
            this.ButtonR3.UseVisualStyleBackColor = false;
            this.ButtonR3.WindowFrame = System.Drawing.Color.Black;
            // 
            // ButtonR2
            // 
            this.ButtonR2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonR2.AppearsAsPressed = false;
            this.ButtonR2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR2.ButtonDkShadow = System.Drawing.Color.Black;
            this.ButtonR2.ButtonHilight = System.Drawing.Color.White;
            this.ButtonR2.ButtonLight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonR2.ButtonShadow = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ButtonR2.FocusRectHeight = 1;
            this.ButtonR2.FocusRectWidth = 1;
            this.ButtonR2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonR2.ForeColor = System.Drawing.Color.Black;
            this.ButtonR2.HatchBrush = false;
            this.ButtonR2.Image = null;
            this.ButtonR2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonR2.Location = new System.Drawing.Point(2, 4);
            this.ButtonR2.Name = "ButtonR2";
            this.ButtonR2.Size = new System.Drawing.Size(52, 38);
            this.ButtonR2.TabIndex = 0;
            this.ButtonR2.Text = "Start";
            this.ButtonR2.UseItAsScrollbar = false;
            this.ButtonR2.UseVisualStyleBackColor = false;
            this.ButtonR2.WindowFrame = System.Drawing.Color.Black;
            // 
            // Select_W7
            // 
            this.Select_W7.AllowDrop = true;
            this.Select_W7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Select_W7.Checked = false;
            this.Select_W7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Select_W7.ForeColor = System.Drawing.Color.White;
            this.Select_W7.Image = null;
            this.Select_W7.ImageWithText = false;
            this.Select_W7.Location = new System.Drawing.Point(225, 359);
            this.Select_W7.Name = "Select_W7";
            this.Select_W7.ShowText = false;
            this.Select_W7.Size = new System.Drawing.Size(40, 40);
            this.Select_W7.TabIndex = 26;
            this.Select_W7.Tag = "Change the preview to Windows 7";
            this.Select_W7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Select_W7.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Select_W7_CheckedChanged);
            // 
            // Button23
            // 
            this.Button23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button23.DrawOnGlass = false;
            this.Button23.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button23.ForeColor = System.Drawing.Color.White;
            this.Button23.Image = null;
            this.Button23.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.Button23.Location = new System.Drawing.Point(351, 16);
            this.Button23.Name = "Button23";
            this.Button23.Size = new System.Drawing.Size(45, 24);
            this.Button23.TabIndex = 7;
            this.Button23.Text = "Hide";
            this.Button23.UseVisualStyleBackColor = false;
            this.Button23.Click += new System.EventHandler(this.Button23_Click);
            // 
            // Select_W81
            // 
            this.Select_W81.AllowDrop = true;
            this.Select_W81.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Select_W81.Checked = false;
            this.Select_W81.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Select_W81.ForeColor = System.Drawing.Color.White;
            this.Select_W81.Image = null;
            this.Select_W81.ImageWithText = false;
            this.Select_W81.Location = new System.Drawing.Point(271, 359);
            this.Select_W81.Name = "Select_W81";
            this.Select_W81.ShowText = false;
            this.Select_W81.Size = new System.Drawing.Size(40, 40);
            this.Select_W81.TabIndex = 25;
            this.Select_W81.Tag = "Change the preview to Windows 8.1";
            this.Select_W81.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Select_W81.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Select_W8_CheckedChanged);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button1.DrawOnGlass = false;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(110)))), ((int)(((byte)(129)))));
            this.Button1.Location = new System.Drawing.Point(397, 16);
            this.Button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(96, 24);
            this.Button1.TabIndex = 4;
            this.Button1.Text = "Thumbnail";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // Select_W10
            // 
            this.Select_W10.AllowDrop = true;
            this.Select_W10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Select_W10.Checked = false;
            this.Select_W10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Select_W10.ForeColor = System.Drawing.Color.White;
            this.Select_W10.Image = null;
            this.Select_W10.ImageWithText = false;
            this.Select_W10.Location = new System.Drawing.Point(317, 359);
            this.Select_W10.Name = "Select_W10";
            this.Select_W10.ShowText = false;
            this.Select_W10.Size = new System.Drawing.Size(40, 40);
            this.Select_W10.TabIndex = 24;
            this.Select_W10.Tag = "Change the preview to Windows 10";
            this.Select_W10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Select_W10.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Select_W10_CheckedChanged);
            // 
            // Button15
            // 
            this.Button15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button15.DrawOnGlass = false;
            this.Button15.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button15.ForeColor = System.Drawing.Color.White;
            this.Button15.Image = ((System.Drawing.Image)(resources.GetObject("Button15.Image")));
            this.Button15.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.Button15.Location = new System.Drawing.Point(494, 16);
            this.Button15.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button15.Name = "Button15";
            this.Button15.Size = new System.Drawing.Size(30, 24);
            this.Button15.TabIndex = 3;
            this.Button15.UseVisualStyleBackColor = false;
            this.Button15.Click += new System.EventHandler(this.Button15_Click);
            // 
            // Select_W11
            // 
            this.Select_W11.AllowDrop = true;
            this.Select_W11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Select_W11.Checked = false;
            this.Select_W11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Select_W11.ForeColor = System.Drawing.Color.White;
            this.Select_W11.Image = null;
            this.Select_W11.ImageWithText = false;
            this.Select_W11.Location = new System.Drawing.Point(363, 359);
            this.Select_W11.Name = "Select_W11";
            this.Select_W11.ShowText = false;
            this.Select_W11.Size = new System.Drawing.Size(40, 40);
            this.Select_W11.TabIndex = 23;
            this.Select_W11.Tag = "Change the preview to Windows 11";
            this.Select_W11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Select_W11.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.Select_W11_CheckedChanged);
            // 
            // PictureBox21
            // 
            this.PictureBox21.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox21.Image")));
            this.PictureBox21.Location = new System.Drawing.Point(6, 10);
            this.PictureBox21.Name = "PictureBox21";
            this.PictureBox21.Size = new System.Drawing.Size(35, 35);
            this.PictureBox21.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox21.TabIndex = 1;
            this.PictureBox21.TabStop = false;
            // 
            // themename_lbl
            // 
            this.themename_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.themename_lbl.AutoEllipsis = true;
            this.themename_lbl.BackColor = System.Drawing.Color.Transparent;
            this.themename_lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.themename_lbl.Location = new System.Drawing.Point(44, 5);
            this.themename_lbl.Name = "themename_lbl";
            this.themename_lbl.Size = new System.Drawing.Size(353, 25);
            this.themename_lbl.TabIndex = 5;
            this.themename_lbl.Text = "Theme (1.0)";
            this.themename_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.themename_lbl.DoubleClick += new System.EventHandler(this.Button10_Click);
            // 
            // author_lbl
            // 
            this.author_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.author_lbl.AutoEllipsis = true;
            this.author_lbl.BackColor = System.Drawing.Color.Transparent;
            this.author_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.author_lbl.Location = new System.Drawing.Point(53, 30);
            this.author_lbl.Name = "author_lbl";
            this.author_lbl.Size = new System.Drawing.Size(344, 15);
            this.author_lbl.TabIndex = 6;
            this.author_lbl.Text = "By: ";
            this.author_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.author_lbl.DoubleClick += new System.EventHandler(this.Button10_Click);
            // 
            // TablessControl1
            // 
            this.TablessControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TablessControl1.Controls.Add(this.TabPage1);
            this.TablessControl1.Controls.Add(this.TabPage2);
            this.TablessControl1.Controls.Add(this.TabPage3);
            this.TablessControl1.Controls.Add(this.TabPage4);
            this.TablessControl1.Controls.Add(this.TabPage8);
            this.TablessControl1.Controls.Add(this.TabPage9);
            this.TablessControl1.Location = new System.Drawing.Point(11, 74);
            this.TablessControl1.Name = "TablessControl1";
            this.TablessControl1.SelectedIndex = 0;
            this.TablessControl1.Size = new System.Drawing.Size(537, 545);
            this.TablessControl1.TabIndex = 33;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage1.Controls.Add(this.PaletteContainer_W11);
            this.TabPage1.Location = new System.Drawing.Point(4, 24);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(529, 517);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "W11";
            // 
            // PaletteContainer_W11
            // 
            this.PaletteContainer_W11.BackColor = System.Drawing.Color.Transparent;
            this.PaletteContainer_W11.Controls.Add(this.GroupBox13);
            this.PaletteContainer_W11.Controls.Add(this.GroupBox5);
            this.PaletteContainer_W11.Controls.Add(this.GroupBox1);
            this.PaletteContainer_W11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PaletteContainer_W11.Location = new System.Drawing.Point(3, 3);
            this.PaletteContainer_W11.Name = "PaletteContainer_W11";
            this.PaletteContainer_W11.Size = new System.Drawing.Size(523, 511);
            this.PaletteContainer_W11.TabIndex = 17;
            // 
            // GroupBox13
            // 
            this.GroupBox13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox13.Controls.Add(this.Button37);
            this.GroupBox13.Controls.Add(this.Button30);
            this.GroupBox13.Controls.Add(this.GroupBox14);
            this.GroupBox13.Controls.Add(this.pnl8);
            this.GroupBox13.Controls.Add(this.pnl7);
            this.GroupBox13.Controls.Add(this.pnl4);
            this.GroupBox13.Controls.Add(this.PictureBox10);
            this.GroupBox13.Controls.Add(this.pnl6);
            this.GroupBox13.Controls.Add(this.pnl1);
            this.GroupBox13.Controls.Add(this.pnl3);
            this.GroupBox13.Controls.Add(this.Label10);
            this.GroupBox13.Controls.Add(this.pnl2);
            this.GroupBox13.Controls.Add(this.pnl5);
            this.GroupBox13.Location = new System.Drawing.Point(0, 213);
            this.GroupBox13.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox13.Name = "GroupBox13";
            this.GroupBox13.Size = new System.Drawing.Size(520, 313);
            this.GroupBox13.TabIndex = 6;
            // 
            // Button37
            // 
            this.Button37.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button37.DrawOnGlass = false;
            this.Button37.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button37.ForeColor = System.Drawing.Color.White;
            this.Button37.Image = null;
            this.Button37.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.Button37.Location = new System.Drawing.Point(203, 9);
            this.Button37.Name = "Button37";
            this.Button37.Size = new System.Drawing.Size(214, 22);
            this.Button37.TabIndex = 29;
            this.Button37.Text = "Copycat from Windows 10 presets";
            this.Button37.UseVisualStyleBackColor = false;
            this.Button37.Click += new System.EventHandler(this.Button37_Click);
            // 
            // Button30
            // 
            this.Button30.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button30.DrawOnGlass = false;
            this.Button30.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button30.ForeColor = System.Drawing.Color.White;
            this.Button30.Image = null;
            this.Button30.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(49)))), ((int)(((byte)(61)))));
            this.Button30.Location = new System.Drawing.Point(423, 9);
            this.Button30.Name = "Button30";
            this.Button30.Size = new System.Drawing.Size(90, 22);
            this.Button30.TabIndex = 28;
            this.Button30.Text = "Important tips";
            this.Button30.UseVisualStyleBackColor = false;
            this.Button30.Click += new System.EventHandler(this.Button30_Click_1);
            // 
            // GroupBox14
            // 
            this.GroupBox14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox14.Controls.Add(this.Label42);
            this.GroupBox14.Controls.Add(this.W11_pic9);
            this.GroupBox14.Controls.Add(this.W11_lbl9);
            this.GroupBox14.Controls.Add(this.W11_Color_Index7);
            this.GroupBox14.Location = new System.Drawing.Point(3, 281);
            this.GroupBox14.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox14.Name = "GroupBox14";
            this.GroupBox14.Size = new System.Drawing.Size(514, 28);
            this.GroupBox14.TabIndex = 25;
            // 
            // Label42
            // 
            this.Label42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label42.AutoEllipsis = true;
            this.Label42.BackColor = System.Drawing.Color.Transparent;
            this.Label42.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label42.Location = new System.Drawing.Point(31, 4);
            this.Label42.Name = "Label42";
            this.Label42.Size = new System.Drawing.Size(14, 19);
            this.Label42.TabIndex = 6;
            this.Label42.Text = "9";
            this.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_pic9
            // 
            this.W11_pic9.BackColor = System.Drawing.Color.Transparent;
            this.W11_pic9.Image = global::WinPaletter.Properties.Resources.Mini_Undefined;
            this.W11_pic9.Location = new System.Drawing.Point(2, 2);
            this.W11_pic9.Name = "W11_pic9";
            this.W11_pic9.Size = new System.Drawing.Size(24, 24);
            this.W11_pic9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W11_pic9.TabIndex = 4;
            this.W11_pic9.TabStop = false;
            // 
            // W11_lbl9
            // 
            this.W11_lbl9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_lbl9.AutoEllipsis = true;
            this.W11_lbl9.BackColor = System.Drawing.Color.Transparent;
            this.W11_lbl9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W11_lbl9.Location = new System.Drawing.Point(48, 4);
            this.W11_lbl9.Name = "W11_lbl9";
            this.W11_lbl9.Size = new System.Drawing.Size(365, 19);
            this.W11_lbl9.TabIndex = 3;
            this.W11_lbl9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_Color_Index7
            // 
            this.W11_Color_Index7.AllowDrop = true;
            this.W11_Color_Index7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_Color_Index7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W11_Color_Index7.DefaultColor = System.Drawing.Color.Black;
            this.W11_Color_Index7.DontShowInfo = false;
            this.W11_Color_Index7.Location = new System.Drawing.Point(420, 4);
            this.W11_Color_Index7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W11_Color_Index7.Name = "W11_Color_Index7";
            this.W11_Color_Index7.Size = new System.Drawing.Size(90, 20);
            this.W11_Color_Index7.TabIndex = 2;
            this.W11_Color_Index7.Click += new System.EventHandler(this.W11_Color_Index7_Click);
            this.W11_Color_Index7.DragDrop += new System.Windows.Forms.DragEventHandler(this.W11_pick_DragDrop);
            // 
            // pnl8
            // 
            this.pnl8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.pnl8.Controls.Add(this.Label37);
            this.pnl8.Controls.Add(this.W11_pic8);
            this.pnl8.Controls.Add(this.W11_lbl8);
            this.pnl8.Controls.Add(this.W11_Color_Index6);
            this.pnl8.Location = new System.Drawing.Point(3, 251);
            this.pnl8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnl8.Name = "pnl8";
            this.pnl8.Size = new System.Drawing.Size(514, 28);
            this.pnl8.TabIndex = 23;
            // 
            // Label37
            // 
            this.Label37.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label37.AutoEllipsis = true;
            this.Label37.BackColor = System.Drawing.Color.Transparent;
            this.Label37.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label37.Location = new System.Drawing.Point(31, 4);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(14, 19);
            this.Label37.TabIndex = 6;
            this.Label37.Text = "8";
            this.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_pic8
            // 
            this.W11_pic8.BackColor = System.Drawing.Color.Transparent;
            this.W11_pic8.Location = new System.Drawing.Point(2, 2);
            this.W11_pic8.Name = "W11_pic8";
            this.W11_pic8.Size = new System.Drawing.Size(24, 24);
            this.W11_pic8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W11_pic8.TabIndex = 4;
            this.W11_pic8.TabStop = false;
            // 
            // W11_lbl8
            // 
            this.W11_lbl8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_lbl8.AutoEllipsis = true;
            this.W11_lbl8.BackColor = System.Drawing.Color.Transparent;
            this.W11_lbl8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W11_lbl8.Location = new System.Drawing.Point(48, 4);
            this.W11_lbl8.Name = "W11_lbl8";
            this.W11_lbl8.Size = new System.Drawing.Size(365, 19);
            this.W11_lbl8.TabIndex = 3;
            this.W11_lbl8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_Color_Index6
            // 
            this.W11_Color_Index6.AllowDrop = true;
            this.W11_Color_Index6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_Color_Index6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W11_Color_Index6.DefaultColor = System.Drawing.Color.Black;
            this.W11_Color_Index6.DontShowInfo = false;
            this.W11_Color_Index6.Location = new System.Drawing.Point(420, 4);
            this.W11_Color_Index6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W11_Color_Index6.Name = "W11_Color_Index6";
            this.W11_Color_Index6.Size = new System.Drawing.Size(90, 20);
            this.W11_Color_Index6.TabIndex = 2;
            this.W11_Color_Index6.Click += new System.EventHandler(this.W11_Color_Index6_Click);
            this.W11_Color_Index6.DragDrop += new System.Windows.Forms.DragEventHandler(this.W11_pick_DragDrop);
            // 
            // pnl7
            // 
            this.pnl7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.pnl7.Controls.Add(this.Label36);
            this.pnl7.Controls.Add(this.W11_pic7);
            this.pnl7.Controls.Add(this.W11_lbl7);
            this.pnl7.Controls.Add(this.W11_Color_Index5);
            this.pnl7.Location = new System.Drawing.Point(3, 221);
            this.pnl7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnl7.Name = "pnl7";
            this.pnl7.Size = new System.Drawing.Size(514, 28);
            this.pnl7.TabIndex = 22;
            // 
            // Label36
            // 
            this.Label36.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label36.AutoEllipsis = true;
            this.Label36.BackColor = System.Drawing.Color.Transparent;
            this.Label36.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label36.Location = new System.Drawing.Point(31, 4);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(14, 19);
            this.Label36.TabIndex = 6;
            this.Label36.Text = "7";
            this.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_pic7
            // 
            this.W11_pic7.BackColor = System.Drawing.Color.Transparent;
            this.W11_pic7.Location = new System.Drawing.Point(2, 2);
            this.W11_pic7.Name = "W11_pic7";
            this.W11_pic7.Size = new System.Drawing.Size(24, 24);
            this.W11_pic7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W11_pic7.TabIndex = 4;
            this.W11_pic7.TabStop = false;
            // 
            // W11_lbl7
            // 
            this.W11_lbl7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_lbl7.AutoEllipsis = true;
            this.W11_lbl7.BackColor = System.Drawing.Color.Transparent;
            this.W11_lbl7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W11_lbl7.Location = new System.Drawing.Point(48, 4);
            this.W11_lbl7.Name = "W11_lbl7";
            this.W11_lbl7.Size = new System.Drawing.Size(365, 19);
            this.W11_lbl7.TabIndex = 3;
            this.W11_lbl7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_Color_Index5
            // 
            this.W11_Color_Index5.AllowDrop = true;
            this.W11_Color_Index5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_Color_Index5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W11_Color_Index5.DefaultColor = System.Drawing.Color.Black;
            this.W11_Color_Index5.DontShowInfo = false;
            this.W11_Color_Index5.Location = new System.Drawing.Point(420, 4);
            this.W11_Color_Index5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W11_Color_Index5.Name = "W11_Color_Index5";
            this.W11_Color_Index5.Size = new System.Drawing.Size(90, 20);
            this.W11_Color_Index5.TabIndex = 2;
            this.W11_Color_Index5.Click += new System.EventHandler(this.W11_Color_Index5_Click);
            this.W11_Color_Index5.DragDrop += new System.Windows.Forms.DragEventHandler(this.W11_pick_DragDrop);
            // 
            // pnl4
            // 
            this.pnl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.pnl4.Controls.Add(this.Label18);
            this.pnl4.Controls.Add(this.W11_pic4);
            this.pnl4.Controls.Add(this.W11_lbl4);
            this.pnl4.Controls.Add(this.W11_Color_Index2);
            this.pnl4.Location = new System.Drawing.Point(3, 131);
            this.pnl4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnl4.Name = "pnl4";
            this.pnl4.Size = new System.Drawing.Size(514, 28);
            this.pnl4.TabIndex = 24;
            // 
            // Label18
            // 
            this.Label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label18.AutoEllipsis = true;
            this.Label18.BackColor = System.Drawing.Color.Transparent;
            this.Label18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(31, 4);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(14, 19);
            this.Label18.TabIndex = 6;
            this.Label18.Text = "4";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_pic4
            // 
            this.W11_pic4.BackColor = System.Drawing.Color.Transparent;
            this.W11_pic4.Location = new System.Drawing.Point(2, 2);
            this.W11_pic4.Name = "W11_pic4";
            this.W11_pic4.Size = new System.Drawing.Size(24, 24);
            this.W11_pic4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W11_pic4.TabIndex = 4;
            this.W11_pic4.TabStop = false;
            // 
            // W11_lbl4
            // 
            this.W11_lbl4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_lbl4.AutoEllipsis = true;
            this.W11_lbl4.BackColor = System.Drawing.Color.Transparent;
            this.W11_lbl4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W11_lbl4.Location = new System.Drawing.Point(48, 4);
            this.W11_lbl4.Name = "W11_lbl4";
            this.W11_lbl4.Size = new System.Drawing.Size(365, 19);
            this.W11_lbl4.TabIndex = 3;
            this.W11_lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_Color_Index2
            // 
            this.W11_Color_Index2.AllowDrop = true;
            this.W11_Color_Index2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_Color_Index2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W11_Color_Index2.DefaultColor = System.Drawing.Color.Black;
            this.W11_Color_Index2.DontShowInfo = false;
            this.W11_Color_Index2.Location = new System.Drawing.Point(420, 4);
            this.W11_Color_Index2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W11_Color_Index2.Name = "W11_Color_Index2";
            this.W11_Color_Index2.Size = new System.Drawing.Size(90, 20);
            this.W11_Color_Index2.TabIndex = 2;
            this.W11_Color_Index2.Click += new System.EventHandler(this.W11_Color_Index2_Click);
            this.W11_Color_Index2.DragDrop += new System.Windows.Forms.DragEventHandler(this.W11_pick_DragDrop);
            // 
            // PictureBox10
            // 
            this.PictureBox10.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox10.Image")));
            this.PictureBox10.Location = new System.Drawing.Point(3, 3);
            this.PictureBox10.Name = "PictureBox10";
            this.PictureBox10.Size = new System.Drawing.Size(35, 35);
            this.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox10.TabIndex = 1;
            this.PictureBox10.TabStop = false;
            // 
            // pnl6
            // 
            this.pnl6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.pnl6.Controls.Add(this.Label35);
            this.pnl6.Controls.Add(this.W11_pic6);
            this.pnl6.Controls.Add(this.W11_lbl6);
            this.pnl6.Controls.Add(this.W11_Color_Index4);
            this.pnl6.Location = new System.Drawing.Point(3, 191);
            this.pnl6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnl6.Name = "pnl6";
            this.pnl6.Size = new System.Drawing.Size(514, 28);
            this.pnl6.TabIndex = 21;
            // 
            // Label35
            // 
            this.Label35.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label35.AutoEllipsis = true;
            this.Label35.BackColor = System.Drawing.Color.Transparent;
            this.Label35.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label35.Location = new System.Drawing.Point(31, 4);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(14, 19);
            this.Label35.TabIndex = 6;
            this.Label35.Text = "6";
            this.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_pic6
            // 
            this.W11_pic6.BackColor = System.Drawing.Color.Transparent;
            this.W11_pic6.Location = new System.Drawing.Point(2, 2);
            this.W11_pic6.Name = "W11_pic6";
            this.W11_pic6.Size = new System.Drawing.Size(24, 24);
            this.W11_pic6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W11_pic6.TabIndex = 4;
            this.W11_pic6.TabStop = false;
            // 
            // W11_lbl6
            // 
            this.W11_lbl6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_lbl6.AutoEllipsis = true;
            this.W11_lbl6.BackColor = System.Drawing.Color.Transparent;
            this.W11_lbl6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W11_lbl6.Location = new System.Drawing.Point(48, 4);
            this.W11_lbl6.Name = "W11_lbl6";
            this.W11_lbl6.Size = new System.Drawing.Size(365, 19);
            this.W11_lbl6.TabIndex = 3;
            this.W11_lbl6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_Color_Index4
            // 
            this.W11_Color_Index4.AllowDrop = true;
            this.W11_Color_Index4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_Color_Index4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W11_Color_Index4.DefaultColor = System.Drawing.Color.Black;
            this.W11_Color_Index4.DontShowInfo = false;
            this.W11_Color_Index4.Location = new System.Drawing.Point(420, 4);
            this.W11_Color_Index4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W11_Color_Index4.Name = "W11_Color_Index4";
            this.W11_Color_Index4.Size = new System.Drawing.Size(90, 20);
            this.W11_Color_Index4.TabIndex = 2;
            this.W11_Color_Index4.Click += new System.EventHandler(this.W11_Color_Index4_pick_Click);
            this.W11_Color_Index4.DragDrop += new System.Windows.Forms.DragEventHandler(this.W11_pick_DragDrop);
            // 
            // pnl1
            // 
            this.pnl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.pnl1.Controls.Add(this.Label3);
            this.pnl1.Controls.Add(this.W11_pic1);
            this.pnl1.Controls.Add(this.W11_TaskbarFrontAndFoldersOnStart_pick);
            this.pnl1.Controls.Add(this.W11_lbl1);
            this.pnl1.Location = new System.Drawing.Point(3, 41);
            this.pnl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnl1.Name = "pnl1";
            this.pnl1.Size = new System.Drawing.Size(514, 28);
            this.pnl1.TabIndex = 17;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label3.AutoEllipsis = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(31, 4);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(14, 19);
            this.Label3.TabIndex = 5;
            this.Label3.Text = "1";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_pic1
            // 
            this.W11_pic1.BackColor = System.Drawing.Color.Transparent;
            this.W11_pic1.Location = new System.Drawing.Point(2, 2);
            this.W11_pic1.Name = "W11_pic1";
            this.W11_pic1.Size = new System.Drawing.Size(24, 24);
            this.W11_pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W11_pic1.TabIndex = 4;
            this.W11_pic1.TabStop = false;
            // 
            // W11_TaskbarFrontAndFoldersOnStart_pick
            // 
            this.W11_TaskbarFrontAndFoldersOnStart_pick.AllowDrop = true;
            this.W11_TaskbarFrontAndFoldersOnStart_pick.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_TaskbarFrontAndFoldersOnStart_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W11_TaskbarFrontAndFoldersOnStart_pick.DefaultColor = System.Drawing.Color.Black;
            this.W11_TaskbarFrontAndFoldersOnStart_pick.DontShowInfo = false;
            this.W11_TaskbarFrontAndFoldersOnStart_pick.Location = new System.Drawing.Point(420, 4);
            this.W11_TaskbarFrontAndFoldersOnStart_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W11_TaskbarFrontAndFoldersOnStart_pick.Name = "W11_TaskbarFrontAndFoldersOnStart_pick";
            this.W11_TaskbarFrontAndFoldersOnStart_pick.Size = new System.Drawing.Size(90, 20);
            this.W11_TaskbarFrontAndFoldersOnStart_pick.TabIndex = 2;
            this.W11_TaskbarFrontAndFoldersOnStart_pick.Click += new System.EventHandler(this.W11_TaskbarFrontAndFoldersOnStart_pick_Click);
            this.W11_TaskbarFrontAndFoldersOnStart_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.W11_pick_DragDrop);
            // 
            // W11_lbl1
            // 
            this.W11_lbl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_lbl1.AutoEllipsis = true;
            this.W11_lbl1.BackColor = System.Drawing.Color.Transparent;
            this.W11_lbl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W11_lbl1.Location = new System.Drawing.Point(48, 4);
            this.W11_lbl1.Name = "W11_lbl1";
            this.W11_lbl1.Size = new System.Drawing.Size(365, 19);
            this.W11_lbl1.TabIndex = 3;
            this.W11_lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl3
            // 
            this.pnl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.pnl3.Controls.Add(this.Label12);
            this.pnl3.Controls.Add(this.W11_pic3);
            this.pnl3.Controls.Add(this.W11_Color_Index1);
            this.pnl3.Controls.Add(this.W11_lbl3);
            this.pnl3.Location = new System.Drawing.Point(3, 101);
            this.pnl3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnl3.Name = "pnl3";
            this.pnl3.Size = new System.Drawing.Size(514, 28);
            this.pnl3.TabIndex = 18;
            // 
            // Label12
            // 
            this.Label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label12.AutoEllipsis = true;
            this.Label12.BackColor = System.Drawing.Color.Transparent;
            this.Label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(31, 4);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(14, 19);
            this.Label12.TabIndex = 6;
            this.Label12.Text = "3";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_pic3
            // 
            this.W11_pic3.BackColor = System.Drawing.Color.Transparent;
            this.W11_pic3.Location = new System.Drawing.Point(2, 2);
            this.W11_pic3.Name = "W11_pic3";
            this.W11_pic3.Size = new System.Drawing.Size(24, 24);
            this.W11_pic3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W11_pic3.TabIndex = 4;
            this.W11_pic3.TabStop = false;
            // 
            // W11_Color_Index1
            // 
            this.W11_Color_Index1.AllowDrop = true;
            this.W11_Color_Index1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_Color_Index1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W11_Color_Index1.DefaultColor = System.Drawing.Color.Black;
            this.W11_Color_Index1.DontShowInfo = false;
            this.W11_Color_Index1.Location = new System.Drawing.Point(420, 4);
            this.W11_Color_Index1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W11_Color_Index1.Name = "W11_Color_Index1";
            this.W11_Color_Index1.Size = new System.Drawing.Size(90, 20);
            this.W11_Color_Index1.TabIndex = 2;
            this.W11_Color_Index1.Click += new System.EventHandler(this.W11_Color_Index1_Click);
            this.W11_Color_Index1.DragDrop += new System.Windows.Forms.DragEventHandler(this.W11_pick_DragDrop);
            // 
            // W11_lbl3
            // 
            this.W11_lbl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_lbl3.AutoEllipsis = true;
            this.W11_lbl3.BackColor = System.Drawing.Color.Transparent;
            this.W11_lbl3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W11_lbl3.Location = new System.Drawing.Point(48, 4);
            this.W11_lbl3.Name = "W11_lbl3";
            this.W11_lbl3.Size = new System.Drawing.Size(365, 19);
            this.W11_lbl3.TabIndex = 3;
            this.W11_lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label10
            // 
            this.Label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label10.BackColor = System.Drawing.Color.Transparent;
            this.Label10.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label10.Location = new System.Drawing.Point(40, 3);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(157, 35);
            this.Label10.TabIndex = 0;
            this.Label10.Text = "Accents";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl2
            // 
            this.pnl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.pnl2.Controls.Add(this.Label4);
            this.pnl2.Controls.Add(this.W11_Color_Index0);
            this.pnl2.Controls.Add(this.W11_pic2);
            this.pnl2.Controls.Add(this.W11_lbl2);
            this.pnl2.Location = new System.Drawing.Point(3, 71);
            this.pnl2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnl2.Name = "pnl2";
            this.pnl2.Size = new System.Drawing.Size(514, 28);
            this.pnl2.TabIndex = 19;
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label4.AutoEllipsis = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(31, 4);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(14, 19);
            this.Label4.TabIndex = 6;
            this.Label4.Text = "2";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_Color_Index0
            // 
            this.W11_Color_Index0.AllowDrop = true;
            this.W11_Color_Index0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_Color_Index0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W11_Color_Index0.DefaultColor = System.Drawing.Color.Black;
            this.W11_Color_Index0.DontShowInfo = false;
            this.W11_Color_Index0.Location = new System.Drawing.Point(420, 4);
            this.W11_Color_Index0.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W11_Color_Index0.Name = "W11_Color_Index0";
            this.W11_Color_Index0.Size = new System.Drawing.Size(90, 20);
            this.W11_Color_Index0.TabIndex = 2;
            this.W11_Color_Index0.Click += new System.EventHandler(this.W11_Color_Index0_Click);
            this.W11_Color_Index0.DragDrop += new System.Windows.Forms.DragEventHandler(this.W11_pick_DragDrop);
            // 
            // W11_pic2
            // 
            this.W11_pic2.BackColor = System.Drawing.Color.Transparent;
            this.W11_pic2.Location = new System.Drawing.Point(2, 2);
            this.W11_pic2.Name = "W11_pic2";
            this.W11_pic2.Size = new System.Drawing.Size(24, 24);
            this.W11_pic2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W11_pic2.TabIndex = 4;
            this.W11_pic2.TabStop = false;
            // 
            // W11_lbl2
            // 
            this.W11_lbl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_lbl2.AutoEllipsis = true;
            this.W11_lbl2.BackColor = System.Drawing.Color.Transparent;
            this.W11_lbl2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W11_lbl2.Location = new System.Drawing.Point(48, 4);
            this.W11_lbl2.Name = "W11_lbl2";
            this.W11_lbl2.Size = new System.Drawing.Size(365, 19);
            this.W11_lbl2.TabIndex = 3;
            this.W11_lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl5
            // 
            this.pnl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.pnl5.Controls.Add(this.Label34);
            this.pnl5.Controls.Add(this.W11_pic5);
            this.pnl5.Controls.Add(this.W11_lbl5);
            this.pnl5.Controls.Add(this.W11_Color_Index3);
            this.pnl5.Location = new System.Drawing.Point(3, 161);
            this.pnl5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnl5.Name = "pnl5";
            this.pnl5.Size = new System.Drawing.Size(514, 28);
            this.pnl5.TabIndex = 20;
            // 
            // Label34
            // 
            this.Label34.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label34.AutoEllipsis = true;
            this.Label34.BackColor = System.Drawing.Color.Transparent;
            this.Label34.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label34.Location = new System.Drawing.Point(31, 4);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(14, 19);
            this.Label34.TabIndex = 6;
            this.Label34.Text = "5";
            this.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_pic5
            // 
            this.W11_pic5.BackColor = System.Drawing.Color.Transparent;
            this.W11_pic5.Location = new System.Drawing.Point(2, 2);
            this.W11_pic5.Name = "W11_pic5";
            this.W11_pic5.Size = new System.Drawing.Size(24, 24);
            this.W11_pic5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W11_pic5.TabIndex = 4;
            this.W11_pic5.TabStop = false;
            // 
            // W11_lbl5
            // 
            this.W11_lbl5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_lbl5.AutoEllipsis = true;
            this.W11_lbl5.BackColor = System.Drawing.Color.Transparent;
            this.W11_lbl5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W11_lbl5.Location = new System.Drawing.Point(48, 4);
            this.W11_lbl5.Name = "W11_lbl5";
            this.W11_lbl5.Size = new System.Drawing.Size(365, 19);
            this.W11_lbl5.TabIndex = 3;
            this.W11_lbl5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_Color_Index3
            // 
            this.W11_Color_Index3.AllowDrop = true;
            this.W11_Color_Index3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_Color_Index3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W11_Color_Index3.DefaultColor = System.Drawing.Color.Black;
            this.W11_Color_Index3.DontShowInfo = false;
            this.W11_Color_Index3.Location = new System.Drawing.Point(420, 4);
            this.W11_Color_Index3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W11_Color_Index3.Name = "W11_Color_Index3";
            this.W11_Color_Index3.Size = new System.Drawing.Size(90, 20);
            this.W11_Color_Index3.TabIndex = 2;
            this.W11_Color_Index3.Click += new System.EventHandler(this.W11_Color_Index3_Click);
            this.W11_Color_Index3.DragDrop += new System.Windows.Forms.DragEventHandler(this.W11_pick_DragDrop);
            // 
            // GroupBox5
            // 
            this.GroupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox5.Controls.Add(this.GroupBox6);
            this.GroupBox5.Controls.Add(this.GroupBox4);
            this.GroupBox5.Controls.Add(this.GroupBox18);
            this.GroupBox5.Controls.Add(this.GroupBox24);
            this.GroupBox5.Controls.Add(this.PictureBox17);
            this.GroupBox5.Controls.Add(this.Label17);
            this.GroupBox5.Location = new System.Drawing.Point(0, 0);
            this.GroupBox5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(520, 133);
            this.GroupBox5.TabIndex = 11;
            // 
            // GroupBox6
            // 
            this.GroupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox6.Controls.Add(this.W11_Accent_Taskbar);
            this.GroupBox6.Controls.Add(this.W11_Accent_StartTaskbar);
            this.GroupBox6.Controls.Add(this.W11_Accent_None);
            this.GroupBox6.Controls.Add(this.PictureBox19);
            this.GroupBox6.Controls.Add(this.Label19);
            this.GroupBox6.Location = new System.Drawing.Point(3, 101);
            this.GroupBox6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox6.Name = "GroupBox6";
            this.GroupBox6.Size = new System.Drawing.Size(514, 29);
            this.GroupBox6.TabIndex = 16;
            // 
            // W11_Accent_Taskbar
            // 
            this.W11_Accent_Taskbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_Accent_Taskbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.W11_Accent_Taskbar.Checked = false;
            this.W11_Accent_Taskbar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W11_Accent_Taskbar.ForeColor = System.Drawing.Color.White;
            this.W11_Accent_Taskbar.Image = null;
            this.W11_Accent_Taskbar.ImageWithText = false;
            this.W11_Accent_Taskbar.Location = new System.Drawing.Point(248, 2);
            this.W11_Accent_Taskbar.Name = "W11_Accent_Taskbar";
            this.W11_Accent_Taskbar.ShowText = true;
            this.W11_Accent_Taskbar.Size = new System.Drawing.Size(76, 25);
            this.W11_Accent_Taskbar.TabIndex = 23;
            this.W11_Accent_Taskbar.Text = "Taskbar";
            this.W11_Accent_Taskbar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.W11_Accent_Taskbar.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.W11_Accent_Taskbar_CheckedChanged);
            // 
            // W11_Accent_StartTaskbar
            // 
            this.W11_Accent_StartTaskbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_Accent_StartTaskbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.W11_Accent_StartTaskbar.Checked = false;
            this.W11_Accent_StartTaskbar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W11_Accent_StartTaskbar.ForeColor = System.Drawing.Color.White;
            this.W11_Accent_StartTaskbar.Image = null;
            this.W11_Accent_StartTaskbar.ImageWithText = false;
            this.W11_Accent_StartTaskbar.Location = new System.Drawing.Point(325, 2);
            this.W11_Accent_StartTaskbar.Name = "W11_Accent_StartTaskbar";
            this.W11_Accent_StartTaskbar.ShowText = true;
            this.W11_Accent_StartTaskbar.Size = new System.Drawing.Size(186, 25);
            this.W11_Accent_StartTaskbar.TabIndex = 22;
            this.W11_Accent_StartTaskbar.Text = "Start, taskbar & action Center";
            this.W11_Accent_StartTaskbar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.W11_Accent_StartTaskbar.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.W11_Accent_StartTaskbar_CheckedChanged);
            // 
            // W11_Accent_None
            // 
            this.W11_Accent_None.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_Accent_None.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.W11_Accent_None.Checked = false;
            this.W11_Accent_None.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W11_Accent_None.ForeColor = System.Drawing.Color.White;
            this.W11_Accent_None.Image = null;
            this.W11_Accent_None.ImageWithText = false;
            this.W11_Accent_None.Location = new System.Drawing.Point(181, 2);
            this.W11_Accent_None.Name = "W11_Accent_None";
            this.W11_Accent_None.ShowText = true;
            this.W11_Accent_None.Size = new System.Drawing.Size(66, 25);
            this.W11_Accent_None.TabIndex = 21;
            this.W11_Accent_None.Text = "Nothing";
            this.W11_Accent_None.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.W11_Accent_None.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.W11_Accent_None_CheckedChanged);
            // 
            // PictureBox19
            // 
            this.PictureBox19.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox19.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox19.Image")));
            this.PictureBox19.Location = new System.Drawing.Point(2, 2);
            this.PictureBox19.Name = "PictureBox19";
            this.PictureBox19.Size = new System.Drawing.Size(24, 24);
            this.PictureBox19.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox19.TabIndex = 4;
            this.PictureBox19.TabStop = false;
            // 
            // Label19
            // 
            this.Label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label19.AutoEllipsis = true;
            this.Label19.BackColor = System.Drawing.Color.Transparent;
            this.Label19.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label19.Location = new System.Drawing.Point(30, 3);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(150, 22);
            this.Label19.TabIndex = 3;
            this.Label19.Text = "Apply accent color on:";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox4
            // 
            this.GroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox4.Controls.Add(this.PictureBox2);
            this.GroupBox4.Controls.Add(this.Label2);
            this.GroupBox4.Controls.Add(this.W11_WinMode_Toggle);
            this.GroupBox4.Location = new System.Drawing.Point(3, 41);
            this.GroupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(256, 28);
            this.GroupBox4.TabIndex = 10;
            // 
            // PictureBox2
            // 
            this.PictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(2, 2);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(24, 24);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox2.TabIndex = 4;
            this.PictureBox2.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.AutoEllipsis = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label2.Location = new System.Drawing.Point(30, 4);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(171, 20);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "Windows mode";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_WinMode_Toggle
            // 
            this.W11_WinMode_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_WinMode_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W11_WinMode_Toggle.Checked = false;
            this.W11_WinMode_Toggle.DarkLight_Toggler = true;
            this.W11_WinMode_Toggle.Location = new System.Drawing.Point(212, 4);
            this.W11_WinMode_Toggle.Name = "W11_WinMode_Toggle";
            this.W11_WinMode_Toggle.Size = new System.Drawing.Size(40, 20);
            this.W11_WinMode_Toggle.TabIndex = 8;
            this.W11_WinMode_Toggle.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.W11_WinMode_Toggle_CheckedChanged);
            // 
            // GroupBox18
            // 
            this.GroupBox18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox18.Controls.Add(this.W11_Transparency_Toggle);
            this.GroupBox18.Controls.Add(this.PictureBox18);
            this.GroupBox18.Controls.Add(this.Label9);
            this.GroupBox18.Location = new System.Drawing.Point(3, 71);
            this.GroupBox18.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox18.Name = "GroupBox18";
            this.GroupBox18.Size = new System.Drawing.Size(514, 28);
            this.GroupBox18.TabIndex = 9;
            // 
            // W11_Transparency_Toggle
            // 
            this.W11_Transparency_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_Transparency_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W11_Transparency_Toggle.Checked = false;
            this.W11_Transparency_Toggle.DarkLight_Toggler = false;
            this.W11_Transparency_Toggle.Location = new System.Drawing.Point(470, 4);
            this.W11_Transparency_Toggle.Name = "W11_Transparency_Toggle";
            this.W11_Transparency_Toggle.Size = new System.Drawing.Size(40, 20);
            this.W11_Transparency_Toggle.TabIndex = 16;
            this.W11_Transparency_Toggle.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.W11_Transparency_Toggle_CheckedChanged);
            // 
            // PictureBox18
            // 
            this.PictureBox18.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox18.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox18.Image")));
            this.PictureBox18.Location = new System.Drawing.Point(2, 2);
            this.PictureBox18.Name = "PictureBox18";
            this.PictureBox18.Size = new System.Drawing.Size(24, 24);
            this.PictureBox18.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox18.TabIndex = 4;
            this.PictureBox18.TabStop = false;
            // 
            // Label9
            // 
            this.Label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label9.AutoEllipsis = true;
            this.Label9.BackColor = System.Drawing.Color.Transparent;
            this.Label9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label9.Location = new System.Drawing.Point(30, 4);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(434, 20);
            this.Label9.TabIndex = 13;
            this.Label9.Text = "Transparency (Mica/Acrylic)";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox24
            // 
            this.GroupBox24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox24.Controls.Add(this.PictureBox20);
            this.GroupBox24.Controls.Add(this.W11_AppMode_Toggle);
            this.GroupBox24.Controls.Add(this.Label7);
            this.GroupBox24.Location = new System.Drawing.Point(261, 41);
            this.GroupBox24.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox24.Name = "GroupBox24";
            this.GroupBox24.Size = new System.Drawing.Size(256, 28);
            this.GroupBox24.TabIndex = 8;
            // 
            // PictureBox20
            // 
            this.PictureBox20.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox20.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox20.Image")));
            this.PictureBox20.Location = new System.Drawing.Point(2, 2);
            this.PictureBox20.Name = "PictureBox20";
            this.PictureBox20.Size = new System.Drawing.Size(24, 24);
            this.PictureBox20.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox20.TabIndex = 4;
            this.PictureBox20.TabStop = false;
            // 
            // W11_AppMode_Toggle
            // 
            this.W11_AppMode_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_AppMode_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W11_AppMode_Toggle.Checked = false;
            this.W11_AppMode_Toggle.DarkLight_Toggler = true;
            this.W11_AppMode_Toggle.Location = new System.Drawing.Point(212, 4);
            this.W11_AppMode_Toggle.Name = "W11_AppMode_Toggle";
            this.W11_AppMode_Toggle.Size = new System.Drawing.Size(40, 20);
            this.W11_AppMode_Toggle.TabIndex = 11;
            this.W11_AppMode_Toggle.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.W11_AppMode_Toggle_CheckedChanged);
            // 
            // Label7
            // 
            this.Label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label7.AutoEllipsis = true;
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label7.Location = new System.Drawing.Point(30, 4);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(169, 20);
            this.Label7.TabIndex = 10;
            this.Label7.Text = "App mode";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox17
            // 
            this.PictureBox17.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox17.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox17.Image")));
            this.PictureBox17.Location = new System.Drawing.Point(3, 3);
            this.PictureBox17.Name = "PictureBox17";
            this.PictureBox17.Size = new System.Drawing.Size(35, 35);
            this.PictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox17.TabIndex = 1;
            this.PictureBox17.TabStop = false;
            // 
            // Label17
            // 
            this.Label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label17.BackColor = System.Drawing.Color.Transparent;
            this.Label17.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label17.Location = new System.Drawing.Point(38, 3);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(479, 35);
            this.Label17.TabIndex = 0;
            this.Label17.Text = "Toggles";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox1.Controls.Add(this.W11_Button8);
            this.GroupBox1.Controls.Add(this.W11_ShowAccentOnTitlebarAndBorders_Toggle);
            this.GroupBox1.Controls.Add(this.GroupBox20);
            this.GroupBox1.Controls.Add(this.PictureBox1);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.GroupBox9);
            this.GroupBox1.Location = new System.Drawing.Point(0, 137);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(520, 72);
            this.GroupBox1.TabIndex = 2;
            // 
            // W11_Button8
            // 
            this.W11_Button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_Button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W11_Button8.DrawOnGlass = false;
            this.W11_Button8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.W11_Button8.ForeColor = System.Drawing.Color.White;
            this.W11_Button8.Image = null;
            this.W11_Button8.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(49)))), ((int)(((byte)(61)))));
            this.W11_Button8.Location = new System.Drawing.Point(448, 10);
            this.W11_Button8.Name = "W11_Button8";
            this.W11_Button8.Size = new System.Drawing.Size(20, 20);
            this.W11_Button8.TabIndex = 27;
            this.W11_Button8.Text = "?";
            this.W11_Button8.UseVisualStyleBackColor = false;
            this.W11_Button8.Click += new System.EventHandler(this.W11_Button8_Click_1);
            // 
            // W11_ShowAccentOnTitlebarAndBorders_Toggle
            // 
            this.W11_ShowAccentOnTitlebarAndBorders_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_ShowAccentOnTitlebarAndBorders_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W11_ShowAccentOnTitlebarAndBorders_Toggle.Checked = false;
            this.W11_ShowAccentOnTitlebarAndBorders_Toggle.DarkLight_Toggler = false;
            this.W11_ShowAccentOnTitlebarAndBorders_Toggle.Location = new System.Drawing.Point(474, 10);
            this.W11_ShowAccentOnTitlebarAndBorders_Toggle.Name = "W11_ShowAccentOnTitlebarAndBorders_Toggle";
            this.W11_ShowAccentOnTitlebarAndBorders_Toggle.Size = new System.Drawing.Size(40, 20);
            this.W11_ShowAccentOnTitlebarAndBorders_Toggle.TabIndex = 6;
            this.W11_ShowAccentOnTitlebarAndBorders_Toggle.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.W11_ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged);
            // 
            // GroupBox20
            // 
            this.GroupBox20.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.GroupBox20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox20.Controls.Add(this.PictureBox11);
            this.GroupBox20.Controls.Add(this.Label11);
            this.GroupBox20.Controls.Add(this.W11_InactiveTitlebar_pick);
            this.GroupBox20.Location = new System.Drawing.Point(261, 41);
            this.GroupBox20.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox20.Name = "GroupBox20";
            this.GroupBox20.Size = new System.Drawing.Size(256, 28);
            this.GroupBox20.TabIndex = 6;
            // 
            // PictureBox11
            // 
            this.PictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox11.Image")));
            this.PictureBox11.Location = new System.Drawing.Point(2, 2);
            this.PictureBox11.Name = "PictureBox11";
            this.PictureBox11.Size = new System.Drawing.Size(24, 24);
            this.PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox11.TabIndex = 4;
            this.PictureBox11.TabStop = false;
            // 
            // Label11
            // 
            this.Label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label11.AutoEllipsis = true;
            this.Label11.BackColor = System.Drawing.Color.Transparent;
            this.Label11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label11.Location = new System.Drawing.Point(30, 4);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(125, 20);
            this.Label11.TabIndex = 3;
            this.Label11.Text = "Inactive titlebar";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_InactiveTitlebar_pick
            // 
            this.W11_InactiveTitlebar_pick.AllowDrop = true;
            this.W11_InactiveTitlebar_pick.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_InactiveTitlebar_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W11_InactiveTitlebar_pick.DefaultColor = System.Drawing.Color.Black;
            this.W11_InactiveTitlebar_pick.DontShowInfo = false;
            this.W11_InactiveTitlebar_pick.Location = new System.Drawing.Point(162, 4);
            this.W11_InactiveTitlebar_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W11_InactiveTitlebar_pick.Name = "W11_InactiveTitlebar_pick";
            this.W11_InactiveTitlebar_pick.Size = new System.Drawing.Size(90, 20);
            this.W11_InactiveTitlebar_pick.TabIndex = 2;
            this.W11_InactiveTitlebar_pick.Click += new System.EventHandler(this.W11_InactiveTitlebar_pick_Click);
            this.W11_InactiveTitlebar_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.W11_pick_DragDrop);
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(3, 3);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(35, 35);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 1;
            this.PictureBox1.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label1.Location = new System.Drawing.Point(40, 3);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(401, 35);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Titlebars";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox9
            // 
            this.GroupBox9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.GroupBox9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox9.Controls.Add(this.PictureBox5);
            this.GroupBox9.Controls.Add(this.Label5);
            this.GroupBox9.Controls.Add(this.W11_ActiveTitlebar_pick);
            this.GroupBox9.Location = new System.Drawing.Point(3, 41);
            this.GroupBox9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox9.Name = "GroupBox9";
            this.GroupBox9.Size = new System.Drawing.Size(256, 28);
            this.GroupBox9.TabIndex = 5;
            // 
            // PictureBox5
            // 
            this.PictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(2, 2);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(24, 24);
            this.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox5.TabIndex = 4;
            this.PictureBox5.TabStop = false;
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.AutoEllipsis = true;
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label5.Location = new System.Drawing.Point(30, 5);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(125, 19);
            this.Label5.TabIndex = 3;
            this.Label5.Text = "Active titlebar";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W11_ActiveTitlebar_pick
            // 
            this.W11_ActiveTitlebar_pick.AllowDrop = true;
            this.W11_ActiveTitlebar_pick.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W11_ActiveTitlebar_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W11_ActiveTitlebar_pick.DefaultColor = System.Drawing.Color.Black;
            this.W11_ActiveTitlebar_pick.DontShowInfo = false;
            this.W11_ActiveTitlebar_pick.Location = new System.Drawing.Point(162, 4);
            this.W11_ActiveTitlebar_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W11_ActiveTitlebar_pick.Name = "W11_ActiveTitlebar_pick";
            this.W11_ActiveTitlebar_pick.Size = new System.Drawing.Size(90, 20);
            this.W11_ActiveTitlebar_pick.TabIndex = 2;
            this.W11_ActiveTitlebar_pick.Click += new System.EventHandler(this.W11_ActiveTitlebar_pick_Click);
            this.W11_ActiveTitlebar_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.W11_pick_DragDrop);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage2.Controls.Add(this.Panel1);
            this.TabPage2.Location = new System.Drawing.Point(4, 24);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(529, 517);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "W10";
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.Controls.Add(this.GroupBox2);
            this.Panel1.Controls.Add(this.GroupBox37);
            this.Panel1.Controls.Add(this.GroupBox44);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(3, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(523, 511);
            this.Panel1.TabIndex = 19;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox2.Controls.Add(this.Button38);
            this.GroupBox2.Controls.Add(this.GroupBox8);
            this.GroupBox2.Controls.Add(this.GroupBox16);
            this.GroupBox2.Controls.Add(this.GroupBox25);
            this.GroupBox2.Controls.Add(this.GroupBox27);
            this.GroupBox2.Controls.Add(this.PictureBox7);
            this.GroupBox2.Controls.Add(this.GroupBox28);
            this.GroupBox2.Controls.Add(this.GroupBox31);
            this.GroupBox2.Controls.Add(this.GroupBox34);
            this.GroupBox2.Controls.Add(this.Label49);
            this.GroupBox2.Controls.Add(this.GroupBox35);
            this.GroupBox2.Controls.Add(this.GroupBox36);
            this.GroupBox2.Location = new System.Drawing.Point(0, 213);
            this.GroupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(520, 313);
            this.GroupBox2.TabIndex = 6;
            // 
            // Button38
            // 
            this.Button38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Button38.DrawOnGlass = false;
            this.Button38.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button38.ForeColor = System.Drawing.Color.White;
            this.Button38.Image = null;
            this.Button38.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.Button38.Location = new System.Drawing.Point(300, 9);
            this.Button38.Name = "Button38";
            this.Button38.Size = new System.Drawing.Size(214, 22);
            this.Button38.TabIndex = 30;
            this.Button38.Text = "Copycat from Windows 11 presets";
            this.Button38.UseVisualStyleBackColor = false;
            this.Button38.Click += new System.EventHandler(this.Button38_Click);
            // 
            // GroupBox8
            // 
            this.GroupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox8.Controls.Add(this.Label13);
            this.GroupBox8.Controls.Add(this.W10_pic9);
            this.GroupBox8.Controls.Add(this.W10_lbl9);
            this.GroupBox8.Controls.Add(this.W10_Color_Index7);
            this.GroupBox8.Location = new System.Drawing.Point(3, 281);
            this.GroupBox8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox8.Name = "GroupBox8";
            this.GroupBox8.Size = new System.Drawing.Size(514, 28);
            this.GroupBox8.TabIndex = 25;
            // 
            // Label13
            // 
            this.Label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label13.AutoEllipsis = true;
            this.Label13.BackColor = System.Drawing.Color.Transparent;
            this.Label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.Location = new System.Drawing.Point(31, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(14, 19);
            this.Label13.TabIndex = 6;
            this.Label13.Text = "9";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_pic9
            // 
            this.W10_pic9.BackColor = System.Drawing.Color.Transparent;
            this.W10_pic9.Image = global::WinPaletter.Properties.Resources.Mini_Undefined;
            this.W10_pic9.Location = new System.Drawing.Point(2, 2);
            this.W10_pic9.Name = "W10_pic9";
            this.W10_pic9.Size = new System.Drawing.Size(24, 24);
            this.W10_pic9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W10_pic9.TabIndex = 4;
            this.W10_pic9.TabStop = false;
            // 
            // W10_lbl9
            // 
            this.W10_lbl9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_lbl9.AutoEllipsis = true;
            this.W10_lbl9.BackColor = System.Drawing.Color.Transparent;
            this.W10_lbl9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W10_lbl9.Location = new System.Drawing.Point(48, 4);
            this.W10_lbl9.Name = "W10_lbl9";
            this.W10_lbl9.Size = new System.Drawing.Size(365, 19);
            this.W10_lbl9.TabIndex = 3;
            this.W10_lbl9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_Color_Index7
            // 
            this.W10_Color_Index7.AllowDrop = true;
            this.W10_Color_Index7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_Color_Index7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W10_Color_Index7.DefaultColor = System.Drawing.Color.Black;
            this.W10_Color_Index7.DontShowInfo = false;
            this.W10_Color_Index7.Location = new System.Drawing.Point(420, 4);
            this.W10_Color_Index7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W10_Color_Index7.Name = "W10_Color_Index7";
            this.W10_Color_Index7.Size = new System.Drawing.Size(90, 20);
            this.W10_Color_Index7.TabIndex = 2;
            this.W10_Color_Index7.Click += new System.EventHandler(this.W10_Color_Index7_Click);
            this.W10_Color_Index7.DragDrop += new System.Windows.Forms.DragEventHandler(this.W10_pick_DragDrop);
            // 
            // GroupBox16
            // 
            this.GroupBox16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox16.Controls.Add(this.Label43);
            this.GroupBox16.Controls.Add(this.W10_pic8);
            this.GroupBox16.Controls.Add(this.W10_lbl8);
            this.GroupBox16.Controls.Add(this.W10_Color_Index6);
            this.GroupBox16.Location = new System.Drawing.Point(3, 251);
            this.GroupBox16.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox16.Name = "GroupBox16";
            this.GroupBox16.Size = new System.Drawing.Size(514, 28);
            this.GroupBox16.TabIndex = 23;
            // 
            // Label43
            // 
            this.Label43.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label43.AutoEllipsis = true;
            this.Label43.BackColor = System.Drawing.Color.Transparent;
            this.Label43.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label43.Location = new System.Drawing.Point(31, 4);
            this.Label43.Name = "Label43";
            this.Label43.Size = new System.Drawing.Size(14, 19);
            this.Label43.TabIndex = 6;
            this.Label43.Text = "8";
            this.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_pic8
            // 
            this.W10_pic8.BackColor = System.Drawing.Color.Transparent;
            this.W10_pic8.Location = new System.Drawing.Point(2, 2);
            this.W10_pic8.Name = "W10_pic8";
            this.W10_pic8.Size = new System.Drawing.Size(24, 24);
            this.W10_pic8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W10_pic8.TabIndex = 4;
            this.W10_pic8.TabStop = false;
            // 
            // W10_lbl8
            // 
            this.W10_lbl8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_lbl8.AutoEllipsis = true;
            this.W10_lbl8.BackColor = System.Drawing.Color.Transparent;
            this.W10_lbl8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W10_lbl8.Location = new System.Drawing.Point(48, 4);
            this.W10_lbl8.Name = "W10_lbl8";
            this.W10_lbl8.Size = new System.Drawing.Size(365, 19);
            this.W10_lbl8.TabIndex = 3;
            this.W10_lbl8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_Color_Index6
            // 
            this.W10_Color_Index6.AllowDrop = true;
            this.W10_Color_Index6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_Color_Index6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W10_Color_Index6.DefaultColor = System.Drawing.Color.Black;
            this.W10_Color_Index6.DontShowInfo = false;
            this.W10_Color_Index6.Location = new System.Drawing.Point(420, 4);
            this.W10_Color_Index6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W10_Color_Index6.Name = "W10_Color_Index6";
            this.W10_Color_Index6.Size = new System.Drawing.Size(90, 20);
            this.W10_Color_Index6.TabIndex = 2;
            this.W10_Color_Index6.Click += new System.EventHandler(this.W10_Color_Index6_Click);
            this.W10_Color_Index6.DragDrop += new System.Windows.Forms.DragEventHandler(this.W10_pick_DragDrop);
            // 
            // GroupBox25
            // 
            this.GroupBox25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox25.Controls.Add(this.Label44);
            this.GroupBox25.Controls.Add(this.W10_pic7);
            this.GroupBox25.Controls.Add(this.W10_lbl7);
            this.GroupBox25.Controls.Add(this.W10_Color_Index5);
            this.GroupBox25.Location = new System.Drawing.Point(3, 221);
            this.GroupBox25.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox25.Name = "GroupBox25";
            this.GroupBox25.Size = new System.Drawing.Size(514, 28);
            this.GroupBox25.TabIndex = 22;
            // 
            // Label44
            // 
            this.Label44.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label44.AutoEllipsis = true;
            this.Label44.BackColor = System.Drawing.Color.Transparent;
            this.Label44.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label44.Location = new System.Drawing.Point(31, 4);
            this.Label44.Name = "Label44";
            this.Label44.Size = new System.Drawing.Size(14, 19);
            this.Label44.TabIndex = 6;
            this.Label44.Text = "7";
            this.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_pic7
            // 
            this.W10_pic7.BackColor = System.Drawing.Color.Transparent;
            this.W10_pic7.Location = new System.Drawing.Point(2, 2);
            this.W10_pic7.Name = "W10_pic7";
            this.W10_pic7.Size = new System.Drawing.Size(24, 24);
            this.W10_pic7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W10_pic7.TabIndex = 4;
            this.W10_pic7.TabStop = false;
            // 
            // W10_lbl7
            // 
            this.W10_lbl7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_lbl7.AutoEllipsis = true;
            this.W10_lbl7.BackColor = System.Drawing.Color.Transparent;
            this.W10_lbl7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W10_lbl7.Location = new System.Drawing.Point(48, 4);
            this.W10_lbl7.Name = "W10_lbl7";
            this.W10_lbl7.Size = new System.Drawing.Size(365, 19);
            this.W10_lbl7.TabIndex = 3;
            this.W10_lbl7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_Color_Index5
            // 
            this.W10_Color_Index5.AllowDrop = true;
            this.W10_Color_Index5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_Color_Index5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W10_Color_Index5.DefaultColor = System.Drawing.Color.Black;
            this.W10_Color_Index5.DontShowInfo = false;
            this.W10_Color_Index5.Location = new System.Drawing.Point(420, 4);
            this.W10_Color_Index5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W10_Color_Index5.Name = "W10_Color_Index5";
            this.W10_Color_Index5.Size = new System.Drawing.Size(90, 20);
            this.W10_Color_Index5.TabIndex = 2;
            this.W10_Color_Index5.Click += new System.EventHandler(this.W10_Color_Index5_Click);
            this.W10_Color_Index5.DragDrop += new System.Windows.Forms.DragEventHandler(this.W10_pick_DragDrop);
            // 
            // GroupBox27
            // 
            this.GroupBox27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox27.Controls.Add(this.Label45);
            this.GroupBox27.Controls.Add(this.W10_pic4);
            this.GroupBox27.Controls.Add(this.W10_lbl4);
            this.GroupBox27.Controls.Add(this.W10_Color_Index2);
            this.GroupBox27.Location = new System.Drawing.Point(3, 131);
            this.GroupBox27.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox27.Name = "GroupBox27";
            this.GroupBox27.Size = new System.Drawing.Size(514, 28);
            this.GroupBox27.TabIndex = 24;
            // 
            // Label45
            // 
            this.Label45.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label45.AutoEllipsis = true;
            this.Label45.BackColor = System.Drawing.Color.Transparent;
            this.Label45.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label45.Location = new System.Drawing.Point(31, 4);
            this.Label45.Name = "Label45";
            this.Label45.Size = new System.Drawing.Size(14, 19);
            this.Label45.TabIndex = 6;
            this.Label45.Text = "4";
            this.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_pic4
            // 
            this.W10_pic4.BackColor = System.Drawing.Color.Transparent;
            this.W10_pic4.Location = new System.Drawing.Point(2, 2);
            this.W10_pic4.Name = "W10_pic4";
            this.W10_pic4.Size = new System.Drawing.Size(24, 24);
            this.W10_pic4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W10_pic4.TabIndex = 4;
            this.W10_pic4.TabStop = false;
            // 
            // W10_lbl4
            // 
            this.W10_lbl4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_lbl4.AutoEllipsis = true;
            this.W10_lbl4.BackColor = System.Drawing.Color.Transparent;
            this.W10_lbl4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W10_lbl4.Location = new System.Drawing.Point(48, 4);
            this.W10_lbl4.Name = "W10_lbl4";
            this.W10_lbl4.Size = new System.Drawing.Size(365, 19);
            this.W10_lbl4.TabIndex = 3;
            this.W10_lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_Color_Index2
            // 
            this.W10_Color_Index2.AllowDrop = true;
            this.W10_Color_Index2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_Color_Index2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W10_Color_Index2.DefaultColor = System.Drawing.Color.Black;
            this.W10_Color_Index2.DontShowInfo = false;
            this.W10_Color_Index2.Location = new System.Drawing.Point(420, 4);
            this.W10_Color_Index2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W10_Color_Index2.Name = "W10_Color_Index2";
            this.W10_Color_Index2.Size = new System.Drawing.Size(90, 20);
            this.W10_Color_Index2.TabIndex = 2;
            this.W10_Color_Index2.Click += new System.EventHandler(this.W10_Color_Index2_Click);
            this.W10_Color_Index2.DragDrop += new System.Windows.Forms.DragEventHandler(this.W10_pick_DragDrop);
            // 
            // PictureBox7
            // 
            this.PictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox7.Image")));
            this.PictureBox7.Location = new System.Drawing.Point(3, 3);
            this.PictureBox7.Name = "PictureBox7";
            this.PictureBox7.Size = new System.Drawing.Size(35, 35);
            this.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox7.TabIndex = 1;
            this.PictureBox7.TabStop = false;
            // 
            // GroupBox28
            // 
            this.GroupBox28.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox28.Controls.Add(this.Label46);
            this.GroupBox28.Controls.Add(this.W10_pic6);
            this.GroupBox28.Controls.Add(this.W10_lbl6);
            this.GroupBox28.Controls.Add(this.W10_Color_Index4);
            this.GroupBox28.Location = new System.Drawing.Point(3, 191);
            this.GroupBox28.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox28.Name = "GroupBox28";
            this.GroupBox28.Size = new System.Drawing.Size(514, 28);
            this.GroupBox28.TabIndex = 21;
            // 
            // Label46
            // 
            this.Label46.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label46.AutoEllipsis = true;
            this.Label46.BackColor = System.Drawing.Color.Transparent;
            this.Label46.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label46.Location = new System.Drawing.Point(31, 4);
            this.Label46.Name = "Label46";
            this.Label46.Size = new System.Drawing.Size(14, 19);
            this.Label46.TabIndex = 6;
            this.Label46.Text = "6";
            this.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_pic6
            // 
            this.W10_pic6.BackColor = System.Drawing.Color.Transparent;
            this.W10_pic6.Location = new System.Drawing.Point(2, 2);
            this.W10_pic6.Name = "W10_pic6";
            this.W10_pic6.Size = new System.Drawing.Size(24, 24);
            this.W10_pic6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W10_pic6.TabIndex = 4;
            this.W10_pic6.TabStop = false;
            // 
            // W10_lbl6
            // 
            this.W10_lbl6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_lbl6.AutoEllipsis = true;
            this.W10_lbl6.BackColor = System.Drawing.Color.Transparent;
            this.W10_lbl6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W10_lbl6.Location = new System.Drawing.Point(48, 4);
            this.W10_lbl6.Name = "W10_lbl6";
            this.W10_lbl6.Size = new System.Drawing.Size(365, 19);
            this.W10_lbl6.TabIndex = 3;
            this.W10_lbl6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_Color_Index4
            // 
            this.W10_Color_Index4.AllowDrop = true;
            this.W10_Color_Index4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_Color_Index4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W10_Color_Index4.DefaultColor = System.Drawing.Color.Black;
            this.W10_Color_Index4.DontShowInfo = false;
            this.W10_Color_Index4.Location = new System.Drawing.Point(420, 4);
            this.W10_Color_Index4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W10_Color_Index4.Name = "W10_Color_Index4";
            this.W10_Color_Index4.Size = new System.Drawing.Size(90, 20);
            this.W10_Color_Index4.TabIndex = 2;
            this.W10_Color_Index4.Click += new System.EventHandler(this.W10_Color_Index4_pick_Click);
            this.W10_Color_Index4.DragDrop += new System.Windows.Forms.DragEventHandler(this.W10_pick_DragDrop);
            // 
            // GroupBox31
            // 
            this.GroupBox31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox31.Controls.Add(this.Label47);
            this.GroupBox31.Controls.Add(this.W10_pic1);
            this.GroupBox31.Controls.Add(this.W10_TaskbarFrontAndFoldersOnStart_pick);
            this.GroupBox31.Controls.Add(this.W10_lbl1);
            this.GroupBox31.Location = new System.Drawing.Point(3, 41);
            this.GroupBox31.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox31.Name = "GroupBox31";
            this.GroupBox31.Size = new System.Drawing.Size(514, 28);
            this.GroupBox31.TabIndex = 17;
            // 
            // Label47
            // 
            this.Label47.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label47.AutoEllipsis = true;
            this.Label47.BackColor = System.Drawing.Color.Transparent;
            this.Label47.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label47.Location = new System.Drawing.Point(31, 4);
            this.Label47.Name = "Label47";
            this.Label47.Size = new System.Drawing.Size(14, 19);
            this.Label47.TabIndex = 5;
            this.Label47.Text = "1";
            this.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_pic1
            // 
            this.W10_pic1.BackColor = System.Drawing.Color.Transparent;
            this.W10_pic1.Location = new System.Drawing.Point(2, 2);
            this.W10_pic1.Name = "W10_pic1";
            this.W10_pic1.Size = new System.Drawing.Size(24, 24);
            this.W10_pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W10_pic1.TabIndex = 4;
            this.W10_pic1.TabStop = false;
            // 
            // W10_TaskbarFrontAndFoldersOnStart_pick
            // 
            this.W10_TaskbarFrontAndFoldersOnStart_pick.AllowDrop = true;
            this.W10_TaskbarFrontAndFoldersOnStart_pick.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_TaskbarFrontAndFoldersOnStart_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W10_TaskbarFrontAndFoldersOnStart_pick.DefaultColor = System.Drawing.Color.Black;
            this.W10_TaskbarFrontAndFoldersOnStart_pick.DontShowInfo = false;
            this.W10_TaskbarFrontAndFoldersOnStart_pick.Location = new System.Drawing.Point(420, 4);
            this.W10_TaskbarFrontAndFoldersOnStart_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W10_TaskbarFrontAndFoldersOnStart_pick.Name = "W10_TaskbarFrontAndFoldersOnStart_pick";
            this.W10_TaskbarFrontAndFoldersOnStart_pick.Size = new System.Drawing.Size(90, 20);
            this.W10_TaskbarFrontAndFoldersOnStart_pick.TabIndex = 2;
            this.W10_TaskbarFrontAndFoldersOnStart_pick.Click += new System.EventHandler(this.W10_TaskbarFrontAndFoldersOnStart_pick_Click);
            this.W10_TaskbarFrontAndFoldersOnStart_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.W10_pick_DragDrop);
            // 
            // W10_lbl1
            // 
            this.W10_lbl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_lbl1.AutoEllipsis = true;
            this.W10_lbl1.BackColor = System.Drawing.Color.Transparent;
            this.W10_lbl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W10_lbl1.Location = new System.Drawing.Point(48, 4);
            this.W10_lbl1.Name = "W10_lbl1";
            this.W10_lbl1.Size = new System.Drawing.Size(365, 19);
            this.W10_lbl1.TabIndex = 3;
            this.W10_lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox34
            // 
            this.GroupBox34.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox34.Controls.Add(this.Label48);
            this.GroupBox34.Controls.Add(this.W10_pic3);
            this.GroupBox34.Controls.Add(this.W10_Color_Index1);
            this.GroupBox34.Controls.Add(this.W10_lbl3);
            this.GroupBox34.Location = new System.Drawing.Point(3, 101);
            this.GroupBox34.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox34.Name = "GroupBox34";
            this.GroupBox34.Size = new System.Drawing.Size(514, 28);
            this.GroupBox34.TabIndex = 18;
            // 
            // Label48
            // 
            this.Label48.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label48.AutoEllipsis = true;
            this.Label48.BackColor = System.Drawing.Color.Transparent;
            this.Label48.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label48.Location = new System.Drawing.Point(31, 4);
            this.Label48.Name = "Label48";
            this.Label48.Size = new System.Drawing.Size(14, 19);
            this.Label48.TabIndex = 6;
            this.Label48.Text = "3";
            this.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_pic3
            // 
            this.W10_pic3.BackColor = System.Drawing.Color.Transparent;
            this.W10_pic3.Location = new System.Drawing.Point(2, 2);
            this.W10_pic3.Name = "W10_pic3";
            this.W10_pic3.Size = new System.Drawing.Size(24, 24);
            this.W10_pic3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W10_pic3.TabIndex = 4;
            this.W10_pic3.TabStop = false;
            // 
            // W10_Color_Index1
            // 
            this.W10_Color_Index1.AllowDrop = true;
            this.W10_Color_Index1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_Color_Index1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W10_Color_Index1.DefaultColor = System.Drawing.Color.Black;
            this.W10_Color_Index1.DontShowInfo = false;
            this.W10_Color_Index1.Location = new System.Drawing.Point(420, 4);
            this.W10_Color_Index1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W10_Color_Index1.Name = "W10_Color_Index1";
            this.W10_Color_Index1.Size = new System.Drawing.Size(90, 20);
            this.W10_Color_Index1.TabIndex = 2;
            this.W10_Color_Index1.Click += new System.EventHandler(this.W10_Color_Index1_Click);
            this.W10_Color_Index1.DragDrop += new System.Windows.Forms.DragEventHandler(this.W10_pick_DragDrop);
            // 
            // W10_lbl3
            // 
            this.W10_lbl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_lbl3.AutoEllipsis = true;
            this.W10_lbl3.BackColor = System.Drawing.Color.Transparent;
            this.W10_lbl3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W10_lbl3.Location = new System.Drawing.Point(48, 4);
            this.W10_lbl3.Name = "W10_lbl3";
            this.W10_lbl3.Size = new System.Drawing.Size(365, 19);
            this.W10_lbl3.TabIndex = 3;
            this.W10_lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label49
            // 
            this.Label49.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label49.BackColor = System.Drawing.Color.Transparent;
            this.Label49.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label49.Location = new System.Drawing.Point(40, 3);
            this.Label49.Name = "Label49";
            this.Label49.Size = new System.Drawing.Size(254, 35);
            this.Label49.TabIndex = 0;
            this.Label49.Text = "Accents";
            this.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox35
            // 
            this.GroupBox35.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox35.Controls.Add(this.Label50);
            this.GroupBox35.Controls.Add(this.W10_Color_Index0);
            this.GroupBox35.Controls.Add(this.W10_pic2);
            this.GroupBox35.Controls.Add(this.W10_lbl2);
            this.GroupBox35.Location = new System.Drawing.Point(3, 71);
            this.GroupBox35.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox35.Name = "GroupBox35";
            this.GroupBox35.Size = new System.Drawing.Size(514, 28);
            this.GroupBox35.TabIndex = 19;
            // 
            // Label50
            // 
            this.Label50.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label50.AutoEllipsis = true;
            this.Label50.BackColor = System.Drawing.Color.Transparent;
            this.Label50.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label50.Location = new System.Drawing.Point(31, 4);
            this.Label50.Name = "Label50";
            this.Label50.Size = new System.Drawing.Size(14, 19);
            this.Label50.TabIndex = 6;
            this.Label50.Text = "2";
            this.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_Color_Index0
            // 
            this.W10_Color_Index0.AllowDrop = true;
            this.W10_Color_Index0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_Color_Index0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W10_Color_Index0.DefaultColor = System.Drawing.Color.Black;
            this.W10_Color_Index0.DontShowInfo = false;
            this.W10_Color_Index0.Location = new System.Drawing.Point(420, 4);
            this.W10_Color_Index0.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W10_Color_Index0.Name = "W10_Color_Index0";
            this.W10_Color_Index0.Size = new System.Drawing.Size(90, 20);
            this.W10_Color_Index0.TabIndex = 2;
            this.W10_Color_Index0.Click += new System.EventHandler(this.W10_Color_Index0_Click);
            this.W10_Color_Index0.DragDrop += new System.Windows.Forms.DragEventHandler(this.W10_pick_DragDrop);
            // 
            // W10_pic2
            // 
            this.W10_pic2.BackColor = System.Drawing.Color.Transparent;
            this.W10_pic2.Location = new System.Drawing.Point(2, 2);
            this.W10_pic2.Name = "W10_pic2";
            this.W10_pic2.Size = new System.Drawing.Size(24, 24);
            this.W10_pic2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W10_pic2.TabIndex = 4;
            this.W10_pic2.TabStop = false;
            // 
            // W10_lbl2
            // 
            this.W10_lbl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_lbl2.AutoEllipsis = true;
            this.W10_lbl2.BackColor = System.Drawing.Color.Transparent;
            this.W10_lbl2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W10_lbl2.Location = new System.Drawing.Point(48, 4);
            this.W10_lbl2.Name = "W10_lbl2";
            this.W10_lbl2.Size = new System.Drawing.Size(365, 19);
            this.W10_lbl2.TabIndex = 3;
            this.W10_lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox36
            // 
            this.GroupBox36.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox36.Controls.Add(this.Label51);
            this.GroupBox36.Controls.Add(this.W10_pic5);
            this.GroupBox36.Controls.Add(this.W10_lbl5);
            this.GroupBox36.Controls.Add(this.W10_Color_Index3);
            this.GroupBox36.Location = new System.Drawing.Point(3, 161);
            this.GroupBox36.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox36.Name = "GroupBox36";
            this.GroupBox36.Size = new System.Drawing.Size(514, 28);
            this.GroupBox36.TabIndex = 20;
            // 
            // Label51
            // 
            this.Label51.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label51.AutoEllipsis = true;
            this.Label51.BackColor = System.Drawing.Color.Transparent;
            this.Label51.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label51.Location = new System.Drawing.Point(31, 4);
            this.Label51.Name = "Label51";
            this.Label51.Size = new System.Drawing.Size(14, 19);
            this.Label51.TabIndex = 6;
            this.Label51.Text = "5";
            this.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_pic5
            // 
            this.W10_pic5.BackColor = System.Drawing.Color.Transparent;
            this.W10_pic5.Location = new System.Drawing.Point(2, 2);
            this.W10_pic5.Name = "W10_pic5";
            this.W10_pic5.Size = new System.Drawing.Size(24, 24);
            this.W10_pic5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.W10_pic5.TabIndex = 4;
            this.W10_pic5.TabStop = false;
            // 
            // W10_lbl5
            // 
            this.W10_lbl5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_lbl5.AutoEllipsis = true;
            this.W10_lbl5.BackColor = System.Drawing.Color.Transparent;
            this.W10_lbl5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W10_lbl5.Location = new System.Drawing.Point(48, 4);
            this.W10_lbl5.Name = "W10_lbl5";
            this.W10_lbl5.Size = new System.Drawing.Size(365, 19);
            this.W10_lbl5.TabIndex = 3;
            this.W10_lbl5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_Color_Index3
            // 
            this.W10_Color_Index3.AllowDrop = true;
            this.W10_Color_Index3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_Color_Index3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W10_Color_Index3.DefaultColor = System.Drawing.Color.Black;
            this.W10_Color_Index3.DontShowInfo = false;
            this.W10_Color_Index3.Location = new System.Drawing.Point(420, 4);
            this.W10_Color_Index3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W10_Color_Index3.Name = "W10_Color_Index3";
            this.W10_Color_Index3.Size = new System.Drawing.Size(90, 20);
            this.W10_Color_Index3.TabIndex = 2;
            this.W10_Color_Index3.Click += new System.EventHandler(this.W10_Color_Index3_Click);
            this.W10_Color_Index3.DragDrop += new System.Windows.Forms.DragEventHandler(this.W10_pick_DragDrop);
            // 
            // GroupBox37
            // 
            this.GroupBox37.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox37.Controls.Add(this.GroupBox23);
            this.GroupBox37.Controls.Add(this.GroupBox38);
            this.GroupBox37.Controls.Add(this.GroupBox40);
            this.GroupBox37.Controls.Add(this.GroupBox42);
            this.GroupBox37.Controls.Add(this.GroupBox43);
            this.GroupBox37.Controls.Add(this.PictureBox31);
            this.GroupBox37.Controls.Add(this.Label56);
            this.GroupBox37.Location = new System.Drawing.Point(0, 0);
            this.GroupBox37.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox37.Name = "GroupBox37";
            this.GroupBox37.Size = new System.Drawing.Size(520, 133);
            this.GroupBox37.TabIndex = 11;
            // 
            // GroupBox23
            // 
            this.GroupBox23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox23.Controls.Add(this.W10_TBTransparency_Toggle);
            this.GroupBox23.Controls.Add(this.PictureBox15);
            this.GroupBox23.Controls.Add(this.Label22);
            this.GroupBox23.Location = new System.Drawing.Point(261, 71);
            this.GroupBox23.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox23.Name = "GroupBox23";
            this.GroupBox23.Size = new System.Drawing.Size(256, 28);
            this.GroupBox23.TabIndex = 17;
            // 
            // W10_TBTransparency_Toggle
            // 
            this.W10_TBTransparency_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_TBTransparency_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W10_TBTransparency_Toggle.Checked = false;
            this.W10_TBTransparency_Toggle.DarkLight_Toggler = false;
            this.W10_TBTransparency_Toggle.Location = new System.Drawing.Point(212, 4);
            this.W10_TBTransparency_Toggle.Name = "W10_TBTransparency_Toggle";
            this.W10_TBTransparency_Toggle.Size = new System.Drawing.Size(40, 20);
            this.W10_TBTransparency_Toggle.TabIndex = 16;
            this.W10_TBTransparency_Toggle.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.W10_TBTransparency_Toggle_CheckedChanged);
            // 
            // PictureBox15
            // 
            this.PictureBox15.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox15.Image")));
            this.PictureBox15.Location = new System.Drawing.Point(2, 2);
            this.PictureBox15.Name = "PictureBox15";
            this.PictureBox15.Size = new System.Drawing.Size(24, 24);
            this.PictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox15.TabIndex = 4;
            this.PictureBox15.TabStop = false;
            // 
            // Label22
            // 
            this.Label22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label22.AutoEllipsis = true;
            this.Label22.BackColor = System.Drawing.Color.Transparent;
            this.Label22.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label22.Location = new System.Drawing.Point(30, 4);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(177, 20);
            this.Label22.TabIndex = 13;
            this.Label22.Text = "Increase taskbar transparency";
            this.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox38
            // 
            this.GroupBox38.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox38.Controls.Add(this.W10_Button25);
            this.GroupBox38.Controls.Add(this.W10_Accent_Taskbar);
            this.GroupBox38.Controls.Add(this.W10_Accent_StartTaskbar);
            this.GroupBox38.Controls.Add(this.W10_Accent_None);
            this.GroupBox38.Controls.Add(this.PictureBox16);
            this.GroupBox38.Controls.Add(this.Label52);
            this.GroupBox38.Location = new System.Drawing.Point(3, 101);
            this.GroupBox38.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox38.Name = "GroupBox38";
            this.GroupBox38.Size = new System.Drawing.Size(514, 29);
            this.GroupBox38.TabIndex = 16;
            // 
            // W10_Button25
            // 
            this.W10_Button25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_Button25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.W10_Button25.DrawOnGlass = false;
            this.W10_Button25.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.W10_Button25.ForeColor = System.Drawing.Color.White;
            this.W10_Button25.Image = null;
            this.W10_Button25.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(49)))), ((int)(((byte)(61)))));
            this.W10_Button25.Location = new System.Drawing.Point(160, 2);
            this.W10_Button25.Name = "W10_Button25";
            this.W10_Button25.Size = new System.Drawing.Size(20, 25);
            this.W10_Button25.TabIndex = 28;
            this.W10_Button25.Text = "?";
            this.W10_Button25.UseVisualStyleBackColor = false;
            this.W10_Button25.Click += new System.EventHandler(this.W10_Button25_Click);
            // 
            // W10_Accent_Taskbar
            // 
            this.W10_Accent_Taskbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_Accent_Taskbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.W10_Accent_Taskbar.Checked = false;
            this.W10_Accent_Taskbar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W10_Accent_Taskbar.ForeColor = System.Drawing.Color.White;
            this.W10_Accent_Taskbar.Image = null;
            this.W10_Accent_Taskbar.ImageWithText = false;
            this.W10_Accent_Taskbar.Location = new System.Drawing.Point(248, 2);
            this.W10_Accent_Taskbar.Name = "W10_Accent_Taskbar";
            this.W10_Accent_Taskbar.ShowText = true;
            this.W10_Accent_Taskbar.Size = new System.Drawing.Size(76, 25);
            this.W10_Accent_Taskbar.TabIndex = 23;
            this.W10_Accent_Taskbar.Text = "Taskbar";
            this.W10_Accent_Taskbar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.W10_Accent_Taskbar.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.W10_Accent_Taskbar_CheckedChanged);
            // 
            // W10_Accent_StartTaskbar
            // 
            this.W10_Accent_StartTaskbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_Accent_StartTaskbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.W10_Accent_StartTaskbar.Checked = false;
            this.W10_Accent_StartTaskbar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W10_Accent_StartTaskbar.ForeColor = System.Drawing.Color.White;
            this.W10_Accent_StartTaskbar.Image = null;
            this.W10_Accent_StartTaskbar.ImageWithText = false;
            this.W10_Accent_StartTaskbar.Location = new System.Drawing.Point(325, 2);
            this.W10_Accent_StartTaskbar.Name = "W10_Accent_StartTaskbar";
            this.W10_Accent_StartTaskbar.ShowText = true;
            this.W10_Accent_StartTaskbar.Size = new System.Drawing.Size(186, 25);
            this.W10_Accent_StartTaskbar.TabIndex = 22;
            this.W10_Accent_StartTaskbar.Text = "Start, taskbar & action Center";
            this.W10_Accent_StartTaskbar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.W10_Accent_StartTaskbar.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.W10_Accent_StartTaskbar_CheckedChanged);
            // 
            // W10_Accent_None
            // 
            this.W10_Accent_None.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_Accent_None.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.W10_Accent_None.Checked = false;
            this.W10_Accent_None.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W10_Accent_None.ForeColor = System.Drawing.Color.White;
            this.W10_Accent_None.Image = null;
            this.W10_Accent_None.ImageWithText = false;
            this.W10_Accent_None.Location = new System.Drawing.Point(182, 2);
            this.W10_Accent_None.Name = "W10_Accent_None";
            this.W10_Accent_None.ShowText = true;
            this.W10_Accent_None.Size = new System.Drawing.Size(64, 25);
            this.W10_Accent_None.TabIndex = 21;
            this.W10_Accent_None.Text = "Nothing";
            this.W10_Accent_None.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.W10_Accent_None.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.W10_Accent_None_CheckedChanged);
            // 
            // PictureBox16
            // 
            this.PictureBox16.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox16.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox16.Image")));
            this.PictureBox16.Location = new System.Drawing.Point(2, 2);
            this.PictureBox16.Name = "PictureBox16";
            this.PictureBox16.Size = new System.Drawing.Size(24, 24);
            this.PictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox16.TabIndex = 4;
            this.PictureBox16.TabStop = false;
            // 
            // Label52
            // 
            this.Label52.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label52.AutoEllipsis = true;
            this.Label52.BackColor = System.Drawing.Color.Transparent;
            this.Label52.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label52.Location = new System.Drawing.Point(30, 3);
            this.Label52.Name = "Label52";
            this.Label52.Size = new System.Drawing.Size(119, 22);
            this.Label52.TabIndex = 3;
            this.Label52.Text = "Accent color on:";
            this.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox40
            // 
            this.GroupBox40.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox40.Controls.Add(this.PictureBox22);
            this.GroupBox40.Controls.Add(this.Label53);
            this.GroupBox40.Controls.Add(this.W10_WinMode_Toggle);
            this.GroupBox40.Location = new System.Drawing.Point(3, 41);
            this.GroupBox40.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox40.Name = "GroupBox40";
            this.GroupBox40.Size = new System.Drawing.Size(256, 28);
            this.GroupBox40.TabIndex = 10;
            // 
            // PictureBox22
            // 
            this.PictureBox22.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PictureBox22.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox22.Image")));
            this.PictureBox22.Location = new System.Drawing.Point(2, 2);
            this.PictureBox22.Name = "PictureBox22";
            this.PictureBox22.Size = new System.Drawing.Size(24, 24);
            this.PictureBox22.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox22.TabIndex = 4;
            this.PictureBox22.TabStop = false;
            // 
            // Label53
            // 
            this.Label53.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label53.AutoEllipsis = true;
            this.Label53.BackColor = System.Drawing.Color.Transparent;
            this.Label53.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label53.Location = new System.Drawing.Point(30, 4);
            this.Label53.Name = "Label53";
            this.Label53.Size = new System.Drawing.Size(171, 20);
            this.Label53.TabIndex = 7;
            this.Label53.Text = "Windows mode";
            this.Label53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_WinMode_Toggle
            // 
            this.W10_WinMode_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_WinMode_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W10_WinMode_Toggle.Checked = false;
            this.W10_WinMode_Toggle.DarkLight_Toggler = true;
            this.W10_WinMode_Toggle.Location = new System.Drawing.Point(212, 4);
            this.W10_WinMode_Toggle.Name = "W10_WinMode_Toggle";
            this.W10_WinMode_Toggle.Size = new System.Drawing.Size(40, 20);
            this.W10_WinMode_Toggle.TabIndex = 8;
            this.W10_WinMode_Toggle.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.W10_WinMode_Toggle_CheckedChanged);
            // 
            // GroupBox42
            // 
            this.GroupBox42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox42.Controls.Add(this.W10_TB_Blur);
            this.GroupBox42.Controls.Add(this.W10_Transparency_Toggle);
            this.GroupBox42.Controls.Add(this.PictureBox26);
            this.GroupBox42.Controls.Add(this.Label54);
            this.GroupBox42.Location = new System.Drawing.Point(3, 71);
            this.GroupBox42.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox42.Name = "GroupBox42";
            this.GroupBox42.Size = new System.Drawing.Size(256, 28);
            this.GroupBox42.TabIndex = 9;
            // 
            // W10_TB_Blur
            // 
            this.W10_TB_Blur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_TB_Blur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W10_TB_Blur.Checked = true;
            this.W10_TB_Blur.DarkLight_Toggler = false;
            this.W10_TB_Blur.Location = new System.Drawing.Point(213, 4);
            this.W10_TB_Blur.Name = "W10_TB_Blur";
            this.W10_TB_Blur.Size = new System.Drawing.Size(40, 20);
            this.W10_TB_Blur.TabIndex = 16;
            this.W10_TB_Blur.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.W10_TB_Blur_CheckedChanged);
            // 
            // W10_Transparency_Toggle
            // 
            this.W10_Transparency_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_Transparency_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W10_Transparency_Toggle.Checked = false;
            this.W10_Transparency_Toggle.DarkLight_Toggler = false;
            this.W10_Transparency_Toggle.Location = new System.Drawing.Point(170, 4);
            this.W10_Transparency_Toggle.Name = "W10_Transparency_Toggle";
            this.W10_Transparency_Toggle.Size = new System.Drawing.Size(40, 20);
            this.W10_Transparency_Toggle.TabIndex = 16;
            this.W10_Transparency_Toggle.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.W10_Transparency_Toggle_CheckedChanged);
            // 
            // PictureBox26
            // 
            this.PictureBox26.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox26.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox26.Image")));
            this.PictureBox26.Location = new System.Drawing.Point(2, 2);
            this.PictureBox26.Name = "PictureBox26";
            this.PictureBox26.Size = new System.Drawing.Size(24, 24);
            this.PictureBox26.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox26.TabIndex = 4;
            this.PictureBox26.TabStop = false;
            // 
            // Label54
            // 
            this.Label54.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label54.AutoEllipsis = true;
            this.Label54.BackColor = System.Drawing.Color.Transparent;
            this.Label54.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label54.Location = new System.Drawing.Point(30, 4);
            this.Label54.Name = "Label54";
            this.Label54.Size = new System.Drawing.Size(134, 20);
            this.Label54.TabIndex = 13;
            this.Label54.Text = "Transparency, TB blur";
            this.Label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox43
            // 
            this.GroupBox43.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox43.Controls.Add(this.PictureBox27);
            this.GroupBox43.Controls.Add(this.W10_AppMode_Toggle);
            this.GroupBox43.Controls.Add(this.Label55);
            this.GroupBox43.Location = new System.Drawing.Point(261, 41);
            this.GroupBox43.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox43.Name = "GroupBox43";
            this.GroupBox43.Size = new System.Drawing.Size(256, 28);
            this.GroupBox43.TabIndex = 8;
            // 
            // PictureBox27
            // 
            this.PictureBox27.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox27.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox27.Image")));
            this.PictureBox27.Location = new System.Drawing.Point(2, 2);
            this.PictureBox27.Name = "PictureBox27";
            this.PictureBox27.Size = new System.Drawing.Size(24, 24);
            this.PictureBox27.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox27.TabIndex = 4;
            this.PictureBox27.TabStop = false;
            // 
            // W10_AppMode_Toggle
            // 
            this.W10_AppMode_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_AppMode_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W10_AppMode_Toggle.Checked = false;
            this.W10_AppMode_Toggle.DarkLight_Toggler = true;
            this.W10_AppMode_Toggle.Location = new System.Drawing.Point(212, 4);
            this.W10_AppMode_Toggle.Name = "W10_AppMode_Toggle";
            this.W10_AppMode_Toggle.Size = new System.Drawing.Size(40, 20);
            this.W10_AppMode_Toggle.TabIndex = 11;
            this.W10_AppMode_Toggle.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.W10_AppMode_Toggle_CheckedChanged);
            // 
            // Label55
            // 
            this.Label55.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label55.AutoEllipsis = true;
            this.Label55.BackColor = System.Drawing.Color.Transparent;
            this.Label55.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label55.Location = new System.Drawing.Point(30, 4);
            this.Label55.Name = "Label55";
            this.Label55.Size = new System.Drawing.Size(169, 20);
            this.Label55.TabIndex = 10;
            this.Label55.Text = "App mode";
            this.Label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox31
            // 
            this.PictureBox31.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox31.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox31.Image")));
            this.PictureBox31.Location = new System.Drawing.Point(3, 3);
            this.PictureBox31.Name = "PictureBox31";
            this.PictureBox31.Size = new System.Drawing.Size(35, 35);
            this.PictureBox31.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox31.TabIndex = 1;
            this.PictureBox31.TabStop = false;
            // 
            // Label56
            // 
            this.Label56.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label56.BackColor = System.Drawing.Color.Transparent;
            this.Label56.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label56.Location = new System.Drawing.Point(38, 3);
            this.Label56.Name = "Label56";
            this.Label56.Size = new System.Drawing.Size(479, 35);
            this.Label56.TabIndex = 0;
            this.Label56.Text = "Toggles";
            this.Label56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox44
            // 
            this.GroupBox44.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox44.Controls.Add(this.W10_Button8);
            this.GroupBox44.Controls.Add(this.W10_ShowAccentOnTitlebarAndBorders_Toggle);
            this.GroupBox44.Controls.Add(this.GroupBox45);
            this.GroupBox44.Controls.Add(this.PictureBox34);
            this.GroupBox44.Controls.Add(this.Label58);
            this.GroupBox44.Controls.Add(this.GroupBox46);
            this.GroupBox44.Location = new System.Drawing.Point(0, 137);
            this.GroupBox44.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox44.Name = "GroupBox44";
            this.GroupBox44.Size = new System.Drawing.Size(520, 72);
            this.GroupBox44.TabIndex = 2;
            // 
            // W10_Button8
            // 
            this.W10_Button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_Button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W10_Button8.DrawOnGlass = false;
            this.W10_Button8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.W10_Button8.ForeColor = System.Drawing.Color.White;
            this.W10_Button8.Image = null;
            this.W10_Button8.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(49)))), ((int)(((byte)(61)))));
            this.W10_Button8.Location = new System.Drawing.Point(448, 10);
            this.W10_Button8.Name = "W10_Button8";
            this.W10_Button8.Size = new System.Drawing.Size(20, 20);
            this.W10_Button8.TabIndex = 27;
            this.W10_Button8.Text = "?";
            this.W10_Button8.UseVisualStyleBackColor = false;
            this.W10_Button8.Click += new System.EventHandler(this.W10_Button8_Click_1);
            // 
            // W10_ShowAccentOnTitlebarAndBorders_Toggle
            // 
            this.W10_ShowAccentOnTitlebarAndBorders_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_ShowAccentOnTitlebarAndBorders_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W10_ShowAccentOnTitlebarAndBorders_Toggle.Checked = false;
            this.W10_ShowAccentOnTitlebarAndBorders_Toggle.DarkLight_Toggler = false;
            this.W10_ShowAccentOnTitlebarAndBorders_Toggle.Location = new System.Drawing.Point(474, 10);
            this.W10_ShowAccentOnTitlebarAndBorders_Toggle.Name = "W10_ShowAccentOnTitlebarAndBorders_Toggle";
            this.W10_ShowAccentOnTitlebarAndBorders_Toggle.Size = new System.Drawing.Size(40, 20);
            this.W10_ShowAccentOnTitlebarAndBorders_Toggle.TabIndex = 6;
            this.W10_ShowAccentOnTitlebarAndBorders_Toggle.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.W10_ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged);
            // 
            // GroupBox45
            // 
            this.GroupBox45.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.GroupBox45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox45.Controls.Add(this.pictureBox35);
            this.GroupBox45.Controls.Add(this.Label57);
            this.GroupBox45.Controls.Add(this.W10_InactiveTitlebar_pick);
            this.GroupBox45.Location = new System.Drawing.Point(261, 41);
            this.GroupBox45.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox45.Name = "GroupBox45";
            this.GroupBox45.Size = new System.Drawing.Size(256, 28);
            this.GroupBox45.TabIndex = 6;
            // 
            // pictureBox35
            // 
            this.pictureBox35.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox35.Image")));
            this.pictureBox35.Location = new System.Drawing.Point(2, 2);
            this.pictureBox35.Name = "pictureBox35";
            this.pictureBox35.Size = new System.Drawing.Size(24, 24);
            this.pictureBox35.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox35.TabIndex = 5;
            this.pictureBox35.TabStop = false;
            // 
            // Label57
            // 
            this.Label57.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label57.AutoEllipsis = true;
            this.Label57.BackColor = System.Drawing.Color.Transparent;
            this.Label57.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label57.Location = new System.Drawing.Point(30, 5);
            this.Label57.Name = "Label57";
            this.Label57.Size = new System.Drawing.Size(125, 19);
            this.Label57.TabIndex = 3;
            this.Label57.Text = "Inactive titlebar";
            this.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_InactiveTitlebar_pick
            // 
            this.W10_InactiveTitlebar_pick.AllowDrop = true;
            this.W10_InactiveTitlebar_pick.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_InactiveTitlebar_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W10_InactiveTitlebar_pick.DefaultColor = System.Drawing.Color.Black;
            this.W10_InactiveTitlebar_pick.DontShowInfo = false;
            this.W10_InactiveTitlebar_pick.Location = new System.Drawing.Point(162, 4);
            this.W10_InactiveTitlebar_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W10_InactiveTitlebar_pick.Name = "W10_InactiveTitlebar_pick";
            this.W10_InactiveTitlebar_pick.Size = new System.Drawing.Size(90, 20);
            this.W10_InactiveTitlebar_pick.TabIndex = 2;
            this.W10_InactiveTitlebar_pick.Click += new System.EventHandler(this.W10_InactiveTitlebar_pick_Click);
            this.W10_InactiveTitlebar_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.W10_pick_DragDrop);
            // 
            // PictureBox34
            // 
            this.PictureBox34.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox34.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox34.Image")));
            this.PictureBox34.Location = new System.Drawing.Point(3, 3);
            this.PictureBox34.Name = "PictureBox34";
            this.PictureBox34.Size = new System.Drawing.Size(35, 35);
            this.PictureBox34.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox34.TabIndex = 1;
            this.PictureBox34.TabStop = false;
            // 
            // Label58
            // 
            this.Label58.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label58.BackColor = System.Drawing.Color.Transparent;
            this.Label58.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label58.Location = new System.Drawing.Point(40, 3);
            this.Label58.Name = "Label58";
            this.Label58.Size = new System.Drawing.Size(393, 35);
            this.Label58.TabIndex = 0;
            this.Label58.Text = "Titlebars";
            this.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox46
            // 
            this.GroupBox46.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.GroupBox46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox46.Controls.Add(this.pictureBox33);
            this.GroupBox46.Controls.Add(this.Label59);
            this.GroupBox46.Controls.Add(this.W10_ActiveTitlebar_pick);
            this.GroupBox46.Location = new System.Drawing.Point(3, 41);
            this.GroupBox46.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox46.Name = "GroupBox46";
            this.GroupBox46.Size = new System.Drawing.Size(256, 28);
            this.GroupBox46.TabIndex = 5;
            // 
            // pictureBox33
            // 
            this.pictureBox33.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox33.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox33.Image")));
            this.pictureBox33.Location = new System.Drawing.Point(2, 2);
            this.pictureBox33.Name = "pictureBox33";
            this.pictureBox33.Size = new System.Drawing.Size(24, 24);
            this.pictureBox33.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox33.TabIndex = 5;
            this.pictureBox33.TabStop = false;
            // 
            // Label59
            // 
            this.Label59.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label59.AutoEllipsis = true;
            this.Label59.BackColor = System.Drawing.Color.Transparent;
            this.Label59.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label59.Location = new System.Drawing.Point(30, 5);
            this.Label59.Name = "Label59";
            this.Label59.Size = new System.Drawing.Size(125, 19);
            this.Label59.TabIndex = 3;
            this.Label59.Text = "Active titlebar";
            this.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W10_ActiveTitlebar_pick
            // 
            this.W10_ActiveTitlebar_pick.AllowDrop = true;
            this.W10_ActiveTitlebar_pick.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W10_ActiveTitlebar_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W10_ActiveTitlebar_pick.DefaultColor = System.Drawing.Color.Black;
            this.W10_ActiveTitlebar_pick.DontShowInfo = false;
            this.W10_ActiveTitlebar_pick.Location = new System.Drawing.Point(162, 4);
            this.W10_ActiveTitlebar_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W10_ActiveTitlebar_pick.Name = "W10_ActiveTitlebar_pick";
            this.W10_ActiveTitlebar_pick.Size = new System.Drawing.Size(90, 20);
            this.W10_ActiveTitlebar_pick.TabIndex = 2;
            this.W10_ActiveTitlebar_pick.Click += new System.EventHandler(this.W10_ActiveTitlebar_pick_Click);
            this.W10_ActiveTitlebar_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.W10_pick_DragDrop);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage3.Controls.Add(this.PaletteContainer_W81);
            this.TabPage3.Location = new System.Drawing.Point(4, 24);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.Size = new System.Drawing.Size(529, 517);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "W8.1";
            // 
            // PaletteContainer_W81
            // 
            this.PaletteContainer_W81.BackColor = System.Drawing.Color.Transparent;
            this.PaletteContainer_W81.Controls.Add(this.GroupBox17);
            this.PaletteContainer_W81.Controls.Add(this.GroupBox32);
            this.PaletteContainer_W81.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PaletteContainer_W81.Location = new System.Drawing.Point(3, 3);
            this.PaletteContainer_W81.Name = "PaletteContainer_W81";
            this.PaletteContainer_W81.Size = new System.Drawing.Size(523, 511);
            this.PaletteContainer_W81.TabIndex = 31;
            // 
            // GroupBox17
            // 
            this.GroupBox17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox17.Controls.Add(this.SeparatorVertical2);
            this.GroupBox17.Controls.Add(this.Label32);
            this.GroupBox17.Controls.Add(this.Label31);
            this.GroupBox17.Controls.Add(this.W81_start);
            this.GroupBox17.Controls.Add(this.Label30);
            this.GroupBox17.Controls.Add(this.Label24);
            this.GroupBox17.Controls.Add(this.W81_theme_aerolite);
            this.GroupBox17.Controls.Add(this.PictureBox37);
            this.GroupBox17.Controls.Add(this.Label40);
            this.GroupBox17.Controls.Add(this.W81_theme_aero);
            this.GroupBox17.Controls.Add(this.W81_logonui);
            this.GroupBox17.Location = new System.Drawing.Point(0, 231);
            this.GroupBox17.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox17.Name = "GroupBox17";
            this.GroupBox17.Size = new System.Drawing.Size(520, 138);
            this.GroupBox17.TabIndex = 12;
            // 
            // SeparatorVertical2
            // 
            this.SeparatorVertical2.AlternativeLook = false;
            this.SeparatorVertical2.Location = new System.Drawing.Point(253, 39);
            this.SeparatorVertical2.Name = "SeparatorVertical2";
            this.SeparatorVertical2.Size = new System.Drawing.Size(1, 90);
            this.SeparatorVertical2.TabIndex = 40;
            this.SeparatorVertical2.TabStop = false;
            this.SeparatorVertical2.Text = "SeparatorVertical2";
            // 
            // Label32
            // 
            this.Label32.AutoEllipsis = true;
            this.Label32.BackColor = System.Drawing.Color.Transparent;
            this.Label32.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label32.Location = new System.Drawing.Point(331, 109);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(64, 22);
            this.Label32.TabIndex = 39;
            this.Label32.Text = "LogonUI*";
            this.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label31
            // 
            this.Label31.AutoEllipsis = true;
            this.Label31.BackColor = System.Drawing.Color.Transparent;
            this.Label31.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label31.Location = new System.Drawing.Point(261, 109);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(64, 22);
            this.Label31.TabIndex = 38;
            this.Label31.Text = "Start";
            this.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // W81_start
            // 
            this.W81_start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W81_start.DrawOnGlass = false;
            this.W81_start.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W81_start.ForeColor = System.Drawing.Color.White;
            this.W81_start.Image = null;
            this.W81_start.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.W81_start.Location = new System.Drawing.Point(261, 39);
            this.W81_start.Name = "W81_start";
            this.W81_start.Size = new System.Drawing.Size(64, 64);
            this.W81_start.TabIndex = 36;
            this.W81_start.UseVisualStyleBackColor = false;
            this.W81_start.Click += new System.EventHandler(this.W8_start_Click);
            // 
            // Label30
            // 
            this.Label30.AutoEllipsis = true;
            this.Label30.BackColor = System.Drawing.Color.Transparent;
            this.Label30.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label30.Location = new System.Drawing.Point(183, 109);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(64, 22);
            this.Label30.TabIndex = 35;
            this.Label30.Text = "Aero Lite";
            this.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label24
            // 
            this.Label24.AutoEllipsis = true;
            this.Label24.BackColor = System.Drawing.Color.Transparent;
            this.Label24.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label24.Location = new System.Drawing.Point(113, 109);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(64, 22);
            this.Label24.TabIndex = 34;
            this.Label24.Text = "Aero 8";
            this.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // W81_theme_aerolite
            // 
            this.W81_theme_aerolite.Checked = false;
            this.W81_theme_aerolite.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W81_theme_aerolite.ForeColor = System.Drawing.Color.White;
            this.W81_theme_aerolite.Image = ((System.Drawing.Image)(resources.GetObject("W81_theme_aerolite.Image")));
            this.W81_theme_aerolite.ImageWithText = false;
            this.W81_theme_aerolite.Location = new System.Drawing.Point(183, 39);
            this.W81_theme_aerolite.Name = "W81_theme_aerolite";
            this.W81_theme_aerolite.ShowText = false;
            this.W81_theme_aerolite.Size = new System.Drawing.Size(64, 64);
            this.W81_theme_aerolite.TabIndex = 30;
            this.W81_theme_aerolite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.W81_theme_aerolite.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.W8_theme_aerolite_CheckedChanged);
            // 
            // PictureBox37
            // 
            this.PictureBox37.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox37.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox37.Image")));
            this.PictureBox37.Location = new System.Drawing.Point(3, 3);
            this.PictureBox37.Name = "PictureBox37";
            this.PictureBox37.Size = new System.Drawing.Size(35, 35);
            this.PictureBox37.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox37.TabIndex = 1;
            this.PictureBox37.TabStop = false;
            // 
            // Label40
            // 
            this.Label40.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label40.BackColor = System.Drawing.Color.Transparent;
            this.Label40.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label40.Location = new System.Drawing.Point(44, 3);
            this.Label40.Name = "Label40";
            this.Label40.Size = new System.Drawing.Size(472, 35);
            this.Label40.TabIndex = 0;
            this.Label40.Text = "Theme";
            this.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W81_theme_aero
            // 
            this.W81_theme_aero.Checked = false;
            this.W81_theme_aero.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W81_theme_aero.ForeColor = System.Drawing.Color.White;
            this.W81_theme_aero.Image = ((System.Drawing.Image)(resources.GetObject("W81_theme_aero.Image")));
            this.W81_theme_aero.ImageWithText = false;
            this.W81_theme_aero.Location = new System.Drawing.Point(113, 39);
            this.W81_theme_aero.Name = "W81_theme_aero";
            this.W81_theme_aero.ShowText = false;
            this.W81_theme_aero.Size = new System.Drawing.Size(64, 64);
            this.W81_theme_aero.TabIndex = 29;
            this.W81_theme_aero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.W81_theme_aero.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.W8_theme_aero_CheckedChanged);
            // 
            // W81_logonui
            // 
            this.W81_logonui.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W81_logonui.DrawOnGlass = false;
            this.W81_logonui.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W81_logonui.ForeColor = System.Drawing.Color.White;
            this.W81_logonui.Image = null;
            this.W81_logonui.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.W81_logonui.Location = new System.Drawing.Point(331, 39);
            this.W81_logonui.Name = "W81_logonui";
            this.W81_logonui.Size = new System.Drawing.Size(64, 64);
            this.W81_logonui.TabIndex = 37;
            this.W81_logonui.UseVisualStyleBackColor = false;
            this.W81_logonui.Click += new System.EventHandler(this.W8_logonui_Click);
            // 
            // GroupBox32
            // 
            this.GroupBox32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox32.Controls.Add(this.GroupBox39);
            this.GroupBox32.Controls.Add(this.GroupBox41);
            this.GroupBox32.Controls.Add(this.GroupBox15);
            this.GroupBox32.Controls.Add(this.GroupBox33);
            this.GroupBox32.Controls.Add(this.GroupBox29);
            this.GroupBox32.Controls.Add(this.PictureBox32);
            this.GroupBox32.Controls.Add(this.Label41);
            this.GroupBox32.Location = new System.Drawing.Point(0, 0);
            this.GroupBox32.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox32.Name = "GroupBox32";
            this.GroupBox32.Size = new System.Drawing.Size(520, 228);
            this.GroupBox32.TabIndex = 11;
            // 
            // GroupBox39
            // 
            this.GroupBox39.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox39.Controls.Add(this.PictureBox28);
            this.GroupBox39.Controls.Add(this.W81_personalcolor_accent_pick);
            this.GroupBox39.Controls.Add(this.Foregrounds);
            this.GroupBox39.Location = new System.Drawing.Point(2, 103);
            this.GroupBox39.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox39.Name = "GroupBox39";
            this.GroupBox39.Size = new System.Drawing.Size(515, 29);
            this.GroupBox39.TabIndex = 26;
            // 
            // PictureBox28
            // 
            this.PictureBox28.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox28.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox28.Image")));
            this.PictureBox28.Location = new System.Drawing.Point(3, 2);
            this.PictureBox28.Name = "PictureBox28";
            this.PictureBox28.Size = new System.Drawing.Size(24, 24);
            this.PictureBox28.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox28.TabIndex = 4;
            this.PictureBox28.TabStop = false;
            // 
            // W81_personalcolor_accent_pick
            // 
            this.W81_personalcolor_accent_pick.AllowDrop = true;
            this.W81_personalcolor_accent_pick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W81_personalcolor_accent_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W81_personalcolor_accent_pick.DefaultColor = System.Drawing.Color.Black;
            this.W81_personalcolor_accent_pick.DontShowInfo = false;
            this.W81_personalcolor_accent_pick.Location = new System.Drawing.Point(401, 4);
            this.W81_personalcolor_accent_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W81_personalcolor_accent_pick.Name = "W81_personalcolor_accent_pick";
            this.W81_personalcolor_accent_pick.Size = new System.Drawing.Size(110, 21);
            this.W81_personalcolor_accent_pick.TabIndex = 2;
            this.W81_personalcolor_accent_pick.Click += new System.EventHandler(this.W8_personalcolor_accent_pick_Click);
            this.W81_personalcolor_accent_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.W81_pick_DragDrop);
            // 
            // Foregrounds
            // 
            this.Foregrounds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Foregrounds.AutoEllipsis = true;
            this.Foregrounds.BackColor = System.Drawing.Color.Transparent;
            this.Foregrounds.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Foregrounds.Location = new System.Drawing.Point(30, 4);
            this.Foregrounds.Name = "Foregrounds";
            this.Foregrounds.Size = new System.Drawing.Size(364, 20);
            this.Foregrounds.TabIndex = 3;
            this.Foregrounds.Text = "Foregrounds (primary: accents)";
            this.Foregrounds.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox41
            // 
            this.GroupBox41.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox41.Controls.Add(this.PictureBox29);
            this.GroupBox41.Controls.Add(this.W81_personalcls_background_pick);
            this.GroupBox41.Controls.Add(this.Label33);
            this.GroupBox41.Location = new System.Drawing.Point(2, 165);
            this.GroupBox41.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox41.Name = "GroupBox41";
            this.GroupBox41.Size = new System.Drawing.Size(515, 29);
            this.GroupBox41.TabIndex = 25;
            // 
            // PictureBox29
            // 
            this.PictureBox29.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox29.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox29.Image")));
            this.PictureBox29.Location = new System.Drawing.Point(3, 2);
            this.PictureBox29.Name = "PictureBox29";
            this.PictureBox29.Size = new System.Drawing.Size(24, 24);
            this.PictureBox29.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox29.TabIndex = 4;
            this.PictureBox29.TabStop = false;
            // 
            // W81_personalcls_background_pick
            // 
            this.W81_personalcls_background_pick.AllowDrop = true;
            this.W81_personalcls_background_pick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W81_personalcls_background_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W81_personalcls_background_pick.DefaultColor = System.Drawing.Color.Black;
            this.W81_personalcls_background_pick.DontShowInfo = false;
            this.W81_personalcls_background_pick.Location = new System.Drawing.Point(401, 4);
            this.W81_personalcls_background_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W81_personalcls_background_pick.Name = "W81_personalcls_background_pick";
            this.W81_personalcls_background_pick.Size = new System.Drawing.Size(110, 21);
            this.W81_personalcls_background_pick.TabIndex = 2;
            this.W81_personalcls_background_pick.Click += new System.EventHandler(this.W8_personalcls_background_pick_Click);
            this.W81_personalcls_background_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.W81_pick_DragDrop);
            // 
            // Label33
            // 
            this.Label33.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label33.AutoEllipsis = true;
            this.Label33.BackColor = System.Drawing.Color.Transparent;
            this.Label33.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label33.Location = new System.Drawing.Point(30, 4);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(364, 20);
            this.Label33.TabIndex = 3;
            this.Label33.Text = "Backgrounds (primary: start, LogonUI)";
            this.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox15
            // 
            this.GroupBox15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox15.Controls.Add(this.PictureBox9);
            this.GroupBox15.Controls.Add(this.W81_start_pick);
            this.GroupBox15.Controls.Add(this.Label20);
            this.GroupBox15.Location = new System.Drawing.Point(2, 196);
            this.GroupBox15.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox15.Name = "GroupBox15";
            this.GroupBox15.Size = new System.Drawing.Size(515, 29);
            this.GroupBox15.TabIndex = 21;
            // 
            // PictureBox9
            // 
            this.PictureBox9.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox9.Image")));
            this.PictureBox9.Location = new System.Drawing.Point(3, 2);
            this.PictureBox9.Name = "PictureBox9";
            this.PictureBox9.Size = new System.Drawing.Size(24, 24);
            this.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox9.TabIndex = 4;
            this.PictureBox9.TabStop = false;
            // 
            // W81_start_pick
            // 
            this.W81_start_pick.AllowDrop = true;
            this.W81_start_pick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W81_start_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W81_start_pick.DefaultColor = System.Drawing.Color.Black;
            this.W81_start_pick.DontShowInfo = false;
            this.W81_start_pick.Location = new System.Drawing.Point(401, 4);
            this.W81_start_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W81_start_pick.Name = "W81_start_pick";
            this.W81_start_pick.Size = new System.Drawing.Size(110, 21);
            this.W81_start_pick.TabIndex = 2;
            this.W81_start_pick.Click += new System.EventHandler(this.W8_start_pick_Click);
            this.W81_start_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.W81_pick_DragDrop);
            // 
            // Label20
            // 
            this.Label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label20.AutoEllipsis = true;
            this.Label20.BackColor = System.Drawing.Color.Transparent;
            this.Label20.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label20.Location = new System.Drawing.Point(30, 4);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(369, 20);
            this.Label20.TabIndex = 3;
            this.Label20.Text = "Backgrounds (secondary: start, LogonUI)";
            this.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox33
            // 
            this.GroupBox33.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox33.Controls.Add(this.W81_ColorizationBalance_val);
            this.GroupBox33.Controls.Add(this.W81_ColorizationBalance_bar);
            this.GroupBox33.Controls.Add(this.PictureBox30);
            this.GroupBox33.Controls.Add(this.W81_ColorizationColor_pick);
            this.GroupBox33.Controls.Add(this.Label39);
            this.GroupBox33.Location = new System.Drawing.Point(2, 44);
            this.GroupBox33.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox33.Name = "GroupBox33";
            this.GroupBox33.Size = new System.Drawing.Size(515, 57);
            this.GroupBox33.TabIndex = 20;
            // 
            // W81_ColorizationBalance_val
            // 
            this.W81_ColorizationBalance_val.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W81_ColorizationBalance_val.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.W81_ColorizationBalance_val.DrawOnGlass = false;
            this.W81_ColorizationBalance_val.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W81_ColorizationBalance_val.ForeColor = System.Drawing.Color.White;
            this.W81_ColorizationBalance_val.Image = null;
            this.W81_ColorizationBalance_val.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.W81_ColorizationBalance_val.Location = new System.Drawing.Point(477, 29);
            this.W81_ColorizationBalance_val.Name = "W81_ColorizationBalance_val";
            this.W81_ColorizationBalance_val.Size = new System.Drawing.Size(34, 24);
            this.W81_ColorizationBalance_val.TabIndex = 130;
            this.W81_ColorizationBalance_val.UseVisualStyleBackColor = false;
            this.W81_ColorizationBalance_val.Click += new System.EventHandler(this.W8_ColorizationBalance_val_Click);
            // 
            // W81_ColorizationBalance_bar
            // 
            this.W81_ColorizationBalance_bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W81_ColorizationBalance_bar.LargeChange = 10;
            this.W81_ColorizationBalance_bar.Location = new System.Drawing.Point(4, 32);
            this.W81_ColorizationBalance_bar.Maximum = 100;
            this.W81_ColorizationBalance_bar.Minimum = 0;
            this.W81_ColorizationBalance_bar.Name = "W81_ColorizationBalance_bar";
            this.W81_ColorizationBalance_bar.Size = new System.Drawing.Size(467, 19);
            this.W81_ColorizationBalance_bar.SmallChange = 1;
            this.W81_ColorizationBalance_bar.TabIndex = 6;
            this.W81_ColorizationBalance_bar.Value = 0;
            this.W81_ColorizationBalance_bar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.W8_ColorizationBalance_bar_Scroll);
            // 
            // PictureBox30
            // 
            this.PictureBox30.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox30.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox30.Image")));
            this.PictureBox30.Location = new System.Drawing.Point(3, 2);
            this.PictureBox30.Name = "PictureBox30";
            this.PictureBox30.Size = new System.Drawing.Size(24, 24);
            this.PictureBox30.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox30.TabIndex = 4;
            this.PictureBox30.TabStop = false;
            // 
            // W81_ColorizationColor_pick
            // 
            this.W81_ColorizationColor_pick.AllowDrop = true;
            this.W81_ColorizationColor_pick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W81_ColorizationColor_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W81_ColorizationColor_pick.DefaultColor = System.Drawing.Color.Black;
            this.W81_ColorizationColor_pick.DontShowInfo = false;
            this.W81_ColorizationColor_pick.Location = new System.Drawing.Point(401, 4);
            this.W81_ColorizationColor_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W81_ColorizationColor_pick.Name = "W81_ColorizationColor_pick";
            this.W81_ColorizationColor_pick.Size = new System.Drawing.Size(110, 21);
            this.W81_ColorizationColor_pick.TabIndex = 2;
            this.W81_ColorizationColor_pick.Click += new System.EventHandler(this.W8_ColorizationColor_pick_Click);
            this.W81_ColorizationColor_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.W81_pick_DragDrop);
            // 
            // Label39
            // 
            this.Label39.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label39.AutoEllipsis = true;
            this.Label39.BackColor = System.Drawing.Color.Transparent;
            this.Label39.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label39.Location = new System.Drawing.Point(30, 4);
            this.Label39.Name = "Label39";
            this.Label39.Size = new System.Drawing.Size(364, 20);
            this.Label39.TabIndex = 3;
            this.Label39.Text = "Windows colors (titlebars && taskbar)";
            this.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox29
            // 
            this.GroupBox29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox29.Controls.Add(this.PictureBox23);
            this.GroupBox29.Controls.Add(this.W81_accent_pick);
            this.GroupBox29.Controls.Add(this.Label29);
            this.GroupBox29.Location = new System.Drawing.Point(2, 134);
            this.GroupBox29.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox29.Name = "GroupBox29";
            this.GroupBox29.Size = new System.Drawing.Size(515, 29);
            this.GroupBox29.TabIndex = 23;
            // 
            // PictureBox23
            // 
            this.PictureBox23.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox23.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox23.Image")));
            this.PictureBox23.Location = new System.Drawing.Point(3, 2);
            this.PictureBox23.Name = "PictureBox23";
            this.PictureBox23.Size = new System.Drawing.Size(24, 24);
            this.PictureBox23.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox23.TabIndex = 4;
            this.PictureBox23.TabStop = false;
            // 
            // W81_accent_pick
            // 
            this.W81_accent_pick.AllowDrop = true;
            this.W81_accent_pick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W81_accent_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W81_accent_pick.DefaultColor = System.Drawing.Color.Black;
            this.W81_accent_pick.DontShowInfo = false;
            this.W81_accent_pick.Location = new System.Drawing.Point(401, 4);
            this.W81_accent_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W81_accent_pick.Name = "W81_accent_pick";
            this.W81_accent_pick.Size = new System.Drawing.Size(110, 21);
            this.W81_accent_pick.TabIndex = 2;
            this.W81_accent_pick.Click += new System.EventHandler(this.W8_accent_pick_Click);
            this.W81_accent_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.W81_pick_DragDrop);
            // 
            // Label29
            // 
            this.Label29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label29.AutoEllipsis = true;
            this.Label29.BackColor = System.Drawing.Color.Transparent;
            this.Label29.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label29.Location = new System.Drawing.Point(30, 4);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(369, 20);
            this.Label29.TabIndex = 3;
            this.Label29.Text = "Foregrounds (secondary: accents)";
            this.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox32
            // 
            this.PictureBox32.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox32.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox32.Image")));
            this.PictureBox32.Location = new System.Drawing.Point(3, 3);
            this.PictureBox32.Name = "PictureBox32";
            this.PictureBox32.Size = new System.Drawing.Size(35, 35);
            this.PictureBox32.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox32.TabIndex = 1;
            this.PictureBox32.TabStop = false;
            // 
            // Label41
            // 
            this.Label41.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label41.BackColor = System.Drawing.Color.Transparent;
            this.Label41.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label41.Location = new System.Drawing.Point(44, 3);
            this.Label41.Name = "Label41";
            this.Label41.Size = new System.Drawing.Size(472, 35);
            this.Label41.TabIndex = 0;
            this.Label41.Text = "Colors";
            this.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage4.Controls.Add(this.PaletteContainer_W7);
            this.TabPage4.Location = new System.Drawing.Point(4, 24);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage4.Size = new System.Drawing.Size(529, 517);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "W7";
            // 
            // PaletteContainer_W7
            // 
            this.PaletteContainer_W7.BackColor = System.Drawing.Color.Transparent;
            this.PaletteContainer_W7.Controls.Add(this.GroupBox11);
            this.PaletteContainer_W7.Controls.Add(this.GroupBox22);
            this.PaletteContainer_W7.Controls.Add(this.GroupBox30);
            this.PaletteContainer_W7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PaletteContainer_W7.Location = new System.Drawing.Point(3, 3);
            this.PaletteContainer_W7.Name = "PaletteContainer_W7";
            this.PaletteContainer_W7.Size = new System.Drawing.Size(523, 511);
            this.PaletteContainer_W7.TabIndex = 30;
            // 
            // GroupBox11
            // 
            this.GroupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox11.Controls.Add(this.Label23);
            this.GroupBox11.Controls.Add(this.PictureBox13);
            this.GroupBox11.Controls.Add(this.W7_theme_aero);
            this.GroupBox11.Controls.Add(this.Label28);
            this.GroupBox11.Controls.Add(this.W7_theme_classic);
            this.GroupBox11.Controls.Add(this.Label25);
            this.GroupBox11.Controls.Add(this.W7_theme_basic);
            this.GroupBox11.Controls.Add(this.Label14);
            this.GroupBox11.Controls.Add(this.W7_theme_aeroopaque);
            this.GroupBox11.Controls.Add(this.Label6);
            this.GroupBox11.Location = new System.Drawing.Point(0, 109);
            this.GroupBox11.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox11.Name = "GroupBox11";
            this.GroupBox11.Padding = new System.Windows.Forms.Padding(1);
            this.GroupBox11.Size = new System.Drawing.Size(519, 140);
            this.GroupBox11.TabIndex = 40;
            // 
            // Label23
            // 
            this.Label23.AutoEllipsis = true;
            this.Label23.BackColor = System.Drawing.Color.Transparent;
            this.Label23.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label23.Location = new System.Drawing.Point(66, 109);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(90, 22);
            this.Label23.TabIndex = 39;
            this.Label23.Text = "Aero";
            this.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PictureBox13
            // 
            this.PictureBox13.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox13.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox13.Image")));
            this.PictureBox13.Location = new System.Drawing.Point(3, 3);
            this.PictureBox13.Name = "PictureBox13";
            this.PictureBox13.Size = new System.Drawing.Size(35, 35);
            this.PictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox13.TabIndex = 1;
            this.PictureBox13.TabStop = false;
            // 
            // W7_theme_aero
            // 
            this.W7_theme_aero.Checked = false;
            this.W7_theme_aero.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W7_theme_aero.ForeColor = System.Drawing.Color.White;
            this.W7_theme_aero.Image = ((System.Drawing.Image)(resources.GetObject("W7_theme_aero.Image")));
            this.W7_theme_aero.ImageWithText = false;
            this.W7_theme_aero.Location = new System.Drawing.Point(79, 40);
            this.W7_theme_aero.Name = "W7_theme_aero";
            this.W7_theme_aero.ShowText = false;
            this.W7_theme_aero.Size = new System.Drawing.Size(64, 64);
            this.W7_theme_aero.TabIndex = 38;
            this.W7_theme_aero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.W7_theme_aero.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.W7_theme_Aero_CheckedChanged);
            // 
            // Label28
            // 
            this.Label28.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label28.BackColor = System.Drawing.Color.Transparent;
            this.Label28.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label28.Location = new System.Drawing.Point(44, 3);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(471, 35);
            this.Label28.TabIndex = 0;
            this.Label28.Text = "Theme";
            this.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // W7_theme_classic
            // 
            this.W7_theme_classic.Checked = false;
            this.W7_theme_classic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W7_theme_classic.ForeColor = System.Drawing.Color.White;
            this.W7_theme_classic.Image = ((System.Drawing.Image)(resources.GetObject("W7_theme_classic.Image")));
            this.W7_theme_classic.ImageWithText = false;
            this.W7_theme_classic.Location = new System.Drawing.Point(370, 40);
            this.W7_theme_classic.Name = "W7_theme_classic";
            this.W7_theme_classic.ShowText = false;
            this.W7_theme_classic.Size = new System.Drawing.Size(64, 64);
            this.W7_theme_classic.TabIndex = 32;
            this.W7_theme_classic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.W7_theme_classic.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.W7_theme_classic_CheckedChanged);
            // 
            // Label25
            // 
            this.Label25.AutoEllipsis = true;
            this.Label25.BackColor = System.Drawing.Color.Transparent;
            this.Label25.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label25.Location = new System.Drawing.Point(162, 109);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(90, 22);
            this.Label25.TabIndex = 37;
            this.Label25.Text = "Aero Opaque";
            this.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // W7_theme_basic
            // 
            this.W7_theme_basic.Checked = false;
            this.W7_theme_basic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W7_theme_basic.ForeColor = System.Drawing.Color.White;
            this.W7_theme_basic.Image = ((System.Drawing.Image)(resources.GetObject("W7_theme_basic.Image")));
            this.W7_theme_basic.ImageWithText = false;
            this.W7_theme_basic.Location = new System.Drawing.Point(274, 40);
            this.W7_theme_basic.Name = "W7_theme_basic";
            this.W7_theme_basic.ShowText = false;
            this.W7_theme_basic.Size = new System.Drawing.Size(64, 64);
            this.W7_theme_basic.TabIndex = 34;
            this.W7_theme_basic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.W7_theme_basic.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.W7_theme_basic_CheckedChanged);
            // 
            // Label14
            // 
            this.Label14.AutoEllipsis = true;
            this.Label14.BackColor = System.Drawing.Color.Transparent;
            this.Label14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label14.Location = new System.Drawing.Point(261, 109);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(90, 22);
            this.Label14.TabIndex = 35;
            this.Label14.Text = "Basic";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // W7_theme_aeroopaque
            // 
            this.W7_theme_aeroopaque.Checked = false;
            this.W7_theme_aeroopaque.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W7_theme_aeroopaque.ForeColor = System.Drawing.Color.White;
            this.W7_theme_aeroopaque.Image = ((System.Drawing.Image)(resources.GetObject("W7_theme_aeroopaque.Image")));
            this.W7_theme_aeroopaque.ImageWithText = false;
            this.W7_theme_aeroopaque.Location = new System.Drawing.Point(175, 40);
            this.W7_theme_aeroopaque.Name = "W7_theme_aeroopaque";
            this.W7_theme_aeroopaque.ShowText = false;
            this.W7_theme_aeroopaque.Size = new System.Drawing.Size(64, 64);
            this.W7_theme_aeroopaque.TabIndex = 36;
            this.W7_theme_aeroopaque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.W7_theme_aeroopaque.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.W7_theme_aeroopaque_CheckedChanged);
            // 
            // Label6
            // 
            this.Label6.AutoEllipsis = true;
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label6.Location = new System.Drawing.Point(357, 109);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(90, 22);
            this.Label6.TabIndex = 33;
            this.Label6.Text = "Classic";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupBox22
            // 
            this.GroupBox22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox22.Controls.Add(this.GroupBox19);
            this.GroupBox22.Controls.Add(this.PictureBox39);
            this.GroupBox22.Controls.Add(this.GroupBox12);
            this.GroupBox22.Controls.Add(this.Label38);
            this.GroupBox22.Controls.Add(this.GroupBox10);
            this.GroupBox22.Controls.Add(this.GroupBox7);
            this.GroupBox22.Location = new System.Drawing.Point(0, 254);
            this.GroupBox22.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox22.Name = "GroupBox22";
            this.GroupBox22.Padding = new System.Windows.Forms.Padding(1);
            this.GroupBox22.Size = new System.Drawing.Size(519, 165);
            this.GroupBox22.TabIndex = 12;
            // 
            // GroupBox19
            // 
            this.GroupBox19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox19.Controls.Add(this.W7_ColorizationGlassReflectionIntensity_val);
            this.GroupBox19.Controls.Add(this.W7_ColorizationGlassReflectionIntensity_bar);
            this.GroupBox19.Controls.Add(this.PictureBox24);
            this.GroupBox19.Controls.Add(this.Label26);
            this.GroupBox19.Location = new System.Drawing.Point(2, 74);
            this.GroupBox19.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox19.Name = "GroupBox19";
            this.GroupBox19.Size = new System.Drawing.Size(514, 28);
            this.GroupBox19.TabIndex = 30;
            // 
            // W7_ColorizationGlassReflectionIntensity_val
            // 
            this.W7_ColorizationGlassReflectionIntensity_val.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W7_ColorizationGlassReflectionIntensity_val.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.W7_ColorizationGlassReflectionIntensity_val.DrawOnGlass = false;
            this.W7_ColorizationGlassReflectionIntensity_val.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W7_ColorizationGlassReflectionIntensity_val.ForeColor = System.Drawing.Color.White;
            this.W7_ColorizationGlassReflectionIntensity_val.Image = null;
            this.W7_ColorizationGlassReflectionIntensity_val.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.W7_ColorizationGlassReflectionIntensity_val.Location = new System.Drawing.Point(476, 4);
            this.W7_ColorizationGlassReflectionIntensity_val.Name = "W7_ColorizationGlassReflectionIntensity_val";
            this.W7_ColorizationGlassReflectionIntensity_val.Size = new System.Drawing.Size(34, 20);
            this.W7_ColorizationGlassReflectionIntensity_val.TabIndex = 132;
            this.W7_ColorizationGlassReflectionIntensity_val.UseVisualStyleBackColor = false;
            this.W7_ColorizationGlassReflectionIntensity_val.Click += new System.EventHandler(this.W7_ColorizationGlassReflectionIntensity_val_Click);
            // 
            // W7_ColorizationGlassReflectionIntensity_bar
            // 
            this.W7_ColorizationGlassReflectionIntensity_bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W7_ColorizationGlassReflectionIntensity_bar.LargeChange = 10;
            this.W7_ColorizationGlassReflectionIntensity_bar.Location = new System.Drawing.Point(185, 4);
            this.W7_ColorizationGlassReflectionIntensity_bar.Maximum = 100;
            this.W7_ColorizationGlassReflectionIntensity_bar.Minimum = 0;
            this.W7_ColorizationGlassReflectionIntensity_bar.Name = "W7_ColorizationGlassReflectionIntensity_bar";
            this.W7_ColorizationGlassReflectionIntensity_bar.Size = new System.Drawing.Size(285, 19);
            this.W7_ColorizationGlassReflectionIntensity_bar.SmallChange = 1;
            this.W7_ColorizationGlassReflectionIntensity_bar.TabIndex = 8;
            this.W7_ColorizationGlassReflectionIntensity_bar.Value = 0;
            this.W7_ColorizationGlassReflectionIntensity_bar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.W7_ColorizationGlassReflectionIntensity_bar_Scroll);
            // 
            // PictureBox24
            // 
            this.PictureBox24.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox24.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox24.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox24.Image")));
            this.PictureBox24.Location = new System.Drawing.Point(3, 2);
            this.PictureBox24.Name = "PictureBox24";
            this.PictureBox24.Size = new System.Drawing.Size(24, 23);
            this.PictureBox24.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox24.TabIndex = 4;
            this.PictureBox24.TabStop = false;
            // 
            // Label26
            // 
            this.Label26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label26.AutoEllipsis = true;
            this.Label26.BackColor = System.Drawing.Color.Transparent;
            this.Label26.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label26.Location = new System.Drawing.Point(30, 4);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(149, 20);
            this.Label26.TabIndex = 3;
            this.Label26.Text = "Glass reflection intensity";
            this.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox39
            // 
            this.PictureBox39.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox39.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox39.Image")));
            this.PictureBox39.Location = new System.Drawing.Point(3, 3);
            this.PictureBox39.Name = "PictureBox39";
            this.PictureBox39.Size = new System.Drawing.Size(35, 35);
            this.PictureBox39.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox39.TabIndex = 1;
            this.PictureBox39.TabStop = false;
            // 
            // GroupBox12
            // 
            this.GroupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox12.Controls.Add(this.W7_ColorizationBlurBalance_val);
            this.GroupBox12.Controls.Add(this.W7_ColorizationBlurBalance_bar);
            this.GroupBox12.Controls.Add(this.PictureBox8);
            this.GroupBox12.Controls.Add(this.Label15);
            this.GroupBox12.Location = new System.Drawing.Point(2, 44);
            this.GroupBox12.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox12.Name = "GroupBox12";
            this.GroupBox12.Size = new System.Drawing.Size(514, 28);
            this.GroupBox12.TabIndex = 29;
            // 
            // W7_ColorizationBlurBalance_val
            // 
            this.W7_ColorizationBlurBalance_val.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W7_ColorizationBlurBalance_val.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.W7_ColorizationBlurBalance_val.DrawOnGlass = false;
            this.W7_ColorizationBlurBalance_val.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W7_ColorizationBlurBalance_val.ForeColor = System.Drawing.Color.White;
            this.W7_ColorizationBlurBalance_val.Image = null;
            this.W7_ColorizationBlurBalance_val.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.W7_ColorizationBlurBalance_val.Location = new System.Drawing.Point(476, 4);
            this.W7_ColorizationBlurBalance_val.Name = "W7_ColorizationBlurBalance_val";
            this.W7_ColorizationBlurBalance_val.Size = new System.Drawing.Size(34, 20);
            this.W7_ColorizationBlurBalance_val.TabIndex = 132;
            this.W7_ColorizationBlurBalance_val.UseVisualStyleBackColor = false;
            this.W7_ColorizationBlurBalance_val.Click += new System.EventHandler(this.W7_ColorizationBlurBalance_val_Click);
            // 
            // W7_ColorizationBlurBalance_bar
            // 
            this.W7_ColorizationBlurBalance_bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W7_ColorizationBlurBalance_bar.LargeChange = 10;
            this.W7_ColorizationBlurBalance_bar.Location = new System.Drawing.Point(185, 4);
            this.W7_ColorizationBlurBalance_bar.Maximum = 100;
            this.W7_ColorizationBlurBalance_bar.Minimum = 0;
            this.W7_ColorizationBlurBalance_bar.Name = "W7_ColorizationBlurBalance_bar";
            this.W7_ColorizationBlurBalance_bar.Size = new System.Drawing.Size(285, 19);
            this.W7_ColorizationBlurBalance_bar.SmallChange = 1;
            this.W7_ColorizationBlurBalance_bar.TabIndex = 7;
            this.W7_ColorizationBlurBalance_bar.Value = 0;
            this.W7_ColorizationBlurBalance_bar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.W7_ColorizationBlurBalance_bar_Scroll);
            // 
            // PictureBox8
            // 
            this.PictureBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox8.Image")));
            this.PictureBox8.Location = new System.Drawing.Point(3, 2);
            this.PictureBox8.Name = "PictureBox8";
            this.PictureBox8.Size = new System.Drawing.Size(24, 23);
            this.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox8.TabIndex = 4;
            this.PictureBox8.TabStop = false;
            // 
            // Label15
            // 
            this.Label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label15.AutoEllipsis = true;
            this.Label15.BackColor = System.Drawing.Color.Transparent;
            this.Label15.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label15.Location = new System.Drawing.Point(30, 4);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(149, 20);
            this.Label15.TabIndex = 3;
            this.Label15.Text = "Colorization blur balance";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label38
            // 
            this.Label38.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label38.BackColor = System.Drawing.Color.Transparent;
            this.Label38.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label38.Location = new System.Drawing.Point(44, 3);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(471, 35);
            this.Label38.TabIndex = 0;
            this.Label38.Text = "Aero tweaks";
            this.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox10
            // 
            this.GroupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox10.Controls.Add(this.W7_EnableAeroPeek_toggle);
            this.GroupBox10.Controls.Add(this.PictureBox4);
            this.GroupBox10.Controls.Add(this.Aero_EnableAeroPeek_lbl);
            this.GroupBox10.Location = new System.Drawing.Point(2, 104);
            this.GroupBox10.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox10.Name = "GroupBox10";
            this.GroupBox10.Size = new System.Drawing.Size(514, 28);
            this.GroupBox10.TabIndex = 22;
            // 
            // W7_EnableAeroPeek_toggle
            // 
            this.W7_EnableAeroPeek_toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W7_EnableAeroPeek_toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W7_EnableAeroPeek_toggle.Checked = false;
            this.W7_EnableAeroPeek_toggle.DarkLight_Toggler = false;
            this.W7_EnableAeroPeek_toggle.Location = new System.Drawing.Point(470, 5);
            this.W7_EnableAeroPeek_toggle.Name = "W7_EnableAeroPeek_toggle";
            this.W7_EnableAeroPeek_toggle.Size = new System.Drawing.Size(40, 20);
            this.W7_EnableAeroPeek_toggle.TabIndex = 16;
            this.W7_EnableAeroPeek_toggle.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.W7_EnableAeroPeek_toggle_CheckedChanged);
            // 
            // PictureBox4
            // 
            this.PictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(3, 2);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(24, 23);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox4.TabIndex = 4;
            this.PictureBox4.TabStop = false;
            // 
            // Aero_EnableAeroPeek_lbl
            // 
            this.Aero_EnableAeroPeek_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Aero_EnableAeroPeek_lbl.AutoEllipsis = true;
            this.Aero_EnableAeroPeek_lbl.BackColor = System.Drawing.Color.Transparent;
            this.Aero_EnableAeroPeek_lbl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Aero_EnableAeroPeek_lbl.Location = new System.Drawing.Point(30, 4);
            this.Aero_EnableAeroPeek_lbl.Name = "Aero_EnableAeroPeek_lbl";
            this.Aero_EnableAeroPeek_lbl.Size = new System.Drawing.Size(429, 20);
            this.Aero_EnableAeroPeek_lbl.TabIndex = 13;
            this.Aero_EnableAeroPeek_lbl.Text = "Aero peek";
            this.Aero_EnableAeroPeek_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox7
            // 
            this.GroupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox7.Controls.Add(this.W7_AlwaysHibernateThumbnails_Toggle);
            this.GroupBox7.Controls.Add(this.PictureBox3);
            this.GroupBox7.Controls.Add(this.Aero_AlwaysHibernateThumbnails_lbl);
            this.GroupBox7.Location = new System.Drawing.Point(2, 134);
            this.GroupBox7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox7.Name = "GroupBox7";
            this.GroupBox7.Size = new System.Drawing.Size(514, 28);
            this.GroupBox7.TabIndex = 23;
            // 
            // W7_AlwaysHibernateThumbnails_Toggle
            // 
            this.W7_AlwaysHibernateThumbnails_Toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W7_AlwaysHibernateThumbnails_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.W7_AlwaysHibernateThumbnails_Toggle.Checked = false;
            this.W7_AlwaysHibernateThumbnails_Toggle.DarkLight_Toggler = false;
            this.W7_AlwaysHibernateThumbnails_Toggle.Location = new System.Drawing.Point(470, 5);
            this.W7_AlwaysHibernateThumbnails_Toggle.Name = "W7_AlwaysHibernateThumbnails_Toggle";
            this.W7_AlwaysHibernateThumbnails_Toggle.Size = new System.Drawing.Size(40, 20);
            this.W7_AlwaysHibernateThumbnails_Toggle.TabIndex = 17;
            this.W7_AlwaysHibernateThumbnails_Toggle.CheckedChanged += new WinPaletter.UI.WP.Toggle.CheckedChangedEventHandler(this.W7_AlwaysHibernateThumbnails_Toggle_CheckedChanged);
            // 
            // PictureBox3
            // 
            this.PictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(3, 2);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(24, 23);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox3.TabIndex = 4;
            this.PictureBox3.TabStop = false;
            // 
            // Aero_AlwaysHibernateThumbnails_lbl
            // 
            this.Aero_AlwaysHibernateThumbnails_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Aero_AlwaysHibernateThumbnails_lbl.AutoEllipsis = true;
            this.Aero_AlwaysHibernateThumbnails_lbl.BackColor = System.Drawing.Color.Transparent;
            this.Aero_AlwaysHibernateThumbnails_lbl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Aero_AlwaysHibernateThumbnails_lbl.Location = new System.Drawing.Point(30, 4);
            this.Aero_AlwaysHibernateThumbnails_lbl.Name = "Aero_AlwaysHibernateThumbnails_lbl";
            this.Aero_AlwaysHibernateThumbnails_lbl.Size = new System.Drawing.Size(429, 19);
            this.Aero_AlwaysHibernateThumbnails_lbl.TabIndex = 3;
            this.Aero_AlwaysHibernateThumbnails_lbl.Text = "Hibernate thumbnails";
            this.Aero_AlwaysHibernateThumbnails_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox30
            // 
            this.GroupBox30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox30.Controls.Add(this.GroupBox21);
            this.GroupBox30.Controls.Add(this.GroupBox26);
            this.GroupBox30.Controls.Add(this.PictureBox25);
            this.GroupBox30.Controls.Add(this.Label27);
            this.GroupBox30.Location = new System.Drawing.Point(0, 0);
            this.GroupBox30.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox30.Name = "GroupBox30";
            this.GroupBox30.Size = new System.Drawing.Size(519, 105);
            this.GroupBox30.TabIndex = 11;
            // 
            // GroupBox21
            // 
            this.GroupBox21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox21.Controls.Add(this.W7_ColorizationColorBalance_val);
            this.GroupBox21.Controls.Add(this.W7_ColorizationColorBalance_bar);
            this.GroupBox21.Controls.Add(this.PictureBox12);
            this.GroupBox21.Controls.Add(this.W7_ColorizationColor_pick);
            this.GroupBox21.Controls.Add(this.Label16);
            this.GroupBox21.Location = new System.Drawing.Point(2, 44);
            this.GroupBox21.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox21.Name = "GroupBox21";
            this.GroupBox21.Size = new System.Drawing.Size(514, 28);
            this.GroupBox21.TabIndex = 20;
            // 
            // W7_ColorizationColorBalance_val
            // 
            this.W7_ColorizationColorBalance_val.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W7_ColorizationColorBalance_val.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.W7_ColorizationColorBalance_val.DrawOnGlass = false;
            this.W7_ColorizationColorBalance_val.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W7_ColorizationColorBalance_val.ForeColor = System.Drawing.Color.White;
            this.W7_ColorizationColorBalance_val.Image = null;
            this.W7_ColorizationColorBalance_val.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.W7_ColorizationColorBalance_val.Location = new System.Drawing.Point(360, 4);
            this.W7_ColorizationColorBalance_val.Name = "W7_ColorizationColorBalance_val";
            this.W7_ColorizationColorBalance_val.Size = new System.Drawing.Size(34, 20);
            this.W7_ColorizationColorBalance_val.TabIndex = 131;
            this.W7_ColorizationColorBalance_val.UseVisualStyleBackColor = false;
            this.W7_ColorizationColorBalance_val.Click += new System.EventHandler(this.W7_ColorizationColorBalance_val_Click);
            // 
            // W7_ColorizationColorBalance_bar
            // 
            this.W7_ColorizationColorBalance_bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W7_ColorizationColorBalance_bar.LargeChange = 10;
            this.W7_ColorizationColorBalance_bar.Location = new System.Drawing.Point(170, 5);
            this.W7_ColorizationColorBalance_bar.Maximum = 100;
            this.W7_ColorizationColorBalance_bar.Minimum = 0;
            this.W7_ColorizationColorBalance_bar.Name = "W7_ColorizationColorBalance_bar";
            this.W7_ColorizationColorBalance_bar.Size = new System.Drawing.Size(184, 19);
            this.W7_ColorizationColorBalance_bar.SmallChange = 1;
            this.W7_ColorizationColorBalance_bar.TabIndex = 6;
            this.W7_ColorizationColorBalance_bar.Value = 0;
            this.W7_ColorizationColorBalance_bar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.W7_ColorizationColorBalance_bar_Scroll);
            // 
            // PictureBox12
            // 
            this.PictureBox12.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox12.Image")));
            this.PictureBox12.Location = new System.Drawing.Point(3, 2);
            this.PictureBox12.Name = "PictureBox12";
            this.PictureBox12.Size = new System.Drawing.Size(24, 24);
            this.PictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox12.TabIndex = 4;
            this.PictureBox12.TabStop = false;
            // 
            // W7_ColorizationColor_pick
            // 
            this.W7_ColorizationColor_pick.AllowDrop = true;
            this.W7_ColorizationColor_pick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W7_ColorizationColor_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W7_ColorizationColor_pick.DefaultColor = System.Drawing.Color.Black;
            this.W7_ColorizationColor_pick.DontShowInfo = false;
            this.W7_ColorizationColor_pick.Location = new System.Drawing.Point(401, 3);
            this.W7_ColorizationColor_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W7_ColorizationColor_pick.Name = "W7_ColorizationColor_pick";
            this.W7_ColorizationColor_pick.Size = new System.Drawing.Size(110, 21);
            this.W7_ColorizationColor_pick.TabIndex = 2;
            this.W7_ColorizationColor_pick.Click += new System.EventHandler(this.W7_ColorizationColor_pick_Click);
            this.W7_ColorizationColor_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.W7_pick_DragDrop);
            // 
            // Label16
            // 
            this.Label16.AutoEllipsis = true;
            this.Label16.BackColor = System.Drawing.Color.Transparent;
            this.Label16.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label16.Location = new System.Drawing.Point(30, 4);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(134, 20);
            this.Label16.TabIndex = 3;
            this.Label16.Text = "Colorization color";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox26
            // 
            this.GroupBox26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupBox26.Controls.Add(this.W7_ColorizationAfterglowBalance_val);
            this.GroupBox26.Controls.Add(this.W7_ColorizationAfterglowBalance_bar);
            this.GroupBox26.Controls.Add(this.W7_ColorizationAfterglow_pick);
            this.GroupBox26.Controls.Add(this.PictureBox14);
            this.GroupBox26.Controls.Add(this.Label21);
            this.GroupBox26.Location = new System.Drawing.Point(2, 74);
            this.GroupBox26.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox26.Name = "GroupBox26";
            this.GroupBox26.Size = new System.Drawing.Size(514, 28);
            this.GroupBox26.TabIndex = 21;
            // 
            // W7_ColorizationAfterglowBalance_val
            // 
            this.W7_ColorizationAfterglowBalance_val.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W7_ColorizationAfterglowBalance_val.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.W7_ColorizationAfterglowBalance_val.DrawOnGlass = false;
            this.W7_ColorizationAfterglowBalance_val.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.W7_ColorizationAfterglowBalance_val.ForeColor = System.Drawing.Color.White;
            this.W7_ColorizationAfterglowBalance_val.Image = null;
            this.W7_ColorizationAfterglowBalance_val.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.W7_ColorizationAfterglowBalance_val.Location = new System.Drawing.Point(360, 4);
            this.W7_ColorizationAfterglowBalance_val.Name = "W7_ColorizationAfterglowBalance_val";
            this.W7_ColorizationAfterglowBalance_val.Size = new System.Drawing.Size(34, 20);
            this.W7_ColorizationAfterglowBalance_val.TabIndex = 132;
            this.W7_ColorizationAfterglowBalance_val.UseVisualStyleBackColor = false;
            this.W7_ColorizationAfterglowBalance_val.Click += new System.EventHandler(this.W7_ColorizationAfterglowBalance_val_Click);
            // 
            // W7_ColorizationAfterglowBalance_bar
            // 
            this.W7_ColorizationAfterglowBalance_bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.W7_ColorizationAfterglowBalance_bar.LargeChange = 10;
            this.W7_ColorizationAfterglowBalance_bar.Location = new System.Drawing.Point(170, 5);
            this.W7_ColorizationAfterglowBalance_bar.Maximum = 100;
            this.W7_ColorizationAfterglowBalance_bar.Minimum = 0;
            this.W7_ColorizationAfterglowBalance_bar.Name = "W7_ColorizationAfterglowBalance_bar";
            this.W7_ColorizationAfterglowBalance_bar.Size = new System.Drawing.Size(184, 19);
            this.W7_ColorizationAfterglowBalance_bar.SmallChange = 1;
            this.W7_ColorizationAfterglowBalance_bar.TabIndex = 5;
            this.W7_ColorizationAfterglowBalance_bar.Value = 0;
            this.W7_ColorizationAfterglowBalance_bar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.W7_ColorizationAfterglowBalance_bar_Scroll);
            // 
            // W7_ColorizationAfterglow_pick
            // 
            this.W7_ColorizationAfterglow_pick.AllowDrop = true;
            this.W7_ColorizationAfterglow_pick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.W7_ColorizationAfterglow_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.W7_ColorizationAfterglow_pick.DefaultColor = System.Drawing.Color.Black;
            this.W7_ColorizationAfterglow_pick.DontShowInfo = false;
            this.W7_ColorizationAfterglow_pick.Location = new System.Drawing.Point(401, 4);
            this.W7_ColorizationAfterglow_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.W7_ColorizationAfterglow_pick.Name = "W7_ColorizationAfterglow_pick";
            this.W7_ColorizationAfterglow_pick.Size = new System.Drawing.Size(110, 21);
            this.W7_ColorizationAfterglow_pick.TabIndex = 2;
            this.W7_ColorizationAfterglow_pick.Click += new System.EventHandler(this.W7_ColorizationAfterglow_pick_Click);
            this.W7_ColorizationAfterglow_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.W7_pick_DragDrop);
            // 
            // PictureBox14
            // 
            this.PictureBox14.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox14.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox14.Image")));
            this.PictureBox14.Location = new System.Drawing.Point(3, 2);
            this.PictureBox14.Name = "PictureBox14";
            this.PictureBox14.Size = new System.Drawing.Size(24, 24);
            this.PictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox14.TabIndex = 4;
            this.PictureBox14.TabStop = false;
            // 
            // Label21
            // 
            this.Label21.AutoEllipsis = true;
            this.Label21.BackColor = System.Drawing.Color.Transparent;
            this.Label21.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label21.Location = new System.Drawing.Point(30, 4);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(134, 20);
            this.Label21.TabIndex = 3;
            this.Label21.Text = "After glow color";
            this.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox25
            // 
            this.PictureBox25.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox25.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox25.Image")));
            this.PictureBox25.Location = new System.Drawing.Point(3, 3);
            this.PictureBox25.Name = "PictureBox25";
            this.PictureBox25.Size = new System.Drawing.Size(35, 35);
            this.PictureBox25.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox25.TabIndex = 1;
            this.PictureBox25.TabStop = false;
            // 
            // Label27
            // 
            this.Label27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label27.BackColor = System.Drawing.Color.Transparent;
            this.Label27.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label27.Location = new System.Drawing.Point(44, 3);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(468, 35);
            this.Label27.TabIndex = 0;
            this.Label27.Text = "Main colors";
            this.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabPage8
            // 
            this.TabPage8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage8.Controls.Add(this.GroupBox50);
            this.TabPage8.Controls.Add(this.GroupBox49);
            this.TabPage8.Location = new System.Drawing.Point(4, 24);
            this.TabPage8.Name = "TabPage8";
            this.TabPage8.Size = new System.Drawing.Size(529, 517);
            this.TabPage8.TabIndex = 5;
            this.TabPage8.Text = "WVista";
            // 
            // GroupBox50
            // 
            this.GroupBox50.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox50.Controls.Add(this.WVista_ColorizationColorBalance_val);
            this.GroupBox50.Controls.Add(this.PictureBox45);
            this.GroupBox50.Controls.Add(this.WVista_ColorizationColorBalance_bar);
            this.GroupBox50.Controls.Add(this.WVista_ColorizationColor_pick);
            this.GroupBox50.Controls.Add(this.Label80);
            this.GroupBox50.Location = new System.Drawing.Point(3, 3);
            this.GroupBox50.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox50.Name = "GroupBox50";
            this.GroupBox50.Size = new System.Drawing.Size(519, 65);
            this.GroupBox50.TabIndex = 47;
            // 
            // WVista_ColorizationColorBalance_val
            // 
            this.WVista_ColorizationColorBalance_val.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WVista_ColorizationColorBalance_val.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.WVista_ColorizationColorBalance_val.DrawOnGlass = false;
            this.WVista_ColorizationColorBalance_val.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WVista_ColorizationColorBalance_val.ForeColor = System.Drawing.Color.White;
            this.WVista_ColorizationColorBalance_val.Image = null;
            this.WVista_ColorizationColorBalance_val.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.WVista_ColorizationColorBalance_val.Location = new System.Drawing.Point(364, 39);
            this.WVista_ColorizationColorBalance_val.Name = "WVista_ColorizationColorBalance_val";
            this.WVista_ColorizationColorBalance_val.Size = new System.Drawing.Size(34, 20);
            this.WVista_ColorizationColorBalance_val.TabIndex = 131;
            this.WVista_ColorizationColorBalance_val.UseVisualStyleBackColor = false;
            this.WVista_ColorizationColorBalance_val.Click += new System.EventHandler(this.WVista_ColorizationColorBalance_val_Click);
            // 
            // PictureBox45
            // 
            this.PictureBox45.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox45.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox45.Image")));
            this.PictureBox45.Location = new System.Drawing.Point(3, 3);
            this.PictureBox45.Name = "PictureBox45";
            this.PictureBox45.Size = new System.Drawing.Size(35, 35);
            this.PictureBox45.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox45.TabIndex = 1;
            this.PictureBox45.TabStop = false;
            // 
            // WVista_ColorizationColorBalance_bar
            // 
            this.WVista_ColorizationColorBalance_bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WVista_ColorizationColorBalance_bar.LargeChange = 10;
            this.WVista_ColorizationColorBalance_bar.Location = new System.Drawing.Point(3, 40);
            this.WVista_ColorizationColorBalance_bar.Maximum = 255;
            this.WVista_ColorizationColorBalance_bar.Minimum = 0;
            this.WVista_ColorizationColorBalance_bar.Name = "WVista_ColorizationColorBalance_bar";
            this.WVista_ColorizationColorBalance_bar.Size = new System.Drawing.Size(355, 19);
            this.WVista_ColorizationColorBalance_bar.SmallChange = 1;
            this.WVista_ColorizationColorBalance_bar.TabIndex = 6;
            this.WVista_ColorizationColorBalance_bar.Value = 0;
            this.WVista_ColorizationColorBalance_bar.Scroll += new WinPaletter.UI.WP.Trackbar.ScrollEventHandler(this.WVista_ColorizationColorBalance_bar_Scroll);
            // 
            // WVista_ColorizationColor_pick
            // 
            this.WVista_ColorizationColor_pick.AllowDrop = true;
            this.WVista_ColorizationColor_pick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WVista_ColorizationColor_pick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(48)))));
            this.WVista_ColorizationColor_pick.DefaultColor = System.Drawing.Color.Black;
            this.WVista_ColorizationColor_pick.DontShowInfo = false;
            this.WVista_ColorizationColor_pick.Location = new System.Drawing.Point(404, 38);
            this.WVista_ColorizationColor_pick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.WVista_ColorizationColor_pick.Name = "WVista_ColorizationColor_pick";
            this.WVista_ColorizationColor_pick.Size = new System.Drawing.Size(110, 22);
            this.WVista_ColorizationColor_pick.TabIndex = 2;
            this.WVista_ColorizationColor_pick.Click += new System.EventHandler(this.WVista_ColorizationColor_pick_Click);
            this.WVista_ColorizationColor_pick.DragDrop += new System.Windows.Forms.DragEventHandler(this.WVista_pick_DragDrop);
            // 
            // Label80
            // 
            this.Label80.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label80.BackColor = System.Drawing.Color.Transparent;
            this.Label80.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label80.Location = new System.Drawing.Point(44, 3);
            this.Label80.Name = "Label80";
            this.Label80.Size = new System.Drawing.Size(468, 35);
            this.Label80.TabIndex = 0;
            this.Label80.Text = "Colorization color (main color)";
            this.Label80.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox49
            // 
            this.GroupBox49.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox49.Controls.Add(this.Label70);
            this.GroupBox49.Controls.Add(this.PictureBox42);
            this.GroupBox49.Controls.Add(this.WVista_theme_aero);
            this.GroupBox49.Controls.Add(this.Label72);
            this.GroupBox49.Controls.Add(this.WVista_theme_classic);
            this.GroupBox49.Controls.Add(this.Label73);
            this.GroupBox49.Controls.Add(this.WVista_theme_basic);
            this.GroupBox49.Controls.Add(this.Label74);
            this.GroupBox49.Controls.Add(this.WVista_theme_aeroopaque);
            this.GroupBox49.Controls.Add(this.Label75);
            this.GroupBox49.Location = new System.Drawing.Point(3, 73);
            this.GroupBox49.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox49.Name = "GroupBox49";
            this.GroupBox49.Padding = new System.Windows.Forms.Padding(1);
            this.GroupBox49.Size = new System.Drawing.Size(519, 145);
            this.GroupBox49.TabIndex = 46;
            // 
            // Label70
            // 
            this.Label70.AutoEllipsis = true;
            this.Label70.BackColor = System.Drawing.Color.Transparent;
            this.Label70.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label70.Location = new System.Drawing.Point(66, 113);
            this.Label70.Name = "Label70";
            this.Label70.Size = new System.Drawing.Size(90, 22);
            this.Label70.TabIndex = 39;
            this.Label70.Text = "Aero";
            this.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PictureBox42
            // 
            this.PictureBox42.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox42.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox42.Image")));
            this.PictureBox42.Location = new System.Drawing.Point(3, 3);
            this.PictureBox42.Name = "PictureBox42";
            this.PictureBox42.Size = new System.Drawing.Size(35, 35);
            this.PictureBox42.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox42.TabIndex = 1;
            this.PictureBox42.TabStop = false;
            // 
            // WVista_theme_aero
            // 
            this.WVista_theme_aero.Checked = false;
            this.WVista_theme_aero.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WVista_theme_aero.ForeColor = System.Drawing.Color.White;
            this.WVista_theme_aero.Image = ((System.Drawing.Image)(resources.GetObject("WVista_theme_aero.Image")));
            this.WVista_theme_aero.ImageWithText = false;
            this.WVista_theme_aero.Location = new System.Drawing.Point(79, 44);
            this.WVista_theme_aero.Name = "WVista_theme_aero";
            this.WVista_theme_aero.ShowText = false;
            this.WVista_theme_aero.Size = new System.Drawing.Size(64, 64);
            this.WVista_theme_aero.TabIndex = 38;
            this.WVista_theme_aero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WVista_theme_aero.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.WVista_theme_Vista_CheckedChanged);
            // 
            // Label72
            // 
            this.Label72.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label72.BackColor = System.Drawing.Color.Transparent;
            this.Label72.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label72.Location = new System.Drawing.Point(44, 3);
            this.Label72.Name = "Label72";
            this.Label72.Size = new System.Drawing.Size(471, 35);
            this.Label72.TabIndex = 0;
            this.Label72.Text = "Theme";
            this.Label72.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WVista_theme_classic
            // 
            this.WVista_theme_classic.Checked = false;
            this.WVista_theme_classic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WVista_theme_classic.ForeColor = System.Drawing.Color.White;
            this.WVista_theme_classic.Image = ((System.Drawing.Image)(resources.GetObject("WVista_theme_classic.Image")));
            this.WVista_theme_classic.ImageWithText = false;
            this.WVista_theme_classic.Location = new System.Drawing.Point(370, 44);
            this.WVista_theme_classic.Name = "WVista_theme_classic";
            this.WVista_theme_classic.ShowText = false;
            this.WVista_theme_classic.Size = new System.Drawing.Size(64, 64);
            this.WVista_theme_classic.TabIndex = 32;
            this.WVista_theme_classic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WVista_theme_classic.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.WVista_theme_classic_CheckedChanged);
            // 
            // Label73
            // 
            this.Label73.AutoEllipsis = true;
            this.Label73.BackColor = System.Drawing.Color.Transparent;
            this.Label73.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label73.Location = new System.Drawing.Point(162, 113);
            this.Label73.Name = "Label73";
            this.Label73.Size = new System.Drawing.Size(90, 22);
            this.Label73.TabIndex = 37;
            this.Label73.Text = "Aero Opaque";
            this.Label73.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WVista_theme_basic
            // 
            this.WVista_theme_basic.Checked = false;
            this.WVista_theme_basic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WVista_theme_basic.ForeColor = System.Drawing.Color.White;
            this.WVista_theme_basic.Image = ((System.Drawing.Image)(resources.GetObject("WVista_theme_basic.Image")));
            this.WVista_theme_basic.ImageWithText = false;
            this.WVista_theme_basic.Location = new System.Drawing.Point(274, 44);
            this.WVista_theme_basic.Name = "WVista_theme_basic";
            this.WVista_theme_basic.ShowText = false;
            this.WVista_theme_basic.Size = new System.Drawing.Size(64, 64);
            this.WVista_theme_basic.TabIndex = 34;
            this.WVista_theme_basic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WVista_theme_basic.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.WVista_theme_basic_CheckedChanged);
            // 
            // Label74
            // 
            this.Label74.AutoEllipsis = true;
            this.Label74.BackColor = System.Drawing.Color.Transparent;
            this.Label74.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label74.Location = new System.Drawing.Point(261, 113);
            this.Label74.Name = "Label74";
            this.Label74.Size = new System.Drawing.Size(90, 22);
            this.Label74.TabIndex = 35;
            this.Label74.Text = "Basic";
            this.Label74.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WVista_theme_aeroopaque
            // 
            this.WVista_theme_aeroopaque.Checked = false;
            this.WVista_theme_aeroopaque.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WVista_theme_aeroopaque.ForeColor = System.Drawing.Color.White;
            this.WVista_theme_aeroopaque.Image = ((System.Drawing.Image)(resources.GetObject("WVista_theme_aeroopaque.Image")));
            this.WVista_theme_aeroopaque.ImageWithText = false;
            this.WVista_theme_aeroopaque.Location = new System.Drawing.Point(175, 44);
            this.WVista_theme_aeroopaque.Name = "WVista_theme_aeroopaque";
            this.WVista_theme_aeroopaque.ShowText = false;
            this.WVista_theme_aeroopaque.Size = new System.Drawing.Size(64, 64);
            this.WVista_theme_aeroopaque.TabIndex = 36;
            this.WVista_theme_aeroopaque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WVista_theme_aeroopaque.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.WVista_theme_aeroopaque_CheckedChanged);
            // 
            // Label75
            // 
            this.Label75.AutoEllipsis = true;
            this.Label75.BackColor = System.Drawing.Color.Transparent;
            this.Label75.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label75.Location = new System.Drawing.Point(357, 113);
            this.Label75.Name = "Label75";
            this.Label75.Size = new System.Drawing.Size(90, 22);
            this.Label75.TabIndex = 33;
            this.Label75.Text = "Classic";
            this.Label75.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TabPage9
            // 
            this.TabPage9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage9.Controls.Add(this.groupBox51);
            this.TabPage9.Controls.Add(this.WXP_Alert1);
            this.TabPage9.Controls.Add(this.GroupBox48);
            this.TabPage9.Controls.Add(this.GroupBox47);
            this.TabPage9.Location = new System.Drawing.Point(4, 24);
            this.TabPage9.Name = "TabPage9";
            this.TabPage9.Size = new System.Drawing.Size(529, 517);
            this.TabPage9.TabIndex = 6;
            this.TabPage9.Text = "WXP";
            // 
            // groupBox51
            // 
            this.groupBox51.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.groupBox51.Controls.Add(this.Label76);
            this.groupBox51.Controls.Add(this.pictureBox36);
            this.groupBox51.Controls.Add(this.WXP_VS_ReplaceColors);
            this.groupBox51.Controls.Add(this.WXP_VS_ReplaceMetrics);
            this.groupBox51.Controls.Add(this.WXP_VS_ReplaceFonts);
            this.groupBox51.Location = new System.Drawing.Point(3, 260);
            this.groupBox51.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox51.Name = "groupBox51";
            this.groupBox51.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox51.Size = new System.Drawing.Size(519, 80);
            this.groupBox51.TabIndex = 54;
            // 
            // Label76
            // 
            this.Label76.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label76.BackColor = System.Drawing.Color.Transparent;
            this.Label76.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label76.Location = new System.Drawing.Point(44, 4);
            this.Label76.Name = "Label76";
            this.Label76.Size = new System.Drawing.Size(471, 35);
            this.Label76.TabIndex = 55;
            this.Label76.Text = "Make choosing any theme overwrites:";
            this.Label76.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox36
            // 
            this.pictureBox36.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox36.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox36.Image")));
            this.pictureBox36.Location = new System.Drawing.Point(3, 4);
            this.pictureBox36.Name = "pictureBox36";
            this.pictureBox36.Size = new System.Drawing.Size(35, 35);
            this.pictureBox36.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox36.TabIndex = 52;
            this.pictureBox36.TabStop = false;
            // 
            // WXP_VS_ReplaceColors
            // 
            this.WXP_VS_ReplaceColors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.WXP_VS_ReplaceColors.Checked = false;
            this.WXP_VS_ReplaceColors.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_VS_ReplaceColors.ForeColor = System.Drawing.Color.White;
            this.WXP_VS_ReplaceColors.Location = new System.Drawing.Point(12, 46);
            this.WXP_VS_ReplaceColors.Name = "WXP_VS_ReplaceColors";
            this.WXP_VS_ReplaceColors.Size = new System.Drawing.Size(150, 23);
            this.WXP_VS_ReplaceColors.TabIndex = 49;
            this.WXP_VS_ReplaceColors.Text = "Colors in classic colors";
            // 
            // WXP_VS_ReplaceMetrics
            // 
            this.WXP_VS_ReplaceMetrics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.WXP_VS_ReplaceMetrics.Checked = false;
            this.WXP_VS_ReplaceMetrics.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_VS_ReplaceMetrics.ForeColor = System.Drawing.Color.White;
            this.WXP_VS_ReplaceMetrics.Location = new System.Drawing.Point(194, 46);
            this.WXP_VS_ReplaceMetrics.Name = "WXP_VS_ReplaceMetrics";
            this.WXP_VS_ReplaceMetrics.Size = new System.Drawing.Size(150, 23);
            this.WXP_VS_ReplaceMetrics.TabIndex = 50;
            this.WXP_VS_ReplaceMetrics.Text = "Metrics values";
            // 
            // WXP_VS_ReplaceFonts
            // 
            this.WXP_VS_ReplaceFonts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.WXP_VS_ReplaceFonts.Checked = false;
            this.WXP_VS_ReplaceFonts.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_VS_ReplaceFonts.ForeColor = System.Drawing.Color.White;
            this.WXP_VS_ReplaceFonts.Location = new System.Drawing.Point(374, 45);
            this.WXP_VS_ReplaceFonts.Name = "WXP_VS_ReplaceFonts";
            this.WXP_VS_ReplaceFonts.Size = new System.Drawing.Size(129, 23);
            this.WXP_VS_ReplaceFonts.TabIndex = 51;
            this.WXP_VS_ReplaceFonts.Text = "System fonts";
            // 
            // WXP_Alert1
            // 
            this.WXP_Alert1.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Simple;
            this.WXP_Alert1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WXP_Alert1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.WXP_Alert1.CenterText = false;
            this.WXP_Alert1.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.WXP_Alert1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_Alert1.Image = null;
            this.WXP_Alert1.Location = new System.Drawing.Point(3, 485);
            this.WXP_Alert1.Name = "WXP_Alert1";
            this.WXP_Alert1.Size = new System.Drawing.Size(519, 29);
            this.WXP_Alert1.TabIndex = 53;
            this.WXP_Alert1.TabStop = false;
            this.WXP_Alert1.Text = "External theme\\visual styles require a UX-theme-patched Windows";
            // 
            // GroupBox48
            // 
            this.GroupBox48.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox48.Controls.Add(this.WXP_VS_ColorsList);
            this.GroupBox48.Controls.Add(this.PictureBox38);
            this.GroupBox48.Controls.Add(this.WXP_VS_Browse);
            this.GroupBox48.Controls.Add(this.Label71);
            this.GroupBox48.Controls.Add(this.PictureBox41);
            this.GroupBox48.Controls.Add(this.PictureBox40);
            this.GroupBox48.Controls.Add(this.Label69);
            this.GroupBox48.Controls.Add(this.Label67);
            this.GroupBox48.Controls.Add(this.WXP_VS_textbox);
            this.GroupBox48.Location = new System.Drawing.Point(3, 153);
            this.GroupBox48.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox48.Name = "GroupBox48";
            this.GroupBox48.Padding = new System.Windows.Forms.Padding(1);
            this.GroupBox48.Size = new System.Drawing.Size(519, 102);
            this.GroupBox48.TabIndex = 42;
            // 
            // WXP_VS_ColorsList
            // 
            this.WXP_VS_ColorsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WXP_VS_ColorsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.WXP_VS_ColorsList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.WXP_VS_ColorsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WXP_VS_ColorsList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_VS_ColorsList.ForeColor = System.Drawing.Color.White;
            this.WXP_VS_ColorsList.FormattingEnabled = true;
            this.WXP_VS_ColorsList.ItemHeight = 20;
            this.WXP_VS_ColorsList.Location = new System.Drawing.Point(93, 72);
            this.WXP_VS_ColorsList.Name = "WXP_VS_ColorsList";
            this.WXP_VS_ColorsList.Size = new System.Drawing.Size(420, 26);
            this.WXP_VS_ColorsList.TabIndex = 5;
            this.WXP_VS_ColorsList.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // PictureBox38
            // 
            this.PictureBox38.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox38.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox38.Image")));
            this.PictureBox38.Location = new System.Drawing.Point(3, 3);
            this.PictureBox38.Name = "PictureBox38";
            this.PictureBox38.Size = new System.Drawing.Size(35, 35);
            this.PictureBox38.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox38.TabIndex = 1;
            this.PictureBox38.TabStop = false;
            // 
            // WXP_VS_Browse
            // 
            this.WXP_VS_Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WXP_VS_Browse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.WXP_VS_Browse.DrawOnGlass = false;
            this.WXP_VS_Browse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_VS_Browse.ForeColor = System.Drawing.Color.White;
            this.WXP_VS_Browse.Image = ((System.Drawing.Image)(resources.GetObject("WXP_VS_Browse.Image")));
            this.WXP_VS_Browse.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(153)))), ((int)(((byte)(68)))));
            this.WXP_VS_Browse.Location = new System.Drawing.Point(479, 43);
            this.WXP_VS_Browse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.WXP_VS_Browse.Name = "WXP_VS_Browse";
            this.WXP_VS_Browse.Size = new System.Drawing.Size(35, 24);
            this.WXP_VS_Browse.TabIndex = 88;
            this.WXP_VS_Browse.UseVisualStyleBackColor = false;
            this.WXP_VS_Browse.Click += new System.EventHandler(this.Button30_Click);
            // 
            // Label71
            // 
            this.Label71.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label71.BackColor = System.Drawing.Color.Transparent;
            this.Label71.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label71.Location = new System.Drawing.Point(44, 3);
            this.Label71.Name = "Label71";
            this.Label71.Size = new System.Drawing.Size(471, 35);
            this.Label71.TabIndex = 0;
            this.Label71.Text = "External theme";
            this.Label71.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox41
            // 
            this.PictureBox41.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox41.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox41.Image")));
            this.PictureBox41.Location = new System.Drawing.Point(5, 73);
            this.PictureBox41.Name = "PictureBox41";
            this.PictureBox41.Size = new System.Drawing.Size(24, 24);
            this.PictureBox41.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox41.TabIndex = 4;
            this.PictureBox41.TabStop = false;
            // 
            // PictureBox40
            // 
            this.PictureBox40.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox40.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox40.Image")));
            this.PictureBox40.Location = new System.Drawing.Point(5, 43);
            this.PictureBox40.Name = "PictureBox40";
            this.PictureBox40.Size = new System.Drawing.Size(24, 24);
            this.PictureBox40.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox40.TabIndex = 4;
            this.PictureBox40.TabStop = false;
            // 
            // Label69
            // 
            this.Label69.AutoEllipsis = true;
            this.Label69.BackColor = System.Drawing.Color.Transparent;
            this.Label69.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label69.Location = new System.Drawing.Point(32, 75);
            this.Label69.Name = "Label69";
            this.Label69.Size = new System.Drawing.Size(55, 20);
            this.Label69.TabIndex = 3;
            this.Label69.Text = "Scheme:";
            this.Label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label67
            // 
            this.Label67.AutoEllipsis = true;
            this.Label67.BackColor = System.Drawing.Color.Transparent;
            this.Label67.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label67.Location = new System.Drawing.Point(32, 45);
            this.Label67.Name = "Label67";
            this.Label67.Size = new System.Drawing.Size(55, 20);
            this.Label67.TabIndex = 3;
            this.Label67.Text = "File:";
            this.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WXP_VS_textbox
            // 
            this.WXP_VS_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WXP_VS_textbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.WXP_VS_textbox.DrawOnGlass = false;
            this.WXP_VS_textbox.ForeColor = System.Drawing.Color.White;
            this.WXP_VS_textbox.Location = new System.Drawing.Point(93, 43);
            this.WXP_VS_textbox.MaxLength = 32767;
            this.WXP_VS_textbox.Multiline = false;
            this.WXP_VS_textbox.Name = "WXP_VS_textbox";
            this.WXP_VS_textbox.ReadOnly = false;
            this.WXP_VS_textbox.Scrollbars = System.Windows.Forms.ScrollBars.None;
            this.WXP_VS_textbox.SelectedText = "";
            this.WXP_VS_textbox.SelectionLength = 0;
            this.WXP_VS_textbox.SelectionStart = 0;
            this.WXP_VS_textbox.Size = new System.Drawing.Size(382, 24);
            this.WXP_VS_textbox.TabIndex = 5;
            this.WXP_VS_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.WXP_VS_textbox.UseSystemPasswordChar = false;
            this.WXP_VS_textbox.WordWrap = true;
            this.WXP_VS_textbox.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // GroupBox47
            // 
            this.GroupBox47.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.GroupBox47.Controls.Add(this.SeparatorVertical3);
            this.GroupBox47.Controls.Add(this.Label68);
            this.GroupBox47.Controls.Add(this.WXP_CustomTheme);
            this.GroupBox47.Controls.Add(this.WXP_Classic);
            this.GroupBox47.Controls.Add(this.Label66);
            this.GroupBox47.Controls.Add(this.Label62);
            this.GroupBox47.Controls.Add(this.PictureBox6);
            this.GroupBox47.Controls.Add(this.WXP_Luna_Blue);
            this.GroupBox47.Controls.Add(this.Label63);
            this.GroupBox47.Controls.Add(this.Label64);
            this.GroupBox47.Controls.Add(this.WXP_Luna_Silver);
            this.GroupBox47.Controls.Add(this.Label65);
            this.GroupBox47.Controls.Add(this.WXP_Luna_OliveGreen);
            this.GroupBox47.Location = new System.Drawing.Point(3, 3);
            this.GroupBox47.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox47.Name = "GroupBox47";
            this.GroupBox47.Padding = new System.Windows.Forms.Padding(1);
            this.GroupBox47.Size = new System.Drawing.Size(519, 145);
            this.GroupBox47.TabIndex = 41;
            // 
            // SeparatorVertical3
            // 
            this.SeparatorVertical3.AlternativeLook = false;
            this.SeparatorVertical3.Location = new System.Drawing.Point(312, 47);
            this.SeparatorVertical3.Name = "SeparatorVertical3";
            this.SeparatorVertical3.Size = new System.Drawing.Size(1, 91);
            this.SeparatorVertical3.TabIndex = 44;
            this.SeparatorVertical3.TabStop = false;
            // 
            // Label68
            // 
            this.Label68.AutoEllipsis = true;
            this.Label68.BackColor = System.Drawing.Color.Transparent;
            this.Label68.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label68.Location = new System.Drawing.Point(315, 116);
            this.Label68.Name = "Label68";
            this.Label68.Size = new System.Drawing.Size(90, 22);
            this.Label68.TabIndex = 43;
            this.Label68.Text = "External";
            this.Label68.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WXP_CustomTheme
            // 
            this.WXP_CustomTheme.Checked = false;
            this.WXP_CustomTheme.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_CustomTheme.ForeColor = System.Drawing.Color.White;
            this.WXP_CustomTheme.Image = ((System.Drawing.Image)(resources.GetObject("WXP_CustomTheme.Image")));
            this.WXP_CustomTheme.ImageWithText = false;
            this.WXP_CustomTheme.Location = new System.Drawing.Point(328, 47);
            this.WXP_CustomTheme.Name = "WXP_CustomTheme";
            this.WXP_CustomTheme.ShowText = false;
            this.WXP_CustomTheme.Size = new System.Drawing.Size(64, 64);
            this.WXP_CustomTheme.TabIndex = 42;
            this.WXP_CustomTheme.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WXP_CustomTheme.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.WXP_CustomTheme_CheckedChanged);
            // 
            // WXP_Classic
            // 
            this.WXP_Classic.Checked = false;
            this.WXP_Classic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_Classic.ForeColor = System.Drawing.Color.White;
            this.WXP_Classic.Image = ((System.Drawing.Image)(resources.GetObject("WXP_Classic.Image")));
            this.WXP_Classic.ImageWithText = false;
            this.WXP_Classic.Location = new System.Drawing.Point(420, 47);
            this.WXP_Classic.Name = "WXP_Classic";
            this.WXP_Classic.ShowText = false;
            this.WXP_Classic.Size = new System.Drawing.Size(64, 64);
            this.WXP_Classic.TabIndex = 32;
            this.WXP_Classic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WXP_Classic.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.WXP_Classic_CheckedChanged);
            // 
            // Label66
            // 
            this.Label66.AutoEllipsis = true;
            this.Label66.BackColor = System.Drawing.Color.Transparent;
            this.Label66.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label66.Location = new System.Drawing.Point(407, 116);
            this.Label66.Name = "Label66";
            this.Label66.Size = new System.Drawing.Size(90, 22);
            this.Label66.TabIndex = 33;
            this.Label66.Text = "Classic";
            this.Label66.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label62
            // 
            this.Label62.AutoEllipsis = true;
            this.Label62.BackColor = System.Drawing.Color.Transparent;
            this.Label62.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label62.Location = new System.Drawing.Point(22, 116);
            this.Label62.Name = "Label62";
            this.Label62.Size = new System.Drawing.Size(90, 22);
            this.Label62.TabIndex = 39;
            this.Label62.Text = "Luna Blue";
            this.Label62.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PictureBox6
            // 
            this.PictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox6.Image")));
            this.PictureBox6.Location = new System.Drawing.Point(3, 3);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(35, 35);
            this.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox6.TabIndex = 1;
            this.PictureBox6.TabStop = false;
            // 
            // WXP_Luna_Blue
            // 
            this.WXP_Luna_Blue.Checked = false;
            this.WXP_Luna_Blue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_Luna_Blue.ForeColor = System.Drawing.Color.White;
            this.WXP_Luna_Blue.Image = ((System.Drawing.Image)(resources.GetObject("WXP_Luna_Blue.Image")));
            this.WXP_Luna_Blue.ImageWithText = false;
            this.WXP_Luna_Blue.Location = new System.Drawing.Point(35, 47);
            this.WXP_Luna_Blue.Name = "WXP_Luna_Blue";
            this.WXP_Luna_Blue.ShowText = false;
            this.WXP_Luna_Blue.Size = new System.Drawing.Size(64, 64);
            this.WXP_Luna_Blue.TabIndex = 38;
            this.WXP_Luna_Blue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WXP_Luna_Blue.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.WXP_Luna_Blue_CheckedChanged);
            // 
            // Label63
            // 
            this.Label63.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label63.BackColor = System.Drawing.Color.Transparent;
            this.Label63.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label63.Location = new System.Drawing.Point(44, 3);
            this.Label63.Name = "Label63";
            this.Label63.Size = new System.Drawing.Size(471, 35);
            this.Label63.TabIndex = 0;
            this.Label63.Text = "Theme";
            this.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label64
            // 
            this.Label64.AutoEllipsis = true;
            this.Label64.BackColor = System.Drawing.Color.Transparent;
            this.Label64.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label64.Location = new System.Drawing.Point(118, 116);
            this.Label64.Name = "Label64";
            this.Label64.Size = new System.Drawing.Size(90, 22);
            this.Label64.TabIndex = 37;
            this.Label64.Text = "Luna Green";
            this.Label64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WXP_Luna_Silver
            // 
            this.WXP_Luna_Silver.Checked = false;
            this.WXP_Luna_Silver.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_Luna_Silver.ForeColor = System.Drawing.Color.White;
            this.WXP_Luna_Silver.Image = ((System.Drawing.Image)(resources.GetObject("WXP_Luna_Silver.Image")));
            this.WXP_Luna_Silver.ImageWithText = false;
            this.WXP_Luna_Silver.Location = new System.Drawing.Point(230, 47);
            this.WXP_Luna_Silver.Name = "WXP_Luna_Silver";
            this.WXP_Luna_Silver.ShowText = false;
            this.WXP_Luna_Silver.Size = new System.Drawing.Size(64, 64);
            this.WXP_Luna_Silver.TabIndex = 34;
            this.WXP_Luna_Silver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WXP_Luna_Silver.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.WXP_Luna_Silver_CheckedChanged);
            // 
            // Label65
            // 
            this.Label65.AutoEllipsis = true;
            this.Label65.BackColor = System.Drawing.Color.Transparent;
            this.Label65.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label65.Location = new System.Drawing.Point(217, 116);
            this.Label65.Name = "Label65";
            this.Label65.Size = new System.Drawing.Size(90, 22);
            this.Label65.TabIndex = 35;
            this.Label65.Text = "Luna Silver";
            this.Label65.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WXP_Luna_OliveGreen
            // 
            this.WXP_Luna_OliveGreen.Checked = false;
            this.WXP_Luna_OliveGreen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WXP_Luna_OliveGreen.ForeColor = System.Drawing.Color.White;
            this.WXP_Luna_OliveGreen.Image = ((System.Drawing.Image)(resources.GetObject("WXP_Luna_OliveGreen.Image")));
            this.WXP_Luna_OliveGreen.ImageWithText = false;
            this.WXP_Luna_OliveGreen.Location = new System.Drawing.Point(131, 47);
            this.WXP_Luna_OliveGreen.Name = "WXP_Luna_OliveGreen";
            this.WXP_Luna_OliveGreen.ShowText = false;
            this.WXP_Luna_OliveGreen.Size = new System.Drawing.Size(64, 64);
            this.WXP_Luna_OliveGreen.TabIndex = 36;
            this.WXP_Luna_OliveGreen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WXP_Luna_OliveGreen.CheckedChanged += new WinPaletter.UI.WP.RadioImage.CheckedChangedEventHandler(this.WXP_Luna_OliveGreen_CheckedChanged);
            // 
            // Button19
            // 
            this.Button19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(35)))));
            this.Button19.DrawOnGlass = false;
            this.Button19.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button19.ForeColor = System.Drawing.Color.White;
            this.Button19.Image = ((System.Drawing.Image)(resources.GetObject("Button19.Image")));
            this.Button19.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button19.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(127)))), ((int)(((byte)(79)))));
            this.Button19.Location = new System.Drawing.Point(836, 655);
            this.Button19.Name = "Button19";
            this.Button19.Size = new System.Drawing.Size(140, 34);
            this.Button19.TabIndex = 29;
            this.Button19.Text = "Restart Explorer";
            this.Button19.UseVisualStyleBackColor = false;
            this.Button19.Click += new System.EventHandler(this.Button19_Click);
            this.Button19.MouseEnter += new System.EventHandler(this.Button19_MouseEnter);
            this.Button19.MouseLeave += new System.EventHandler(this.Button19_MouseLeave);
            // 
            // apply_btn
            // 
            this.apply_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.apply_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(35)))));
            this.apply_btn.DrawOnGlass = false;
            this.apply_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.apply_btn.ForeColor = System.Drawing.Color.White;
            this.apply_btn.Image = ((System.Drawing.Image)(resources.GetObject("apply_btn.Image")));
            this.apply_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.apply_btn.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(133)))), ((int)(((byte)(186)))));
            this.apply_btn.Location = new System.Drawing.Point(982, 655);
            this.apply_btn.Name = "apply_btn";
            this.apply_btn.Size = new System.Drawing.Size(100, 34);
            this.apply_btn.TabIndex = 16;
            this.apply_btn.Text = "Apply";
            this.apply_btn.UseVisualStyleBackColor = false;
            this.apply_btn.Click += new System.EventHandler(this.Button4_Click);
            this.apply_btn.MouseEnter += new System.EventHandler(this.Apply_btn_MouseEnter);
            this.apply_btn.MouseLeave += new System.EventHandler(this.Apply_btn_MouseLeave);
            // 
            // BetaBadge
            // 
            this.BetaBadge.AlertStyle = WinPaletter.UI.WP.AlertBox.Style.Warning;
            this.BetaBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BetaBadge.BackColor = System.Drawing.Color.Transparent;
            this.BetaBadge.CenterText = true;
            this.BetaBadge.CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(210)))));
            this.BetaBadge.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BetaBadge.Image = null;
            this.BetaBadge.Location = new System.Drawing.Point(1036, 534);
            this.BetaBadge.Name = "BetaBadge";
            this.BetaBadge.Size = new System.Drawing.Size(47, 22);
            this.BetaBadge.TabIndex = 55;
            this.BetaBadge.TabStop = false;
            this.BetaBadge.Text = "BETA";
            this.BetaBadge.Visible = false;
            // 
            // Button13
            // 
            this.Button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(35)))));
            this.Button13.DrawOnGlass = false;
            this.Button13.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button13.ForeColor = System.Drawing.Color.White;
            this.Button13.Image = null;
            this.Button13.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(49)))), ((int)(((byte)(61)))));
            this.Button13.Location = new System.Drawing.Point(644, 655);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(80, 34);
            this.Button13.TabIndex = 26;
            this.Button13.Text = "Cancel";
            this.Button13.UseVisualStyleBackColor = false;
            this.Button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // MainToolbar
            // 
            this.MainToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(35)))));
            this.MainToolbar.Controls.Add(this.userButton);
            this.MainToolbar.Controls.Add(this.button8);
            this.MainToolbar.Controls.Add(this.Button40);
            this.MainToolbar.Controls.Add(this.Button39);
            this.MainToolbar.Controls.Add(this.Button36);
            this.MainToolbar.Controls.Add(this.Button31);
            this.MainToolbar.Controls.Add(this.Button20);
            this.MainToolbar.Controls.Add(this.Button18);
            this.MainToolbar.Controls.Add(this.Button17);
            this.MainToolbar.Controls.Add(this.Button12);
            this.MainToolbar.Controls.Add(this.Button5);
            this.MainToolbar.Controls.Add(this.Button6);
            this.MainToolbar.Controls.Add(this.Button10);
            this.MainToolbar.Controls.Add(this.Button11);
            this.MainToolbar.Controls.Add(this.SeparatorVertical1);
            this.MainToolbar.Controls.Add(this.status_lbl);
            this.MainToolbar.Controls.Add(this.Button7);
            this.MainToolbar.Controls.Add(this.Button9);
            this.MainToolbar.Controls.Add(this.Button3);
            this.MainToolbar.Controls.Add(this.Button2);
            this.MainToolbar.Location = new System.Drawing.Point(13, 11);
            this.MainToolbar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MainToolbar.Name = "MainToolbar";
            this.MainToolbar.Size = new System.Drawing.Size(1070, 60);
            this.MainToolbar.TabIndex = 1;
            // 
            // userButton
            // 
            this.userButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.userButton.DrawOnGlass = false;
            this.userButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.userButton.ForeColor = System.Drawing.Color.White;
            this.userButton.Image = null;
            this.userButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(94)))), ((int)(((byte)(130)))));
            this.userButton.Location = new System.Drawing.Point(1012, 3);
            this.userButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.userButton.Name = "userButton";
            this.userButton.Size = new System.Drawing.Size(54, 54);
            this.userButton.TabIndex = 36;
            this.userButton.Tag = "About";
            this.userButton.UseVisualStyleBackColor = false;
            this.userButton.Click += new System.EventHandler(this.userButton_Click);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.button8.DrawOnGlass = false;
            this.button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.button8.Location = new System.Drawing.Point(542, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(40, 54);
            this.button8.TabIndex = 36;
            this.button8.Tag = "Rescue tools";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // Button40
            // 
            this.Button40.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button40.DrawOnGlass = false;
            this.Button40.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button40.ForeColor = System.Drawing.Color.White;
            this.Button40.Image = ((System.Drawing.Image)(resources.GetObject("Button40.Image")));
            this.Button40.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.Button40.Location = new System.Drawing.Point(208, 3);
            this.Button40.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button40.Name = "Button40";
            this.Button40.Size = new System.Drawing.Size(40, 54);
            this.Button40.TabIndex = 58;
            this.Button40.Tag = "Generate a palette for your theme";
            this.Button40.UseVisualStyleBackColor = false;
            this.Button40.Click += new System.EventHandler(this.Button40_Click);
            // 
            // Button39
            // 
            this.Button39.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button39.DrawOnGlass = false;
            this.Button39.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button39.ForeColor = System.Drawing.Color.White;
            this.Button39.Image = ((System.Drawing.Image)(resources.GetObject("Button39.Image")));
            this.Button39.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(100)))), ((int)(((byte)(136)))));
            this.Button39.Location = new System.Drawing.Point(624, 3);
            this.Button39.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button39.Name = "Button39";
            this.Button39.Size = new System.Drawing.Size(40, 54);
            this.Button39.TabIndex = 57;
            this.Button39.Tag = "Help (Wiki)";
            this.Button39.UseVisualStyleBackColor = false;
            this.Button39.Click += new System.EventHandler(this.Button39_Click);
            // 
            // Button36
            // 
            this.Button36.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button36.DrawOnGlass = false;
            this.Button36.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button36.ForeColor = System.Drawing.Color.White;
            this.Button36.Image = ((System.Drawing.Image)(resources.GetObject("Button36.Image")));
            this.Button36.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(85)))), ((int)(((byte)(117)))));
            this.Button36.Location = new System.Drawing.Point(372, 3);
            this.Button36.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button36.Name = "Button36";
            this.Button36.Size = new System.Drawing.Size(40, 54);
            this.Button36.TabIndex = 56;
            this.Button36.Tag = "WinPaletter themes converter";
            this.Button36.UseVisualStyleBackColor = false;
            this.Button36.Click += new System.EventHandler(this.Button36_Click);
            // 
            // Button31
            // 
            this.Button31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button31.DrawOnGlass = false;
            this.Button31.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button31.ForeColor = System.Drawing.Color.White;
            this.Button31.Image = ((System.Drawing.Image)(resources.GetObject("Button31.Image")));
            this.Button31.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(109)))), ((int)(((byte)(112)))));
            this.Button31.Location = new System.Drawing.Point(501, 3);
            this.Button31.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button31.Name = "Button31";
            this.Button31.Size = new System.Drawing.Size(40, 54);
            this.Button31.TabIndex = 23;
            this.Button31.Tag = "WinPaletter Store";
            this.Button31.UseVisualStyleBackColor = false;
            this.Button31.Click += new System.EventHandler(this.Button31_Click);
            // 
            // Button20
            // 
            this.Button20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button20.DrawOnGlass = false;
            this.Button20.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button20.ForeColor = System.Drawing.Color.White;
            this.Button20.Image = ((System.Drawing.Image)(resources.GetObject("Button20.Image")));
            this.Button20.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(134)))), ((int)(((byte)(136)))));
            this.Button20.Location = new System.Drawing.Point(44, 3);
            this.Button20.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button20.Name = "Button20";
            this.Button20.Size = new System.Drawing.Size(40, 54);
            this.Button20.TabIndex = 22;
            this.Button20.Tag = "Create new WinPaletter theme File based on native Windows preferences";
            this.Button20.UseVisualStyleBackColor = false;
            this.Button20.Click += new System.EventHandler(this.Button20_Click);
            // 
            // Button18
            // 
            this.Button18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button18.DrawOnGlass = false;
            this.Button18.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button18.ForeColor = System.Drawing.Color.White;
            this.Button18.Image = ((System.Drawing.Image)(resources.GetObject("Button18.Image")));
            this.Button18.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(14)))), ((int)(((byte)(1)))));
            this.Button18.Location = new System.Drawing.Point(290, 3);
            this.Button18.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button18.Name = "Button18";
            this.Button18.Size = new System.Drawing.Size(40, 54);
            this.Button18.TabIndex = 21;
            this.Button18.Tag = "Reload first theme";
            this.Button18.UseVisualStyleBackColor = false;
            this.Button18.Click += new System.EventHandler(this.Button18_Click);
            // 
            // Button17
            // 
            this.Button17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button17.DrawOnGlass = false;
            this.Button17.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button17.ForeColor = System.Drawing.Color.White;
            this.Button17.Image = ((System.Drawing.Image)(resources.GetObject("Button17.Image")));
            this.Button17.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(34)))), ((int)(((byte)(58)))));
            this.Button17.Location = new System.Drawing.Point(249, 3);
            this.Button17.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button17.Name = "Button17";
            this.Button17.Size = new System.Drawing.Size(40, 54);
            this.Button17.TabIndex = 20;
            this.Button17.Tag = "Reload previous theme (similar to undo)";
            this.Button17.UseVisualStyleBackColor = false;
            this.Button17.Click += new System.EventHandler(this.Button17_Click);
            // 
            // Button12
            // 
            this.Button12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button12.DrawOnGlass = false;
            this.Button12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button12.ForeColor = System.Drawing.Color.White;
            this.Button12.Image = ((System.Drawing.Image)(resources.GetObject("Button12.Image")));
            this.Button12.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(94)))), ((int)(((byte)(130)))));
            this.Button12.Location = new System.Drawing.Point(665, 3);
            this.Button12.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button12.Name = "Button12";
            this.Button12.Size = new System.Drawing.Size(40, 54);
            this.Button12.TabIndex = 12;
            this.Button12.Tag = "About";
            this.Button12.UseVisualStyleBackColor = false;
            this.Button12.Click += new System.EventHandler(this.Button12_Click);
            // 
            // Button5
            // 
            this.Button5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button5.DrawOnGlass = false;
            this.Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Image = ((System.Drawing.Image)(resources.GetObject("Button5.Image")));
            this.Button5.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(53)))), ((int)(((byte)(75)))));
            this.Button5.Location = new System.Drawing.Point(460, 3);
            this.Button5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(40, 54);
            this.Button5.TabIndex = 10;
            this.Button5.Tag = "Updates";
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Button6
            // 
            this.Button6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button6.DrawOnGlass = false;
            this.Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button6.ForeColor = System.Drawing.Color.White;
            this.Button6.Image = ((System.Drawing.Image)(resources.GetObject("Button6.Image")));
            this.Button6.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(45)))), ((int)(((byte)(137)))));
            this.Button6.Location = new System.Drawing.Point(583, 3);
            this.Button6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(40, 54);
            this.Button6.TabIndex = 11;
            this.Button6.Tag = "What\'s new";
            this.Button6.UseVisualStyleBackColor = false;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Button10
            // 
            this.Button10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button10.DrawOnGlass = false;
            this.Button10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button10.ForeColor = System.Drawing.Color.White;
            this.Button10.Image = ((System.Drawing.Image)(resources.GetObject("Button10.Image")));
            this.Button10.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(38)))), ((int)(((byte)(12)))));
            this.Button10.Location = new System.Drawing.Point(331, 3);
            this.Button10.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(40, 54);
            this.Button10.TabIndex = 8;
            this.Button10.Tag = "Edit information of current WinPaletter theme";
            this.Button10.UseVisualStyleBackColor = false;
            this.Button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // Button11
            // 
            this.Button11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button11.DrawOnGlass = false;
            this.Button11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button11.ForeColor = System.Drawing.Color.White;
            this.Button11.Image = ((System.Drawing.Image)(resources.GetObject("Button11.Image")));
            this.Button11.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(73)))), ((int)(((byte)(81)))));
            this.Button11.Location = new System.Drawing.Point(419, 3);
            this.Button11.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button11.Name = "Button11";
            this.Button11.Size = new System.Drawing.Size(40, 54);
            this.Button11.TabIndex = 9;
            this.Button11.Tag = "Settings";
            this.Button11.UseVisualStyleBackColor = false;
            this.Button11.Click += new System.EventHandler(this.Button11_Click);
            // 
            // SeparatorVertical1
            // 
            this.SeparatorVertical1.AlternativeLook = false;
            this.SeparatorVertical1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SeparatorVertical1.Location = new System.Drawing.Point(415, 5);
            this.SeparatorVertical1.Name = "SeparatorVertical1";
            this.SeparatorVertical1.Size = new System.Drawing.Size(1, 50);
            this.SeparatorVertical1.TabIndex = 7;
            this.SeparatorVertical1.TabStop = false;
            this.SeparatorVertical1.Text = "SeparatorVertical1";
            // 
            // status_lbl
            // 
            this.status_lbl.BackColor = System.Drawing.Color.Transparent;
            this.status_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.status_lbl.Location = new System.Drawing.Point(713, 5);
            this.status_lbl.Name = "status_lbl";
            this.status_lbl.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.status_lbl.Size = new System.Drawing.Size(295, 50);
            this.status_lbl.TabIndex = 19;
            this.status_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button7
            // 
            this.Button7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button7.DrawOnGlass = false;
            this.Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button7.ForeColor = System.Drawing.Color.White;
            this.Button7.Image = ((System.Drawing.Image)(resources.GetObject("Button7.Image")));
            this.Button7.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(113)))), ((int)(((byte)(132)))));
            this.Button7.Location = new System.Drawing.Point(126, 3);
            this.Button7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(40, 54);
            this.Button7.TabIndex = 3;
            this.Button7.Tag = "Save WinPaletter theme file";
            this.Button7.UseVisualStyleBackColor = false;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Button9
            // 
            this.Button9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button9.DrawOnGlass = false;
            this.Button9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button9.ForeColor = System.Drawing.Color.White;
            this.Button9.Image = ((System.Drawing.Image)(resources.GetObject("Button9.Image")));
            this.Button9.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(114)))), ((int)(((byte)(126)))));
            this.Button9.Location = new System.Drawing.Point(167, 3);
            this.Button9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(40, 54);
            this.Button9.TabIndex = 4;
            this.Button9.Tag = "Save WinPaletter theme file as ...";
            this.Button9.UseVisualStyleBackColor = false;
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button3.DrawOnGlass = false;
            this.Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Image = ((System.Drawing.Image)(resources.GetObject("Button3.Image")));
            this.Button3.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(145)))), ((int)(((byte)(136)))));
            this.Button3.Location = new System.Drawing.Point(3, 3);
            this.Button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(40, 54);
            this.Button3.TabIndex = 2;
            this.Button3.Tag = "Create new WinPaletter theme file based on the current applied preferences";
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button2.DrawOnGlass = false;
            this.Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Image = ((System.Drawing.Image)(resources.GetObject("Button2.Image")));
            this.Button2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(122)))), ((int)(((byte)(24)))));
            this.Button2.Location = new System.Drawing.Point(85, 3);
            this.Button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(40, 54);
            this.Button2.TabIndex = 2;
            this.Button2.Tag = "Open a WinPaletter theme file";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(35)))));
            this.GroupBox3.Controls.Add(this.Button26);
            this.GroupBox3.Controls.Add(this.Button35);
            this.GroupBox3.Controls.Add(this.Button34);
            this.GroupBox3.Controls.Add(this.Button33);
            this.GroupBox3.Controls.Add(this.Button32);
            this.GroupBox3.Controls.Add(this.Button29);
            this.GroupBox3.Controls.Add(this.Button27);
            this.GroupBox3.Controls.Add(this.Button24);
            this.GroupBox3.Controls.Add(this.Button21);
            this.GroupBox3.Controls.Add(this.Button16);
            this.GroupBox3.Controls.Add(this.Button4);
            this.GroupBox3.Location = new System.Drawing.Point(13, 624);
            this.GroupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(530, 66);
            this.GroupBox3.TabIndex = 28;
            // 
            // Button26
            // 
            this.Button26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button26.DrawOnGlass = false;
            this.Button26.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button26.ForeColor = System.Drawing.Color.White;
            this.Button26.Image = ((System.Drawing.Image)(resources.GetObject("Button26.Image")));
            this.Button26.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(106)))), ((int)(((byte)(113)))));
            this.Button26.Location = new System.Drawing.Point(77, 3);
            this.Button26.Name = "Button26";
            this.Button26.Size = new System.Drawing.Size(40, 60);
            this.Button26.TabIndex = 37;
            this.Button26.Tag = "WinPaletter application theme";
            this.Button26.UseVisualStyleBackColor = false;
            this.Button26.Click += new System.EventHandler(this.Button26_Click);
            // 
            // Button35
            // 
            this.Button35.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button35.DrawOnGlass = false;
            this.Button35.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button35.ForeColor = System.Drawing.Color.White;
            this.Button35.Image = ((System.Drawing.Image)(resources.GetObject("Button35.Image")));
            this.Button35.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(88)))), ((int)(((byte)(135)))));
            this.Button35.Location = new System.Drawing.Point(282, 3);
            this.Button35.Name = "Button35";
            this.Button35.Size = new System.Drawing.Size(40, 60);
            this.Button35.TabIndex = 36;
            this.Button35.Tag = "Wallpaper";
            this.Button35.UseVisualStyleBackColor = false;
            this.Button35.Click += new System.EventHandler(this.Button35_Click);
            // 
            // Button34
            // 
            this.Button34.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button34.DrawOnGlass = false;
            this.Button34.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button34.ForeColor = System.Drawing.Color.White;
            this.Button34.Image = ((System.Drawing.Image)(resources.GetObject("Button34.Image")));
            this.Button34.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(12)))), ((int)(((byte)(27)))));
            this.Button34.Location = new System.Drawing.Point(118, 3);
            this.Button34.Name = "Button34";
            this.Button34.Size = new System.Drawing.Size(40, 60);
            this.Button34.TabIndex = 35;
            this.Button34.Tag = "Sounds";
            this.Button34.UseVisualStyleBackColor = false;
            this.Button34.Click += new System.EventHandler(this.Button34_Click);
            // 
            // Button33
            // 
            this.Button33.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button33.DrawOnGlass = false;
            this.Button33.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button33.ForeColor = System.Drawing.Color.White;
            this.Button33.Image = ((System.Drawing.Image)(resources.GetObject("Button33.Image")));
            this.Button33.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(67)))));
            this.Button33.Location = new System.Drawing.Point(159, 3);
            this.Button33.Name = "Button33";
            this.Button33.Size = new System.Drawing.Size(40, 60);
            this.Button33.TabIndex = 34;
            this.Button33.Tag = "Screen Saver";
            this.Button33.UseVisualStyleBackColor = false;
            this.Button33.Click += new System.EventHandler(this.Button33_Click);
            // 
            // Button32
            // 
            this.Button32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button32.DrawOnGlass = false;
            this.Button32.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button32.ForeColor = System.Drawing.Color.White;
            this.Button32.Image = ((System.Drawing.Image)(resources.GetObject("Button32.Image")));
            this.Button32.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(77)))), ((int)(((byte)(82)))));
            this.Button32.Location = new System.Drawing.Point(200, 3);
            this.Button32.Name = "Button32";
            this.Button32.Size = new System.Drawing.Size(40, 60);
            this.Button32.TabIndex = 33;
            this.Button32.Tag = "Windows Switcher (Alt+Tab)";
            this.Button32.UseVisualStyleBackColor = false;
            this.Button32.Click += new System.EventHandler(this.Button32_Click);
            // 
            // Button29
            // 
            this.Button29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button29.DrawOnGlass = false;
            this.Button29.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button29.ForeColor = System.Drawing.Color.White;
            this.Button29.Image = ((System.Drawing.Image)(resources.GetObject("Button29.Image")));
            this.Button29.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(111)))));
            this.Button29.Location = new System.Drawing.Point(241, 3);
            this.Button29.Name = "Button29";
            this.Button29.Size = new System.Drawing.Size(40, 60);
            this.Button29.TabIndex = 32;
            this.Button29.Tag = "Windows Effects";
            this.Button29.UseVisualStyleBackColor = false;
            this.Button29.Click += new System.EventHandler(this.Button29_Click);
            // 
            // Button27
            // 
            this.Button27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button27.DrawOnGlass = false;
            this.Button27.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button27.ForeColor = System.Drawing.Color.White;
            this.Button27.Image = ((System.Drawing.Image)(resources.GetObject("Button27.Image")));
            this.Button27.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(107)))), ((int)(((byte)(1)))));
            this.Button27.Location = new System.Drawing.Point(323, 3);
            this.Button27.Name = "Button27";
            this.Button27.Size = new System.Drawing.Size(40, 60);
            this.Button27.TabIndex = 29;
            this.Button27.Tag = "Metrics and Fonts";
            this.Button27.UseVisualStyleBackColor = false;
            this.Button27.Click += new System.EventHandler(this.Button27_Click);
            // 
            // Button24
            // 
            this.Button24.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button24.DrawOnGlass = false;
            this.Button24.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button24.ForeColor = System.Drawing.Color.White;
            this.Button24.Image = ((System.Drawing.Image)(resources.GetObject("Button24.Image")));
            this.Button24.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.Button24.Location = new System.Drawing.Point(364, 3);
            this.Button24.Name = "Button24";
            this.Button24.Size = new System.Drawing.Size(40, 60);
            this.Button24.TabIndex = 28;
            this.Button24.Tag = "Terminals ...";
            this.Button24.UseVisualStyleBackColor = false;
            this.Button24.Click += new System.EventHandler(this.Button24_Click);
            // 
            // Button21
            // 
            this.Button21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button21.DrawOnGlass = false;
            this.Button21.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button21.ForeColor = System.Drawing.Color.White;
            this.Button21.Image = ((System.Drawing.Image)(resources.GetObject("Button21.Image")));
            this.Button21.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(42)))), ((int)(((byte)(56)))));
            this.Button21.Location = new System.Drawing.Point(405, 3);
            this.Button21.Name = "Button21";
            this.Button21.Size = new System.Drawing.Size(40, 60);
            this.Button21.TabIndex = 27;
            this.Button21.Tag = "Cursors";
            this.Button21.UseVisualStyleBackColor = false;
            this.Button21.Click += new System.EventHandler(this.Button21_Click);
            // 
            // Button16
            // 
            this.Button16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button16.DrawOnGlass = false;
            this.Button16.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button16.ForeColor = System.Drawing.Color.White;
            this.Button16.Image = ((System.Drawing.Image)(resources.GetObject("Button16.Image")));
            this.Button16.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(129)))), ((int)(((byte)(113)))));
            this.Button16.Location = new System.Drawing.Point(487, 3);
            this.Button16.Name = "Button16";
            this.Button16.Size = new System.Drawing.Size(40, 60);
            this.Button16.TabIndex = 26;
            this.Button16.Tag = "LogonUI";
            this.Button16.UseVisualStyleBackColor = false;
            this.Button16.Click += new System.EventHandler(this.Button16_Click);
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Button4.DrawOnGlass = false;
            this.Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.Image = ((System.Drawing.Image)(resources.GetObject("Button4.Image")));
            this.Button4.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(100)))), ((int)(((byte)(81)))));
            this.Button4.Location = new System.Drawing.Point(446, 3);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(40, 60);
            this.Button4.TabIndex = 25;
            this.Button4.Tag = "Classic Colors";
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click_1);
            // 
            // button41
            // 
            this.button41.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(35)))));
            this.button41.DrawOnGlass = false;
            this.button41.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button41.ForeColor = System.Drawing.Color.White;
            this.button41.Image = ((System.Drawing.Image)(resources.GetObject("button41.Image")));
            this.button41.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button41.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(52)))), ((int)(((byte)(84)))));
            this.button41.Location = new System.Drawing.Point(910, 490);
            this.button41.Name = "button41";
            this.button41.Size = new System.Drawing.Size(174, 38);
            this.button41.TabIndex = 35;
            this.button41.Text = "Support via PayPal";
            this.button41.UseVisualStyleBackColor = false;
            this.button41.Click += new System.EventHandler(this.button41_Click);
            // 
            // MainFrm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(1094, 701);
            this.Controls.Add(this.Button28);
            this.Controls.Add(this.previewContainer);
            this.Controls.Add(this.TablessControl1);
            this.Controls.Add(this.Button19);
            this.Controls.Add(this.apply_btn);
            this.Controls.Add(this.BetaBadge);
            this.Controls.Add(this.Button13);
            this.Controls.Add(this.MainToolbar);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.button41);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(1110, 728);
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinPaletter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrm_FormClosed);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.Shown += new System.EventHandler(this.MainFrm_Shown);
            this.ResizeBegin += new System.EventHandler(this.MainFrm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainFrm_ResizeEnd);
            this.previewContainer.ResumeLayout(false);
            this.tabs_preview.ResumeLayout(false);
            this.TabPage6.ResumeLayout(false);
            this.pnl_preview.ResumeLayout(false);
            this.Window1.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.TabPage7.ResumeLayout(false);
            this.pnl_preview_classic.ResumeLayout(false);
            this.ClassicTaskbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox21)).EndInit();
            this.TablessControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.PaletteContainer_W11.ResumeLayout(false);
            this.GroupBox13.ResumeLayout(false);
            this.GroupBox14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic9)).EndInit();
            this.pnl8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic8)).EndInit();
            this.pnl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic7)).EndInit();
            this.pnl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox10)).EndInit();
            this.pnl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic6)).EndInit();
            this.pnl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic1)).EndInit();
            this.pnl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic3)).EndInit();
            this.pnl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic2)).EndInit();
            this.pnl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W11_pic5)).EndInit();
            this.GroupBox5.ResumeLayout(false);
            this.GroupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox19)).EndInit();
            this.GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.GroupBox18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox18)).EndInit();
            this.GroupBox24.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox17)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox20.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.GroupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic9)).EndInit();
            this.GroupBox16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic8)).EndInit();
            this.GroupBox25.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic7)).EndInit();
            this.GroupBox27.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).EndInit();
            this.GroupBox28.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic6)).EndInit();
            this.GroupBox31.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic1)).EndInit();
            this.GroupBox34.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic3)).EndInit();
            this.GroupBox35.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic2)).EndInit();
            this.GroupBox36.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.W10_pic5)).EndInit();
            this.GroupBox37.ResumeLayout(false);
            this.GroupBox23.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox15)).EndInit();
            this.GroupBox38.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox16)).EndInit();
            this.GroupBox40.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox22)).EndInit();
            this.GroupBox42.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox26)).EndInit();
            this.GroupBox43.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox31)).EndInit();
            this.GroupBox44.ResumeLayout(false);
            this.GroupBox45.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox35)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox34)).EndInit();
            this.GroupBox46.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox33)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.PaletteContainer_W81.ResumeLayout(false);
            this.GroupBox17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox37)).EndInit();
            this.GroupBox32.ResumeLayout(false);
            this.GroupBox39.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox28)).EndInit();
            this.GroupBox41.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox29)).EndInit();
            this.GroupBox15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox9)).EndInit();
            this.GroupBox33.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox30)).EndInit();
            this.GroupBox29.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox32)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.PaletteContainer_W7.ResumeLayout(false);
            this.GroupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox13)).EndInit();
            this.GroupBox22.ResumeLayout(false);
            this.GroupBox19.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox39)).EndInit();
            this.GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox8)).EndInit();
            this.GroupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            this.GroupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            this.GroupBox30.ResumeLayout(false);
            this.GroupBox21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox12)).EndInit();
            this.GroupBox26.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox25)).EndInit();
            this.TabPage8.ResumeLayout(false);
            this.GroupBox50.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox45)).EndInit();
            this.GroupBox49.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox42)).EndInit();
            this.TabPage9.ResumeLayout(false);
            this.groupBox51.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox36)).EndInit();
            this.GroupBox48.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox38)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox41)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox40)).EndInit();
            this.GroupBox47.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            this.MainToolbar.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal UI.WP.TablessControl TablessControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal UI.WP.Button Button3;
        internal UI.WP.Button Button2;
        internal UI.WP.GroupBox GroupBox1;
        internal PictureBox PictureBox1;
        internal Label Label1;
        internal UI.WP.Toggle W11_ShowAccentOnTitlebarAndBorders_Toggle;
        internal UI.WP.GroupBox GroupBox20;
        internal PictureBox PictureBox11;
        internal Label Label11;
        internal UI.Controllers.ColorItem W11_InactiveTitlebar_pick;
        internal UI.WP.GroupBox GroupBox9;
        internal PictureBox PictureBox5;
        internal Label Label5;
        internal UI.Controllers.ColorItem W11_ActiveTitlebar_pick;
        internal UI.WP.GroupBox GroupBox13;
        internal PictureBox PictureBox10;
        internal Label Label10;
        internal UI.Controllers.ColorItem W11_Color_Index5;
        internal UI.Controllers.ColorItem W11_Color_Index1;
        internal UI.Controllers.ColorItem W11_TaskbarFrontAndFoldersOnStart_pick;
        internal UI.Controllers.ColorItem W11_Color_Index2;
        internal UI.Controllers.ColorItem W11_Color_Index4;
        internal UI.WP.GroupBox GroupBox6;
        internal PictureBox PictureBox19;
        internal Label Label19;
        internal UI.WP.GroupBox GroupBox5;
        internal UI.WP.Toggle W11_Transparency_Toggle;
        internal Label Label9;
        internal UI.WP.Toggle W11_AppMode_Toggle;
        internal Label Label7;
        internal UI.WP.Toggle W11_WinMode_Toggle;
        internal Label Label2;
        internal PictureBox PictureBox17;
        internal Label Label17;
        internal UI.WP.GroupBox GroupBox4;
        internal PictureBox PictureBox2;
        internal UI.WP.GroupBox GroupBox18;
        internal PictureBox PictureBox18;
        internal UI.WP.GroupBox GroupBox24;
        internal PictureBox PictureBox20;
        internal UI.WP.GroupBox previewContainer;
        internal PictureBox PictureBox21;
        internal Panel pnl_preview;
        internal UI.Simulation.WinElement taskbar;
        internal UI.Simulation.WinElement start;
        internal UI.Simulation.Window Window2;
        internal UI.Simulation.Window Window1;
        internal UI.Controllers.ColorItem W11_Color_Index3;
        internal UI.WP.Button apply_btn;
        internal UI.Controllers.ColorItem W11_Color_Index0;
        internal UI.Controllers.ColorItem W11_Color_Index6;
        internal UI.Simulation.WinElement ActionCenter;
        internal UI.WP.GroupBox pnl4;
        internal PictureBox W11_pic4;
        internal Label W11_lbl4;
        internal UI.WP.GroupBox pnl8;
        internal PictureBox W11_pic8;
        internal Label W11_lbl8;
        internal UI.WP.GroupBox pnl7;
        internal PictureBox W11_pic7;
        internal Label W11_lbl7;
        internal UI.WP.GroupBox pnl6;
        internal PictureBox W11_pic6;
        internal Label W11_lbl6;
        internal UI.WP.GroupBox pnl5;
        internal PictureBox W11_pic5;
        internal Label W11_lbl5;
        internal UI.WP.GroupBox pnl2;
        internal PictureBox W11_pic2;
        internal Label W11_lbl2;
        internal UI.WP.GroupBox pnl3;
        internal PictureBox W11_pic3;
        internal Label W11_lbl3;
        internal UI.WP.GroupBox pnl1;
        internal PictureBox W11_pic1;
        internal Label W11_lbl1;
        internal Panel PaletteContainer_W11;
        internal Panel Panel3;
        internal UI.WP.LabelAlt lnk_preview;
        internal UI.WP.LabelAlt Label8;
        internal UI.WP.LabelAlt setting_icon_preview;
        internal SaveFileDialog SaveFileDialog1;
        internal UI.WP.Button Button7;
        internal UI.WP.Button Button9;
        internal Label author_lbl;
        internal Label themename_lbl;
        internal UI.WP.SeparatorV SeparatorVertical1;
        internal UI.WP.Button Button12;
        internal UI.WP.Button Button5;
        internal UI.WP.Button Button6;
        internal UI.WP.Button Button10;
        internal UI.WP.Button Button11;
        internal UI.WP.Button Button4;
        internal UI.WP.Button Button13;
        internal UI.WP.Button Button15;
        internal UI.WP.GroupBox GroupBox3;
        internal UI.WP.Button Button16;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker1;
        internal UI.WP.Button Button1;
        internal SaveFileDialog SaveFileDialog2;
        internal UI.WP.Button W11_Button8;
        internal UI.WP.Button Button17;
        internal UI.WP.Button Button18;
        internal UI.WP.Button Button19;
        internal UI.WP.Button Button20;
        internal UI.WP.Button Button21;
        internal Panel PaletteContainer_W7;
        internal UI.WP.GroupBox GroupBox30;
        internal UI.WP.GroupBox GroupBox21;
        internal PictureBox PictureBox12;
        internal UI.Controllers.ColorItem W7_ColorizationColor_pick;
        internal Label Label16;
        internal UI.WP.GroupBox GroupBox26;
        internal UI.Controllers.ColorItem W7_ColorizationAfterglow_pick;
        internal PictureBox PictureBox14;
        internal Label Label21;
        internal PictureBox PictureBox25;
        internal Label Label27;
        internal UI.WP.GroupBox GroupBox7;
        internal UI.WP.Toggle W7_AlwaysHibernateThumbnails_Toggle;
        internal PictureBox PictureBox3;
        internal Label Aero_AlwaysHibernateThumbnails_lbl;
        internal UI.WP.GroupBox GroupBox10;
        internal UI.WP.Toggle W7_EnableAeroPeek_toggle;
        internal PictureBox PictureBox4;
        internal Label Aero_EnableAeroPeek_lbl;
        internal UI.WP.GroupBox GroupBox19;
        internal PictureBox PictureBox24;
        internal Label Label26;
        internal UI.WP.GroupBox GroupBox12;
        internal PictureBox PictureBox8;
        internal Label Label15;
        internal UI.WP.GroupBox GroupBox22;
        internal PictureBox PictureBox39;
        internal Label Label38;
        internal Label Label23;
        internal UI.WP.RadioImage W7_theme_aero;
        internal Label Label25;
        internal UI.WP.RadioImage W7_theme_aeroopaque;
        internal Label Label14;
        internal UI.WP.RadioImage W7_theme_basic;
        internal Label Label6;
        internal UI.WP.RadioImage W7_theme_classic;
        internal UI.WP.Trackbar W7_ColorizationColorBalance_bar;
        internal UI.WP.Trackbar W7_ColorizationAfterglowBalance_bar;
        internal UI.WP.Trackbar W7_ColorizationBlurBalance_bar;
        internal UI.WP.Trackbar W7_ColorizationGlassReflectionIntensity_bar;
        internal UI.WP.GroupBox GroupBox11;
        internal PictureBox PictureBox13;
        internal Label Label28;
        internal Panel PaletteContainer_W81;
        internal UI.WP.GroupBox GroupBox32;
        internal UI.WP.GroupBox GroupBox33;
        internal UI.WP.Trackbar W81_ColorizationBalance_bar;
        internal PictureBox PictureBox30;
        internal UI.Controllers.ColorItem W81_ColorizationColor_pick;
        internal Label Label39;
        internal PictureBox PictureBox32;
        internal Label Label41;
        internal UI.WP.GroupBox GroupBox15;
        internal PictureBox PictureBox9;
        internal UI.Controllers.ColorItem W81_start_pick;
        internal Label Label20;
        internal UI.WP.GroupBox GroupBox39;
        internal PictureBox PictureBox28;
        internal UI.Controllers.ColorItem W81_personalcolor_accent_pick;
        internal Label Foregrounds;
        internal UI.WP.GroupBox GroupBox41;
        internal PictureBox PictureBox29;
        internal UI.Controllers.ColorItem W81_personalcls_background_pick;
        internal Label Label33;
        internal UI.WP.GroupBox GroupBox29;
        internal PictureBox PictureBox23;
        internal UI.Controllers.ColorItem W81_accent_pick;
        internal Label Label29;
        internal UI.WP.RadioImage W81_theme_aerolite;
        internal UI.WP.RadioImage W81_theme_aero;
        internal UI.WP.GroupBox GroupBox17;
        internal PictureBox PictureBox37;
        internal Label Label40;
        internal Label Label32;
        internal Label Label31;
        internal UI.WP.Button W81_start;
        internal Label Label30;
        internal Label Label24;
        internal UI.WP.Button W81_logonui;
        internal UI.WP.SeparatorV SeparatorVertical2;
        internal UI.WP.Button Button23;
        internal UI.WP.Button Button24;
        internal UI.WP.RadioImage W11_Accent_Taskbar;
        internal UI.WP.RadioImage W11_Accent_StartTaskbar;
        internal UI.WP.RadioImage W11_Accent_None;
        internal UI.WP.GroupBox GroupBox14;
        internal Label Label42;
        internal PictureBox W11_pic9;
        internal Label W11_lbl9;
        internal UI.Controllers.ColorItem W11_Color_Index7;
        internal Label Label37;
        internal Label Label36;
        internal Label Label18;
        internal Label Label35;
        internal Label Label3;
        internal Label Label12;
        internal Label Label4;
        internal Label Label34;
        internal UI.WP.Button Button27;
        internal Panel Panel1;
        internal UI.WP.GroupBox GroupBox2;
        internal UI.WP.GroupBox GroupBox8;
        internal Label Label13;
        internal PictureBox W10_pic9;
        internal Label W10_lbl9;
        internal UI.Controllers.ColorItem W10_Color_Index7;
        internal UI.WP.GroupBox GroupBox16;
        internal Label Label43;
        internal PictureBox W10_pic8;
        internal Label W10_lbl8;
        internal UI.Controllers.ColorItem W10_Color_Index6;
        internal UI.WP.GroupBox GroupBox25;
        internal Label Label44;
        internal PictureBox W10_pic7;
        internal Label W10_lbl7;
        internal UI.Controllers.ColorItem W10_Color_Index5;
        internal UI.WP.GroupBox GroupBox27;
        internal Label Label45;
        internal PictureBox W10_pic4;
        internal Label W10_lbl4;
        internal UI.Controllers.ColorItem W10_Color_Index2;
        internal PictureBox PictureBox7;
        internal UI.WP.GroupBox GroupBox28;
        internal Label Label46;
        internal PictureBox W10_pic6;
        internal Label W10_lbl6;
        internal UI.Controllers.ColorItem W10_Color_Index4;
        internal UI.WP.GroupBox GroupBox31;
        internal Label Label47;
        internal PictureBox W10_pic1;
        internal UI.Controllers.ColorItem W10_TaskbarFrontAndFoldersOnStart_pick;
        internal Label W10_lbl1;
        internal UI.WP.GroupBox GroupBox34;
        internal Label Label48;
        internal PictureBox W10_pic3;
        internal UI.Controllers.ColorItem W10_Color_Index1;
        internal Label W10_lbl3;
        internal Label Label49;
        internal UI.WP.GroupBox GroupBox35;
        internal Label Label50;
        internal UI.Controllers.ColorItem W10_Color_Index0;
        internal PictureBox W10_pic2;
        internal Label W10_lbl2;
        internal UI.WP.GroupBox GroupBox36;
        internal Label Label51;
        internal PictureBox W10_pic5;
        internal Label W10_lbl5;
        internal UI.Controllers.ColorItem W10_Color_Index3;
        internal UI.WP.GroupBox GroupBox37;
        internal UI.WP.GroupBox GroupBox38;
        internal UI.WP.Button W10_Button25;
        internal UI.WP.RadioImage W10_Accent_Taskbar;
        internal UI.WP.RadioImage W10_Accent_StartTaskbar;
        internal UI.WP.RadioImage W10_Accent_None;
        internal PictureBox PictureBox16;
        internal Label Label52;
        internal UI.WP.GroupBox GroupBox40;
        internal PictureBox PictureBox22;
        internal Label Label53;
        internal UI.WP.Toggle W10_WinMode_Toggle;
        internal UI.WP.GroupBox GroupBox42;
        internal UI.WP.Toggle W10_Transparency_Toggle;
        internal PictureBox PictureBox26;
        internal Label Label54;
        internal UI.WP.GroupBox GroupBox43;
        internal PictureBox PictureBox27;
        internal UI.WP.Toggle W10_AppMode_Toggle;
        internal Label Label55;
        internal PictureBox PictureBox31;
        internal Label Label56;
        internal UI.WP.GroupBox GroupBox44;
        internal UI.WP.Button W10_Button8;
        internal UI.WP.Toggle W10_ShowAccentOnTitlebarAndBorders_Toggle;
        internal UI.WP.GroupBox GroupBox45;
        internal Label Label57;
        internal UI.Controllers.ColorItem W10_InactiveTitlebar_pick;
        internal PictureBox PictureBox34;
        internal Label Label58;
        internal UI.WP.GroupBox GroupBox46;
        internal Label Label59;
        internal UI.Controllers.ColorItem W10_ActiveTitlebar_pick;
        internal UI.WP.RadioImage Select_W7;
        internal UI.WP.RadioImage Select_W81;
        internal UI.WP.RadioImage Select_W10;
        internal UI.WP.RadioImage Select_W11;
        internal SaveFileDialog SaveFileDialog3;
        internal UI.WP.Button W81_ColorizationBalance_val;
        internal UI.WP.Button W7_ColorizationGlassReflectionIntensity_val;
        internal UI.WP.Button W7_ColorizationBlurBalance_val;
        internal UI.WP.Button W7_ColorizationColorBalance_val;
        internal UI.WP.Button W7_ColorizationAfterglowBalance_val;
        internal NotifyIcon NotifyUpdates;
        internal UI.Retro.PanelRaisedR ClassicTaskbar;
        internal UI.Retro.WindowR ClassicWindow2;
        internal UI.Retro.WindowR ClassicWindow1;
        internal UI.Retro.ButtonR ButtonR2;
        internal UI.Retro.ButtonR ButtonR4;
        internal UI.Retro.ButtonR ButtonR3;
        internal Panel pnl_preview_classic;
        internal UI.WP.TablessControl tabs_preview;
        internal TabPage TabPage6;
        internal TabPage TabPage7;
        internal UI.WP.Button Button28;
        internal UI.WP.Button Button29;
        internal UI.WP.RadioImage Select_WXP;
        internal UI.WP.RadioImage Select_WVista;
        internal TabPage TabPage8;
        internal TabPage TabPage9;
        internal UI.WP.GroupBox GroupBox47;
        internal Label Label68;
        internal UI.WP.RadioImage WXP_CustomTheme;
        internal UI.WP.RadioImage WXP_Classic;
        internal Label Label66;
        internal Label Label62;
        internal PictureBox PictureBox6;
        internal UI.WP.RadioImage WXP_Luna_Blue;
        internal Label Label63;
        internal Label Label64;
        internal UI.WP.RadioImage WXP_Luna_Silver;
        internal Label Label65;
        internal UI.WP.RadioImage WXP_Luna_OliveGreen;
        internal UI.WP.GroupBox GroupBox48;
        internal PictureBox PictureBox38;
        internal Label Label71;
        internal UI.WP.SeparatorV SeparatorVertical3;
        internal UI.WP.TextBox WXP_VS_textbox;
        internal PictureBox PictureBox40;
        internal Label Label67;
        internal UI.WP.ComboBox WXP_VS_ColorsList;
        internal PictureBox PictureBox41;
        internal Label Label69;
        internal UI.WP.Button WXP_VS_Browse;
        internal OpenFileDialog OpenFileDialog2;
        internal UI.WP.GroupBox GroupBox49;
        internal Label Label70;
        internal PictureBox PictureBox42;
        internal UI.WP.RadioImage WVista_theme_aero;
        internal Label Label72;
        internal UI.WP.RadioImage WVista_theme_classic;
        internal Label Label73;
        internal UI.WP.RadioImage WVista_theme_basic;
        internal Label Label74;
        internal UI.WP.RadioImage WVista_theme_aeroopaque;
        internal Label Label75;
        internal UI.WP.Button WVista_ColorizationColorBalance_val;
        internal UI.WP.Trackbar WVista_ColorizationColorBalance_bar;
        internal UI.Controllers.ColorItem WVista_ColorizationColor_pick;
        internal UI.WP.CheckBox WXP_VS_ReplaceColors;
        internal UI.WP.CheckBox WXP_VS_ReplaceFonts;
        internal UI.WP.CheckBox WXP_VS_ReplaceMetrics;
        internal UI.WP.GroupBox GroupBox50;
        internal PictureBox PictureBox45;
        internal Label Label80;
        internal UI.WP.AlertBox WXP_Alert2;
        internal UI.WP.AlertBox WXP_Alert1;
        internal UI.WP.Button Button32;
        internal UI.WP.GroupBox GroupBox23;
        internal UI.WP.Toggle W10_TBTransparency_Toggle;
        internal PictureBox PictureBox15;
        internal Label Label22;
        internal UI.WP.Toggle W10_TB_Blur;
        internal UI.WP.Button Button30;
        internal UI.WP.Button Button31;
        internal UI.WP.Button Button33;
        internal UI.WP.Button Button34;
        internal UI.WP.Button Button35;
        internal UI.WP.Button Button26;
        internal UI.WP.AlertBox BetaBadge;
        internal UI.WP.Button Button36;
        internal UI.WP.Button Button37;
        internal UI.WP.Button Button38;
        internal UI.WP.Button Button39;
        internal OpenFileDialog OpenFileDialog1;
        internal UI.WP.Button Button40;
        internal UI.WP.Button button41;
        internal UI.WP.Button button8;
        internal PictureBox pictureBox35;
        internal PictureBox pictureBox33;
        internal UI.WP.Button userButton;
        internal Label status_lbl;
        internal UI.WP.GroupBox MainToolbar;
        internal Label Label76;
        internal UI.WP.GroupBox groupBox51;
        internal PictureBox pictureBox36;
    }
}