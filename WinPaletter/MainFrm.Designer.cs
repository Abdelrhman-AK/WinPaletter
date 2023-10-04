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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            OpenFileDialog1 = new OpenFileDialog();
            SaveFileDialog1 = new SaveFileDialog();
            BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            BackgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(BackgroundWorker1_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);
            SaveFileDialog2 = new SaveFileDialog();
            Timer1 = new Timer(components);
            Timer1.Tick += new EventHandler(Timer1_Tick);
            SaveFileDialog3 = new SaveFileDialog();
            NotifyUpdates = new NotifyIcon(components);
            NotifyUpdates.BalloonTipClicked += new EventHandler(NotifyIcon1_BalloonTipClicked);
            OpenFileDialog2 = new OpenFileDialog();
            Button28 = new UI.WP.Button();
            Button28.Click += new EventHandler(Button28_Click);
            Button28.MouseEnter += new EventHandler(Button28_MouseEnter);
            Button28.MouseLeave += new EventHandler(Button28_MouseLeave);
            previewContainer = new UI.WP.GroupBox();
            Select_WXP = new UI.WP.RadioImage();
            Select_WXP.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Select_WXP_CheckedChanged);
            Select_WVista = new UI.WP.RadioImage();
            Select_WVista.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Select_WVista_CheckedChanged);
            tabs_preview = new UI.WP.TablessControl();
            TabPage6 = new TabPage();
            pnl_preview = new Panel();
            WXP_Alert2 = new UI.WP.AlertBox();
            ActionCenter = new UI.Simulation.WinElement();
            start = new UI.Simulation.WinElement();
            taskbar = new UI.Simulation.WinElement();
            Window2 = new UI.Simulation.Window();
            Window1 = new UI.Simulation.Window();
            Panel3 = new Panel();
            Label8 = new UI.WP.LabelAlt();
            setting_icon_preview = new UI.WP.LabelAlt();
            lnk_preview = new UI.WP.LabelAlt();
            TabPage7 = new TabPage();
            pnl_preview_classic = new Panel();
            ClassicWindow1 = new UI.Retro.WindowR();
            ClassicWindow2 = new UI.Retro.WindowR();
            ClassicTaskbar = new UI.Retro.PanelRaisedR();
            ButtonR4 = new UI.Retro.ButtonR();
            ButtonR3 = new UI.Retro.ButtonR();
            ButtonR2 = new UI.Retro.ButtonR();
            Select_W7 = new UI.WP.RadioImage();
            Select_W7.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Select_W7_CheckedChanged);
            Button23 = new UI.WP.Button();
            Button23.Click += new EventHandler(Button23_Click);
            Select_W81 = new UI.WP.RadioImage();
            Select_W81.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Select_W8_CheckedChanged);
            Button1 = new UI.WP.Button();
            Button1.Click += new EventHandler(Button1_Click_1);
            Select_W10 = new UI.WP.RadioImage();
            Select_W10.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Select_W10_CheckedChanged);
            Button15 = new UI.WP.Button();
            Button15.Click += new EventHandler(Button15_Click);
            Select_W11 = new UI.WP.RadioImage();
            Select_W11.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(Select_W11_CheckedChanged);
            PictureBox21 = new PictureBox();
            themename_lbl = new Label();
            themename_lbl.DoubleClick += new EventHandler(Button10_Click);
            author_lbl = new Label();
            author_lbl.DoubleClick += new EventHandler(Button10_Click);
            TablessControl1 = new UI.WP.TablessControl();
            TabPage1 = new TabPage();
            PaletteContainer_W11 = new Panel();
            GroupBox13 = new UI.WP.GroupBox();
            Button37 = new UI.WP.Button();
            Button37.Click += new EventHandler(Button37_Click);
            Button30 = new UI.WP.Button();
            Button30.Click += new EventHandler(Button30_Click_1);
            GroupBox14 = new UI.WP.GroupBox();
            Label42 = new Label();
            W11_pic9 = new PictureBox();
            W11_lbl9 = new Label();
            W11_Color_Index7 = new UI.Controllers.ColorItem();
            W11_Color_Index7.DragDrop += new DragEventHandler(W11_pick_DragDrop);
            W11_Color_Index7.Click += new EventHandler(W11_Color_Index7_Click);
            pnl8 = new UI.WP.GroupBox();
            Label37 = new Label();
            W11_pic8 = new PictureBox();
            W11_lbl8 = new Label();
            W11_Color_Index6 = new UI.Controllers.ColorItem();
            W11_Color_Index6.DragDrop += new DragEventHandler(W11_pick_DragDrop);
            W11_Color_Index6.Click += new EventHandler(W11_Color_Index6_Click);
            pnl7 = new UI.WP.GroupBox();
            Label36 = new Label();
            W11_pic7 = new PictureBox();
            W11_lbl7 = new Label();
            W11_Color_Index5 = new UI.Controllers.ColorItem();
            W11_Color_Index5.DragDrop += new DragEventHandler(W11_pick_DragDrop);
            W11_Color_Index5.Click += new EventHandler(W11_Color_Index5_Click);
            pnl4 = new UI.WP.GroupBox();
            Label18 = new Label();
            W11_pic4 = new PictureBox();
            W11_lbl4 = new Label();
            W11_Color_Index2 = new UI.Controllers.ColorItem();
            W11_Color_Index2.DragDrop += new DragEventHandler(W11_pick_DragDrop);
            W11_Color_Index2.Click += new EventHandler(W11_Color_Index2_Click);
            PictureBox10 = new PictureBox();
            pnl6 = new UI.WP.GroupBox();
            Label35 = new Label();
            W11_pic6 = new PictureBox();
            W11_lbl6 = new Label();
            W11_Color_Index4 = new UI.Controllers.ColorItem();
            W11_Color_Index4.DragDrop += new DragEventHandler(W11_pick_DragDrop);
            W11_Color_Index4.Click += new EventHandler(W11_Color_Index4_pick_Click);
            pnl1 = new UI.WP.GroupBox();
            Label3 = new Label();
            W11_pic1 = new PictureBox();
            W11_TaskbarFrontAndFoldersOnStart_pick = new UI.Controllers.ColorItem();
            W11_TaskbarFrontAndFoldersOnStart_pick.DragDrop += new DragEventHandler(W11_pick_DragDrop);
            W11_TaskbarFrontAndFoldersOnStart_pick.Click += new EventHandler(W11_TaskbarFrontAndFoldersOnStart_pick_Click);
            W11_lbl1 = new Label();
            pnl3 = new UI.WP.GroupBox();
            Label12 = new Label();
            W11_pic3 = new PictureBox();
            W11_Color_Index1 = new UI.Controllers.ColorItem();
            W11_Color_Index1.DragDrop += new DragEventHandler(W11_pick_DragDrop);
            W11_Color_Index1.Click += new EventHandler(W11_Color_Index1_Click);
            W11_lbl3 = new Label();
            Label10 = new Label();
            pnl2 = new UI.WP.GroupBox();
            Label4 = new Label();
            W11_Color_Index0 = new UI.Controllers.ColorItem();
            W11_Color_Index0.DragDrop += new DragEventHandler(W11_pick_DragDrop);
            W11_Color_Index0.Click += new EventHandler(W11_Color_Index0_Click);
            W11_pic2 = new PictureBox();
            W11_lbl2 = new Label();
            pnl5 = new UI.WP.GroupBox();
            Label34 = new Label();
            W11_pic5 = new PictureBox();
            W11_lbl5 = new Label();
            W11_Color_Index3 = new UI.Controllers.ColorItem();
            W11_Color_Index3.DragDrop += new DragEventHandler(W11_pick_DragDrop);
            W11_Color_Index3.Click += new EventHandler(W11_Color_Index3_Click);
            GroupBox5 = new UI.WP.GroupBox();
            GroupBox6 = new UI.WP.GroupBox();
            W11_Accent_Taskbar = new UI.WP.RadioImage();
            W11_Accent_Taskbar.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(W11_Accent_Taskbar_CheckedChanged);
            W11_Accent_StartTaskbar = new UI.WP.RadioImage();
            W11_Accent_StartTaskbar.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(W11_Accent_StartTaskbar_CheckedChanged);
            W11_Accent_None = new UI.WP.RadioImage();
            W11_Accent_None.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(W11_Accent_None_CheckedChanged);
            PictureBox19 = new PictureBox();
            Label19 = new Label();
            GroupBox4 = new UI.WP.GroupBox();
            PictureBox2 = new PictureBox();
            Label2 = new Label();
            W11_WinMode_Toggle = new UI.WP.Toggle();
            W11_WinMode_Toggle.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(W11_WinMode_Toggle_CheckedChanged);
            GroupBox18 = new UI.WP.GroupBox();
            W11_Transparency_Toggle = new UI.WP.Toggle();
            W11_Transparency_Toggle.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(W11_Transparency_Toggle_CheckedChanged);
            PictureBox18 = new PictureBox();
            Label9 = new Label();
            GroupBox24 = new UI.WP.GroupBox();
            PictureBox20 = new PictureBox();
            W11_AppMode_Toggle = new UI.WP.Toggle();
            W11_AppMode_Toggle.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(W11_AppMode_Toggle_CheckedChanged);
            Label7 = new Label();
            PictureBox17 = new PictureBox();
            Label17 = new Label();
            GroupBox1 = new UI.WP.GroupBox();
            W11_Button8 = new UI.WP.Button();
            W11_Button8.Click += new EventHandler(W11_Button8_Click_1);
            W11_ShowAccentOnTitlebarAndBorders_Toggle = new UI.WP.Toggle();
            W11_ShowAccentOnTitlebarAndBorders_Toggle.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(W11_ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged);
            GroupBox20 = new UI.WP.GroupBox();
            PictureBox11 = new PictureBox();
            Label11 = new Label();
            W11_InactiveTitlebar_pick = new UI.Controllers.ColorItem();
            W11_InactiveTitlebar_pick.DragDrop += new DragEventHandler(W11_pick_DragDrop);
            W11_InactiveTitlebar_pick.Click += new EventHandler(W11_InactiveTitlebar_pick_Click);
            PictureBox1 = new PictureBox();
            Label1 = new Label();
            GroupBox9 = new UI.WP.GroupBox();
            PictureBox5 = new PictureBox();
            Label5 = new Label();
            W11_ActiveTitlebar_pick = new UI.Controllers.ColorItem();
            W11_ActiveTitlebar_pick.DragDrop += new DragEventHandler(W11_pick_DragDrop);
            W11_ActiveTitlebar_pick.Click += new EventHandler(W11_ActiveTitlebar_pick_Click);
            TabPage2 = new TabPage();
            Panel1 = new Panel();
            GroupBox2 = new UI.WP.GroupBox();
            Button38 = new UI.WP.Button();
            Button38.Click += new EventHandler(Button38_Click);
            GroupBox8 = new UI.WP.GroupBox();
            Label13 = new Label();
            W10_pic9 = new PictureBox();
            W10_lbl9 = new Label();
            W10_Color_Index7 = new UI.Controllers.ColorItem();
            W10_Color_Index7.DragDrop += new DragEventHandler(W10_pick_DragDrop);
            W10_Color_Index7.Click += new EventHandler(W10_Color_Index7_Click);
            GroupBox16 = new UI.WP.GroupBox();
            Label43 = new Label();
            W10_pic8 = new PictureBox();
            W10_lbl8 = new Label();
            W10_Color_Index6 = new UI.Controllers.ColorItem();
            W10_Color_Index6.DragDrop += new DragEventHandler(W10_pick_DragDrop);
            W10_Color_Index6.Click += new EventHandler(W10_Color_Index6_Click);
            GroupBox25 = new UI.WP.GroupBox();
            Label44 = new Label();
            W10_pic7 = new PictureBox();
            W10_lbl7 = new Label();
            W10_Color_Index5 = new UI.Controllers.ColorItem();
            W10_Color_Index5.DragDrop += new DragEventHandler(W10_pick_DragDrop);
            W10_Color_Index5.Click += new EventHandler(W10_Color_Index5_Click);
            GroupBox27 = new UI.WP.GroupBox();
            Label45 = new Label();
            W10_pic4 = new PictureBox();
            W10_lbl4 = new Label();
            W10_Color_Index2 = new UI.Controllers.ColorItem();
            W10_Color_Index2.DragDrop += new DragEventHandler(W10_pick_DragDrop);
            W10_Color_Index2.Click += new EventHandler(W10_Color_Index2_Click);
            PictureBox7 = new PictureBox();
            GroupBox28 = new UI.WP.GroupBox();
            Label46 = new Label();
            W10_pic6 = new PictureBox();
            W10_lbl6 = new Label();
            W10_Color_Index4 = new UI.Controllers.ColorItem();
            W10_Color_Index4.DragDrop += new DragEventHandler(W10_pick_DragDrop);
            W10_Color_Index4.Click += new EventHandler(W10_Color_Index4_pick_Click);
            GroupBox31 = new UI.WP.GroupBox();
            Label47 = new Label();
            W10_pic1 = new PictureBox();
            W10_TaskbarFrontAndFoldersOnStart_pick = new UI.Controllers.ColorItem();
            W10_TaskbarFrontAndFoldersOnStart_pick.DragDrop += new DragEventHandler(W10_pick_DragDrop);
            W10_TaskbarFrontAndFoldersOnStart_pick.Click += new EventHandler(W10_TaskbarFrontAndFoldersOnStart_pick_Click);
            W10_lbl1 = new Label();
            GroupBox34 = new UI.WP.GroupBox();
            Label48 = new Label();
            W10_pic3 = new PictureBox();
            W10_Color_Index1 = new UI.Controllers.ColorItem();
            W10_Color_Index1.DragDrop += new DragEventHandler(W10_pick_DragDrop);
            W10_Color_Index1.Click += new EventHandler(W10_Color_Index1_Click);
            W10_lbl3 = new Label();
            Label49 = new Label();
            GroupBox35 = new UI.WP.GroupBox();
            Label50 = new Label();
            W10_Color_Index0 = new UI.Controllers.ColorItem();
            W10_Color_Index0.DragDrop += new DragEventHandler(W10_pick_DragDrop);
            W10_Color_Index0.Click += new EventHandler(W10_Color_Index0_Click);
            W10_pic2 = new PictureBox();
            W10_lbl2 = new Label();
            GroupBox36 = new UI.WP.GroupBox();
            Label51 = new Label();
            W10_pic5 = new PictureBox();
            W10_lbl5 = new Label();
            W10_Color_Index3 = new UI.Controllers.ColorItem();
            W10_Color_Index3.DragDrop += new DragEventHandler(W10_pick_DragDrop);
            W10_Color_Index3.Click += new EventHandler(W10_Color_Index3_Click);
            GroupBox37 = new UI.WP.GroupBox();
            GroupBox23 = new UI.WP.GroupBox();
            W10_TBTransparency_Toggle = new UI.WP.Toggle();
            W10_TBTransparency_Toggle.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(W10_TBTransparency_Toggle_CheckedChanged);
            PictureBox15 = new PictureBox();
            Label22 = new Label();
            GroupBox38 = new UI.WP.GroupBox();
            W10_Button25 = new UI.WP.Button();
            W10_Button25.Click += new EventHandler(W10_Button25_Click);
            W10_Accent_Taskbar = new UI.WP.RadioImage();
            W10_Accent_Taskbar.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(W10_Accent_Taskbar_CheckedChanged);
            W10_Accent_StartTaskbar = new UI.WP.RadioImage();
            W10_Accent_StartTaskbar.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(W10_Accent_StartTaskbar_CheckedChanged);
            W10_Accent_None = new UI.WP.RadioImage();
            W10_Accent_None.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(W10_Accent_None_CheckedChanged);
            PictureBox16 = new PictureBox();
            Label52 = new Label();
            GroupBox40 = new UI.WP.GroupBox();
            PictureBox22 = new PictureBox();
            Label53 = new Label();
            W10_WinMode_Toggle = new UI.WP.Toggle();
            W10_WinMode_Toggle.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(W10_WinMode_Toggle_CheckedChanged);
            GroupBox42 = new UI.WP.GroupBox();
            W10_TB_Blur = new UI.WP.Toggle();
            W10_TB_Blur.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(W10_TB_Blur_CheckedChanged);
            W10_Transparency_Toggle = new UI.WP.Toggle();
            W10_Transparency_Toggle.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(W10_Transparency_Toggle_CheckedChanged);
            PictureBox26 = new PictureBox();
            Label54 = new Label();
            GroupBox43 = new UI.WP.GroupBox();
            PictureBox27 = new PictureBox();
            W10_AppMode_Toggle = new UI.WP.Toggle();
            W10_AppMode_Toggle.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(W10_AppMode_Toggle_CheckedChanged);
            Label55 = new Label();
            PictureBox31 = new PictureBox();
            Label56 = new Label();
            GroupBox44 = new UI.WP.GroupBox();
            W10_Button8 = new UI.WP.Button();
            W10_Button8.Click += new EventHandler(W10_Button8_Click_1);
            W10_ShowAccentOnTitlebarAndBorders_Toggle = new UI.WP.Toggle();
            W10_ShowAccentOnTitlebarAndBorders_Toggle.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(W10_ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged);
            GroupBox45 = new UI.WP.GroupBox();
            PictureBox33 = new PictureBox();
            Label57 = new Label();
            W10_InactiveTitlebar_pick = new UI.Controllers.ColorItem();
            W10_InactiveTitlebar_pick.DragDrop += new DragEventHandler(W10_pick_DragDrop);
            W10_InactiveTitlebar_pick.Click += new EventHandler(W10_InactiveTitlebar_pick_Click);
            PictureBox34 = new PictureBox();
            Label58 = new Label();
            GroupBox46 = new UI.WP.GroupBox();
            PictureBox35 = new PictureBox();
            Label59 = new Label();
            W10_ActiveTitlebar_pick = new UI.Controllers.ColorItem();
            W10_ActiveTitlebar_pick.DragDrop += new DragEventHandler(W10_pick_DragDrop);
            W10_ActiveTitlebar_pick.Click += new EventHandler(W10_ActiveTitlebar_pick_Click);
            TabPage3 = new TabPage();
            PaletteContainer_W81 = new Panel();
            GroupBox17 = new UI.WP.GroupBox();
            SeparatorVertical2 = new UI.WP.SeparatorV();
            Label32 = new Label();
            Label31 = new Label();
            W81_start = new UI.WP.Button();
            W81_start.Click += new EventHandler(W8_start_Click);
            Label30 = new Label();
            Label24 = new Label();
            W81_theme_aerolite = new UI.WP.RadioImage();
            W81_theme_aerolite.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(W8_theme_aerolite_CheckedChanged);
            PictureBox37 = new PictureBox();
            Label40 = new Label();
            W81_theme_aero = new UI.WP.RadioImage();
            W81_theme_aero.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(W8_theme_aero_CheckedChanged);
            W81_logonui = new UI.WP.Button();
            W81_logonui.Click += new EventHandler(W8_logonui_Click);
            GroupBox32 = new UI.WP.GroupBox();
            GroupBox39 = new UI.WP.GroupBox();
            PictureBox28 = new PictureBox();
            W81_personalcolor_accent_pick = new UI.Controllers.ColorItem();
            W81_personalcolor_accent_pick.DragDrop += new DragEventHandler(W81_pick_DragDrop);
            W81_personalcolor_accent_pick.Click += new EventHandler(W8_personalcolor_accent_pick_Click);
            Foregrounds = new Label();
            GroupBox41 = new UI.WP.GroupBox();
            PictureBox29 = new PictureBox();
            W81_personalcls_background_pick = new UI.Controllers.ColorItem();
            W81_personalcls_background_pick.DragDrop += new DragEventHandler(W81_pick_DragDrop);
            W81_personalcls_background_pick.Click += new EventHandler(W8_personalcls_background_pick_Click);
            Label33 = new Label();
            GroupBox15 = new UI.WP.GroupBox();
            PictureBox9 = new PictureBox();
            W81_start_pick = new UI.Controllers.ColorItem();
            W81_start_pick.DragDrop += new DragEventHandler(W81_pick_DragDrop);
            W81_start_pick.Click += new EventHandler(W8_start_pick_Click);
            Label20 = new Label();
            GroupBox33 = new UI.WP.GroupBox();
            W81_ColorizationBalance_val = new UI.WP.Button();
            W81_ColorizationBalance_val.Click += new EventHandler(W8_ColorizationBalance_val_Click);
            W81_ColorizationBalance_bar = new UI.WP.Trackbar();
            W81_ColorizationBalance_bar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(W8_ColorizationBalance_bar_Scroll);
            PictureBox30 = new PictureBox();
            W81_ColorizationColor_pick = new UI.Controllers.ColorItem();
            W81_ColorizationColor_pick.DragDrop += new DragEventHandler(W81_pick_DragDrop);
            W81_ColorizationColor_pick.Click += new EventHandler(W8_ColorizationColor_pick_Click);
            Label39 = new Label();
            GroupBox29 = new UI.WP.GroupBox();
            PictureBox23 = new PictureBox();
            W81_accent_pick = new UI.Controllers.ColorItem();
            W81_accent_pick.DragDrop += new DragEventHandler(W81_pick_DragDrop);
            W81_accent_pick.Click += new EventHandler(W8_accent_pick_Click);
            Label29 = new Label();
            PictureBox32 = new PictureBox();
            Label41 = new Label();
            TabPage4 = new TabPage();
            PaletteContainer_W7 = new Panel();
            GroupBox11 = new UI.WP.GroupBox();
            Label23 = new Label();
            PictureBox13 = new PictureBox();
            W7_theme_aero = new UI.WP.RadioImage();
            W7_theme_aero.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(W7_theme_Aero_CheckedChanged);
            Label28 = new Label();
            W7_theme_classic = new UI.WP.RadioImage();
            W7_theme_classic.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(W7_theme_classic_CheckedChanged);
            Label25 = new Label();
            W7_theme_basic = new UI.WP.RadioImage();
            W7_theme_basic.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(W7_theme_basic_CheckedChanged);
            Label14 = new Label();
            W7_theme_aeroopaque = new UI.WP.RadioImage();
            W7_theme_aeroopaque.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(W7_theme_aeroopaque_CheckedChanged);
            Label6 = new Label();
            GroupBox22 = new UI.WP.GroupBox();
            GroupBox19 = new UI.WP.GroupBox();
            W7_ColorizationGlassReflectionIntensity_val = new UI.WP.Button();
            W7_ColorizationGlassReflectionIntensity_val.Click += new EventHandler(W7_ColorizationGlassReflectionIntensity_val_Click);
            W7_ColorizationGlassReflectionIntensity_bar = new UI.WP.Trackbar();
            W7_ColorizationGlassReflectionIntensity_bar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(W7_ColorizationGlassReflectionIntensity_bar_Scroll);
            PictureBox24 = new PictureBox();
            Label26 = new Label();
            PictureBox39 = new PictureBox();
            GroupBox12 = new UI.WP.GroupBox();
            W7_ColorizationBlurBalance_val = new UI.WP.Button();
            W7_ColorizationBlurBalance_val.Click += new EventHandler(W7_ColorizationBlurBalance_val_Click);
            W7_ColorizationBlurBalance_bar = new UI.WP.Trackbar();
            W7_ColorizationBlurBalance_bar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(W7_ColorizationBlurBalance_bar_Scroll);
            PictureBox8 = new PictureBox();
            Label15 = new Label();
            Label38 = new Label();
            GroupBox10 = new UI.WP.GroupBox();
            W7_EnableAeroPeek_toggle = new UI.WP.Toggle();
            W7_EnableAeroPeek_toggle.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(W7_EnableAeroPeek_toggle_CheckedChanged);
            PictureBox4 = new PictureBox();
            Aero_EnableAeroPeek_lbl = new Label();
            GroupBox7 = new UI.WP.GroupBox();
            W7_AlwaysHibernateThumbnails_Toggle = new UI.WP.Toggle();
            W7_AlwaysHibernateThumbnails_Toggle.CheckedChanged += new UI.WP.Toggle.CheckedChangedEventHandler(W7_AlwaysHibernateThumbnails_Toggle_CheckedChanged);
            PictureBox3 = new PictureBox();
            Aero_AlwaysHibernateThumbnails_lbl = new Label();
            GroupBox30 = new UI.WP.GroupBox();
            GroupBox21 = new UI.WP.GroupBox();
            W7_ColorizationColorBalance_val = new UI.WP.Button();
            W7_ColorizationColorBalance_val.Click += new EventHandler(W7_ColorizationColorBalance_val_Click);
            W7_ColorizationColorBalance_bar = new UI.WP.Trackbar();
            W7_ColorizationColorBalance_bar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(W7_ColorizationColorBalance_bar_Scroll);
            PictureBox12 = new PictureBox();
            W7_ColorizationColor_pick = new UI.Controllers.ColorItem();
            W7_ColorizationColor_pick.DragDrop += new DragEventHandler(W7_pick_DragDrop);
            W7_ColorizationColor_pick.Click += new EventHandler(W7_ColorizationColor_pick_Click);
            Label16 = new Label();
            GroupBox26 = new UI.WP.GroupBox();
            W7_ColorizationAfterglowBalance_val = new UI.WP.Button();
            W7_ColorizationAfterglowBalance_val.Click += new EventHandler(W7_ColorizationAfterglowBalance_val_Click);
            W7_ColorizationAfterglowBalance_bar = new UI.WP.Trackbar();
            W7_ColorizationAfterglowBalance_bar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(W7_ColorizationAfterglowBalance_bar_Scroll);
            W7_ColorizationAfterglow_pick = new UI.Controllers.ColorItem();
            W7_ColorizationAfterglow_pick.DragDrop += new DragEventHandler(W7_pick_DragDrop);
            W7_ColorizationAfterglow_pick.Click += new EventHandler(W7_ColorizationAfterglow_pick_Click);
            PictureBox14 = new PictureBox();
            Label21 = new Label();
            PictureBox25 = new PictureBox();
            Label27 = new Label();
            TabPage8 = new TabPage();
            GroupBox50 = new UI.WP.GroupBox();
            WVista_ColorizationColorBalance_val = new UI.WP.Button();
            WVista_ColorizationColorBalance_val.Click += new EventHandler(WVista_ColorizationColorBalance_val_Click);
            PictureBox45 = new PictureBox();
            WVista_ColorizationColorBalance_bar = new UI.WP.Trackbar();
            WVista_ColorizationColorBalance_bar.Scroll += new UI.WP.Trackbar.ScrollEventHandler(WVista_ColorizationColorBalance_bar_Scroll);
            WVista_ColorizationColor_pick = new UI.Controllers.ColorItem();
            WVista_ColorizationColor_pick.DragDrop += new DragEventHandler(WVista_pick_DragDrop);
            WVista_ColorizationColor_pick.Click += new EventHandler(WVista_ColorizationColor_pick_Click);
            Label80 = new Label();
            GroupBox49 = new UI.WP.GroupBox();
            Label70 = new Label();
            PictureBox42 = new PictureBox();
            WVista_theme_aero = new UI.WP.RadioImage();
            WVista_theme_aero.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(WVista_theme_Vista_CheckedChanged);
            Label72 = new Label();
            WVista_theme_classic = new UI.WP.RadioImage();
            WVista_theme_classic.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(WVista_theme_classic_CheckedChanged);
            Label73 = new Label();
            WVista_theme_basic = new UI.WP.RadioImage();
            WVista_theme_basic.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(WVista_theme_basic_CheckedChanged);
            Label74 = new Label();
            WVista_theme_aeroopaque = new UI.WP.RadioImage();
            WVista_theme_aeroopaque.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(WVista_theme_aeroopaque_CheckedChanged);
            Label75 = new Label();
            TabPage9 = new TabPage();
            WXP_Alert1 = new UI.WP.AlertBox();
            Label76 = new Label();
            WXP_VS_ReplaceFonts = new UI.WP.CheckBox();
            WXP_VS_ReplaceMetrics = new UI.WP.CheckBox();
            WXP_VS_ReplaceColors = new UI.WP.CheckBox();
            WXP_Alert3 = new UI.WP.AlertBox();
            GroupBox48 = new UI.WP.GroupBox();
            WXP_VS_ColorsList = new UI.WP.ComboBox();
            WXP_VS_ColorsList.SelectedIndexChanged += new EventHandler(ComboBox1_SelectedIndexChanged);
            PictureBox38 = new PictureBox();
            WXP_VS_Browse = new UI.WP.Button();
            WXP_VS_Browse.Click += new EventHandler(Button30_Click);
            Label71 = new Label();
            PictureBox41 = new PictureBox();
            PictureBox40 = new PictureBox();
            Label69 = new Label();
            Label67 = new Label();
            WXP_VS_textbox = new UI.WP.TextBox();
            WXP_VS_textbox.TextChanged += new EventHandler(TextBox1_TextChanged);
            GroupBox47 = new UI.WP.GroupBox();
            SeparatorVertical3 = new UI.WP.SeparatorV();
            Label68 = new Label();
            WXP_CustomTheme = new UI.WP.RadioImage();
            WXP_CustomTheme.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(WXP_CustomTheme_CheckedChanged);
            WXP_Classic = new UI.WP.RadioImage();
            WXP_Classic.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(WXP_Classic_CheckedChanged);
            Label66 = new Label();
            Label62 = new Label();
            PictureBox6 = new PictureBox();
            WXP_Luna_Blue = new UI.WP.RadioImage();
            WXP_Luna_Blue.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(WXP_Luna_Blue_CheckedChanged);
            Label63 = new Label();
            Label64 = new Label();
            WXP_Luna_Silver = new UI.WP.RadioImage();
            WXP_Luna_Silver.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(WXP_Luna_Silver_CheckedChanged);
            Label65 = new Label();
            WXP_Luna_OliveGreen = new UI.WP.RadioImage();
            WXP_Luna_OliveGreen.CheckedChanged += new UI.WP.RadioImage.CheckedChangedEventHandler(WXP_Luna_OliveGreen_CheckedChanged);
            TabPage5 = new TabPage();
            Button25 = new UI.WP.Button();
            Button25.Click += new EventHandler(Button25_Click);
            Button22 = new UI.WP.Button();
            Button22.Click += new EventHandler(Button22_Click);
            log_lbl = new Label();
            Button14 = new UI.WP.Button();
            Button14.Click += new EventHandler(Button14_Click);
            Button8 = new UI.WP.Button();
            Button8.Click += new EventHandler(Button8_Click);
            TreeView1 = new TreeView();
            Separator1 = new UI.WP.SeparatorH();
            Label60 = new Label();
            PictureBox36 = new PictureBox();
            Button19 = new UI.WP.Button();
            Button19.MouseEnter += new EventHandler(Button19_MouseEnter);
            Button19.MouseLeave += new EventHandler(Button19_MouseLeave);
            Button19.Click += new EventHandler(Button19_Click);
            apply_btn = new UI.WP.Button();
            apply_btn.Click += new EventHandler(Button4_Click);
            apply_btn.MouseEnter += new EventHandler(Apply_btn_MouseEnter);
            apply_btn.MouseLeave += new EventHandler(Apply_btn_MouseLeave);
            Button13 = new UI.WP.Button();
            Button13.Click += new EventHandler(Button13_Click);
            MainToolbar = new UI.WP.GroupBox();
            Button40 = new UI.WP.Button();
            Button40.Click += new EventHandler(Button40_Click);
            Button39 = new UI.WP.Button();
            Button39.Click += new EventHandler(Button39_Click);
            Button36 = new UI.WP.Button();
            Button36.Click += new EventHandler(Button36_Click);
            BetaBadge = new UI.WP.AlertBox();
            Button31 = new UI.WP.Button();
            Button31.Click += new EventHandler(Button31_Click);
            Button20 = new UI.WP.Button();
            Button20.Click += new EventHandler(Button20_Click);
            Button18 = new UI.WP.Button();
            Button18.Click += new EventHandler(Button18_Click);
            Button17 = new UI.WP.Button();
            Button17.Click += new EventHandler(Button17_Click);
            Button12 = new UI.WP.Button();
            Button12.Click += new EventHandler(Button12_Click);
            Button5 = new UI.WP.Button();
            Button5.Click += new EventHandler(Button5_Click);
            Button6 = new UI.WP.Button();
            Button6.Click += new EventHandler(Button6_Click);
            Button10 = new UI.WP.Button();
            Button10.Click += new EventHandler(Button10_Click);
            Button11 = new UI.WP.Button();
            Button11.Click += new EventHandler(Button11_Click);
            SeparatorVertical1 = new UI.WP.SeparatorV();
            status_lbl = new Label();
            Button7 = new UI.WP.Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button9 = new UI.WP.Button();
            Button9.Click += new EventHandler(Button9_Click);
            Button3 = new UI.WP.Button();
            Button3.Click += new EventHandler(Button3_Click);
            Button2 = new UI.WP.Button();
            Button2.Click += new EventHandler(Button2_Click);
            GroupBox3 = new UI.WP.GroupBox();
            Button26 = new UI.WP.Button();
            Button26.Click += new EventHandler(Button26_Click);
            Button35 = new UI.WP.Button();
            Button35.Click += new EventHandler(Button35_Click);
            Button34 = new UI.WP.Button();
            Button34.Click += new EventHandler(Button34_Click);
            Button33 = new UI.WP.Button();
            Button33.Click += new EventHandler(Button33_Click);
            Button32 = new UI.WP.Button();
            Button32.Click += new EventHandler(Button32_Click);
            Button29 = new UI.WP.Button();
            Button29.Click += new EventHandler(Button29_Click);
            Button27 = new UI.WP.Button();
            Button27.Click += new EventHandler(Button27_Click);
            Button24 = new UI.WP.Button();
            Button24.Click += new EventHandler(Button24_Click);
            Button21 = new UI.WP.Button();
            Button21.Click += new EventHandler(Button21_Click);
            Button16 = new UI.WP.Button();
            Button16.Click += new EventHandler(Button16_Click);
            Button4 = new UI.WP.Button();
            Button4.Click += new EventHandler(Button4_Click_1);
            previewContainer.SuspendLayout();
            tabs_preview.SuspendLayout();
            TabPage6.SuspendLayout();
            pnl_preview.SuspendLayout();
            Window1.SuspendLayout();
            Panel3.SuspendLayout();
            TabPage7.SuspendLayout();
            pnl_preview_classic.SuspendLayout();
            ClassicTaskbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox21).BeginInit();
            TablessControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            PaletteContainer_W11.SuspendLayout();
            GroupBox13.SuspendLayout();
            GroupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W11_pic9).BeginInit();
            pnl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W11_pic8).BeginInit();
            pnl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W11_pic7).BeginInit();
            pnl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W11_pic4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).BeginInit();
            pnl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W11_pic6).BeginInit();
            pnl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W11_pic1).BeginInit();
            pnl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W11_pic3).BeginInit();
            pnl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W11_pic2).BeginInit();
            pnl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W11_pic5).BeginInit();
            GroupBox5.SuspendLayout();
            GroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox19).BeginInit();
            GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            GroupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox18).BeginInit();
            GroupBox24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).BeginInit();
            GroupBox1.SuspendLayout();
            GroupBox20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            GroupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            TabPage2.SuspendLayout();
            Panel1.SuspendLayout();
            GroupBox2.SuspendLayout();
            GroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W10_pic9).BeginInit();
            GroupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W10_pic8).BeginInit();
            GroupBox25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W10_pic7).BeginInit();
            GroupBox27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W10_pic4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).BeginInit();
            GroupBox28.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W10_pic6).BeginInit();
            GroupBox31.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W10_pic1).BeginInit();
            GroupBox34.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W10_pic3).BeginInit();
            GroupBox35.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W10_pic2).BeginInit();
            GroupBox36.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)W10_pic5).BeginInit();
            GroupBox37.SuspendLayout();
            GroupBox23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox15).BeginInit();
            GroupBox38.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox16).BeginInit();
            GroupBox40.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox22).BeginInit();
            GroupBox42.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox26).BeginInit();
            GroupBox43.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox27).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox31).BeginInit();
            GroupBox44.SuspendLayout();
            GroupBox45.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox33).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox34).BeginInit();
            GroupBox46.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox35).BeginInit();
            TabPage3.SuspendLayout();
            PaletteContainer_W81.SuspendLayout();
            GroupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox37).BeginInit();
            GroupBox32.SuspendLayout();
            GroupBox39.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox28).BeginInit();
            GroupBox41.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox29).BeginInit();
            GroupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).BeginInit();
            GroupBox33.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox30).BeginInit();
            GroupBox29.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox23).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox32).BeginInit();
            TabPage4.SuspendLayout();
            PaletteContainer_W7.SuspendLayout();
            GroupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox13).BeginInit();
            GroupBox22.SuspendLayout();
            GroupBox19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox24).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox39).BeginInit();
            GroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox8).BeginInit();
            GroupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox4).BeginInit();
            GroupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            GroupBox30.SuspendLayout();
            GroupBox21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox12).BeginInit();
            GroupBox26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox25).BeginInit();
            TabPage8.SuspendLayout();
            GroupBox50.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox45).BeginInit();
            GroupBox49.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox42).BeginInit();
            TabPage9.SuspendLayout();
            GroupBox48.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox38).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox41).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox40).BeginInit();
            GroupBox47.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            TabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox36).BeginInit();
            MainToolbar.SuspendLayout();
            GroupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.DefaultExt = "wpt";
            OpenFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth|All Files|*.*";
            // 
            // SaveFileDialog1
            // 
            SaveFileDialog1.DefaultExt = "wpt";
            SaveFileDialog1.Filter = "WinPaletter Theme (*.wpth)|*.wpth";
            // 
            // BackgroundWorker1
            // 
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // SaveFileDialog2
            // 
            SaveFileDialog2.DefaultExt = "wpt";
            SaveFileDialog2.Filter = "PNG File|*.png";
            // 
            // Timer1
            // 
            Timer1.Interval = 1000;
            // 
            // SaveFileDialog3
            // 
            SaveFileDialog3.DefaultExt = "wpt";
            SaveFileDialog3.Filter = "Test File (*.txt)|*.txt";
            // 
            // NotifyUpdates
            // 
            NotifyUpdates.Text = "WinPaletter";
            // 
            // OpenFileDialog2
            // 
            OpenFileDialog2.DefaultExt = "wpt";
            OpenFileDialog2.Filter = "Visual Styles File (*.msstyles)|*.msstyles|Theme File (*.theme)|*.theme";
            // 
            // Button28
            // 
            Button28.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button28.BackColor = Color.FromArgb(33, 33, 35);
            Button28.DrawOnGlass = false;
            Button28.Font = new Font("Segoe UI", 9.0f);
            Button28.ForeColor = Color.White;
            Button28.Image = (Image)resources.GetObject("Button28.Image");
            Button28.ImageAlign = ContentAlignment.MiddleLeft;
            Button28.LineColor = Color.FromArgb(18, 99, 149);
            Button28.Location = new Point(730, 655);
            Button28.Name = "Button28";
            Button28.Size = new Size(100, 34);
            Button28.TabIndex = 34;
            Button28.Text = "Logoff";
            Button28.UseVisualStyleBackColor = false;
            // 
            // previewContainer
            // 
            previewContainer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            previewContainer.BackColor = Color.FromArgb(33, 33, 35);
            previewContainer.Controls.Add(Select_WXP);
            previewContainer.Controls.Add(Select_WVista);
            previewContainer.Controls.Add(tabs_preview);
            previewContainer.Controls.Add(Select_W7);
            previewContainer.Controls.Add(Button23);
            previewContainer.Controls.Add(Select_W81);
            previewContainer.Controls.Add(Button1);
            previewContainer.Controls.Add(Select_W10);
            previewContainer.Controls.Add(Button15);
            previewContainer.Controls.Add(Select_W11);
            previewContainer.Controls.Add(PictureBox21);
            previewContainer.Controls.Add(themename_lbl);
            previewContainer.Controls.Add(author_lbl);
            previewContainer.Location = new Point(547, 77);
            previewContainer.Margin = new Padding(4, 3, 4, 3);
            previewContainer.Name = "previewContainer";
            previewContainer.Padding = new Padding(1);
            previewContainer.Size = new Size(536, 406);
            previewContainer.TabIndex = 14;
            // 
            // Select_WXP
            // 
            Select_WXP.AllowDrop = true;
            Select_WXP.Checked = false;
            Select_WXP.Font = new Font("Segoe UI", 9.0f);
            Select_WXP.ForeColor = Color.White;
            Select_WXP.Image = null;
            Select_WXP.Location = new Point(133, 359);
            Select_WXP.Name = "Select_WXP";
            Select_WXP.ShowText = false;
            Select_WXP.Size = new Size(40, 40);
            Select_WXP.TabIndex = 37;
            Select_WXP.Tag = "Change the preview to Windows XP";
            // 
            // Select_WVista
            // 
            Select_WVista.AllowDrop = true;
            Select_WVista.Checked = false;
            Select_WVista.Font = new Font("Segoe UI", 9.0f);
            Select_WVista.ForeColor = Color.White;
            Select_WVista.Image = null;
            Select_WVista.Location = new Point(179, 359);
            Select_WVista.Name = "Select_WVista";
            Select_WVista.ShowText = false;
            Select_WVista.Size = new Size(40, 40);
            Select_WVista.TabIndex = 36;
            Select_WVista.Tag = "Change the preview to Windows Vista";
            // 
            // tabs_preview
            // 
            tabs_preview.Controls.Add(TabPage6);
            tabs_preview.Controls.Add(TabPage7);
            tabs_preview.Location = new Point(3, 52);
            tabs_preview.Name = "tabs_preview";
            tabs_preview.SelectedIndex = 0;
            tabs_preview.Size = new Size(528, 297);
            tabs_preview.TabIndex = 35;
            // 
            // TabPage6
            // 
            TabPage6.BackColor = Color.FromArgb(25, 25, 25);
            TabPage6.Controls.Add(pnl_preview);
            TabPage6.Location = new Point(4, 24);
            TabPage6.Margin = new Padding(0);
            TabPage6.Name = "TabPage6";
            TabPage6.Size = new Size(520, 269);
            TabPage6.TabIndex = 0;
            TabPage6.Text = "0";
            // 
            // pnl_preview
            // 
            pnl_preview.BackColor = Color.Black;
            pnl_preview.BackgroundImageLayout = ImageLayout.Center;
            pnl_preview.Controls.Add(WXP_Alert2);
            pnl_preview.Controls.Add(ActionCenter);
            pnl_preview.Controls.Add(start);
            pnl_preview.Controls.Add(taskbar);
            pnl_preview.Controls.Add(Window2);
            pnl_preview.Controls.Add(Window1);
            pnl_preview.Location = new Point(0, 0);
            pnl_preview.Name = "pnl_preview";
            pnl_preview.Size = new Size(528, 297);
            pnl_preview.TabIndex = 2;
            // 
            // WXP_Alert2
            // 
            WXP_Alert2.AlertStyle = UI.WP.AlertBox.Style.Warning;
            WXP_Alert2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            WXP_Alert2.BackColor = Color.FromArgb(125, 20, 30);
            WXP_Alert2.CenterText = true;
            WXP_Alert2.CustomColor = Color.FromArgb(0, 81, 210);
            WXP_Alert2.Font = new Font("Segoe UI", 9.0f);
            WXP_Alert2.Image = null;
            WXP_Alert2.Location = new Point(7, 8);
            WXP_Alert2.Name = "WXP_Alert2";
            WXP_Alert2.Size = new Size(135, 36);
            WXP_Alert2.TabIndex = 54;
            WXP_Alert2.TabStop = false;
            WXP_Alert2.Text = "Classic theme is enabled. The preview won't work for other themes due to some lim" + "itations in visual styles Previewer. Apply another theme first then reopen WinPa" + "letter.";
            WXP_Alert2.Visible = false;
            // 
            // ActionCenter
            // 
            ActionCenter.ActionCenterButton_Hover = Color.Empty;
            ActionCenter.ActionCenterButton_Normal = Color.FromArgb(0, 120, 212);
            ActionCenter.ActionCenterButton_Pressed = Color.Empty;
            ActionCenter.AppBackground = Color.Empty;
            ActionCenter.AppUnderline = Color.Empty;
            ActionCenter.BackColor = Color.Transparent;
            ActionCenter.BackColorAlpha = 50;
            ActionCenter.Background = Color.Empty;
            ActionCenter.Background2 = Color.Empty;
            ActionCenter.BlurPower = 8;
            ActionCenter.DarkMode = true;
            ActionCenter.LinkColor = Color.Empty;
            ActionCenter.Location = new Point(400, 165);
            ActionCenter.Name = "ActionCenter";
            ActionCenter.NoisePower = 0.2f;
            ActionCenter.Padding = new Padding(2);
            ActionCenter.Shadow = true;
            ActionCenter.Size = new Size(120, 85);
            ActionCenter.StartColor = Color.Empty;
            ActionCenter.Style = UI.Simulation.WinElement.Styles.ActionCenter11;
            ActionCenter.SuspendRefresh = false;
            ActionCenter.TabIndex = 5;
            ActionCenter.Transparency = true;
            ActionCenter.UseWin11ORB_WithWin10 = false;
            ActionCenter.UseWin11RoundedCorners_WithWin10_Level1 = false;
            ActionCenter.UseWin11RoundedCorners_WithWin10_Level2 = false;
            ActionCenter.Win7ColorBal = 100;
            ActionCenter.Win7GlowBal = 100;
            // 
            // start
            // 
            start.ActionCenterButton_Hover = Color.Empty;
            start.ActionCenterButton_Normal = Color.Empty;
            start.ActionCenterButton_Pressed = Color.Empty;
            start.AppBackground = Color.Empty;
            start.AppUnderline = Color.Empty;
            start.BackColor = Color.Transparent;
            start.BackColorAlpha = 150;
            start.Background = Color.Empty;
            start.Background2 = Color.Empty;
            start.BlurPower = 7;
            start.DarkMode = true;
            start.LinkColor = Color.Empty;
            start.Location = new Point(7, 50);
            start.Name = "start";
            start.NoisePower = 0.2f;
            start.Padding = new Padding(2);
            start.Shadow = true;
            start.Size = new Size(135, 200);
            start.StartColor = Color.Empty;
            start.Style = UI.Simulation.WinElement.Styles.Start11;
            start.SuspendRefresh = false;
            start.TabIndex = 1;
            start.Transparency = true;
            start.UseWin11ORB_WithWin10 = false;
            start.UseWin11RoundedCorners_WithWin10_Level1 = false;
            start.UseWin11RoundedCorners_WithWin10_Level2 = false;
            start.Win7ColorBal = 100;
            start.Win7GlowBal = 100;
            // 
            // taskbar
            // 
            taskbar.ActionCenterButton_Hover = Color.Empty;
            taskbar.ActionCenterButton_Normal = Color.Empty;
            taskbar.ActionCenterButton_Pressed = Color.Empty;
            taskbar.AppBackground = Color.Empty;
            taskbar.AppUnderline = Color.Empty;
            taskbar.BackColor = Color.Transparent;
            taskbar.BackColorAlpha = 130;
            taskbar.Background = Color.Empty;
            taskbar.Background2 = Color.Empty;
            taskbar.BlurPower = 12;
            taskbar.DarkMode = true;
            taskbar.Dock = DockStyle.Bottom;
            taskbar.LinkColor = Color.Empty;
            taskbar.Location = new Point(0, 255);
            taskbar.Name = "taskbar";
            taskbar.NoisePower = 0.2f;
            taskbar.Shadow = true;
            taskbar.Size = new Size(528, 42);
            taskbar.StartColor = Color.Empty;
            taskbar.Style = UI.Simulation.WinElement.Styles.Taskbar11;
            taskbar.SuspendRefresh = false;
            taskbar.TabIndex = 0;
            taskbar.Transparency = true;
            taskbar.UseWin11ORB_WithWin10 = false;
            taskbar.UseWin11RoundedCorners_WithWin10_Level1 = false;
            taskbar.UseWin11RoundedCorners_WithWin10_Level2 = false;
            taskbar.Win7ColorBal = 100;
            taskbar.Win7GlowBal = 100;
            // 
            // Window2
            // 
            Window2.AccentColor_Active = Color.FromArgb(10, 10, 100);
            Window2.AccentColor_Enabled = true;
            Window2.AccentColor_Inactive = Color.FromArgb(32, 32, 32);
            Window2.AccentColor2_Active = Color.FromArgb(0, 120, 212);
            Window2.AccentColor2_Inactive = Color.FromArgb(32, 32, 32);
            Window2.Active = false;
            Window2.BackColor = Color.Transparent;
            Window2.DarkMode = true;
            Window2.Font = new Font("Segoe UI", 9.0f);
            Window2.Location = new Point(172, 160);
            Window2.Metrics_BorderWidth = 1;
            Window2.Metrics_CaptionHeight = 22;
            Window2.Metrics_PaddedBorderWidth = 4;
            Window2.Name = "Window2";
            Window2.Padding = new Padding(4, 40, 4, 4);
            Window2.Preview = UI.Simulation.Window.Preview_Enum.W11;
            Window2.Radius = 5;
            Window2.Shadow = true;
            Window2.Size = new Size(189, 85);
            Window2.SuspendRefresh = false;
            Window2.TabIndex = 3;
            Window2.Text = "Inactive app";
            Window2.ToolWindow = false;
            Window2.Win7Alpha = 100;
            Window2.Win7ColorBal = 100;
            Window2.Win7GlowBal = 100;
            Window2.Win7Noise = 1.0f;
            Window2.WinVista = false;
            // 
            // Window1
            // 
            Window1.AccentColor_Active = Color.FromArgb(0, 120, 212);
            Window1.AccentColor_Enabled = true;
            Window1.AccentColor_Inactive = Color.FromArgb(32, 32, 32);
            Window1.AccentColor2_Active = Color.FromArgb(0, 120, 212);
            Window1.AccentColor2_Inactive = Color.FromArgb(32, 32, 32);
            Window1.Active = true;
            Window1.BackColor = Color.Transparent;
            Window1.Controls.Add(Panel3);
            Window1.Controls.Add(lnk_preview);
            Window1.DarkMode = true;
            Window1.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Window1.Location = new Point(172, 13);
            Window1.Metrics_BorderWidth = 1;
            Window1.Metrics_CaptionHeight = 22;
            Window1.Metrics_PaddedBorderWidth = 4;
            Window1.Name = "Window1";
            Window1.Padding = new Padding(4, 40, 4, 4);
            Window1.Preview = UI.Simulation.Window.Preview_Enum.W11;
            Window1.Radius = 5;
            Window1.Shadow = true;
            Window1.Size = new Size(189, 147);
            Window1.SuspendRefresh = false;
            Window1.TabIndex = 2;
            Window1.Text = "App preview";
            Window1.ToolWindow = false;
            Window1.Win7Alpha = 100;
            Window1.Win7ColorBal = 100;
            Window1.Win7GlowBal = 100;
            Window1.Win7Noise = 1.0f;
            Window1.WinVista = false;
            // 
            // Panel3
            // 
            Panel3.BackColor = Color.Transparent;
            Panel3.Controls.Add(Label8);
            Panel3.Controls.Add(setting_icon_preview);
            Panel3.Dock = DockStyle.Fill;
            Panel3.Location = new Point(4, 40);
            Panel3.Name = "Panel3";
            Panel3.Padding = new Padding(1);
            Panel3.Size = new Size(181, 78);
            Panel3.TabIndex = 0;
            // 
            // Label8
            // 
            Label8.BackColor = Color.Transparent;
            Label8.Dock = DockStyle.Fill;
            Label8.DrawOnGlass = false;
            Label8.Font = new Font("Segoe UI", 9.0f);
            Label8.Location = new Point(1, 46);
            Label8.Name = "Label8";
            Label8.Size = new Size(179, 31);
            Label8.TabIndex = 15;
            Label8.Text = "This is a setting icon";
            Label8.TextAlign = ContentAlignment.TopCenter;
            // 
            // setting_icon_preview
            // 
            setting_icon_preview.BackColor = Color.Transparent;
            setting_icon_preview.Dock = DockStyle.Top;
            setting_icon_preview.DrawOnGlass = false;
            setting_icon_preview.Font = new Font("Segoe MDL2 Assets", 21.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            setting_icon_preview.Location = new Point(1, 1);
            setting_icon_preview.Name = "setting_icon_preview";
            setting_icon_preview.Size = new Size(179, 45);
            setting_icon_preview.TabIndex = 14;
            setting_icon_preview.Text = "";
            setting_icon_preview.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lnk_preview
            // 
            lnk_preview.BackColor = Color.Transparent;
            lnk_preview.Dock = DockStyle.Bottom;
            lnk_preview.DrawOnGlass = false;
            lnk_preview.Font = new Font("Segoe UI", 9.0f);
            lnk_preview.ForeColor = Color.Brown;
            lnk_preview.Location = new Point(4, 118);
            lnk_preview.Name = "lnk_preview";
            lnk_preview.Size = new Size(181, 25);
            lnk_preview.TabIndex = 16;
            lnk_preview.Text = "Settings link preview";
            lnk_preview.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TabPage7
            // 
            TabPage7.BackColor = Color.FromArgb(25, 25, 25);
            TabPage7.Controls.Add(pnl_preview_classic);
            TabPage7.Location = new Point(4, 24);
            TabPage7.Margin = new Padding(0);
            TabPage7.Name = "TabPage7";
            TabPage7.Size = new Size(520, 269);
            TabPage7.TabIndex = 1;
            TabPage7.Text = "1";
            // 
            // pnl_preview_classic
            // 
            pnl_preview_classic.BackColor = Color.Black;
            pnl_preview_classic.BackgroundImageLayout = ImageLayout.Center;
            pnl_preview_classic.Controls.Add(ClassicWindow1);
            pnl_preview_classic.Controls.Add(ClassicWindow2);
            pnl_preview_classic.Controls.Add(ClassicTaskbar);
            pnl_preview_classic.Location = new Point(0, 0);
            pnl_preview_classic.Name = "pnl_preview_classic";
            pnl_preview_classic.Size = new Size(528, 297);
            pnl_preview_classic.TabIndex = 34;
            // 
            // ClassicWindow1
            // 
            ClassicWindow1.BackColor = Color.FromArgb(192, 192, 192);
            ClassicWindow1.ButtonDkShadow = Color.Black;
            ClassicWindow1.ButtonFace = Color.FromArgb(192, 192, 192);
            ClassicWindow1.ButtonHilight = Color.White;
            ClassicWindow1.ButtonLight = Color.FromArgb(192, 192, 192);
            ClassicWindow1.ButtonShadow = Color.FromArgb(128, 128, 128);
            ClassicWindow1.ButtonText = Color.Black;
            ClassicWindow1.Color1 = Color.FromArgb(0, 0, 128);
            ClassicWindow1.Color2 = Color.FromArgb(16, 132, 208);
            ClassicWindow1.ColorBorder = Color.FromArgb(192, 192, 192);
            ClassicWindow1.ColorGradient = true;
            ClassicWindow1.ControlBox = true;
            ClassicWindow1.Flat = false;
            ClassicWindow1.Font = new Font("Microsoft Sans Serif", 8.0f);
            ClassicWindow1.ForeColor = Color.White;
            ClassicWindow1.Location = new Point(174, 20);
            ClassicWindow1.MaximizeBox = false;
            ClassicWindow1.Metrics_BorderWidth = 0;
            ClassicWindow1.Metrics_CaptionHeight = 18;
            ClassicWindow1.Metrics_CaptionWidth = 0;
            ClassicWindow1.Metrics_PaddedBorderWidth = 0;
            ClassicWindow1.MinimizeBox = false;
            ClassicWindow1.Name = "ClassicWindow1";
            ClassicWindow1.Padding = new Padding(4, 22, 4, 4);
            ClassicWindow1.Size = new Size(181, 146);
            ClassicWindow1.TabIndex = 4;
            ClassicWindow1.Text = "App preview";
            ClassicWindow1.UseItAsMenu = false;
            // 
            // ClassicWindow2
            // 
            ClassicWindow2.BackColor = Color.FromArgb(192, 192, 192);
            ClassicWindow2.ButtonDkShadow = Color.Black;
            ClassicWindow2.ButtonFace = Color.FromArgb(192, 192, 192);
            ClassicWindow2.ButtonHilight = Color.White;
            ClassicWindow2.ButtonLight = Color.FromArgb(192, 192, 192);
            ClassicWindow2.ButtonShadow = Color.FromArgb(128, 128, 128);
            ClassicWindow2.ButtonText = Color.Black;
            ClassicWindow2.Color1 = Color.FromArgb(51, 51, 51);
            ClassicWindow2.Color2 = Color.FromArgb(181, 181, 181);
            ClassicWindow2.ColorBorder = Color.FromArgb(192, 192, 192);
            ClassicWindow2.ColorGradient = true;
            ClassicWindow2.ControlBox = true;
            ClassicWindow2.Flat = false;
            ClassicWindow2.Font = new Font("Microsoft Sans Serif", 8.0f);
            ClassicWindow2.ForeColor = Color.White;
            ClassicWindow2.Location = new Point(174, 172);
            ClassicWindow2.MaximizeBox = false;
            ClassicWindow2.Metrics_BorderWidth = 0;
            ClassicWindow2.Metrics_CaptionHeight = 18;
            ClassicWindow2.Metrics_CaptionWidth = 0;
            ClassicWindow2.Metrics_PaddedBorderWidth = 0;
            ClassicWindow2.MinimizeBox = false;
            ClassicWindow2.Name = "ClassicWindow2";
            ClassicWindow2.Padding = new Padding(4, 22, 4, 4);
            ClassicWindow2.Size = new Size(181, 60);
            ClassicWindow2.TabIndex = 5;
            ClassicWindow2.Text = "Inactive app";
            ClassicWindow2.UseItAsMenu = false;
            // 
            // ClassicTaskbar
            // 
            ClassicTaskbar.BackColor = Color.FromArgb(192, 192, 192);
            ClassicTaskbar.ButtonDkShadow = Color.FromArgb(105, 105, 105);
            ClassicTaskbar.ButtonHilight = Color.White;
            ClassicTaskbar.ButtonLight = Color.FromArgb(227, 227, 227);
            ClassicTaskbar.ButtonShadow = Color.FromArgb(128, 128, 128);
            ClassicTaskbar.Controls.Add(ButtonR4);
            ClassicTaskbar.Controls.Add(ButtonR3);
            ClassicTaskbar.Controls.Add(ButtonR2);
            ClassicTaskbar.Dock = DockStyle.Bottom;
            ClassicTaskbar.Flat = false;
            ClassicTaskbar.Font = new Font("Microsoft Sans Serif", 8.0f);
            ClassicTaskbar.ForeColor = Color.Black;
            ClassicTaskbar.Location = new Point(0, 253);
            ClassicTaskbar.Name = "ClassicTaskbar";
            ClassicTaskbar.Size = new Size(528, 44);
            ClassicTaskbar.Style2 = false;
            ClassicTaskbar.TabIndex = 0;
            ClassicTaskbar.UseItAsWin7Taskbar = true;
            // 
            // ButtonR4
            // 
            ButtonR4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ButtonR4.AppearsAsPressed = false;
            ButtonR4.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR4.ButtonDkShadow = Color.Black;
            ButtonR4.ButtonHilight = Color.White;
            ButtonR4.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR4.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR4.FocusRectHeight = 1;
            ButtonR4.FocusRectWidth = 1;
            ButtonR4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ButtonR4.ForeColor = Color.Black;
            ButtonR4.HatchBrush = false;
            ButtonR4.Image = null;
            ButtonR4.Location = new Point(113, 4);
            ButtonR4.Name = "ButtonR4";
            ButtonR4.Size = new Size(48, 38);
            ButtonR4.TabIndex = 2;
            ButtonR4.UseItAsScrollbar = false;
            ButtonR4.UseVisualStyleBackColor = false;
            ButtonR4.WindowFrame = Color.Black;
            // 
            // ButtonR3
            // 
            ButtonR3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ButtonR3.AppearsAsPressed = true;
            ButtonR3.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR3.ButtonDkShadow = Color.Black;
            ButtonR3.ButtonHilight = Color.White;
            ButtonR3.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR3.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR3.FocusRectHeight = 1;
            ButtonR3.FocusRectWidth = 1;
            ButtonR3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            ButtonR3.ForeColor = Color.Black;
            ButtonR3.HatchBrush = false;
            ButtonR3.Image = null;
            ButtonR3.Location = new Point(63, 4);
            ButtonR3.Name = "ButtonR3";
            ButtonR3.Size = new Size(48, 38);
            ButtonR3.TabIndex = 1;
            ButtonR3.UseItAsScrollbar = false;
            ButtonR3.UseVisualStyleBackColor = false;
            ButtonR3.WindowFrame = Color.Black;
            // 
            // ButtonR2
            // 
            ButtonR2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ButtonR2.AppearsAsPressed = false;
            ButtonR2.BackColor = Color.FromArgb(192, 192, 192);
            ButtonR2.ButtonDkShadow = Color.Black;
            ButtonR2.ButtonHilight = Color.White;
            ButtonR2.ButtonLight = Color.FromArgb(192, 192, 192);
            ButtonR2.ButtonShadow = Color.FromArgb(128, 128, 128);
            ButtonR2.FocusRectHeight = 1;
            ButtonR2.FocusRectWidth = 1;
            ButtonR2.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            ButtonR2.ForeColor = Color.Black;
            ButtonR2.HatchBrush = false;
            ButtonR2.Image = null;
            ButtonR2.ImageAlign = ContentAlignment.MiddleRight;
            ButtonR2.Location = new Point(2, 4);
            ButtonR2.Name = "ButtonR2";
            ButtonR2.Size = new Size(52, 38);
            ButtonR2.TabIndex = 0;
            ButtonR2.Text = "Start";
            ButtonR2.UseItAsScrollbar = false;
            ButtonR2.UseVisualStyleBackColor = false;
            ButtonR2.WindowFrame = Color.Black;
            // 
            // Select_W7
            // 
            Select_W7.AllowDrop = true;
            Select_W7.Checked = false;
            Select_W7.Font = new Font("Segoe UI", 9.0f);
            Select_W7.ForeColor = Color.White;
            Select_W7.Image = null;
            Select_W7.Location = new Point(225, 359);
            Select_W7.Name = "Select_W7";
            Select_W7.ShowText = false;
            Select_W7.Size = new Size(40, 40);
            Select_W7.TabIndex = 26;
            Select_W7.Tag = "Change the preview to Windows 7";
            // 
            // Button23
            // 
            Button23.BackColor = Color.FromArgb(42, 42, 44);
            Button23.DrawOnGlass = false;
            Button23.Font = new Font("Segoe UI", 9.0f);
            Button23.ForeColor = Color.White;
            Button23.Image = null;
            Button23.LineColor = Color.FromArgb(0, 81, 210);
            Button23.Location = new Point(351, 16);
            Button23.Name = "Button23";
            Button23.Size = new Size(45, 24);
            Button23.TabIndex = 7;
            Button23.Text = "Hide";
            Button23.UseVisualStyleBackColor = false;
            // 
            // Select_W81
            // 
            Select_W81.AllowDrop = true;
            Select_W81.Checked = false;
            Select_W81.Font = new Font("Segoe UI", 9.0f);
            Select_W81.ForeColor = Color.White;
            Select_W81.Image = null;
            Select_W81.Location = new Point(271, 359);
            Select_W81.Name = "Select_W81";
            Select_W81.ShowText = false;
            Select_W81.Size = new Size(40, 40);
            Select_W81.TabIndex = 25;
            Select_W81.Tag = "Change the preview to Windows 8.1";
            // 
            // Button1
            // 
            Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button1.BackColor = Color.FromArgb(42, 42, 44);
            Button1.DrawOnGlass = false;
            Button1.Font = new Font("Segoe UI", 9.0f);
            Button1.ForeColor = Color.White;
            Button1.Image = (Image)resources.GetObject("Button1.Image");
            Button1.ImageAlign = ContentAlignment.MiddleRight;
            Button1.LineColor = Color.FromArgb(69, 110, 129);
            Button1.Location = new Point(397, 16);
            Button1.Margin = new Padding(4, 3, 4, 3);
            Button1.Name = "Button1";
            Button1.Size = new Size(96, 24);
            Button1.TabIndex = 4;
            Button1.Text = "Thumbnail";
            Button1.UseVisualStyleBackColor = false;
            // 
            // Select_W10
            // 
            Select_W10.AllowDrop = true;
            Select_W10.Checked = false;
            Select_W10.Font = new Font("Segoe UI", 9.0f);
            Select_W10.ForeColor = Color.White;
            Select_W10.Image = null;
            Select_W10.Location = new Point(317, 359);
            Select_W10.Name = "Select_W10";
            Select_W10.ShowText = false;
            Select_W10.Size = new Size(40, 40);
            Select_W10.TabIndex = 24;
            Select_W10.Tag = "Change the preview to Windows 10";
            // 
            // Button15
            // 
            Button15.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button15.BackColor = Color.FromArgb(42, 42, 44);
            Button15.DrawOnGlass = false;
            Button15.Font = new Font("Segoe UI", 9.0f);
            Button15.ForeColor = Color.White;
            Button15.Image = (Image)resources.GetObject("Button15.Image");
            Button15.LineColor = Color.FromArgb(10, 60, 90);
            Button15.Location = new Point(494, 16);
            Button15.Margin = new Padding(4, 3, 4, 3);
            Button15.Name = "Button15";
            Button15.Size = new Size(30, 24);
            Button15.TabIndex = 3;
            Button15.UseVisualStyleBackColor = false;
            // 
            // Select_W11
            // 
            Select_W11.AllowDrop = true;
            Select_W11.Checked = false;
            Select_W11.Font = new Font("Segoe UI", 9.0f);
            Select_W11.ForeColor = Color.White;
            Select_W11.Image = null;
            Select_W11.Location = new Point(363, 359);
            Select_W11.Name = "Select_W11";
            Select_W11.ShowText = false;
            Select_W11.Size = new Size(40, 40);
            Select_W11.TabIndex = 23;
            Select_W11.Tag = "Change the preview to Windows 11";
            // 
            // PictureBox21
            // 
            PictureBox21.Image = (Image)resources.GetObject("PictureBox21.Image");
            PictureBox21.Location = new Point(6, 10);
            PictureBox21.Name = "PictureBox21";
            PictureBox21.Size = new Size(35, 35);
            PictureBox21.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox21.TabIndex = 1;
            PictureBox21.TabStop = false;
            // 
            // themename_lbl
            // 
            themename_lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            themename_lbl.AutoEllipsis = true;
            themename_lbl.BackColor = Color.Transparent;
            themename_lbl.Font = new Font("Segoe UI", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            themename_lbl.Location = new Point(44, 5);
            themename_lbl.Name = "themename_lbl";
            themename_lbl.Size = new Size(353, 25);
            themename_lbl.TabIndex = 5;
            themename_lbl.Text = "Theme (1.0)";
            themename_lbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // author_lbl
            // 
            author_lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            author_lbl.AutoEllipsis = true;
            author_lbl.BackColor = Color.Transparent;
            author_lbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            author_lbl.Location = new Point(53, 30);
            author_lbl.Name = "author_lbl";
            author_lbl.Size = new Size(344, 15);
            author_lbl.TabIndex = 6;
            author_lbl.Text = "By: ";
            author_lbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TablessControl1
            // 
            TablessControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TablessControl1.Controls.Add(TabPage1);
            TablessControl1.Controls.Add(TabPage2);
            TablessControl1.Controls.Add(TabPage3);
            TablessControl1.Controls.Add(TabPage4);
            TablessControl1.Controls.Add(TabPage8);
            TablessControl1.Controls.Add(TabPage9);
            TablessControl1.Controls.Add(TabPage5);
            TablessControl1.Location = new Point(11, 74);
            TablessControl1.Name = "TablessControl1";
            TablessControl1.SelectedIndex = 0;
            TablessControl1.Size = new Size(537, 545);
            TablessControl1.TabIndex = 33;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.FromArgb(25, 25, 25);
            TabPage1.Controls.Add(PaletteContainer_W11);
            TabPage1.Location = new Point(4, 24);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(529, 517);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "W11";
            // 
            // PaletteContainer_W11
            // 
            PaletteContainer_W11.BackColor = Color.Transparent;
            PaletteContainer_W11.Controls.Add(GroupBox13);
            PaletteContainer_W11.Controls.Add(GroupBox5);
            PaletteContainer_W11.Controls.Add(GroupBox1);
            PaletteContainer_W11.Dock = DockStyle.Fill;
            PaletteContainer_W11.Location = new Point(3, 3);
            PaletteContainer_W11.Name = "PaletteContainer_W11";
            PaletteContainer_W11.Size = new Size(523, 511);
            PaletteContainer_W11.TabIndex = 17;
            // 
            // GroupBox13
            // 
            GroupBox13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox13.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox13.Controls.Add(Button37);
            GroupBox13.Controls.Add(Button30);
            GroupBox13.Controls.Add(GroupBox14);
            GroupBox13.Controls.Add(pnl8);
            GroupBox13.Controls.Add(pnl7);
            GroupBox13.Controls.Add(pnl4);
            GroupBox13.Controls.Add(PictureBox10);
            GroupBox13.Controls.Add(pnl6);
            GroupBox13.Controls.Add(pnl1);
            GroupBox13.Controls.Add(pnl3);
            GroupBox13.Controls.Add(Label10);
            GroupBox13.Controls.Add(pnl2);
            GroupBox13.Controls.Add(pnl5);
            GroupBox13.Location = new Point(0, 213);
            GroupBox13.Margin = new Padding(4, 3, 4, 3);
            GroupBox13.Name = "GroupBox13";
            GroupBox13.Size = new Size(520, 313);
            GroupBox13.TabIndex = 6;
            // 
            // Button37
            // 
            Button37.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button37.BackColor = Color.FromArgb(43, 43, 43);
            Button37.DrawOnGlass = false;
            Button37.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Button37.ForeColor = Color.White;
            Button37.Image = null;
            Button37.LineColor = Color.FromArgb(0, 81, 210);
            Button37.Location = new Point(203, 9);
            Button37.Name = "Button37";
            Button37.Size = new Size(214, 22);
            Button37.TabIndex = 29;
            Button37.Text = "Copycat from Windows 10 presets";
            Button37.UseVisualStyleBackColor = false;
            // 
            // Button30
            // 
            Button30.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button30.BackColor = Color.FromArgb(43, 43, 43);
            Button30.DrawOnGlass = false;
            Button30.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Button30.ForeColor = Color.White;
            Button30.Image = null;
            Button30.LineColor = Color.FromArgb(199, 49, 61);
            Button30.Location = new Point(423, 9);
            Button30.Name = "Button30";
            Button30.Size = new Size(90, 22);
            Button30.TabIndex = 28;
            Button30.Text = "Important tips";
            Button30.UseVisualStyleBackColor = false;
            // 
            // GroupBox14
            // 
            GroupBox14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox14.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox14.Controls.Add(Label42);
            GroupBox14.Controls.Add(W11_pic9);
            GroupBox14.Controls.Add(W11_lbl9);
            GroupBox14.Controls.Add(W11_Color_Index7);
            GroupBox14.Location = new Point(3, 281);
            GroupBox14.Margin = new Padding(4, 3, 4, 3);
            GroupBox14.Name = "GroupBox14";
            GroupBox14.Size = new Size(514, 28);
            GroupBox14.TabIndex = 25;
            // 
            // Label42
            // 
            Label42.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label42.AutoEllipsis = true;
            Label42.BackColor = Color.Transparent;
            Label42.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label42.Location = new Point(31, 4);
            Label42.Name = "Label42";
            Label42.Size = new Size(14, 19);
            Label42.TabIndex = 6;
            Label42.Text = "9";
            Label42.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_pic9
            // 
            W11_pic9.BackColor = Color.Transparent;
            W11_pic9.Image = Properties.Resources.Mini_Undefined;
            W11_pic9.Location = new Point(2, 2);
            W11_pic9.Name = "W11_pic9";
            W11_pic9.Size = new Size(24, 24);
            W11_pic9.SizeMode = PictureBoxSizeMode.CenterImage;
            W11_pic9.TabIndex = 4;
            W11_pic9.TabStop = false;
            // 
            // W11_lbl9
            // 
            W11_lbl9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W11_lbl9.AutoEllipsis = true;
            W11_lbl9.BackColor = Color.Transparent;
            W11_lbl9.Font = new Font("Segoe UI", 9.0f);
            W11_lbl9.Location = new Point(48, 4);
            W11_lbl9.Name = "W11_lbl9";
            W11_lbl9.Size = new Size(365, 19);
            W11_lbl9.TabIndex = 3;
            W11_lbl9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_Color_Index7
            // 
            W11_Color_Index7.AllowDrop = true;
            W11_Color_Index7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W11_Color_Index7.BackColor = Color.FromArgb(47, 47, 48);
            W11_Color_Index7.DefaultColor = Color.Black;
            W11_Color_Index7.DontShowInfo = false;
            W11_Color_Index7.Location = new Point(420, 4);
            W11_Color_Index7.Margin = new Padding(4, 3, 4, 3);
            W11_Color_Index7.Name = "W11_Color_Index7";
            W11_Color_Index7.Size = new Size(90, 20);
            W11_Color_Index7.TabIndex = 2;
            // 
            // pnl8
            // 
            pnl8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnl8.BackColor = Color.FromArgb(43, 43, 43);
            pnl8.Controls.Add(Label37);
            pnl8.Controls.Add(W11_pic8);
            pnl8.Controls.Add(W11_lbl8);
            pnl8.Controls.Add(W11_Color_Index6);
            pnl8.Location = new Point(3, 251);
            pnl8.Margin = new Padding(4, 3, 4, 3);
            pnl8.Name = "pnl8";
            pnl8.Size = new Size(514, 28);
            pnl8.TabIndex = 23;
            // 
            // Label37
            // 
            Label37.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label37.AutoEllipsis = true;
            Label37.BackColor = Color.Transparent;
            Label37.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label37.Location = new Point(31, 4);
            Label37.Name = "Label37";
            Label37.Size = new Size(14, 19);
            Label37.TabIndex = 6;
            Label37.Text = "8";
            Label37.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_pic8
            // 
            W11_pic8.BackColor = Color.Transparent;
            W11_pic8.Location = new Point(2, 2);
            W11_pic8.Name = "W11_pic8";
            W11_pic8.Size = new Size(24, 24);
            W11_pic8.SizeMode = PictureBoxSizeMode.CenterImage;
            W11_pic8.TabIndex = 4;
            W11_pic8.TabStop = false;
            // 
            // W11_lbl8
            // 
            W11_lbl8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W11_lbl8.AutoEllipsis = true;
            W11_lbl8.BackColor = Color.Transparent;
            W11_lbl8.Font = new Font("Segoe UI", 9.0f);
            W11_lbl8.Location = new Point(48, 4);
            W11_lbl8.Name = "W11_lbl8";
            W11_lbl8.Size = new Size(365, 19);
            W11_lbl8.TabIndex = 3;
            W11_lbl8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_Color_Index6
            // 
            W11_Color_Index6.AllowDrop = true;
            W11_Color_Index6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W11_Color_Index6.BackColor = Color.FromArgb(47, 47, 48);
            W11_Color_Index6.DefaultColor = Color.Black;
            W11_Color_Index6.DontShowInfo = false;
            W11_Color_Index6.Location = new Point(420, 4);
            W11_Color_Index6.Margin = new Padding(4, 3, 4, 3);
            W11_Color_Index6.Name = "W11_Color_Index6";
            W11_Color_Index6.Size = new Size(90, 20);
            W11_Color_Index6.TabIndex = 2;
            // 
            // pnl7
            // 
            pnl7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnl7.BackColor = Color.FromArgb(43, 43, 43);
            pnl7.Controls.Add(Label36);
            pnl7.Controls.Add(W11_pic7);
            pnl7.Controls.Add(W11_lbl7);
            pnl7.Controls.Add(W11_Color_Index5);
            pnl7.Location = new Point(3, 221);
            pnl7.Margin = new Padding(4, 3, 4, 3);
            pnl7.Name = "pnl7";
            pnl7.Size = new Size(514, 28);
            pnl7.TabIndex = 22;
            // 
            // Label36
            // 
            Label36.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label36.AutoEllipsis = true;
            Label36.BackColor = Color.Transparent;
            Label36.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label36.Location = new Point(31, 4);
            Label36.Name = "Label36";
            Label36.Size = new Size(14, 19);
            Label36.TabIndex = 6;
            Label36.Text = "7";
            Label36.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_pic7
            // 
            W11_pic7.BackColor = Color.Transparent;
            W11_pic7.Location = new Point(2, 2);
            W11_pic7.Name = "W11_pic7";
            W11_pic7.Size = new Size(24, 24);
            W11_pic7.SizeMode = PictureBoxSizeMode.CenterImage;
            W11_pic7.TabIndex = 4;
            W11_pic7.TabStop = false;
            // 
            // W11_lbl7
            // 
            W11_lbl7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W11_lbl7.AutoEllipsis = true;
            W11_lbl7.BackColor = Color.Transparent;
            W11_lbl7.Font = new Font("Segoe UI", 9.0f);
            W11_lbl7.Location = new Point(48, 4);
            W11_lbl7.Name = "W11_lbl7";
            W11_lbl7.Size = new Size(365, 19);
            W11_lbl7.TabIndex = 3;
            W11_lbl7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_Color_Index5
            // 
            W11_Color_Index5.AllowDrop = true;
            W11_Color_Index5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W11_Color_Index5.BackColor = Color.FromArgb(47, 47, 48);
            W11_Color_Index5.DefaultColor = Color.Black;
            W11_Color_Index5.DontShowInfo = false;
            W11_Color_Index5.Location = new Point(420, 4);
            W11_Color_Index5.Margin = new Padding(4, 3, 4, 3);
            W11_Color_Index5.Name = "W11_Color_Index5";
            W11_Color_Index5.Size = new Size(90, 20);
            W11_Color_Index5.TabIndex = 2;
            // 
            // pnl4
            // 
            pnl4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnl4.BackColor = Color.FromArgb(43, 43, 43);
            pnl4.Controls.Add(Label18);
            pnl4.Controls.Add(W11_pic4);
            pnl4.Controls.Add(W11_lbl4);
            pnl4.Controls.Add(W11_Color_Index2);
            pnl4.Location = new Point(3, 131);
            pnl4.Margin = new Padding(4, 3, 4, 3);
            pnl4.Name = "pnl4";
            pnl4.Size = new Size(514, 28);
            pnl4.TabIndex = 24;
            // 
            // Label18
            // 
            Label18.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label18.AutoEllipsis = true;
            Label18.BackColor = Color.Transparent;
            Label18.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label18.Location = new Point(31, 4);
            Label18.Name = "Label18";
            Label18.Size = new Size(14, 19);
            Label18.TabIndex = 6;
            Label18.Text = "4";
            Label18.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_pic4
            // 
            W11_pic4.BackColor = Color.Transparent;
            W11_pic4.Location = new Point(2, 2);
            W11_pic4.Name = "W11_pic4";
            W11_pic4.Size = new Size(24, 24);
            W11_pic4.SizeMode = PictureBoxSizeMode.CenterImage;
            W11_pic4.TabIndex = 4;
            W11_pic4.TabStop = false;
            // 
            // W11_lbl4
            // 
            W11_lbl4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W11_lbl4.AutoEllipsis = true;
            W11_lbl4.BackColor = Color.Transparent;
            W11_lbl4.Font = new Font("Segoe UI", 9.0f);
            W11_lbl4.Location = new Point(48, 4);
            W11_lbl4.Name = "W11_lbl4";
            W11_lbl4.Size = new Size(365, 19);
            W11_lbl4.TabIndex = 3;
            W11_lbl4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_Color_Index2
            // 
            W11_Color_Index2.AllowDrop = true;
            W11_Color_Index2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W11_Color_Index2.BackColor = Color.FromArgb(47, 47, 48);
            W11_Color_Index2.DefaultColor = Color.Black;
            W11_Color_Index2.DontShowInfo = false;
            W11_Color_Index2.Location = new Point(420, 4);
            W11_Color_Index2.Margin = new Padding(4, 3, 4, 3);
            W11_Color_Index2.Name = "W11_Color_Index2";
            W11_Color_Index2.Size = new Size(90, 20);
            W11_Color_Index2.TabIndex = 2;
            // 
            // PictureBox10
            // 
            PictureBox10.BackColor = Color.Transparent;
            PictureBox10.Image = (Image)resources.GetObject("PictureBox10.Image");
            PictureBox10.Location = new Point(3, 3);
            PictureBox10.Name = "PictureBox10";
            PictureBox10.Size = new Size(35, 35);
            PictureBox10.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox10.TabIndex = 1;
            PictureBox10.TabStop = false;
            // 
            // pnl6
            // 
            pnl6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnl6.BackColor = Color.FromArgb(43, 43, 43);
            pnl6.Controls.Add(Label35);
            pnl6.Controls.Add(W11_pic6);
            pnl6.Controls.Add(W11_lbl6);
            pnl6.Controls.Add(W11_Color_Index4);
            pnl6.Location = new Point(3, 191);
            pnl6.Margin = new Padding(4, 3, 4, 3);
            pnl6.Name = "pnl6";
            pnl6.Size = new Size(514, 28);
            pnl6.TabIndex = 21;
            // 
            // Label35
            // 
            Label35.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label35.AutoEllipsis = true;
            Label35.BackColor = Color.Transparent;
            Label35.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label35.Location = new Point(31, 4);
            Label35.Name = "Label35";
            Label35.Size = new Size(14, 19);
            Label35.TabIndex = 6;
            Label35.Text = "6";
            Label35.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_pic6
            // 
            W11_pic6.BackColor = Color.Transparent;
            W11_pic6.Location = new Point(2, 2);
            W11_pic6.Name = "W11_pic6";
            W11_pic6.Size = new Size(24, 24);
            W11_pic6.SizeMode = PictureBoxSizeMode.CenterImage;
            W11_pic6.TabIndex = 4;
            W11_pic6.TabStop = false;
            // 
            // W11_lbl6
            // 
            W11_lbl6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W11_lbl6.AutoEllipsis = true;
            W11_lbl6.BackColor = Color.Transparent;
            W11_lbl6.Font = new Font("Segoe UI", 9.0f);
            W11_lbl6.Location = new Point(48, 4);
            W11_lbl6.Name = "W11_lbl6";
            W11_lbl6.Size = new Size(365, 19);
            W11_lbl6.TabIndex = 3;
            W11_lbl6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_Color_Index4
            // 
            W11_Color_Index4.AllowDrop = true;
            W11_Color_Index4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W11_Color_Index4.BackColor = Color.FromArgb(47, 47, 48);
            W11_Color_Index4.DefaultColor = Color.Black;
            W11_Color_Index4.DontShowInfo = false;
            W11_Color_Index4.Location = new Point(420, 4);
            W11_Color_Index4.Margin = new Padding(4, 3, 4, 3);
            W11_Color_Index4.Name = "W11_Color_Index4";
            W11_Color_Index4.Size = new Size(90, 20);
            W11_Color_Index4.TabIndex = 2;
            // 
            // pnl1
            // 
            pnl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnl1.BackColor = Color.FromArgb(43, 43, 43);
            pnl1.Controls.Add(Label3);
            pnl1.Controls.Add(W11_pic1);
            pnl1.Controls.Add(W11_TaskbarFrontAndFoldersOnStart_pick);
            pnl1.Controls.Add(W11_lbl1);
            pnl1.Location = new Point(3, 41);
            pnl1.Margin = new Padding(4, 3, 4, 3);
            pnl1.Name = "pnl1";
            pnl1.Size = new Size(514, 28);
            pnl1.TabIndex = 17;
            // 
            // Label3
            // 
            Label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label3.AutoEllipsis = true;
            Label3.BackColor = Color.Transparent;
            Label3.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label3.Location = new Point(31, 4);
            Label3.Name = "Label3";
            Label3.Size = new Size(14, 19);
            Label3.TabIndex = 5;
            Label3.Text = "1";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_pic1
            // 
            W11_pic1.BackColor = Color.Transparent;
            W11_pic1.Location = new Point(2, 2);
            W11_pic1.Name = "W11_pic1";
            W11_pic1.Size = new Size(24, 24);
            W11_pic1.SizeMode = PictureBoxSizeMode.CenterImage;
            W11_pic1.TabIndex = 4;
            W11_pic1.TabStop = false;
            // 
            // W11_TaskbarFrontAndFoldersOnStart_pick
            // 
            W11_TaskbarFrontAndFoldersOnStart_pick.AllowDrop = true;
            W11_TaskbarFrontAndFoldersOnStart_pick.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W11_TaskbarFrontAndFoldersOnStart_pick.BackColor = Color.FromArgb(47, 47, 48);
            W11_TaskbarFrontAndFoldersOnStart_pick.DefaultColor = Color.Black;
            W11_TaskbarFrontAndFoldersOnStart_pick.DontShowInfo = false;
            W11_TaskbarFrontAndFoldersOnStart_pick.Location = new Point(420, 4);
            W11_TaskbarFrontAndFoldersOnStart_pick.Margin = new Padding(4, 3, 4, 3);
            W11_TaskbarFrontAndFoldersOnStart_pick.Name = "W11_TaskbarFrontAndFoldersOnStart_pick";
            W11_TaskbarFrontAndFoldersOnStart_pick.Size = new Size(90, 20);
            W11_TaskbarFrontAndFoldersOnStart_pick.TabIndex = 2;
            // 
            // W11_lbl1
            // 
            W11_lbl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W11_lbl1.AutoEllipsis = true;
            W11_lbl1.BackColor = Color.Transparent;
            W11_lbl1.Font = new Font("Segoe UI", 9.0f);
            W11_lbl1.Location = new Point(48, 4);
            W11_lbl1.Name = "W11_lbl1";
            W11_lbl1.Size = new Size(365, 19);
            W11_lbl1.TabIndex = 3;
            W11_lbl1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnl3
            // 
            pnl3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnl3.BackColor = Color.FromArgb(43, 43, 43);
            pnl3.Controls.Add(Label12);
            pnl3.Controls.Add(W11_pic3);
            pnl3.Controls.Add(W11_Color_Index1);
            pnl3.Controls.Add(W11_lbl3);
            pnl3.Location = new Point(3, 101);
            pnl3.Margin = new Padding(4, 3, 4, 3);
            pnl3.Name = "pnl3";
            pnl3.Size = new Size(514, 28);
            pnl3.TabIndex = 18;
            // 
            // Label12
            // 
            Label12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label12.AutoEllipsis = true;
            Label12.BackColor = Color.Transparent;
            Label12.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label12.Location = new Point(31, 4);
            Label12.Name = "Label12";
            Label12.Size = new Size(14, 19);
            Label12.TabIndex = 6;
            Label12.Text = "3";
            Label12.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_pic3
            // 
            W11_pic3.BackColor = Color.Transparent;
            W11_pic3.Location = new Point(2, 2);
            W11_pic3.Name = "W11_pic3";
            W11_pic3.Size = new Size(24, 24);
            W11_pic3.SizeMode = PictureBoxSizeMode.CenterImage;
            W11_pic3.TabIndex = 4;
            W11_pic3.TabStop = false;
            // 
            // W11_Color_Index1
            // 
            W11_Color_Index1.AllowDrop = true;
            W11_Color_Index1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W11_Color_Index1.BackColor = Color.FromArgb(47, 47, 48);
            W11_Color_Index1.DefaultColor = Color.Black;
            W11_Color_Index1.DontShowInfo = false;
            W11_Color_Index1.Location = new Point(420, 4);
            W11_Color_Index1.Margin = new Padding(4, 3, 4, 3);
            W11_Color_Index1.Name = "W11_Color_Index1";
            W11_Color_Index1.Size = new Size(90, 20);
            W11_Color_Index1.TabIndex = 2;
            // 
            // W11_lbl3
            // 
            W11_lbl3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W11_lbl3.AutoEllipsis = true;
            W11_lbl3.BackColor = Color.Transparent;
            W11_lbl3.Font = new Font("Segoe UI", 9.0f);
            W11_lbl3.Location = new Point(48, 4);
            W11_lbl3.Name = "W11_lbl3";
            W11_lbl3.Size = new Size(365, 19);
            W11_lbl3.TabIndex = 3;
            W11_lbl3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label10
            // 
            Label10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label10.BackColor = Color.Transparent;
            Label10.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label10.Location = new Point(40, 3);
            Label10.Name = "Label10";
            Label10.Size = new Size(157, 35);
            Label10.TabIndex = 0;
            Label10.Text = "Accents";
            Label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnl2
            // 
            pnl2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnl2.BackColor = Color.FromArgb(43, 43, 43);
            pnl2.Controls.Add(Label4);
            pnl2.Controls.Add(W11_Color_Index0);
            pnl2.Controls.Add(W11_pic2);
            pnl2.Controls.Add(W11_lbl2);
            pnl2.Location = new Point(3, 71);
            pnl2.Margin = new Padding(4, 3, 4, 3);
            pnl2.Name = "pnl2";
            pnl2.Size = new Size(514, 28);
            pnl2.TabIndex = 19;
            // 
            // Label4
            // 
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label4.AutoEllipsis = true;
            Label4.BackColor = Color.Transparent;
            Label4.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label4.Location = new Point(31, 4);
            Label4.Name = "Label4";
            Label4.Size = new Size(14, 19);
            Label4.TabIndex = 6;
            Label4.Text = "2";
            Label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_Color_Index0
            // 
            W11_Color_Index0.AllowDrop = true;
            W11_Color_Index0.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W11_Color_Index0.BackColor = Color.FromArgb(47, 47, 48);
            W11_Color_Index0.DefaultColor = Color.Black;
            W11_Color_Index0.DontShowInfo = false;
            W11_Color_Index0.Location = new Point(420, 4);
            W11_Color_Index0.Margin = new Padding(4, 3, 4, 3);
            W11_Color_Index0.Name = "W11_Color_Index0";
            W11_Color_Index0.Size = new Size(90, 20);
            W11_Color_Index0.TabIndex = 2;
            // 
            // W11_pic2
            // 
            W11_pic2.BackColor = Color.Transparent;
            W11_pic2.Location = new Point(2, 2);
            W11_pic2.Name = "W11_pic2";
            W11_pic2.Size = new Size(24, 24);
            W11_pic2.SizeMode = PictureBoxSizeMode.CenterImage;
            W11_pic2.TabIndex = 4;
            W11_pic2.TabStop = false;
            // 
            // W11_lbl2
            // 
            W11_lbl2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W11_lbl2.AutoEllipsis = true;
            W11_lbl2.BackColor = Color.Transparent;
            W11_lbl2.Font = new Font("Segoe UI", 9.0f);
            W11_lbl2.Location = new Point(48, 4);
            W11_lbl2.Name = "W11_lbl2";
            W11_lbl2.Size = new Size(365, 19);
            W11_lbl2.TabIndex = 3;
            W11_lbl2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnl5
            // 
            pnl5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnl5.BackColor = Color.FromArgb(43, 43, 43);
            pnl5.Controls.Add(Label34);
            pnl5.Controls.Add(W11_pic5);
            pnl5.Controls.Add(W11_lbl5);
            pnl5.Controls.Add(W11_Color_Index3);
            pnl5.Location = new Point(3, 161);
            pnl5.Margin = new Padding(4, 3, 4, 3);
            pnl5.Name = "pnl5";
            pnl5.Size = new Size(514, 28);
            pnl5.TabIndex = 20;
            // 
            // Label34
            // 
            Label34.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label34.AutoEllipsis = true;
            Label34.BackColor = Color.Transparent;
            Label34.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label34.Location = new Point(31, 4);
            Label34.Name = "Label34";
            Label34.Size = new Size(14, 19);
            Label34.TabIndex = 6;
            Label34.Text = "5";
            Label34.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_pic5
            // 
            W11_pic5.BackColor = Color.Transparent;
            W11_pic5.Location = new Point(2, 2);
            W11_pic5.Name = "W11_pic5";
            W11_pic5.Size = new Size(24, 24);
            W11_pic5.SizeMode = PictureBoxSizeMode.CenterImage;
            W11_pic5.TabIndex = 4;
            W11_pic5.TabStop = false;
            // 
            // W11_lbl5
            // 
            W11_lbl5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W11_lbl5.AutoEllipsis = true;
            W11_lbl5.BackColor = Color.Transparent;
            W11_lbl5.Font = new Font("Segoe UI", 9.0f);
            W11_lbl5.Location = new Point(48, 4);
            W11_lbl5.Name = "W11_lbl5";
            W11_lbl5.Size = new Size(365, 19);
            W11_lbl5.TabIndex = 3;
            W11_lbl5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_Color_Index3
            // 
            W11_Color_Index3.AllowDrop = true;
            W11_Color_Index3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W11_Color_Index3.BackColor = Color.FromArgb(47, 47, 48);
            W11_Color_Index3.DefaultColor = Color.Black;
            W11_Color_Index3.DontShowInfo = false;
            W11_Color_Index3.Location = new Point(420, 4);
            W11_Color_Index3.Margin = new Padding(4, 3, 4, 3);
            W11_Color_Index3.Name = "W11_Color_Index3";
            W11_Color_Index3.Size = new Size(90, 20);
            W11_Color_Index3.TabIndex = 2;
            // 
            // GroupBox5
            // 
            GroupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox5.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox5.Controls.Add(GroupBox6);
            GroupBox5.Controls.Add(GroupBox4);
            GroupBox5.Controls.Add(GroupBox18);
            GroupBox5.Controls.Add(GroupBox24);
            GroupBox5.Controls.Add(PictureBox17);
            GroupBox5.Controls.Add(Label17);
            GroupBox5.Location = new Point(0, 0);
            GroupBox5.Margin = new Padding(4, 3, 4, 3);
            GroupBox5.Name = "GroupBox5";
            GroupBox5.Size = new Size(520, 133);
            GroupBox5.TabIndex = 11;
            // 
            // GroupBox6
            // 
            GroupBox6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox6.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox6.Controls.Add(W11_Accent_Taskbar);
            GroupBox6.Controls.Add(W11_Accent_StartTaskbar);
            GroupBox6.Controls.Add(W11_Accent_None);
            GroupBox6.Controls.Add(PictureBox19);
            GroupBox6.Controls.Add(Label19);
            GroupBox6.Location = new Point(3, 101);
            GroupBox6.Margin = new Padding(4, 3, 4, 3);
            GroupBox6.Name = "GroupBox6";
            GroupBox6.Size = new Size(514, 29);
            GroupBox6.TabIndex = 16;
            // 
            // W11_Accent_Taskbar
            // 
            W11_Accent_Taskbar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W11_Accent_Taskbar.BackColor = Color.FromArgb(15, 15, 15);
            W11_Accent_Taskbar.Checked = false;
            W11_Accent_Taskbar.Font = new Font("Segoe UI", 9.0f);
            W11_Accent_Taskbar.ForeColor = Color.White;
            W11_Accent_Taskbar.Image = null;
            W11_Accent_Taskbar.Location = new Point(248, 3);
            W11_Accent_Taskbar.Name = "W11_Accent_Taskbar";
            W11_Accent_Taskbar.ShowText = true;
            W11_Accent_Taskbar.Size = new Size(76, 23);
            W11_Accent_Taskbar.TabIndex = 23;
            W11_Accent_Taskbar.Text = "Taskbar";
            // 
            // W11_Accent_StartTaskbar
            // 
            W11_Accent_StartTaskbar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W11_Accent_StartTaskbar.BackColor = Color.FromArgb(15, 15, 15);
            W11_Accent_StartTaskbar.Checked = false;
            W11_Accent_StartTaskbar.Font = new Font("Segoe UI", 9.0f);
            W11_Accent_StartTaskbar.ForeColor = Color.White;
            W11_Accent_StartTaskbar.Image = null;
            W11_Accent_StartTaskbar.Location = new Point(325, 3);
            W11_Accent_StartTaskbar.Name = "W11_Accent_StartTaskbar";
            W11_Accent_StartTaskbar.ShowText = true;
            W11_Accent_StartTaskbar.Size = new Size(186, 23);
            W11_Accent_StartTaskbar.TabIndex = 22;
            W11_Accent_StartTaskbar.Text = "Start, taskbar & action Center";
            // 
            // W11_Accent_None
            // 
            W11_Accent_None.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W11_Accent_None.BackColor = Color.FromArgb(15, 15, 15);
            W11_Accent_None.Checked = false;
            W11_Accent_None.Font = new Font("Segoe UI", 9.0f);
            W11_Accent_None.ForeColor = Color.White;
            W11_Accent_None.Image = null;
            W11_Accent_None.Location = new Point(181, 3);
            W11_Accent_None.Name = "W11_Accent_None";
            W11_Accent_None.ShowText = true;
            W11_Accent_None.Size = new Size(66, 23);
            W11_Accent_None.TabIndex = 21;
            W11_Accent_None.Text = "Nothing";
            // 
            // PictureBox19
            // 
            PictureBox19.BackColor = Color.Transparent;
            PictureBox19.Image = (Image)resources.GetObject("PictureBox19.Image");
            PictureBox19.Location = new Point(2, 2);
            PictureBox19.Name = "PictureBox19";
            PictureBox19.Size = new Size(24, 24);
            PictureBox19.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox19.TabIndex = 4;
            PictureBox19.TabStop = false;
            // 
            // Label19
            // 
            Label19.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label19.AutoEllipsis = true;
            Label19.BackColor = Color.Transparent;
            Label19.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label19.Location = new Point(30, 3);
            Label19.Name = "Label19";
            Label19.Size = new Size(150, 22);
            Label19.TabIndex = 3;
            Label19.Text = "Apply accent color on:";
            Label19.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox4
            // 
            GroupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox4.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox4.Controls.Add(PictureBox2);
            GroupBox4.Controls.Add(Label2);
            GroupBox4.Controls.Add(W11_WinMode_Toggle);
            GroupBox4.Location = new Point(3, 41);
            GroupBox4.Margin = new Padding(4, 3, 4, 3);
            GroupBox4.Name = "GroupBox4";
            GroupBox4.Size = new Size(262, 28);
            GroupBox4.TabIndex = 10;
            // 
            // PictureBox2
            // 
            PictureBox2.BackColor = Color.Transparent;
            PictureBox2.BackgroundImageLayout = ImageLayout.Center;
            PictureBox2.Image = (Image)resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(2, 2);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(24, 24);
            PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox2.TabIndex = 4;
            PictureBox2.TabStop = false;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label2.AutoEllipsis = true;
            Label2.BackColor = Color.Transparent;
            Label2.Font = new Font("Segoe UI", 9.0f);
            Label2.Location = new Point(30, 4);
            Label2.Name = "Label2";
            Label2.Size = new Size(177, 20);
            Label2.TabIndex = 7;
            Label2.Text = "Windows mode";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_WinMode_Toggle
            // 
            W11_WinMode_Toggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W11_WinMode_Toggle.BackColor = Color.FromArgb(43, 43, 43);
            W11_WinMode_Toggle.Checked = false;
            W11_WinMode_Toggle.DarkLight_Toggler = true;
            W11_WinMode_Toggle.Location = new Point(218, 4);
            W11_WinMode_Toggle.Name = "W11_WinMode_Toggle";
            W11_WinMode_Toggle.Size = new Size(40, 20);
            W11_WinMode_Toggle.TabIndex = 8;
            // 
            // GroupBox18
            // 
            GroupBox18.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox18.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox18.Controls.Add(W11_Transparency_Toggle);
            GroupBox18.Controls.Add(PictureBox18);
            GroupBox18.Controls.Add(Label9);
            GroupBox18.Location = new Point(3, 71);
            GroupBox18.Margin = new Padding(4, 3, 4, 3);
            GroupBox18.Name = "GroupBox18";
            GroupBox18.Size = new Size(514, 28);
            GroupBox18.TabIndex = 9;
            // 
            // W11_Transparency_Toggle
            // 
            W11_Transparency_Toggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W11_Transparency_Toggle.BackColor = Color.FromArgb(43, 43, 43);
            W11_Transparency_Toggle.Checked = false;
            W11_Transparency_Toggle.DarkLight_Toggler = false;
            W11_Transparency_Toggle.Location = new Point(470, 4);
            W11_Transparency_Toggle.Name = "W11_Transparency_Toggle";
            W11_Transparency_Toggle.Size = new Size(40, 20);
            W11_Transparency_Toggle.TabIndex = 16;
            // 
            // PictureBox18
            // 
            PictureBox18.BackColor = Color.Transparent;
            PictureBox18.Image = (Image)resources.GetObject("PictureBox18.Image");
            PictureBox18.Location = new Point(2, 2);
            PictureBox18.Name = "PictureBox18";
            PictureBox18.Size = new Size(24, 24);
            PictureBox18.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox18.TabIndex = 4;
            PictureBox18.TabStop = false;
            // 
            // Label9
            // 
            Label9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label9.AutoEllipsis = true;
            Label9.BackColor = Color.Transparent;
            Label9.Font = new Font("Segoe UI", 9.0f);
            Label9.Location = new Point(30, 4);
            Label9.Name = "Label9";
            Label9.Size = new Size(434, 20);
            Label9.TabIndex = 13;
            Label9.Text = "Transparency (Mica/Acrylic)";
            Label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox24
            // 
            GroupBox24.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GroupBox24.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox24.Controls.Add(PictureBox20);
            GroupBox24.Controls.Add(W11_AppMode_Toggle);
            GroupBox24.Controls.Add(Label7);
            GroupBox24.Location = new Point(267, 41);
            GroupBox24.Margin = new Padding(4, 3, 4, 3);
            GroupBox24.Name = "GroupBox24";
            GroupBox24.Size = new Size(250, 28);
            GroupBox24.TabIndex = 8;
            // 
            // PictureBox20
            // 
            PictureBox20.BackColor = Color.Transparent;
            PictureBox20.Image = (Image)resources.GetObject("PictureBox20.Image");
            PictureBox20.Location = new Point(2, 2);
            PictureBox20.Name = "PictureBox20";
            PictureBox20.Size = new Size(24, 24);
            PictureBox20.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox20.TabIndex = 4;
            PictureBox20.TabStop = false;
            // 
            // W11_AppMode_Toggle
            // 
            W11_AppMode_Toggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W11_AppMode_Toggle.BackColor = Color.FromArgb(43, 43, 43);
            W11_AppMode_Toggle.Checked = false;
            W11_AppMode_Toggle.DarkLight_Toggler = true;
            W11_AppMode_Toggle.Location = new Point(206, 4);
            W11_AppMode_Toggle.Name = "W11_AppMode_Toggle";
            W11_AppMode_Toggle.Size = new Size(40, 20);
            W11_AppMode_Toggle.TabIndex = 11;
            // 
            // Label7
            // 
            Label7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label7.AutoEllipsis = true;
            Label7.BackColor = Color.Transparent;
            Label7.Font = new Font("Segoe UI", 9.0f);
            Label7.Location = new Point(30, 4);
            Label7.Name = "Label7";
            Label7.Size = new Size(163, 20);
            Label7.TabIndex = 10;
            Label7.Text = "App mode";
            Label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox17
            // 
            PictureBox17.BackColor = Color.Transparent;
            PictureBox17.Image = (Image)resources.GetObject("PictureBox17.Image");
            PictureBox17.Location = new Point(3, 3);
            PictureBox17.Name = "PictureBox17";
            PictureBox17.Size = new Size(35, 35);
            PictureBox17.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox17.TabIndex = 1;
            PictureBox17.TabStop = false;
            // 
            // Label17
            // 
            Label17.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label17.BackColor = Color.Transparent;
            Label17.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label17.Location = new Point(38, 3);
            Label17.Name = "Label17";
            Label17.Size = new Size(479, 35);
            Label17.TabIndex = 0;
            Label17.Text = "Toggles";
            Label17.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox1.Controls.Add(W11_Button8);
            GroupBox1.Controls.Add(W11_ShowAccentOnTitlebarAndBorders_Toggle);
            GroupBox1.Controls.Add(GroupBox20);
            GroupBox1.Controls.Add(PictureBox1);
            GroupBox1.Controls.Add(Label1);
            GroupBox1.Controls.Add(GroupBox9);
            GroupBox1.Location = new Point(0, 137);
            GroupBox1.Margin = new Padding(4, 3, 4, 3);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(520, 72);
            GroupBox1.TabIndex = 2;
            // 
            // W11_Button8
            // 
            W11_Button8.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            W11_Button8.BackColor = Color.FromArgb(43, 43, 43);
            W11_Button8.DrawOnGlass = false;
            W11_Button8.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            W11_Button8.ForeColor = Color.White;
            W11_Button8.Image = null;
            W11_Button8.LineColor = Color.FromArgb(199, 49, 61);
            W11_Button8.Location = new Point(448, 10);
            W11_Button8.Name = "W11_Button8";
            W11_Button8.Size = new Size(20, 20);
            W11_Button8.TabIndex = 27;
            W11_Button8.Text = "?";
            W11_Button8.UseVisualStyleBackColor = false;
            // 
            // W11_ShowAccentOnTitlebarAndBorders_Toggle
            // 
            W11_ShowAccentOnTitlebarAndBorders_Toggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W11_ShowAccentOnTitlebarAndBorders_Toggle.BackColor = Color.FromArgb(43, 43, 43);
            W11_ShowAccentOnTitlebarAndBorders_Toggle.Checked = false;
            W11_ShowAccentOnTitlebarAndBorders_Toggle.DarkLight_Toggler = false;
            W11_ShowAccentOnTitlebarAndBorders_Toggle.Location = new Point(474, 10);
            W11_ShowAccentOnTitlebarAndBorders_Toggle.Name = "W11_ShowAccentOnTitlebarAndBorders_Toggle";
            W11_ShowAccentOnTitlebarAndBorders_Toggle.Size = new Size(40, 20);
            W11_ShowAccentOnTitlebarAndBorders_Toggle.TabIndex = 6;
            // 
            // GroupBox20
            // 
            GroupBox20.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GroupBox20.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox20.Controls.Add(PictureBox11);
            GroupBox20.Controls.Add(Label11);
            GroupBox20.Controls.Add(W11_InactiveTitlebar_pick);
            GroupBox20.Location = new Point(267, 41);
            GroupBox20.Margin = new Padding(4, 3, 4, 3);
            GroupBox20.Name = "GroupBox20";
            GroupBox20.Size = new Size(250, 28);
            GroupBox20.TabIndex = 6;
            // 
            // PictureBox11
            // 
            PictureBox11.Image = (Image)resources.GetObject("PictureBox11.Image");
            PictureBox11.Location = new Point(2, 2);
            PictureBox11.Name = "PictureBox11";
            PictureBox11.Size = new Size(24, 24);
            PictureBox11.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox11.TabIndex = 4;
            PictureBox11.TabStop = false;
            // 
            // Label11
            // 
            Label11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label11.AutoEllipsis = true;
            Label11.BackColor = Color.Transparent;
            Label11.Font = new Font("Segoe UI", 9.0f);
            Label11.Location = new Point(30, 4);
            Label11.Name = "Label11";
            Label11.Size = new Size(119, 20);
            Label11.TabIndex = 3;
            Label11.Text = "Inactive titlebar";
            Label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_InactiveTitlebar_pick
            // 
            W11_InactiveTitlebar_pick.AllowDrop = true;
            W11_InactiveTitlebar_pick.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W11_InactiveTitlebar_pick.BackColor = Color.FromArgb(47, 47, 48);
            W11_InactiveTitlebar_pick.DefaultColor = Color.Black;
            W11_InactiveTitlebar_pick.DontShowInfo = false;
            W11_InactiveTitlebar_pick.Location = new Point(156, 4);
            W11_InactiveTitlebar_pick.Margin = new Padding(4, 3, 4, 3);
            W11_InactiveTitlebar_pick.Name = "W11_InactiveTitlebar_pick";
            W11_InactiveTitlebar_pick.Size = new Size(90, 20);
            W11_InactiveTitlebar_pick.TabIndex = 2;
            // 
            // PictureBox1
            // 
            PictureBox1.BackColor = Color.Transparent;
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(3, 3);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(35, 35);
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox1.TabIndex = 1;
            PictureBox1.TabStop = false;
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label1.Location = new Point(40, 3);
            Label1.Name = "Label1";
            Label1.Size = new Size(401, 35);
            Label1.TabIndex = 0;
            Label1.Text = "Titlebars";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox9
            // 
            GroupBox9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox9.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox9.Controls.Add(PictureBox5);
            GroupBox9.Controls.Add(Label5);
            GroupBox9.Controls.Add(W11_ActiveTitlebar_pick);
            GroupBox9.Location = new Point(3, 41);
            GroupBox9.Margin = new Padding(4, 3, 4, 3);
            GroupBox9.Name = "GroupBox9";
            GroupBox9.Size = new Size(262, 28);
            GroupBox9.TabIndex = 5;
            // 
            // PictureBox5
            // 
            PictureBox5.BackColor = Color.Transparent;
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(2, 2);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(24, 24);
            PictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox5.TabIndex = 4;
            PictureBox5.TabStop = false;
            // 
            // Label5
            // 
            Label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label5.AutoEllipsis = true;
            Label5.BackColor = Color.Transparent;
            Label5.Font = new Font("Segoe UI", 9.0f);
            Label5.Location = new Point(30, 5);
            Label5.Name = "Label5";
            Label5.Size = new Size(131, 19);
            Label5.TabIndex = 3;
            Label5.Text = "Active titlebar";
            Label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W11_ActiveTitlebar_pick
            // 
            W11_ActiveTitlebar_pick.AllowDrop = true;
            W11_ActiveTitlebar_pick.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W11_ActiveTitlebar_pick.BackColor = Color.FromArgb(47, 47, 48);
            W11_ActiveTitlebar_pick.DefaultColor = Color.Black;
            W11_ActiveTitlebar_pick.DontShowInfo = false;
            W11_ActiveTitlebar_pick.Location = new Point(168, 4);
            W11_ActiveTitlebar_pick.Margin = new Padding(4, 3, 4, 3);
            W11_ActiveTitlebar_pick.Name = "W11_ActiveTitlebar_pick";
            W11_ActiveTitlebar_pick.Size = new Size(90, 20);
            W11_ActiveTitlebar_pick.TabIndex = 2;
            // 
            // TabPage2
            // 
            TabPage2.BackColor = Color.FromArgb(25, 25, 25);
            TabPage2.Controls.Add(Panel1);
            TabPage2.Location = new Point(4, 24);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(3);
            TabPage2.Size = new Size(529, 517);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "W10";
            // 
            // Panel1
            // 
            Panel1.BackColor = Color.Transparent;
            Panel1.Controls.Add(GroupBox2);
            Panel1.Controls.Add(GroupBox37);
            Panel1.Controls.Add(GroupBox44);
            Panel1.Dock = DockStyle.Fill;
            Panel1.Location = new Point(3, 3);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(523, 511);
            Panel1.TabIndex = 19;
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox2.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox2.Controls.Add(Button38);
            GroupBox2.Controls.Add(GroupBox8);
            GroupBox2.Controls.Add(GroupBox16);
            GroupBox2.Controls.Add(GroupBox25);
            GroupBox2.Controls.Add(GroupBox27);
            GroupBox2.Controls.Add(PictureBox7);
            GroupBox2.Controls.Add(GroupBox28);
            GroupBox2.Controls.Add(GroupBox31);
            GroupBox2.Controls.Add(GroupBox34);
            GroupBox2.Controls.Add(Label49);
            GroupBox2.Controls.Add(GroupBox35);
            GroupBox2.Controls.Add(GroupBox36);
            GroupBox2.Location = new Point(0, 213);
            GroupBox2.Margin = new Padding(4, 3, 4, 3);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(520, 313);
            GroupBox2.TabIndex = 6;
            // 
            // Button38
            // 
            Button38.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button38.BackColor = Color.FromArgb(43, 43, 43);
            Button38.DrawOnGlass = false;
            Button38.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Button38.ForeColor = Color.White;
            Button38.Image = null;
            Button38.LineColor = Color.FromArgb(0, 81, 210);
            Button38.Location = new Point(300, 9);
            Button38.Name = "Button38";
            Button38.Size = new Size(214, 22);
            Button38.TabIndex = 30;
            Button38.Text = "Copycat from Windows 11 presets";
            Button38.UseVisualStyleBackColor = false;
            // 
            // GroupBox8
            // 
            GroupBox8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox8.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox8.Controls.Add(Label13);
            GroupBox8.Controls.Add(W10_pic9);
            GroupBox8.Controls.Add(W10_lbl9);
            GroupBox8.Controls.Add(W10_Color_Index7);
            GroupBox8.Location = new Point(3, 281);
            GroupBox8.Margin = new Padding(4, 3, 4, 3);
            GroupBox8.Name = "GroupBox8";
            GroupBox8.Size = new Size(514, 28);
            GroupBox8.TabIndex = 25;
            // 
            // Label13
            // 
            Label13.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label13.AutoEllipsis = true;
            Label13.BackColor = Color.Transparent;
            Label13.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label13.Location = new Point(31, 4);
            Label13.Name = "Label13";
            Label13.Size = new Size(14, 19);
            Label13.TabIndex = 6;
            Label13.Text = "9";
            Label13.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_pic9
            // 
            W10_pic9.BackColor = Color.Transparent;
            W10_pic9.Image = Properties.Resources.Mini_Undefined;
            W10_pic9.Location = new Point(2, 2);
            W10_pic9.Name = "W10_pic9";
            W10_pic9.Size = new Size(24, 24);
            W10_pic9.SizeMode = PictureBoxSizeMode.CenterImage;
            W10_pic9.TabIndex = 4;
            W10_pic9.TabStop = false;
            // 
            // W10_lbl9
            // 
            W10_lbl9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W10_lbl9.AutoEllipsis = true;
            W10_lbl9.BackColor = Color.Transparent;
            W10_lbl9.Font = new Font("Segoe UI", 9.0f);
            W10_lbl9.Location = new Point(48, 4);
            W10_lbl9.Name = "W10_lbl9";
            W10_lbl9.Size = new Size(365, 19);
            W10_lbl9.TabIndex = 3;
            W10_lbl9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_Color_Index7
            // 
            W10_Color_Index7.AllowDrop = true;
            W10_Color_Index7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W10_Color_Index7.BackColor = Color.FromArgb(47, 47, 48);
            W10_Color_Index7.DefaultColor = Color.Black;
            W10_Color_Index7.DontShowInfo = false;
            W10_Color_Index7.Location = new Point(420, 4);
            W10_Color_Index7.Margin = new Padding(4, 3, 4, 3);
            W10_Color_Index7.Name = "W10_Color_Index7";
            W10_Color_Index7.Size = new Size(90, 20);
            W10_Color_Index7.TabIndex = 2;
            // 
            // GroupBox16
            // 
            GroupBox16.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox16.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox16.Controls.Add(Label43);
            GroupBox16.Controls.Add(W10_pic8);
            GroupBox16.Controls.Add(W10_lbl8);
            GroupBox16.Controls.Add(W10_Color_Index6);
            GroupBox16.Location = new Point(3, 251);
            GroupBox16.Margin = new Padding(4, 3, 4, 3);
            GroupBox16.Name = "GroupBox16";
            GroupBox16.Size = new Size(514, 28);
            GroupBox16.TabIndex = 23;
            // 
            // Label43
            // 
            Label43.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label43.AutoEllipsis = true;
            Label43.BackColor = Color.Transparent;
            Label43.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label43.Location = new Point(31, 4);
            Label43.Name = "Label43";
            Label43.Size = new Size(14, 19);
            Label43.TabIndex = 6;
            Label43.Text = "8";
            Label43.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_pic8
            // 
            W10_pic8.BackColor = Color.Transparent;
            W10_pic8.Location = new Point(2, 2);
            W10_pic8.Name = "W10_pic8";
            W10_pic8.Size = new Size(24, 24);
            W10_pic8.SizeMode = PictureBoxSizeMode.CenterImage;
            W10_pic8.TabIndex = 4;
            W10_pic8.TabStop = false;
            // 
            // W10_lbl8
            // 
            W10_lbl8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W10_lbl8.AutoEllipsis = true;
            W10_lbl8.BackColor = Color.Transparent;
            W10_lbl8.Font = new Font("Segoe UI", 9.0f);
            W10_lbl8.Location = new Point(48, 4);
            W10_lbl8.Name = "W10_lbl8";
            W10_lbl8.Size = new Size(365, 19);
            W10_lbl8.TabIndex = 3;
            W10_lbl8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_Color_Index6
            // 
            W10_Color_Index6.AllowDrop = true;
            W10_Color_Index6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W10_Color_Index6.BackColor = Color.FromArgb(47, 47, 48);
            W10_Color_Index6.DefaultColor = Color.Black;
            W10_Color_Index6.DontShowInfo = false;
            W10_Color_Index6.Location = new Point(420, 4);
            W10_Color_Index6.Margin = new Padding(4, 3, 4, 3);
            W10_Color_Index6.Name = "W10_Color_Index6";
            W10_Color_Index6.Size = new Size(90, 20);
            W10_Color_Index6.TabIndex = 2;
            // 
            // GroupBox25
            // 
            GroupBox25.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox25.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox25.Controls.Add(Label44);
            GroupBox25.Controls.Add(W10_pic7);
            GroupBox25.Controls.Add(W10_lbl7);
            GroupBox25.Controls.Add(W10_Color_Index5);
            GroupBox25.Location = new Point(3, 221);
            GroupBox25.Margin = new Padding(4, 3, 4, 3);
            GroupBox25.Name = "GroupBox25";
            GroupBox25.Size = new Size(514, 28);
            GroupBox25.TabIndex = 22;
            // 
            // Label44
            // 
            Label44.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label44.AutoEllipsis = true;
            Label44.BackColor = Color.Transparent;
            Label44.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label44.Location = new Point(31, 4);
            Label44.Name = "Label44";
            Label44.Size = new Size(14, 19);
            Label44.TabIndex = 6;
            Label44.Text = "7";
            Label44.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_pic7
            // 
            W10_pic7.BackColor = Color.Transparent;
            W10_pic7.Location = new Point(2, 2);
            W10_pic7.Name = "W10_pic7";
            W10_pic7.Size = new Size(24, 24);
            W10_pic7.SizeMode = PictureBoxSizeMode.CenterImage;
            W10_pic7.TabIndex = 4;
            W10_pic7.TabStop = false;
            // 
            // W10_lbl7
            // 
            W10_lbl7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W10_lbl7.AutoEllipsis = true;
            W10_lbl7.BackColor = Color.Transparent;
            W10_lbl7.Font = new Font("Segoe UI", 9.0f);
            W10_lbl7.Location = new Point(48, 4);
            W10_lbl7.Name = "W10_lbl7";
            W10_lbl7.Size = new Size(365, 19);
            W10_lbl7.TabIndex = 3;
            W10_lbl7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_Color_Index5
            // 
            W10_Color_Index5.AllowDrop = true;
            W10_Color_Index5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W10_Color_Index5.BackColor = Color.FromArgb(47, 47, 48);
            W10_Color_Index5.DefaultColor = Color.Black;
            W10_Color_Index5.DontShowInfo = false;
            W10_Color_Index5.Location = new Point(420, 4);
            W10_Color_Index5.Margin = new Padding(4, 3, 4, 3);
            W10_Color_Index5.Name = "W10_Color_Index5";
            W10_Color_Index5.Size = new Size(90, 20);
            W10_Color_Index5.TabIndex = 2;
            // 
            // GroupBox27
            // 
            GroupBox27.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox27.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox27.Controls.Add(Label45);
            GroupBox27.Controls.Add(W10_pic4);
            GroupBox27.Controls.Add(W10_lbl4);
            GroupBox27.Controls.Add(W10_Color_Index2);
            GroupBox27.Location = new Point(3, 131);
            GroupBox27.Margin = new Padding(4, 3, 4, 3);
            GroupBox27.Name = "GroupBox27";
            GroupBox27.Size = new Size(514, 28);
            GroupBox27.TabIndex = 24;
            // 
            // Label45
            // 
            Label45.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label45.AutoEllipsis = true;
            Label45.BackColor = Color.Transparent;
            Label45.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label45.Location = new Point(31, 4);
            Label45.Name = "Label45";
            Label45.Size = new Size(14, 19);
            Label45.TabIndex = 6;
            Label45.Text = "4";
            Label45.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_pic4
            // 
            W10_pic4.BackColor = Color.Transparent;
            W10_pic4.Location = new Point(2, 2);
            W10_pic4.Name = "W10_pic4";
            W10_pic4.Size = new Size(24, 24);
            W10_pic4.SizeMode = PictureBoxSizeMode.CenterImage;
            W10_pic4.TabIndex = 4;
            W10_pic4.TabStop = false;
            // 
            // W10_lbl4
            // 
            W10_lbl4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W10_lbl4.AutoEllipsis = true;
            W10_lbl4.BackColor = Color.Transparent;
            W10_lbl4.Font = new Font("Segoe UI", 9.0f);
            W10_lbl4.Location = new Point(48, 4);
            W10_lbl4.Name = "W10_lbl4";
            W10_lbl4.Size = new Size(365, 19);
            W10_lbl4.TabIndex = 3;
            W10_lbl4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_Color_Index2
            // 
            W10_Color_Index2.AllowDrop = true;
            W10_Color_Index2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W10_Color_Index2.BackColor = Color.FromArgb(47, 47, 48);
            W10_Color_Index2.DefaultColor = Color.Black;
            W10_Color_Index2.DontShowInfo = false;
            W10_Color_Index2.Location = new Point(420, 4);
            W10_Color_Index2.Margin = new Padding(4, 3, 4, 3);
            W10_Color_Index2.Name = "W10_Color_Index2";
            W10_Color_Index2.Size = new Size(90, 20);
            W10_Color_Index2.TabIndex = 2;
            // 
            // PictureBox7
            // 
            PictureBox7.BackColor = Color.Transparent;
            PictureBox7.Image = (Image)resources.GetObject("PictureBox7.Image");
            PictureBox7.Location = new Point(3, 3);
            PictureBox7.Name = "PictureBox7";
            PictureBox7.Size = new Size(35, 35);
            PictureBox7.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox7.TabIndex = 1;
            PictureBox7.TabStop = false;
            // 
            // GroupBox28
            // 
            GroupBox28.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox28.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox28.Controls.Add(Label46);
            GroupBox28.Controls.Add(W10_pic6);
            GroupBox28.Controls.Add(W10_lbl6);
            GroupBox28.Controls.Add(W10_Color_Index4);
            GroupBox28.Location = new Point(3, 191);
            GroupBox28.Margin = new Padding(4, 3, 4, 3);
            GroupBox28.Name = "GroupBox28";
            GroupBox28.Size = new Size(514, 28);
            GroupBox28.TabIndex = 21;
            // 
            // Label46
            // 
            Label46.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label46.AutoEllipsis = true;
            Label46.BackColor = Color.Transparent;
            Label46.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label46.Location = new Point(31, 4);
            Label46.Name = "Label46";
            Label46.Size = new Size(14, 19);
            Label46.TabIndex = 6;
            Label46.Text = "6";
            Label46.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_pic6
            // 
            W10_pic6.BackColor = Color.Transparent;
            W10_pic6.Location = new Point(2, 2);
            W10_pic6.Name = "W10_pic6";
            W10_pic6.Size = new Size(24, 24);
            W10_pic6.SizeMode = PictureBoxSizeMode.CenterImage;
            W10_pic6.TabIndex = 4;
            W10_pic6.TabStop = false;
            // 
            // W10_lbl6
            // 
            W10_lbl6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W10_lbl6.AutoEllipsis = true;
            W10_lbl6.BackColor = Color.Transparent;
            W10_lbl6.Font = new Font("Segoe UI", 9.0f);
            W10_lbl6.Location = new Point(48, 4);
            W10_lbl6.Name = "W10_lbl6";
            W10_lbl6.Size = new Size(365, 19);
            W10_lbl6.TabIndex = 3;
            W10_lbl6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_Color_Index4
            // 
            W10_Color_Index4.AllowDrop = true;
            W10_Color_Index4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W10_Color_Index4.BackColor = Color.FromArgb(47, 47, 48);
            W10_Color_Index4.DefaultColor = Color.Black;
            W10_Color_Index4.DontShowInfo = false;
            W10_Color_Index4.Location = new Point(420, 4);
            W10_Color_Index4.Margin = new Padding(4, 3, 4, 3);
            W10_Color_Index4.Name = "W10_Color_Index4";
            W10_Color_Index4.Size = new Size(90, 20);
            W10_Color_Index4.TabIndex = 2;
            // 
            // GroupBox31
            // 
            GroupBox31.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox31.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox31.Controls.Add(Label47);
            GroupBox31.Controls.Add(W10_pic1);
            GroupBox31.Controls.Add(W10_TaskbarFrontAndFoldersOnStart_pick);
            GroupBox31.Controls.Add(W10_lbl1);
            GroupBox31.Location = new Point(3, 41);
            GroupBox31.Margin = new Padding(4, 3, 4, 3);
            GroupBox31.Name = "GroupBox31";
            GroupBox31.Size = new Size(514, 28);
            GroupBox31.TabIndex = 17;
            // 
            // Label47
            // 
            Label47.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label47.AutoEllipsis = true;
            Label47.BackColor = Color.Transparent;
            Label47.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label47.Location = new Point(31, 4);
            Label47.Name = "Label47";
            Label47.Size = new Size(14, 19);
            Label47.TabIndex = 5;
            Label47.Text = "1";
            Label47.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_pic1
            // 
            W10_pic1.BackColor = Color.Transparent;
            W10_pic1.Location = new Point(2, 2);
            W10_pic1.Name = "W10_pic1";
            W10_pic1.Size = new Size(24, 24);
            W10_pic1.SizeMode = PictureBoxSizeMode.CenterImage;
            W10_pic1.TabIndex = 4;
            W10_pic1.TabStop = false;
            // 
            // W10_TaskbarFrontAndFoldersOnStart_pick
            // 
            W10_TaskbarFrontAndFoldersOnStart_pick.AllowDrop = true;
            W10_TaskbarFrontAndFoldersOnStart_pick.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W10_TaskbarFrontAndFoldersOnStart_pick.BackColor = Color.FromArgb(47, 47, 48);
            W10_TaskbarFrontAndFoldersOnStart_pick.DefaultColor = Color.Black;
            W10_TaskbarFrontAndFoldersOnStart_pick.DontShowInfo = false;
            W10_TaskbarFrontAndFoldersOnStart_pick.Location = new Point(420, 4);
            W10_TaskbarFrontAndFoldersOnStart_pick.Margin = new Padding(4, 3, 4, 3);
            W10_TaskbarFrontAndFoldersOnStart_pick.Name = "W10_TaskbarFrontAndFoldersOnStart_pick";
            W10_TaskbarFrontAndFoldersOnStart_pick.Size = new Size(90, 20);
            W10_TaskbarFrontAndFoldersOnStart_pick.TabIndex = 2;
            // 
            // W10_lbl1
            // 
            W10_lbl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W10_lbl1.AutoEllipsis = true;
            W10_lbl1.BackColor = Color.Transparent;
            W10_lbl1.Font = new Font("Segoe UI", 9.0f);
            W10_lbl1.Location = new Point(48, 4);
            W10_lbl1.Name = "W10_lbl1";
            W10_lbl1.Size = new Size(365, 19);
            W10_lbl1.TabIndex = 3;
            W10_lbl1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox34
            // 
            GroupBox34.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox34.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox34.Controls.Add(Label48);
            GroupBox34.Controls.Add(W10_pic3);
            GroupBox34.Controls.Add(W10_Color_Index1);
            GroupBox34.Controls.Add(W10_lbl3);
            GroupBox34.Location = new Point(3, 101);
            GroupBox34.Margin = new Padding(4, 3, 4, 3);
            GroupBox34.Name = "GroupBox34";
            GroupBox34.Size = new Size(514, 28);
            GroupBox34.TabIndex = 18;
            // 
            // Label48
            // 
            Label48.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label48.AutoEllipsis = true;
            Label48.BackColor = Color.Transparent;
            Label48.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label48.Location = new Point(31, 4);
            Label48.Name = "Label48";
            Label48.Size = new Size(14, 19);
            Label48.TabIndex = 6;
            Label48.Text = "3";
            Label48.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_pic3
            // 
            W10_pic3.BackColor = Color.Transparent;
            W10_pic3.Location = new Point(2, 2);
            W10_pic3.Name = "W10_pic3";
            W10_pic3.Size = new Size(24, 24);
            W10_pic3.SizeMode = PictureBoxSizeMode.CenterImage;
            W10_pic3.TabIndex = 4;
            W10_pic3.TabStop = false;
            // 
            // W10_Color_Index1
            // 
            W10_Color_Index1.AllowDrop = true;
            W10_Color_Index1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W10_Color_Index1.BackColor = Color.FromArgb(47, 47, 48);
            W10_Color_Index1.DefaultColor = Color.Black;
            W10_Color_Index1.DontShowInfo = false;
            W10_Color_Index1.Location = new Point(420, 4);
            W10_Color_Index1.Margin = new Padding(4, 3, 4, 3);
            W10_Color_Index1.Name = "W10_Color_Index1";
            W10_Color_Index1.Size = new Size(90, 20);
            W10_Color_Index1.TabIndex = 2;
            // 
            // W10_lbl3
            // 
            W10_lbl3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W10_lbl3.AutoEllipsis = true;
            W10_lbl3.BackColor = Color.Transparent;
            W10_lbl3.Font = new Font("Segoe UI", 9.0f);
            W10_lbl3.Location = new Point(48, 4);
            W10_lbl3.Name = "W10_lbl3";
            W10_lbl3.Size = new Size(365, 19);
            W10_lbl3.TabIndex = 3;
            W10_lbl3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label49
            // 
            Label49.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label49.BackColor = Color.Transparent;
            Label49.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label49.Location = new Point(40, 3);
            Label49.Name = "Label49";
            Label49.Size = new Size(254, 35);
            Label49.TabIndex = 0;
            Label49.Text = "Accents";
            Label49.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox35
            // 
            GroupBox35.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox35.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox35.Controls.Add(Label50);
            GroupBox35.Controls.Add(W10_Color_Index0);
            GroupBox35.Controls.Add(W10_pic2);
            GroupBox35.Controls.Add(W10_lbl2);
            GroupBox35.Location = new Point(3, 71);
            GroupBox35.Margin = new Padding(4, 3, 4, 3);
            GroupBox35.Name = "GroupBox35";
            GroupBox35.Size = new Size(514, 28);
            GroupBox35.TabIndex = 19;
            // 
            // Label50
            // 
            Label50.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label50.AutoEllipsis = true;
            Label50.BackColor = Color.Transparent;
            Label50.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label50.Location = new Point(31, 4);
            Label50.Name = "Label50";
            Label50.Size = new Size(14, 19);
            Label50.TabIndex = 6;
            Label50.Text = "2";
            Label50.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_Color_Index0
            // 
            W10_Color_Index0.AllowDrop = true;
            W10_Color_Index0.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W10_Color_Index0.BackColor = Color.FromArgb(47, 47, 48);
            W10_Color_Index0.DefaultColor = Color.Black;
            W10_Color_Index0.DontShowInfo = false;
            W10_Color_Index0.Location = new Point(420, 4);
            W10_Color_Index0.Margin = new Padding(4, 3, 4, 3);
            W10_Color_Index0.Name = "W10_Color_Index0";
            W10_Color_Index0.Size = new Size(90, 20);
            W10_Color_Index0.TabIndex = 2;
            // 
            // W10_pic2
            // 
            W10_pic2.BackColor = Color.Transparent;
            W10_pic2.Location = new Point(2, 2);
            W10_pic2.Name = "W10_pic2";
            W10_pic2.Size = new Size(24, 24);
            W10_pic2.SizeMode = PictureBoxSizeMode.CenterImage;
            W10_pic2.TabIndex = 4;
            W10_pic2.TabStop = false;
            // 
            // W10_lbl2
            // 
            W10_lbl2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W10_lbl2.AutoEllipsis = true;
            W10_lbl2.BackColor = Color.Transparent;
            W10_lbl2.Font = new Font("Segoe UI", 9.0f);
            W10_lbl2.Location = new Point(48, 4);
            W10_lbl2.Name = "W10_lbl2";
            W10_lbl2.Size = new Size(365, 19);
            W10_lbl2.TabIndex = 3;
            W10_lbl2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox36
            // 
            GroupBox36.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox36.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox36.Controls.Add(Label51);
            GroupBox36.Controls.Add(W10_pic5);
            GroupBox36.Controls.Add(W10_lbl5);
            GroupBox36.Controls.Add(W10_Color_Index3);
            GroupBox36.Location = new Point(3, 161);
            GroupBox36.Margin = new Padding(4, 3, 4, 3);
            GroupBox36.Name = "GroupBox36";
            GroupBox36.Size = new Size(514, 28);
            GroupBox36.TabIndex = 20;
            // 
            // Label51
            // 
            Label51.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label51.AutoEllipsis = true;
            Label51.BackColor = Color.Transparent;
            Label51.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label51.Location = new Point(31, 4);
            Label51.Name = "Label51";
            Label51.Size = new Size(14, 19);
            Label51.TabIndex = 6;
            Label51.Text = "5";
            Label51.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_pic5
            // 
            W10_pic5.BackColor = Color.Transparent;
            W10_pic5.Location = new Point(2, 2);
            W10_pic5.Name = "W10_pic5";
            W10_pic5.Size = new Size(24, 24);
            W10_pic5.SizeMode = PictureBoxSizeMode.CenterImage;
            W10_pic5.TabIndex = 4;
            W10_pic5.TabStop = false;
            // 
            // W10_lbl5
            // 
            W10_lbl5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            W10_lbl5.AutoEllipsis = true;
            W10_lbl5.BackColor = Color.Transparent;
            W10_lbl5.Font = new Font("Segoe UI", 9.0f);
            W10_lbl5.Location = new Point(48, 4);
            W10_lbl5.Name = "W10_lbl5";
            W10_lbl5.Size = new Size(365, 19);
            W10_lbl5.TabIndex = 3;
            W10_lbl5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_Color_Index3
            // 
            W10_Color_Index3.AllowDrop = true;
            W10_Color_Index3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W10_Color_Index3.BackColor = Color.FromArgb(47, 47, 48);
            W10_Color_Index3.DefaultColor = Color.Black;
            W10_Color_Index3.DontShowInfo = false;
            W10_Color_Index3.Location = new Point(420, 4);
            W10_Color_Index3.Margin = new Padding(4, 3, 4, 3);
            W10_Color_Index3.Name = "W10_Color_Index3";
            W10_Color_Index3.Size = new Size(90, 20);
            W10_Color_Index3.TabIndex = 2;
            // 
            // GroupBox37
            // 
            GroupBox37.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox37.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox37.Controls.Add(GroupBox23);
            GroupBox37.Controls.Add(GroupBox38);
            GroupBox37.Controls.Add(GroupBox40);
            GroupBox37.Controls.Add(GroupBox42);
            GroupBox37.Controls.Add(GroupBox43);
            GroupBox37.Controls.Add(PictureBox31);
            GroupBox37.Controls.Add(Label56);
            GroupBox37.Location = new Point(0, 0);
            GroupBox37.Margin = new Padding(4, 3, 4, 3);
            GroupBox37.Name = "GroupBox37";
            GroupBox37.Size = new Size(520, 133);
            GroupBox37.TabIndex = 11;
            // 
            // GroupBox23
            // 
            GroupBox23.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GroupBox23.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox23.Controls.Add(W10_TBTransparency_Toggle);
            GroupBox23.Controls.Add(PictureBox15);
            GroupBox23.Controls.Add(Label22);
            GroupBox23.Location = new Point(267, 71);
            GroupBox23.Margin = new Padding(4, 3, 4, 3);
            GroupBox23.Name = "GroupBox23";
            GroupBox23.Size = new Size(250, 28);
            GroupBox23.TabIndex = 17;
            // 
            // W10_TBTransparency_Toggle
            // 
            W10_TBTransparency_Toggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W10_TBTransparency_Toggle.BackColor = Color.FromArgb(43, 43, 43);
            W10_TBTransparency_Toggle.Checked = false;
            W10_TBTransparency_Toggle.DarkLight_Toggler = false;
            W10_TBTransparency_Toggle.Location = new Point(206, 4);
            W10_TBTransparency_Toggle.Name = "W10_TBTransparency_Toggle";
            W10_TBTransparency_Toggle.Size = new Size(40, 20);
            W10_TBTransparency_Toggle.TabIndex = 16;
            // 
            // PictureBox15
            // 
            PictureBox15.BackColor = Color.Transparent;
            PictureBox15.Image = (Image)resources.GetObject("PictureBox15.Image");
            PictureBox15.Location = new Point(2, 2);
            PictureBox15.Name = "PictureBox15";
            PictureBox15.Size = new Size(24, 24);
            PictureBox15.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox15.TabIndex = 4;
            PictureBox15.TabStop = false;
            // 
            // Label22
            // 
            Label22.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label22.AutoEllipsis = true;
            Label22.BackColor = Color.Transparent;
            Label22.Font = new Font("Segoe UI", 9.0f);
            Label22.Location = new Point(30, 4);
            Label22.Name = "Label22";
            Label22.Size = new Size(171, 20);
            Label22.TabIndex = 13;
            Label22.Text = "Increase taskbar transparency";
            Label22.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox38
            // 
            GroupBox38.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox38.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox38.Controls.Add(W10_Button25);
            GroupBox38.Controls.Add(W10_Accent_Taskbar);
            GroupBox38.Controls.Add(W10_Accent_StartTaskbar);
            GroupBox38.Controls.Add(W10_Accent_None);
            GroupBox38.Controls.Add(PictureBox16);
            GroupBox38.Controls.Add(Label52);
            GroupBox38.Location = new Point(3, 101);
            GroupBox38.Margin = new Padding(4, 3, 4, 3);
            GroupBox38.Name = "GroupBox38";
            GroupBox38.Size = new Size(514, 29);
            GroupBox38.TabIndex = 16;
            // 
            // W10_Button25
            // 
            W10_Button25.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            W10_Button25.BackColor = Color.FromArgb(51, 51, 51);
            W10_Button25.DrawOnGlass = false;
            W10_Button25.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            W10_Button25.ForeColor = Color.White;
            W10_Button25.Image = null;
            W10_Button25.LineColor = Color.FromArgb(199, 49, 61);
            W10_Button25.Location = new Point(160, 3);
            W10_Button25.Name = "W10_Button25";
            W10_Button25.Size = new Size(20, 23);
            W10_Button25.TabIndex = 28;
            W10_Button25.Text = "?";
            W10_Button25.UseVisualStyleBackColor = false;
            // 
            // W10_Accent_Taskbar
            // 
            W10_Accent_Taskbar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W10_Accent_Taskbar.BackColor = Color.FromArgb(15, 15, 15);
            W10_Accent_Taskbar.Checked = false;
            W10_Accent_Taskbar.Font = new Font("Segoe UI", 9.0f);
            W10_Accent_Taskbar.ForeColor = Color.White;
            W10_Accent_Taskbar.Image = null;
            W10_Accent_Taskbar.Location = new Point(248, 3);
            W10_Accent_Taskbar.Name = "W10_Accent_Taskbar";
            W10_Accent_Taskbar.ShowText = true;
            W10_Accent_Taskbar.Size = new Size(76, 23);
            W10_Accent_Taskbar.TabIndex = 23;
            W10_Accent_Taskbar.Text = "Taskbar";
            // 
            // W10_Accent_StartTaskbar
            // 
            W10_Accent_StartTaskbar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W10_Accent_StartTaskbar.BackColor = Color.FromArgb(15, 15, 15);
            W10_Accent_StartTaskbar.Checked = false;
            W10_Accent_StartTaskbar.Font = new Font("Segoe UI", 9.0f);
            W10_Accent_StartTaskbar.ForeColor = Color.White;
            W10_Accent_StartTaskbar.Image = null;
            W10_Accent_StartTaskbar.Location = new Point(325, 3);
            W10_Accent_StartTaskbar.Name = "W10_Accent_StartTaskbar";
            W10_Accent_StartTaskbar.ShowText = true;
            W10_Accent_StartTaskbar.Size = new Size(186, 23);
            W10_Accent_StartTaskbar.TabIndex = 22;
            W10_Accent_StartTaskbar.Text = "Start, taskbar & action Center";
            // 
            // W10_Accent_None
            // 
            W10_Accent_None.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W10_Accent_None.BackColor = Color.FromArgb(15, 15, 15);
            W10_Accent_None.Checked = false;
            W10_Accent_None.Font = new Font("Segoe UI", 9.0f);
            W10_Accent_None.ForeColor = Color.White;
            W10_Accent_None.Image = null;
            W10_Accent_None.Location = new Point(182, 3);
            W10_Accent_None.Name = "W10_Accent_None";
            W10_Accent_None.ShowText = true;
            W10_Accent_None.Size = new Size(64, 23);
            W10_Accent_None.TabIndex = 21;
            W10_Accent_None.Text = "Nothing";
            // 
            // PictureBox16
            // 
            PictureBox16.BackColor = Color.Transparent;
            PictureBox16.Image = (Image)resources.GetObject("PictureBox16.Image");
            PictureBox16.Location = new Point(2, 2);
            PictureBox16.Name = "PictureBox16";
            PictureBox16.Size = new Size(24, 24);
            PictureBox16.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox16.TabIndex = 4;
            PictureBox16.TabStop = false;
            // 
            // Label52
            // 
            Label52.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label52.AutoEllipsis = true;
            Label52.BackColor = Color.Transparent;
            Label52.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label52.Location = new Point(30, 3);
            Label52.Name = "Label52";
            Label52.Size = new Size(119, 22);
            Label52.TabIndex = 3;
            Label52.Text = "Accent color on:";
            Label52.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox40
            // 
            GroupBox40.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox40.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox40.Controls.Add(PictureBox22);
            GroupBox40.Controls.Add(Label53);
            GroupBox40.Controls.Add(W10_WinMode_Toggle);
            GroupBox40.Location = new Point(3, 41);
            GroupBox40.Margin = new Padding(4, 3, 4, 3);
            GroupBox40.Name = "GroupBox40";
            GroupBox40.Size = new Size(262, 28);
            GroupBox40.TabIndex = 10;
            // 
            // PictureBox22
            // 
            PictureBox22.BackColor = Color.Transparent;
            PictureBox22.BackgroundImageLayout = ImageLayout.Center;
            PictureBox22.Image = (Image)resources.GetObject("PictureBox22.Image");
            PictureBox22.Location = new Point(2, 2);
            PictureBox22.Name = "PictureBox22";
            PictureBox22.Size = new Size(24, 24);
            PictureBox22.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox22.TabIndex = 4;
            PictureBox22.TabStop = false;
            // 
            // Label53
            // 
            Label53.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label53.AutoEllipsis = true;
            Label53.BackColor = Color.Transparent;
            Label53.Font = new Font("Segoe UI", 9.0f);
            Label53.Location = new Point(30, 4);
            Label53.Name = "Label53";
            Label53.Size = new Size(177, 20);
            Label53.TabIndex = 7;
            Label53.Text = "Windows mode";
            Label53.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_WinMode_Toggle
            // 
            W10_WinMode_Toggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W10_WinMode_Toggle.BackColor = Color.FromArgb(43, 43, 43);
            W10_WinMode_Toggle.Checked = false;
            W10_WinMode_Toggle.DarkLight_Toggler = true;
            W10_WinMode_Toggle.Location = new Point(218, 4);
            W10_WinMode_Toggle.Name = "W10_WinMode_Toggle";
            W10_WinMode_Toggle.Size = new Size(40, 20);
            W10_WinMode_Toggle.TabIndex = 8;
            // 
            // GroupBox42
            // 
            GroupBox42.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox42.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox42.Controls.Add(W10_TB_Blur);
            GroupBox42.Controls.Add(W10_Transparency_Toggle);
            GroupBox42.Controls.Add(PictureBox26);
            GroupBox42.Controls.Add(Label54);
            GroupBox42.Location = new Point(3, 71);
            GroupBox42.Margin = new Padding(4, 3, 4, 3);
            GroupBox42.Name = "GroupBox42";
            GroupBox42.Size = new Size(262, 28);
            GroupBox42.TabIndex = 9;
            // 
            // W10_TB_Blur
            // 
            W10_TB_Blur.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W10_TB_Blur.BackColor = Color.FromArgb(43, 43, 43);
            W10_TB_Blur.Checked = true;
            W10_TB_Blur.DarkLight_Toggler = false;
            W10_TB_Blur.Location = new Point(219, 4);
            W10_TB_Blur.Name = "W10_TB_Blur";
            W10_TB_Blur.Size = new Size(40, 20);
            W10_TB_Blur.TabIndex = 16;
            // 
            // W10_Transparency_Toggle
            // 
            W10_Transparency_Toggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W10_Transparency_Toggle.BackColor = Color.FromArgb(43, 43, 43);
            W10_Transparency_Toggle.Checked = false;
            W10_Transparency_Toggle.DarkLight_Toggler = false;
            W10_Transparency_Toggle.Location = new Point(176, 4);
            W10_Transparency_Toggle.Name = "W10_Transparency_Toggle";
            W10_Transparency_Toggle.Size = new Size(40, 20);
            W10_Transparency_Toggle.TabIndex = 16;
            // 
            // PictureBox26
            // 
            PictureBox26.BackColor = Color.Transparent;
            PictureBox26.Image = (Image)resources.GetObject("PictureBox26.Image");
            PictureBox26.Location = new Point(2, 2);
            PictureBox26.Name = "PictureBox26";
            PictureBox26.Size = new Size(24, 24);
            PictureBox26.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox26.TabIndex = 4;
            PictureBox26.TabStop = false;
            // 
            // Label54
            // 
            Label54.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label54.AutoEllipsis = true;
            Label54.BackColor = Color.Transparent;
            Label54.Font = new Font("Segoe UI", 9.0f);
            Label54.Location = new Point(30, 4);
            Label54.Name = "Label54";
            Label54.Size = new Size(140, 20);
            Label54.TabIndex = 13;
            Label54.Text = "Transparency, TB blur";
            Label54.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox43
            // 
            GroupBox43.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GroupBox43.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox43.Controls.Add(PictureBox27);
            GroupBox43.Controls.Add(W10_AppMode_Toggle);
            GroupBox43.Controls.Add(Label55);
            GroupBox43.Location = new Point(267, 41);
            GroupBox43.Margin = new Padding(4, 3, 4, 3);
            GroupBox43.Name = "GroupBox43";
            GroupBox43.Size = new Size(250, 28);
            GroupBox43.TabIndex = 8;
            // 
            // PictureBox27
            // 
            PictureBox27.BackColor = Color.Transparent;
            PictureBox27.Image = (Image)resources.GetObject("PictureBox27.Image");
            PictureBox27.Location = new Point(2, 2);
            PictureBox27.Name = "PictureBox27";
            PictureBox27.Size = new Size(24, 24);
            PictureBox27.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox27.TabIndex = 4;
            PictureBox27.TabStop = false;
            // 
            // W10_AppMode_Toggle
            // 
            W10_AppMode_Toggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W10_AppMode_Toggle.BackColor = Color.FromArgb(43, 43, 43);
            W10_AppMode_Toggle.Checked = false;
            W10_AppMode_Toggle.DarkLight_Toggler = true;
            W10_AppMode_Toggle.Location = new Point(206, 4);
            W10_AppMode_Toggle.Name = "W10_AppMode_Toggle";
            W10_AppMode_Toggle.Size = new Size(40, 20);
            W10_AppMode_Toggle.TabIndex = 11;
            // 
            // Label55
            // 
            Label55.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label55.AutoEllipsis = true;
            Label55.BackColor = Color.Transparent;
            Label55.Font = new Font("Segoe UI", 9.0f);
            Label55.Location = new Point(30, 4);
            Label55.Name = "Label55";
            Label55.Size = new Size(163, 20);
            Label55.TabIndex = 10;
            Label55.Text = "App mode";
            Label55.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox31
            // 
            PictureBox31.BackColor = Color.Transparent;
            PictureBox31.Image = (Image)resources.GetObject("PictureBox31.Image");
            PictureBox31.Location = new Point(3, 3);
            PictureBox31.Name = "PictureBox31";
            PictureBox31.Size = new Size(35, 35);
            PictureBox31.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox31.TabIndex = 1;
            PictureBox31.TabStop = false;
            // 
            // Label56
            // 
            Label56.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label56.BackColor = Color.Transparent;
            Label56.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label56.Location = new Point(38, 3);
            Label56.Name = "Label56";
            Label56.Size = new Size(479, 35);
            Label56.TabIndex = 0;
            Label56.Text = "Toggles";
            Label56.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox44
            // 
            GroupBox44.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox44.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox44.Controls.Add(W10_Button8);
            GroupBox44.Controls.Add(W10_ShowAccentOnTitlebarAndBorders_Toggle);
            GroupBox44.Controls.Add(GroupBox45);
            GroupBox44.Controls.Add(PictureBox34);
            GroupBox44.Controls.Add(Label58);
            GroupBox44.Controls.Add(GroupBox46);
            GroupBox44.Location = new Point(0, 137);
            GroupBox44.Margin = new Padding(4, 3, 4, 3);
            GroupBox44.Name = "GroupBox44";
            GroupBox44.Size = new Size(520, 72);
            GroupBox44.TabIndex = 2;
            // 
            // W10_Button8
            // 
            W10_Button8.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            W10_Button8.BackColor = Color.FromArgb(43, 43, 43);
            W10_Button8.DrawOnGlass = false;
            W10_Button8.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            W10_Button8.ForeColor = Color.White;
            W10_Button8.Image = null;
            W10_Button8.LineColor = Color.FromArgb(199, 49, 61);
            W10_Button8.Location = new Point(448, 10);
            W10_Button8.Name = "W10_Button8";
            W10_Button8.Size = new Size(20, 20);
            W10_Button8.TabIndex = 27;
            W10_Button8.Text = "?";
            W10_Button8.UseVisualStyleBackColor = false;
            // 
            // W10_ShowAccentOnTitlebarAndBorders_Toggle
            // 
            W10_ShowAccentOnTitlebarAndBorders_Toggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W10_ShowAccentOnTitlebarAndBorders_Toggle.BackColor = Color.FromArgb(43, 43, 43);
            W10_ShowAccentOnTitlebarAndBorders_Toggle.Checked = false;
            W10_ShowAccentOnTitlebarAndBorders_Toggle.DarkLight_Toggler = false;
            W10_ShowAccentOnTitlebarAndBorders_Toggle.Location = new Point(474, 10);
            W10_ShowAccentOnTitlebarAndBorders_Toggle.Name = "W10_ShowAccentOnTitlebarAndBorders_Toggle";
            W10_ShowAccentOnTitlebarAndBorders_Toggle.Size = new Size(40, 20);
            W10_ShowAccentOnTitlebarAndBorders_Toggle.TabIndex = 6;
            // 
            // GroupBox45
            // 
            GroupBox45.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GroupBox45.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox45.Controls.Add(PictureBox33);
            GroupBox45.Controls.Add(Label57);
            GroupBox45.Controls.Add(W10_InactiveTitlebar_pick);
            GroupBox45.Location = new Point(267, 41);
            GroupBox45.Margin = new Padding(4, 3, 4, 3);
            GroupBox45.Name = "GroupBox45";
            GroupBox45.Size = new Size(250, 28);
            GroupBox45.TabIndex = 6;
            // 
            // PictureBox33
            // 
            PictureBox33.BackColor = Color.Transparent;
            PictureBox33.Image = (Image)resources.GetObject("PictureBox33.Image");
            PictureBox33.Location = new Point(2, 2);
            PictureBox33.Name = "PictureBox33";
            PictureBox33.Size = new Size(24, 24);
            PictureBox33.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox33.TabIndex = 4;
            PictureBox33.TabStop = false;
            // 
            // Label57
            // 
            Label57.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label57.AutoEllipsis = true;
            Label57.BackColor = Color.Transparent;
            Label57.Font = new Font("Segoe UI", 9.0f);
            Label57.Location = new Point(30, 4);
            Label57.Name = "Label57";
            Label57.Size = new Size(119, 20);
            Label57.TabIndex = 3;
            Label57.Text = "Inactive titlebar";
            Label57.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_InactiveTitlebar_pick
            // 
            W10_InactiveTitlebar_pick.AllowDrop = true;
            W10_InactiveTitlebar_pick.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W10_InactiveTitlebar_pick.BackColor = Color.FromArgb(47, 47, 48);
            W10_InactiveTitlebar_pick.DefaultColor = Color.Black;
            W10_InactiveTitlebar_pick.DontShowInfo = false;
            W10_InactiveTitlebar_pick.Location = new Point(156, 4);
            W10_InactiveTitlebar_pick.Margin = new Padding(4, 3, 4, 3);
            W10_InactiveTitlebar_pick.Name = "W10_InactiveTitlebar_pick";
            W10_InactiveTitlebar_pick.Size = new Size(90, 20);
            W10_InactiveTitlebar_pick.TabIndex = 2;
            // 
            // PictureBox34
            // 
            PictureBox34.BackColor = Color.Transparent;
            PictureBox34.Image = (Image)resources.GetObject("PictureBox34.Image");
            PictureBox34.Location = new Point(3, 3);
            PictureBox34.Name = "PictureBox34";
            PictureBox34.Size = new Size(35, 35);
            PictureBox34.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox34.TabIndex = 1;
            PictureBox34.TabStop = false;
            // 
            // Label58
            // 
            Label58.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label58.BackColor = Color.Transparent;
            Label58.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label58.Location = new Point(40, 3);
            Label58.Name = "Label58";
            Label58.Size = new Size(393, 35);
            Label58.TabIndex = 0;
            Label58.Text = "Titlebars";
            Label58.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox46
            // 
            GroupBox46.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox46.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox46.Controls.Add(PictureBox35);
            GroupBox46.Controls.Add(Label59);
            GroupBox46.Controls.Add(W10_ActiveTitlebar_pick);
            GroupBox46.Location = new Point(3, 41);
            GroupBox46.Margin = new Padding(4, 3, 4, 3);
            GroupBox46.Name = "GroupBox46";
            GroupBox46.Size = new Size(262, 28);
            GroupBox46.TabIndex = 5;
            // 
            // PictureBox35
            // 
            PictureBox35.BackColor = Color.Transparent;
            PictureBox35.Image = (Image)resources.GetObject("PictureBox35.Image");
            PictureBox35.Location = new Point(2, 2);
            PictureBox35.Name = "PictureBox35";
            PictureBox35.Size = new Size(24, 24);
            PictureBox35.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox35.TabIndex = 4;
            PictureBox35.TabStop = false;
            // 
            // Label59
            // 
            Label59.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label59.AutoEllipsis = true;
            Label59.BackColor = Color.Transparent;
            Label59.Font = new Font("Segoe UI", 9.0f);
            Label59.Location = new Point(30, 5);
            Label59.Name = "Label59";
            Label59.Size = new Size(131, 19);
            Label59.TabIndex = 3;
            Label59.Text = "Active titlebar";
            Label59.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W10_ActiveTitlebar_pick
            // 
            W10_ActiveTitlebar_pick.AllowDrop = true;
            W10_ActiveTitlebar_pick.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            W10_ActiveTitlebar_pick.BackColor = Color.FromArgb(47, 47, 48);
            W10_ActiveTitlebar_pick.DefaultColor = Color.Black;
            W10_ActiveTitlebar_pick.DontShowInfo = false;
            W10_ActiveTitlebar_pick.Location = new Point(168, 4);
            W10_ActiveTitlebar_pick.Margin = new Padding(4, 3, 4, 3);
            W10_ActiveTitlebar_pick.Name = "W10_ActiveTitlebar_pick";
            W10_ActiveTitlebar_pick.Size = new Size(90, 20);
            W10_ActiveTitlebar_pick.TabIndex = 2;
            // 
            // TabPage3
            // 
            TabPage3.BackColor = Color.FromArgb(25, 25, 25);
            TabPage3.Controls.Add(PaletteContainer_W81);
            TabPage3.Location = new Point(4, 24);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(3);
            TabPage3.Size = new Size(529, 517);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "W8.1";
            // 
            // PaletteContainer_W81
            // 
            PaletteContainer_W81.BackColor = Color.Transparent;
            PaletteContainer_W81.Controls.Add(GroupBox17);
            PaletteContainer_W81.Controls.Add(GroupBox32);
            PaletteContainer_W81.Dock = DockStyle.Fill;
            PaletteContainer_W81.Location = new Point(3, 3);
            PaletteContainer_W81.Name = "PaletteContainer_W81";
            PaletteContainer_W81.Size = new Size(523, 511);
            PaletteContainer_W81.TabIndex = 31;
            // 
            // GroupBox17
            // 
            GroupBox17.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox17.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox17.Controls.Add(SeparatorVertical2);
            GroupBox17.Controls.Add(Label32);
            GroupBox17.Controls.Add(Label31);
            GroupBox17.Controls.Add(W81_start);
            GroupBox17.Controls.Add(Label30);
            GroupBox17.Controls.Add(Label24);
            GroupBox17.Controls.Add(W81_theme_aerolite);
            GroupBox17.Controls.Add(PictureBox37);
            GroupBox17.Controls.Add(Label40);
            GroupBox17.Controls.Add(W81_theme_aero);
            GroupBox17.Controls.Add(W81_logonui);
            GroupBox17.Location = new Point(0, 231);
            GroupBox17.Margin = new Padding(4, 3, 4, 3);
            GroupBox17.Name = "GroupBox17";
            GroupBox17.Size = new Size(520, 138);
            GroupBox17.TabIndex = 12;
            // 
            // SeparatorVertical2
            // 
            SeparatorVertical2.AlternativeLook = false;
            SeparatorVertical2.Location = new Point(253, 39);
            SeparatorVertical2.Name = "SeparatorVertical2";
            SeparatorVertical2.Size = new Size(1, 90);
            SeparatorVertical2.TabIndex = 40;
            SeparatorVertical2.TabStop = false;
            SeparatorVertical2.Text = "SeparatorVertical2";
            // 
            // Label32
            // 
            Label32.AutoEllipsis = true;
            Label32.BackColor = Color.Transparent;
            Label32.Font = new Font("Segoe UI", 9.0f);
            Label32.Location = new Point(331, 109);
            Label32.Name = "Label32";
            Label32.Size = new Size(64, 22);
            Label32.TabIndex = 39;
            Label32.Text = "LogonUI*";
            Label32.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label31
            // 
            Label31.AutoEllipsis = true;
            Label31.BackColor = Color.Transparent;
            Label31.Font = new Font("Segoe UI", 9.0f);
            Label31.Location = new Point(261, 109);
            Label31.Name = "Label31";
            Label31.Size = new Size(64, 22);
            Label31.TabIndex = 38;
            Label31.Text = "Start";
            Label31.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // W81_start
            // 
            W81_start.BackColor = Color.FromArgb(43, 43, 43);
            W81_start.DrawOnGlass = false;
            W81_start.Font = new Font("Segoe UI", 9.0f);
            W81_start.ForeColor = Color.White;
            W81_start.Image = null;
            W81_start.LineColor = Color.FromArgb(0, 81, 210);
            W81_start.Location = new Point(261, 39);
            W81_start.Name = "W81_start";
            W81_start.Size = new Size(64, 64);
            W81_start.TabIndex = 36;
            W81_start.UseVisualStyleBackColor = false;
            // 
            // Label30
            // 
            Label30.AutoEllipsis = true;
            Label30.BackColor = Color.Transparent;
            Label30.Font = new Font("Segoe UI", 9.0f);
            Label30.Location = new Point(183, 109);
            Label30.Name = "Label30";
            Label30.Size = new Size(64, 22);
            Label30.TabIndex = 35;
            Label30.Text = "Aero Lite";
            Label30.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label24
            // 
            Label24.AutoEllipsis = true;
            Label24.BackColor = Color.Transparent;
            Label24.Font = new Font("Segoe UI", 9.0f);
            Label24.Location = new Point(113, 109);
            Label24.Name = "Label24";
            Label24.Size = new Size(64, 22);
            Label24.TabIndex = 34;
            Label24.Text = "Aero 8";
            Label24.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // W81_theme_aerolite
            // 
            W81_theme_aerolite.Checked = false;
            W81_theme_aerolite.Font = new Font("Segoe UI", 9.0f);
            W81_theme_aerolite.ForeColor = Color.White;
            W81_theme_aerolite.Image = (Image)resources.GetObject("W81_theme_aerolite.Image");
            W81_theme_aerolite.Location = new Point(183, 39);
            W81_theme_aerolite.Name = "W81_theme_aerolite";
            W81_theme_aerolite.ShowText = false;
            W81_theme_aerolite.Size = new Size(64, 64);
            W81_theme_aerolite.TabIndex = 30;
            // 
            // PictureBox37
            // 
            PictureBox37.BackColor = Color.Transparent;
            PictureBox37.Image = (Image)resources.GetObject("PictureBox37.Image");
            PictureBox37.Location = new Point(3, 3);
            PictureBox37.Name = "PictureBox37";
            PictureBox37.Size = new Size(35, 35);
            PictureBox37.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox37.TabIndex = 1;
            PictureBox37.TabStop = false;
            // 
            // Label40
            // 
            Label40.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label40.BackColor = Color.Transparent;
            Label40.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label40.Location = new Point(44, 3);
            Label40.Name = "Label40";
            Label40.Size = new Size(472, 35);
            Label40.TabIndex = 0;
            Label40.Text = "Theme";
            Label40.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W81_theme_aero
            // 
            W81_theme_aero.Checked = false;
            W81_theme_aero.Font = new Font("Segoe UI", 9.0f);
            W81_theme_aero.ForeColor = Color.White;
            W81_theme_aero.Image = (Image)resources.GetObject("W81_theme_aero.Image");
            W81_theme_aero.Location = new Point(113, 39);
            W81_theme_aero.Name = "W81_theme_aero";
            W81_theme_aero.ShowText = false;
            W81_theme_aero.Size = new Size(64, 64);
            W81_theme_aero.TabIndex = 29;
            // 
            // W81_logonui
            // 
            W81_logonui.BackColor = Color.FromArgb(43, 43, 43);
            W81_logonui.DrawOnGlass = false;
            W81_logonui.Font = new Font("Segoe UI", 9.0f);
            W81_logonui.ForeColor = Color.White;
            W81_logonui.Image = null;
            W81_logonui.LineColor = Color.FromArgb(0, 81, 210);
            W81_logonui.Location = new Point(331, 39);
            W81_logonui.Name = "W81_logonui";
            W81_logonui.Size = new Size(64, 64);
            W81_logonui.TabIndex = 37;
            W81_logonui.UseVisualStyleBackColor = false;
            // 
            // GroupBox32
            // 
            GroupBox32.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox32.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox32.Controls.Add(GroupBox39);
            GroupBox32.Controls.Add(GroupBox41);
            GroupBox32.Controls.Add(GroupBox15);
            GroupBox32.Controls.Add(GroupBox33);
            GroupBox32.Controls.Add(GroupBox29);
            GroupBox32.Controls.Add(PictureBox32);
            GroupBox32.Controls.Add(Label41);
            GroupBox32.Location = new Point(0, 0);
            GroupBox32.Margin = new Padding(4, 3, 4, 3);
            GroupBox32.Name = "GroupBox32";
            GroupBox32.Size = new Size(520, 228);
            GroupBox32.TabIndex = 11;
            // 
            // GroupBox39
            // 
            GroupBox39.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox39.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox39.Controls.Add(PictureBox28);
            GroupBox39.Controls.Add(W81_personalcolor_accent_pick);
            GroupBox39.Controls.Add(Foregrounds);
            GroupBox39.Location = new Point(2, 103);
            GroupBox39.Margin = new Padding(4, 3, 4, 3);
            GroupBox39.Name = "GroupBox39";
            GroupBox39.Size = new Size(515, 29);
            GroupBox39.TabIndex = 26;
            // 
            // PictureBox28
            // 
            PictureBox28.BackColor = Color.Transparent;
            PictureBox28.Image = (Image)resources.GetObject("PictureBox28.Image");
            PictureBox28.Location = new Point(3, 2);
            PictureBox28.Name = "PictureBox28";
            PictureBox28.Size = new Size(24, 24);
            PictureBox28.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox28.TabIndex = 4;
            PictureBox28.TabStop = false;
            // 
            // W81_personalcolor_accent_pick
            // 
            W81_personalcolor_accent_pick.AllowDrop = true;
            W81_personalcolor_accent_pick.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W81_personalcolor_accent_pick.BackColor = Color.FromArgb(47, 47, 48);
            W81_personalcolor_accent_pick.DefaultColor = Color.Black;
            W81_personalcolor_accent_pick.DontShowInfo = false;
            W81_personalcolor_accent_pick.Location = new Point(401, 4);
            W81_personalcolor_accent_pick.Margin = new Padding(4, 3, 4, 3);
            W81_personalcolor_accent_pick.Name = "W81_personalcolor_accent_pick";
            W81_personalcolor_accent_pick.Size = new Size(110, 21);
            W81_personalcolor_accent_pick.TabIndex = 2;
            // 
            // Foregrounds
            // 
            Foregrounds.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Foregrounds.AutoEllipsis = true;
            Foregrounds.BackColor = Color.Transparent;
            Foregrounds.Font = new Font("Segoe UI", 9.0f);
            Foregrounds.Location = new Point(30, 4);
            Foregrounds.Name = "Foregrounds";
            Foregrounds.Size = new Size(364, 20);
            Foregrounds.TabIndex = 3;
            Foregrounds.Text = "Foregrounds (primary: accents)";
            Foregrounds.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox41
            // 
            GroupBox41.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox41.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox41.Controls.Add(PictureBox29);
            GroupBox41.Controls.Add(W81_personalcls_background_pick);
            GroupBox41.Controls.Add(Label33);
            GroupBox41.Location = new Point(2, 165);
            GroupBox41.Margin = new Padding(4, 3, 4, 3);
            GroupBox41.Name = "GroupBox41";
            GroupBox41.Size = new Size(515, 29);
            GroupBox41.TabIndex = 25;
            // 
            // PictureBox29
            // 
            PictureBox29.BackColor = Color.Transparent;
            PictureBox29.Image = (Image)resources.GetObject("PictureBox29.Image");
            PictureBox29.Location = new Point(3, 2);
            PictureBox29.Name = "PictureBox29";
            PictureBox29.Size = new Size(24, 24);
            PictureBox29.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox29.TabIndex = 4;
            PictureBox29.TabStop = false;
            // 
            // W81_personalcls_background_pick
            // 
            W81_personalcls_background_pick.AllowDrop = true;
            W81_personalcls_background_pick.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W81_personalcls_background_pick.BackColor = Color.FromArgb(47, 47, 48);
            W81_personalcls_background_pick.DefaultColor = Color.Black;
            W81_personalcls_background_pick.DontShowInfo = false;
            W81_personalcls_background_pick.Location = new Point(401, 4);
            W81_personalcls_background_pick.Margin = new Padding(4, 3, 4, 3);
            W81_personalcls_background_pick.Name = "W81_personalcls_background_pick";
            W81_personalcls_background_pick.Size = new Size(110, 21);
            W81_personalcls_background_pick.TabIndex = 2;
            // 
            // Label33
            // 
            Label33.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label33.AutoEllipsis = true;
            Label33.BackColor = Color.Transparent;
            Label33.Font = new Font("Segoe UI", 9.0f);
            Label33.Location = new Point(30, 4);
            Label33.Name = "Label33";
            Label33.Size = new Size(364, 20);
            Label33.TabIndex = 3;
            Label33.Text = "Backgrounds (primary: start, LogonUI)";
            Label33.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox15
            // 
            GroupBox15.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox15.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox15.Controls.Add(PictureBox9);
            GroupBox15.Controls.Add(W81_start_pick);
            GroupBox15.Controls.Add(Label20);
            GroupBox15.Location = new Point(2, 196);
            GroupBox15.Margin = new Padding(4, 3, 4, 3);
            GroupBox15.Name = "GroupBox15";
            GroupBox15.Size = new Size(515, 29);
            GroupBox15.TabIndex = 21;
            // 
            // PictureBox9
            // 
            PictureBox9.BackColor = Color.Transparent;
            PictureBox9.Image = (Image)resources.GetObject("PictureBox9.Image");
            PictureBox9.Location = new Point(3, 2);
            PictureBox9.Name = "PictureBox9";
            PictureBox9.Size = new Size(24, 24);
            PictureBox9.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox9.TabIndex = 4;
            PictureBox9.TabStop = false;
            // 
            // W81_start_pick
            // 
            W81_start_pick.AllowDrop = true;
            W81_start_pick.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W81_start_pick.BackColor = Color.FromArgb(47, 47, 48);
            W81_start_pick.DefaultColor = Color.Black;
            W81_start_pick.DontShowInfo = false;
            W81_start_pick.Location = new Point(401, 4);
            W81_start_pick.Margin = new Padding(4, 3, 4, 3);
            W81_start_pick.Name = "W81_start_pick";
            W81_start_pick.Size = new Size(110, 21);
            W81_start_pick.TabIndex = 2;
            // 
            // Label20
            // 
            Label20.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label20.AutoEllipsis = true;
            Label20.BackColor = Color.Transparent;
            Label20.Font = new Font("Segoe UI", 9.0f);
            Label20.Location = new Point(30, 4);
            Label20.Name = "Label20";
            Label20.Size = new Size(369, 20);
            Label20.TabIndex = 3;
            Label20.Text = "Backgrounds (secondary: start, LogonUI)";
            Label20.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox33
            // 
            GroupBox33.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox33.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox33.Controls.Add(W81_ColorizationBalance_val);
            GroupBox33.Controls.Add(W81_ColorizationBalance_bar);
            GroupBox33.Controls.Add(PictureBox30);
            GroupBox33.Controls.Add(W81_ColorizationColor_pick);
            GroupBox33.Controls.Add(Label39);
            GroupBox33.Location = new Point(2, 44);
            GroupBox33.Margin = new Padding(4, 3, 4, 3);
            GroupBox33.Name = "GroupBox33";
            GroupBox33.Size = new Size(515, 57);
            GroupBox33.TabIndex = 20;
            // 
            // W81_ColorizationBalance_val
            // 
            W81_ColorizationBalance_val.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W81_ColorizationBalance_val.BackColor = Color.FromArgb(51, 51, 51);
            W81_ColorizationBalance_val.DrawOnGlass = false;
            W81_ColorizationBalance_val.Font = new Font("Segoe UI", 9.0f);
            W81_ColorizationBalance_val.ForeColor = Color.White;
            W81_ColorizationBalance_val.Image = null;
            W81_ColorizationBalance_val.LineColor = Color.FromArgb(0, 81, 210);
            W81_ColorizationBalance_val.Location = new Point(477, 29);
            W81_ColorizationBalance_val.Name = "W81_ColorizationBalance_val";
            W81_ColorizationBalance_val.Size = new Size(34, 24);
            W81_ColorizationBalance_val.TabIndex = 130;
            W81_ColorizationBalance_val.UseVisualStyleBackColor = false;
            // 
            // W81_ColorizationBalance_bar
            // 
            W81_ColorizationBalance_bar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            W81_ColorizationBalance_bar.LargeChange = 10;
            W81_ColorizationBalance_bar.Location = new Point(4, 32);
            W81_ColorizationBalance_bar.Maximum = 100;
            W81_ColorizationBalance_bar.Minimum = 0;
            W81_ColorizationBalance_bar.Name = "W81_ColorizationBalance_bar";
            W81_ColorizationBalance_bar.Size = new Size(467, 19);
            W81_ColorizationBalance_bar.SmallChange = 1;
            W81_ColorizationBalance_bar.TabIndex = 6;
            W81_ColorizationBalance_bar.Value = 0;
            // 
            // PictureBox30
            // 
            PictureBox30.BackColor = Color.Transparent;
            PictureBox30.Image = (Image)resources.GetObject("PictureBox30.Image");
            PictureBox30.Location = new Point(3, 2);
            PictureBox30.Name = "PictureBox30";
            PictureBox30.Size = new Size(24, 24);
            PictureBox30.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox30.TabIndex = 4;
            PictureBox30.TabStop = false;
            // 
            // W81_ColorizationColor_pick
            // 
            W81_ColorizationColor_pick.AllowDrop = true;
            W81_ColorizationColor_pick.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W81_ColorizationColor_pick.BackColor = Color.FromArgb(47, 47, 48);
            W81_ColorizationColor_pick.DefaultColor = Color.Black;
            W81_ColorizationColor_pick.DontShowInfo = false;
            W81_ColorizationColor_pick.Location = new Point(401, 4);
            W81_ColorizationColor_pick.Margin = new Padding(4, 3, 4, 3);
            W81_ColorizationColor_pick.Name = "W81_ColorizationColor_pick";
            W81_ColorizationColor_pick.Size = new Size(110, 21);
            W81_ColorizationColor_pick.TabIndex = 2;
            // 
            // Label39
            // 
            Label39.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label39.AutoEllipsis = true;
            Label39.BackColor = Color.Transparent;
            Label39.Font = new Font("Segoe UI", 9.0f);
            Label39.Location = new Point(30, 4);
            Label39.Name = "Label39";
            Label39.Size = new Size(364, 20);
            Label39.TabIndex = 3;
            Label39.Text = "Windows colors (titlebars && taskbar)";
            Label39.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox29
            // 
            GroupBox29.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox29.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox29.Controls.Add(PictureBox23);
            GroupBox29.Controls.Add(W81_accent_pick);
            GroupBox29.Controls.Add(Label29);
            GroupBox29.Location = new Point(2, 134);
            GroupBox29.Margin = new Padding(4, 3, 4, 3);
            GroupBox29.Name = "GroupBox29";
            GroupBox29.Size = new Size(515, 29);
            GroupBox29.TabIndex = 23;
            // 
            // PictureBox23
            // 
            PictureBox23.BackColor = Color.Transparent;
            PictureBox23.Image = (Image)resources.GetObject("PictureBox23.Image");
            PictureBox23.Location = new Point(3, 2);
            PictureBox23.Name = "PictureBox23";
            PictureBox23.Size = new Size(24, 24);
            PictureBox23.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox23.TabIndex = 4;
            PictureBox23.TabStop = false;
            // 
            // W81_accent_pick
            // 
            W81_accent_pick.AllowDrop = true;
            W81_accent_pick.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W81_accent_pick.BackColor = Color.FromArgb(47, 47, 48);
            W81_accent_pick.DefaultColor = Color.Black;
            W81_accent_pick.DontShowInfo = false;
            W81_accent_pick.Location = new Point(401, 4);
            W81_accent_pick.Margin = new Padding(4, 3, 4, 3);
            W81_accent_pick.Name = "W81_accent_pick";
            W81_accent_pick.Size = new Size(110, 21);
            W81_accent_pick.TabIndex = 2;
            // 
            // Label29
            // 
            Label29.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label29.AutoEllipsis = true;
            Label29.BackColor = Color.Transparent;
            Label29.Font = new Font("Segoe UI", 9.0f);
            Label29.Location = new Point(30, 4);
            Label29.Name = "Label29";
            Label29.Size = new Size(369, 20);
            Label29.TabIndex = 3;
            Label29.Text = "Foregrounds (secondary: accents)";
            Label29.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox32
            // 
            PictureBox32.BackColor = Color.Transparent;
            PictureBox32.Image = (Image)resources.GetObject("PictureBox32.Image");
            PictureBox32.Location = new Point(3, 3);
            PictureBox32.Name = "PictureBox32";
            PictureBox32.Size = new Size(35, 35);
            PictureBox32.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox32.TabIndex = 1;
            PictureBox32.TabStop = false;
            // 
            // Label41
            // 
            Label41.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label41.BackColor = Color.Transparent;
            Label41.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label41.Location = new Point(44, 3);
            Label41.Name = "Label41";
            Label41.Size = new Size(472, 35);
            Label41.TabIndex = 0;
            Label41.Text = "Colors";
            Label41.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage4
            // 
            TabPage4.BackColor = Color.FromArgb(25, 25, 25);
            TabPage4.Controls.Add(PaletteContainer_W7);
            TabPage4.Location = new Point(4, 24);
            TabPage4.Name = "TabPage4";
            TabPage4.Padding = new Padding(3);
            TabPage4.Size = new Size(529, 517);
            TabPage4.TabIndex = 3;
            TabPage4.Text = "W7";
            // 
            // PaletteContainer_W7
            // 
            PaletteContainer_W7.BackColor = Color.Transparent;
            PaletteContainer_W7.Controls.Add(GroupBox11);
            PaletteContainer_W7.Controls.Add(GroupBox22);
            PaletteContainer_W7.Controls.Add(GroupBox30);
            PaletteContainer_W7.Dock = DockStyle.Fill;
            PaletteContainer_W7.Location = new Point(3, 3);
            PaletteContainer_W7.Name = "PaletteContainer_W7";
            PaletteContainer_W7.Size = new Size(523, 511);
            PaletteContainer_W7.TabIndex = 30;
            // 
            // GroupBox11
            // 
            GroupBox11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox11.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox11.Controls.Add(Label23);
            GroupBox11.Controls.Add(PictureBox13);
            GroupBox11.Controls.Add(W7_theme_aero);
            GroupBox11.Controls.Add(Label28);
            GroupBox11.Controls.Add(W7_theme_classic);
            GroupBox11.Controls.Add(Label25);
            GroupBox11.Controls.Add(W7_theme_basic);
            GroupBox11.Controls.Add(Label14);
            GroupBox11.Controls.Add(W7_theme_aeroopaque);
            GroupBox11.Controls.Add(Label6);
            GroupBox11.Location = new Point(0, 109);
            GroupBox11.Margin = new Padding(4, 3, 4, 3);
            GroupBox11.Name = "GroupBox11";
            GroupBox11.Padding = new Padding(1);
            GroupBox11.Size = new Size(519, 140);
            GroupBox11.TabIndex = 40;
            // 
            // Label23
            // 
            Label23.AutoEllipsis = true;
            Label23.BackColor = Color.Transparent;
            Label23.Font = new Font("Segoe UI", 9.0f);
            Label23.Location = new Point(66, 109);
            Label23.Name = "Label23";
            Label23.Size = new Size(90, 22);
            Label23.TabIndex = 39;
            Label23.Text = "Aero";
            Label23.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PictureBox13
            // 
            PictureBox13.BackColor = Color.Transparent;
            PictureBox13.Image = (Image)resources.GetObject("PictureBox13.Image");
            PictureBox13.Location = new Point(3, 3);
            PictureBox13.Name = "PictureBox13";
            PictureBox13.Size = new Size(35, 35);
            PictureBox13.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox13.TabIndex = 1;
            PictureBox13.TabStop = false;
            // 
            // W7_theme_aero
            // 
            W7_theme_aero.Checked = false;
            W7_theme_aero.Font = new Font("Segoe UI", 9.0f);
            W7_theme_aero.ForeColor = Color.White;
            W7_theme_aero.Image = (Image)resources.GetObject("W7_theme_aero.Image");
            W7_theme_aero.Location = new Point(79, 40);
            W7_theme_aero.Name = "W7_theme_aero";
            W7_theme_aero.ShowText = false;
            W7_theme_aero.Size = new Size(64, 64);
            W7_theme_aero.TabIndex = 38;
            // 
            // Label28
            // 
            Label28.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label28.BackColor = Color.Transparent;
            Label28.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label28.Location = new Point(44, 3);
            Label28.Name = "Label28";
            Label28.Size = new Size(471, 35);
            Label28.TabIndex = 0;
            Label28.Text = "Theme";
            Label28.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // W7_theme_classic
            // 
            W7_theme_classic.Checked = false;
            W7_theme_classic.Font = new Font("Segoe UI", 9.0f);
            W7_theme_classic.ForeColor = Color.White;
            W7_theme_classic.Image = (Image)resources.GetObject("W7_theme_classic.Image");
            W7_theme_classic.Location = new Point(370, 40);
            W7_theme_classic.Name = "W7_theme_classic";
            W7_theme_classic.ShowText = false;
            W7_theme_classic.Size = new Size(64, 64);
            W7_theme_classic.TabIndex = 32;
            // 
            // Label25
            // 
            Label25.AutoEllipsis = true;
            Label25.BackColor = Color.Transparent;
            Label25.Font = new Font("Segoe UI", 9.0f);
            Label25.Location = new Point(162, 109);
            Label25.Name = "Label25";
            Label25.Size = new Size(90, 22);
            Label25.TabIndex = 37;
            Label25.Text = "Aero Opaque";
            Label25.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // W7_theme_basic
            // 
            W7_theme_basic.Checked = false;
            W7_theme_basic.Font = new Font("Segoe UI", 9.0f);
            W7_theme_basic.ForeColor = Color.White;
            W7_theme_basic.Image = (Image)resources.GetObject("W7_theme_basic.Image");
            W7_theme_basic.Location = new Point(274, 40);
            W7_theme_basic.Name = "W7_theme_basic";
            W7_theme_basic.ShowText = false;
            W7_theme_basic.Size = new Size(64, 64);
            W7_theme_basic.TabIndex = 34;
            // 
            // Label14
            // 
            Label14.AutoEllipsis = true;
            Label14.BackColor = Color.Transparent;
            Label14.Font = new Font("Segoe UI", 9.0f);
            Label14.Location = new Point(261, 109);
            Label14.Name = "Label14";
            Label14.Size = new Size(90, 22);
            Label14.TabIndex = 35;
            Label14.Text = "Basic";
            Label14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // W7_theme_aeroopaque
            // 
            W7_theme_aeroopaque.Checked = false;
            W7_theme_aeroopaque.Font = new Font("Segoe UI", 9.0f);
            W7_theme_aeroopaque.ForeColor = Color.White;
            W7_theme_aeroopaque.Image = (Image)resources.GetObject("W7_theme_aeroopaque.Image");
            W7_theme_aeroopaque.Location = new Point(175, 40);
            W7_theme_aeroopaque.Name = "W7_theme_aeroopaque";
            W7_theme_aeroopaque.ShowText = false;
            W7_theme_aeroopaque.Size = new Size(64, 64);
            W7_theme_aeroopaque.TabIndex = 36;
            // 
            // Label6
            // 
            Label6.AutoEllipsis = true;
            Label6.BackColor = Color.Transparent;
            Label6.Font = new Font("Segoe UI", 9.0f);
            Label6.Location = new Point(357, 109);
            Label6.Name = "Label6";
            Label6.Size = new Size(90, 22);
            Label6.TabIndex = 33;
            Label6.Text = "Classic";
            Label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GroupBox22
            // 
            GroupBox22.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox22.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox22.Controls.Add(GroupBox19);
            GroupBox22.Controls.Add(PictureBox39);
            GroupBox22.Controls.Add(GroupBox12);
            GroupBox22.Controls.Add(Label38);
            GroupBox22.Controls.Add(GroupBox10);
            GroupBox22.Controls.Add(GroupBox7);
            GroupBox22.Location = new Point(0, 254);
            GroupBox22.Margin = new Padding(4, 3, 4, 3);
            GroupBox22.Name = "GroupBox22";
            GroupBox22.Padding = new Padding(1);
            GroupBox22.Size = new Size(519, 165);
            GroupBox22.TabIndex = 12;
            // 
            // GroupBox19
            // 
            GroupBox19.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox19.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox19.Controls.Add(W7_ColorizationGlassReflectionIntensity_val);
            GroupBox19.Controls.Add(W7_ColorizationGlassReflectionIntensity_bar);
            GroupBox19.Controls.Add(PictureBox24);
            GroupBox19.Controls.Add(Label26);
            GroupBox19.Location = new Point(2, 74);
            GroupBox19.Margin = new Padding(4, 3, 4, 3);
            GroupBox19.Name = "GroupBox19";
            GroupBox19.Size = new Size(514, 28);
            GroupBox19.TabIndex = 30;
            // 
            // W7_ColorizationGlassReflectionIntensity_val
            // 
            W7_ColorizationGlassReflectionIntensity_val.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W7_ColorizationGlassReflectionIntensity_val.BackColor = Color.FromArgb(51, 51, 51);
            W7_ColorizationGlassReflectionIntensity_val.DrawOnGlass = false;
            W7_ColorizationGlassReflectionIntensity_val.Font = new Font("Segoe UI", 9.0f);
            W7_ColorizationGlassReflectionIntensity_val.ForeColor = Color.White;
            W7_ColorizationGlassReflectionIntensity_val.Image = null;
            W7_ColorizationGlassReflectionIntensity_val.LineColor = Color.FromArgb(0, 81, 210);
            W7_ColorizationGlassReflectionIntensity_val.Location = new Point(476, 4);
            W7_ColorizationGlassReflectionIntensity_val.Name = "W7_ColorizationGlassReflectionIntensity_val";
            W7_ColorizationGlassReflectionIntensity_val.Size = new Size(34, 20);
            W7_ColorizationGlassReflectionIntensity_val.TabIndex = 132;
            W7_ColorizationGlassReflectionIntensity_val.UseVisualStyleBackColor = false;
            // 
            // W7_ColorizationGlassReflectionIntensity_bar
            // 
            W7_ColorizationGlassReflectionIntensity_bar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            W7_ColorizationGlassReflectionIntensity_bar.LargeChange = 10;
            W7_ColorizationGlassReflectionIntensity_bar.Location = new Point(185, 4);
            W7_ColorizationGlassReflectionIntensity_bar.Maximum = 100;
            W7_ColorizationGlassReflectionIntensity_bar.Minimum = 0;
            W7_ColorizationGlassReflectionIntensity_bar.Name = "W7_ColorizationGlassReflectionIntensity_bar";
            W7_ColorizationGlassReflectionIntensity_bar.Size = new Size(285, 19);
            W7_ColorizationGlassReflectionIntensity_bar.SmallChange = 1;
            W7_ColorizationGlassReflectionIntensity_bar.TabIndex = 8;
            W7_ColorizationGlassReflectionIntensity_bar.Value = 0;
            // 
            // PictureBox24
            // 
            PictureBox24.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox24.BackColor = Color.Transparent;
            PictureBox24.Image = (Image)resources.GetObject("PictureBox24.Image");
            PictureBox24.Location = new Point(3, 2);
            PictureBox24.Name = "PictureBox24";
            PictureBox24.Size = new Size(24, 23);
            PictureBox24.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox24.TabIndex = 4;
            PictureBox24.TabStop = false;
            // 
            // Label26
            // 
            Label26.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label26.AutoEllipsis = true;
            Label26.BackColor = Color.Transparent;
            Label26.Font = new Font("Segoe UI", 9.0f);
            Label26.Location = new Point(30, 4);
            Label26.Name = "Label26";
            Label26.Size = new Size(149, 20);
            Label26.TabIndex = 3;
            Label26.Text = "Glass reflection intensity";
            Label26.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox39
            // 
            PictureBox39.BackColor = Color.Transparent;
            PictureBox39.Image = (Image)resources.GetObject("PictureBox39.Image");
            PictureBox39.Location = new Point(3, 3);
            PictureBox39.Name = "PictureBox39";
            PictureBox39.Size = new Size(35, 35);
            PictureBox39.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox39.TabIndex = 1;
            PictureBox39.TabStop = false;
            // 
            // GroupBox12
            // 
            GroupBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox12.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox12.Controls.Add(W7_ColorizationBlurBalance_val);
            GroupBox12.Controls.Add(W7_ColorizationBlurBalance_bar);
            GroupBox12.Controls.Add(PictureBox8);
            GroupBox12.Controls.Add(Label15);
            GroupBox12.Location = new Point(2, 44);
            GroupBox12.Margin = new Padding(4, 3, 4, 3);
            GroupBox12.Name = "GroupBox12";
            GroupBox12.Size = new Size(514, 28);
            GroupBox12.TabIndex = 29;
            // 
            // W7_ColorizationBlurBalance_val
            // 
            W7_ColorizationBlurBalance_val.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W7_ColorizationBlurBalance_val.BackColor = Color.FromArgb(51, 51, 51);
            W7_ColorizationBlurBalance_val.DrawOnGlass = false;
            W7_ColorizationBlurBalance_val.Font = new Font("Segoe UI", 9.0f);
            W7_ColorizationBlurBalance_val.ForeColor = Color.White;
            W7_ColorizationBlurBalance_val.Image = null;
            W7_ColorizationBlurBalance_val.LineColor = Color.FromArgb(0, 81, 210);
            W7_ColorizationBlurBalance_val.Location = new Point(476, 4);
            W7_ColorizationBlurBalance_val.Name = "W7_ColorizationBlurBalance_val";
            W7_ColorizationBlurBalance_val.Size = new Size(34, 20);
            W7_ColorizationBlurBalance_val.TabIndex = 132;
            W7_ColorizationBlurBalance_val.UseVisualStyleBackColor = false;
            // 
            // W7_ColorizationBlurBalance_bar
            // 
            W7_ColorizationBlurBalance_bar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            W7_ColorizationBlurBalance_bar.LargeChange = 10;
            W7_ColorizationBlurBalance_bar.Location = new Point(185, 4);
            W7_ColorizationBlurBalance_bar.Maximum = 100;
            W7_ColorizationBlurBalance_bar.Minimum = 0;
            W7_ColorizationBlurBalance_bar.Name = "W7_ColorizationBlurBalance_bar";
            W7_ColorizationBlurBalance_bar.Size = new Size(285, 19);
            W7_ColorizationBlurBalance_bar.SmallChange = 1;
            W7_ColorizationBlurBalance_bar.TabIndex = 7;
            W7_ColorizationBlurBalance_bar.Value = 0;
            // 
            // PictureBox8
            // 
            PictureBox8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox8.BackColor = Color.Transparent;
            PictureBox8.Image = (Image)resources.GetObject("PictureBox8.Image");
            PictureBox8.Location = new Point(3, 2);
            PictureBox8.Name = "PictureBox8";
            PictureBox8.Size = new Size(24, 23);
            PictureBox8.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox8.TabIndex = 4;
            PictureBox8.TabStop = false;
            // 
            // Label15
            // 
            Label15.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Label15.AutoEllipsis = true;
            Label15.BackColor = Color.Transparent;
            Label15.Font = new Font("Segoe UI", 9.0f);
            Label15.Location = new Point(30, 4);
            Label15.Name = "Label15";
            Label15.Size = new Size(149, 20);
            Label15.TabIndex = 3;
            Label15.Text = "Colorization blur balance";
            Label15.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label38
            // 
            Label38.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label38.BackColor = Color.Transparent;
            Label38.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label38.Location = new Point(44, 3);
            Label38.Name = "Label38";
            Label38.Size = new Size(471, 35);
            Label38.TabIndex = 0;
            Label38.Text = "Aero tweaks";
            Label38.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox10
            // 
            GroupBox10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox10.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox10.Controls.Add(W7_EnableAeroPeek_toggle);
            GroupBox10.Controls.Add(PictureBox4);
            GroupBox10.Controls.Add(Aero_EnableAeroPeek_lbl);
            GroupBox10.Location = new Point(2, 104);
            GroupBox10.Margin = new Padding(4, 3, 4, 3);
            GroupBox10.Name = "GroupBox10";
            GroupBox10.Size = new Size(514, 28);
            GroupBox10.TabIndex = 22;
            // 
            // W7_EnableAeroPeek_toggle
            // 
            W7_EnableAeroPeek_toggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W7_EnableAeroPeek_toggle.BackColor = Color.FromArgb(43, 43, 43);
            W7_EnableAeroPeek_toggle.Checked = false;
            W7_EnableAeroPeek_toggle.DarkLight_Toggler = false;
            W7_EnableAeroPeek_toggle.Location = new Point(470, 5);
            W7_EnableAeroPeek_toggle.Name = "W7_EnableAeroPeek_toggle";
            W7_EnableAeroPeek_toggle.Size = new Size(40, 20);
            W7_EnableAeroPeek_toggle.TabIndex = 16;
            // 
            // PictureBox4
            // 
            PictureBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox4.BackColor = Color.Transparent;
            PictureBox4.Image = (Image)resources.GetObject("PictureBox4.Image");
            PictureBox4.Location = new Point(3, 2);
            PictureBox4.Name = "PictureBox4";
            PictureBox4.Size = new Size(24, 23);
            PictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox4.TabIndex = 4;
            PictureBox4.TabStop = false;
            // 
            // Aero_EnableAeroPeek_lbl
            // 
            Aero_EnableAeroPeek_lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Aero_EnableAeroPeek_lbl.AutoEllipsis = true;
            Aero_EnableAeroPeek_lbl.BackColor = Color.Transparent;
            Aero_EnableAeroPeek_lbl.Font = new Font("Segoe UI", 9.0f);
            Aero_EnableAeroPeek_lbl.Location = new Point(30, 4);
            Aero_EnableAeroPeek_lbl.Name = "Aero_EnableAeroPeek_lbl";
            Aero_EnableAeroPeek_lbl.Size = new Size(429, 20);
            Aero_EnableAeroPeek_lbl.TabIndex = 13;
            Aero_EnableAeroPeek_lbl.Text = "Aero peek";
            Aero_EnableAeroPeek_lbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox7
            // 
            GroupBox7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox7.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox7.Controls.Add(W7_AlwaysHibernateThumbnails_Toggle);
            GroupBox7.Controls.Add(PictureBox3);
            GroupBox7.Controls.Add(Aero_AlwaysHibernateThumbnails_lbl);
            GroupBox7.Location = new Point(2, 134);
            GroupBox7.Margin = new Padding(4, 3, 4, 3);
            GroupBox7.Name = "GroupBox7";
            GroupBox7.Size = new Size(514, 28);
            GroupBox7.TabIndex = 23;
            // 
            // W7_AlwaysHibernateThumbnails_Toggle
            // 
            W7_AlwaysHibernateThumbnails_Toggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W7_AlwaysHibernateThumbnails_Toggle.BackColor = Color.FromArgb(43, 43, 43);
            W7_AlwaysHibernateThumbnails_Toggle.Checked = false;
            W7_AlwaysHibernateThumbnails_Toggle.DarkLight_Toggler = false;
            W7_AlwaysHibernateThumbnails_Toggle.Location = new Point(470, 5);
            W7_AlwaysHibernateThumbnails_Toggle.Name = "W7_AlwaysHibernateThumbnails_Toggle";
            W7_AlwaysHibernateThumbnails_Toggle.Size = new Size(40, 20);
            W7_AlwaysHibernateThumbnails_Toggle.TabIndex = 17;
            // 
            // PictureBox3
            // 
            PictureBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox3.BackColor = Color.Transparent;
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(3, 2);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(24, 23);
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox3.TabIndex = 4;
            PictureBox3.TabStop = false;
            // 
            // Aero_AlwaysHibernateThumbnails_lbl
            // 
            Aero_AlwaysHibernateThumbnails_lbl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Aero_AlwaysHibernateThumbnails_lbl.AutoEllipsis = true;
            Aero_AlwaysHibernateThumbnails_lbl.BackColor = Color.Transparent;
            Aero_AlwaysHibernateThumbnails_lbl.Font = new Font("Segoe UI", 9.0f);
            Aero_AlwaysHibernateThumbnails_lbl.Location = new Point(30, 4);
            Aero_AlwaysHibernateThumbnails_lbl.Name = "Aero_AlwaysHibernateThumbnails_lbl";
            Aero_AlwaysHibernateThumbnails_lbl.Size = new Size(429, 19);
            Aero_AlwaysHibernateThumbnails_lbl.TabIndex = 3;
            Aero_AlwaysHibernateThumbnails_lbl.Text = "Hibernate thumbnails";
            Aero_AlwaysHibernateThumbnails_lbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox30
            // 
            GroupBox30.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox30.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox30.Controls.Add(GroupBox21);
            GroupBox30.Controls.Add(GroupBox26);
            GroupBox30.Controls.Add(PictureBox25);
            GroupBox30.Controls.Add(Label27);
            GroupBox30.Location = new Point(0, 0);
            GroupBox30.Margin = new Padding(4, 3, 4, 3);
            GroupBox30.Name = "GroupBox30";
            GroupBox30.Size = new Size(519, 105);
            GroupBox30.TabIndex = 11;
            // 
            // GroupBox21
            // 
            GroupBox21.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox21.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox21.Controls.Add(W7_ColorizationColorBalance_val);
            GroupBox21.Controls.Add(W7_ColorizationColorBalance_bar);
            GroupBox21.Controls.Add(PictureBox12);
            GroupBox21.Controls.Add(W7_ColorizationColor_pick);
            GroupBox21.Controls.Add(Label16);
            GroupBox21.Location = new Point(2, 44);
            GroupBox21.Margin = new Padding(4, 3, 4, 3);
            GroupBox21.Name = "GroupBox21";
            GroupBox21.Size = new Size(514, 28);
            GroupBox21.TabIndex = 20;
            // 
            // W7_ColorizationColorBalance_val
            // 
            W7_ColorizationColorBalance_val.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W7_ColorizationColorBalance_val.BackColor = Color.FromArgb(51, 51, 51);
            W7_ColorizationColorBalance_val.DrawOnGlass = false;
            W7_ColorizationColorBalance_val.Font = new Font("Segoe UI", 9.0f);
            W7_ColorizationColorBalance_val.ForeColor = Color.White;
            W7_ColorizationColorBalance_val.Image = null;
            W7_ColorizationColorBalance_val.LineColor = Color.FromArgb(0, 81, 210);
            W7_ColorizationColorBalance_val.Location = new Point(360, 4);
            W7_ColorizationColorBalance_val.Name = "W7_ColorizationColorBalance_val";
            W7_ColorizationColorBalance_val.Size = new Size(34, 20);
            W7_ColorizationColorBalance_val.TabIndex = 131;
            W7_ColorizationColorBalance_val.UseVisualStyleBackColor = false;
            // 
            // W7_ColorizationColorBalance_bar
            // 
            W7_ColorizationColorBalance_bar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            W7_ColorizationColorBalance_bar.LargeChange = 10;
            W7_ColorizationColorBalance_bar.Location = new Point(170, 5);
            W7_ColorizationColorBalance_bar.Maximum = 100;
            W7_ColorizationColorBalance_bar.Minimum = 0;
            W7_ColorizationColorBalance_bar.Name = "W7_ColorizationColorBalance_bar";
            W7_ColorizationColorBalance_bar.Size = new Size(184, 19);
            W7_ColorizationColorBalance_bar.SmallChange = 1;
            W7_ColorizationColorBalance_bar.TabIndex = 6;
            W7_ColorizationColorBalance_bar.Value = 0;
            // 
            // PictureBox12
            // 
            PictureBox12.BackColor = Color.Transparent;
            PictureBox12.Image = (Image)resources.GetObject("PictureBox12.Image");
            PictureBox12.Location = new Point(3, 2);
            PictureBox12.Name = "PictureBox12";
            PictureBox12.Size = new Size(24, 24);
            PictureBox12.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox12.TabIndex = 4;
            PictureBox12.TabStop = false;
            // 
            // W7_ColorizationColor_pick
            // 
            W7_ColorizationColor_pick.AllowDrop = true;
            W7_ColorizationColor_pick.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W7_ColorizationColor_pick.BackColor = Color.FromArgb(47, 47, 48);
            W7_ColorizationColor_pick.DefaultColor = Color.Black;
            W7_ColorizationColor_pick.DontShowInfo = false;
            W7_ColorizationColor_pick.Location = new Point(401, 3);
            W7_ColorizationColor_pick.Margin = new Padding(4, 3, 4, 3);
            W7_ColorizationColor_pick.Name = "W7_ColorizationColor_pick";
            W7_ColorizationColor_pick.Size = new Size(110, 21);
            W7_ColorizationColor_pick.TabIndex = 2;
            // 
            // Label16
            // 
            Label16.AutoEllipsis = true;
            Label16.BackColor = Color.Transparent;
            Label16.Font = new Font("Segoe UI", 9.0f);
            Label16.Location = new Point(30, 4);
            Label16.Name = "Label16";
            Label16.Size = new Size(134, 20);
            Label16.TabIndex = 3;
            Label16.Text = "Colorization color";
            Label16.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox26
            // 
            GroupBox26.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox26.BackColor = Color.FromArgb(43, 43, 43);
            GroupBox26.Controls.Add(W7_ColorizationAfterglowBalance_val);
            GroupBox26.Controls.Add(W7_ColorizationAfterglowBalance_bar);
            GroupBox26.Controls.Add(W7_ColorizationAfterglow_pick);
            GroupBox26.Controls.Add(PictureBox14);
            GroupBox26.Controls.Add(Label21);
            GroupBox26.Location = new Point(2, 74);
            GroupBox26.Margin = new Padding(4, 3, 4, 3);
            GroupBox26.Name = "GroupBox26";
            GroupBox26.Size = new Size(514, 28);
            GroupBox26.TabIndex = 21;
            // 
            // W7_ColorizationAfterglowBalance_val
            // 
            W7_ColorizationAfterglowBalance_val.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W7_ColorizationAfterglowBalance_val.BackColor = Color.FromArgb(51, 51, 51);
            W7_ColorizationAfterglowBalance_val.DrawOnGlass = false;
            W7_ColorizationAfterglowBalance_val.Font = new Font("Segoe UI", 9.0f);
            W7_ColorizationAfterglowBalance_val.ForeColor = Color.White;
            W7_ColorizationAfterglowBalance_val.Image = null;
            W7_ColorizationAfterglowBalance_val.LineColor = Color.FromArgb(0, 81, 210);
            W7_ColorizationAfterglowBalance_val.Location = new Point(360, 4);
            W7_ColorizationAfterglowBalance_val.Name = "W7_ColorizationAfterglowBalance_val";
            W7_ColorizationAfterglowBalance_val.Size = new Size(34, 20);
            W7_ColorizationAfterglowBalance_val.TabIndex = 132;
            W7_ColorizationAfterglowBalance_val.UseVisualStyleBackColor = false;
            // 
            // W7_ColorizationAfterglowBalance_bar
            // 
            W7_ColorizationAfterglowBalance_bar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            W7_ColorizationAfterglowBalance_bar.LargeChange = 10;
            W7_ColorizationAfterglowBalance_bar.Location = new Point(170, 5);
            W7_ColorizationAfterglowBalance_bar.Maximum = 100;
            W7_ColorizationAfterglowBalance_bar.Minimum = 0;
            W7_ColorizationAfterglowBalance_bar.Name = "W7_ColorizationAfterglowBalance_bar";
            W7_ColorizationAfterglowBalance_bar.Size = new Size(184, 19);
            W7_ColorizationAfterglowBalance_bar.SmallChange = 1;
            W7_ColorizationAfterglowBalance_bar.TabIndex = 5;
            W7_ColorizationAfterglowBalance_bar.Value = 0;
            // 
            // W7_ColorizationAfterglow_pick
            // 
            W7_ColorizationAfterglow_pick.AllowDrop = true;
            W7_ColorizationAfterglow_pick.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            W7_ColorizationAfterglow_pick.BackColor = Color.FromArgb(47, 47, 48);
            W7_ColorizationAfterglow_pick.DefaultColor = Color.Black;
            W7_ColorizationAfterglow_pick.DontShowInfo = false;
            W7_ColorizationAfterglow_pick.Location = new Point(401, 4);
            W7_ColorizationAfterglow_pick.Margin = new Padding(4, 3, 4, 3);
            W7_ColorizationAfterglow_pick.Name = "W7_ColorizationAfterglow_pick";
            W7_ColorizationAfterglow_pick.Size = new Size(110, 21);
            W7_ColorizationAfterglow_pick.TabIndex = 2;
            // 
            // PictureBox14
            // 
            PictureBox14.BackColor = Color.Transparent;
            PictureBox14.Image = (Image)resources.GetObject("PictureBox14.Image");
            PictureBox14.Location = new Point(3, 2);
            PictureBox14.Name = "PictureBox14";
            PictureBox14.Size = new Size(24, 24);
            PictureBox14.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox14.TabIndex = 4;
            PictureBox14.TabStop = false;
            // 
            // Label21
            // 
            Label21.AutoEllipsis = true;
            Label21.BackColor = Color.Transparent;
            Label21.Font = new Font("Segoe UI", 9.0f);
            Label21.Location = new Point(30, 4);
            Label21.Name = "Label21";
            Label21.Size = new Size(134, 20);
            Label21.TabIndex = 3;
            Label21.Text = "After glow color";
            Label21.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox25
            // 
            PictureBox25.BackColor = Color.Transparent;
            PictureBox25.Image = (Image)resources.GetObject("PictureBox25.Image");
            PictureBox25.Location = new Point(3, 3);
            PictureBox25.Name = "PictureBox25";
            PictureBox25.Size = new Size(35, 35);
            PictureBox25.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox25.TabIndex = 1;
            PictureBox25.TabStop = false;
            // 
            // Label27
            // 
            Label27.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label27.BackColor = Color.Transparent;
            Label27.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label27.Location = new Point(44, 3);
            Label27.Name = "Label27";
            Label27.Size = new Size(468, 35);
            Label27.TabIndex = 0;
            Label27.Text = "Main colors";
            Label27.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabPage8
            // 
            TabPage8.BackColor = Color.FromArgb(25, 25, 25);
            TabPage8.Controls.Add(GroupBox50);
            TabPage8.Controls.Add(GroupBox49);
            TabPage8.Location = new Point(4, 24);
            TabPage8.Name = "TabPage8";
            TabPage8.Size = new Size(529, 517);
            TabPage8.TabIndex = 5;
            TabPage8.Text = "WVista";
            // 
            // GroupBox50
            // 
            GroupBox50.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox50.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox50.Controls.Add(WVista_ColorizationColorBalance_val);
            GroupBox50.Controls.Add(PictureBox45);
            GroupBox50.Controls.Add(WVista_ColorizationColorBalance_bar);
            GroupBox50.Controls.Add(WVista_ColorizationColor_pick);
            GroupBox50.Controls.Add(Label80);
            GroupBox50.Location = new Point(3, 3);
            GroupBox50.Margin = new Padding(4, 3, 4, 3);
            GroupBox50.Name = "GroupBox50";
            GroupBox50.Size = new Size(519, 65);
            GroupBox50.TabIndex = 47;
            // 
            // WVista_ColorizationColorBalance_val
            // 
            WVista_ColorizationColorBalance_val.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            WVista_ColorizationColorBalance_val.BackColor = Color.FromArgb(43, 43, 43);
            WVista_ColorizationColorBalance_val.DrawOnGlass = false;
            WVista_ColorizationColorBalance_val.Font = new Font("Segoe UI", 9.0f);
            WVista_ColorizationColorBalance_val.ForeColor = Color.White;
            WVista_ColorizationColorBalance_val.Image = null;
            WVista_ColorizationColorBalance_val.LineColor = Color.FromArgb(0, 81, 210);
            WVista_ColorizationColorBalance_val.Location = new Point(364, 39);
            WVista_ColorizationColorBalance_val.Name = "WVista_ColorizationColorBalance_val";
            WVista_ColorizationColorBalance_val.Size = new Size(34, 20);
            WVista_ColorizationColorBalance_val.TabIndex = 131;
            WVista_ColorizationColorBalance_val.UseVisualStyleBackColor = false;
            // 
            // PictureBox45
            // 
            PictureBox45.BackColor = Color.Transparent;
            PictureBox45.Image = (Image)resources.GetObject("PictureBox45.Image");
            PictureBox45.Location = new Point(3, 3);
            PictureBox45.Name = "PictureBox45";
            PictureBox45.Size = new Size(35, 35);
            PictureBox45.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox45.TabIndex = 1;
            PictureBox45.TabStop = false;
            // 
            // WVista_ColorizationColorBalance_bar
            // 
            WVista_ColorizationColorBalance_bar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            WVista_ColorizationColorBalance_bar.LargeChange = 10;
            WVista_ColorizationColorBalance_bar.Location = new Point(3, 40);
            WVista_ColorizationColorBalance_bar.Maximum = 255;
            WVista_ColorizationColorBalance_bar.Minimum = 0;
            WVista_ColorizationColorBalance_bar.Name = "WVista_ColorizationColorBalance_bar";
            WVista_ColorizationColorBalance_bar.Size = new Size(355, 19);
            WVista_ColorizationColorBalance_bar.SmallChange = 1;
            WVista_ColorizationColorBalance_bar.TabIndex = 6;
            WVista_ColorizationColorBalance_bar.Value = 0;
            // 
            // WVista_ColorizationColor_pick
            // 
            WVista_ColorizationColor_pick.AllowDrop = true;
            WVista_ColorizationColor_pick.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            WVista_ColorizationColor_pick.BackColor = Color.FromArgb(47, 47, 48);
            WVista_ColorizationColor_pick.DefaultColor = Color.Black;
            WVista_ColorizationColor_pick.DontShowInfo = false;
            WVista_ColorizationColor_pick.Location = new Point(404, 38);
            WVista_ColorizationColor_pick.Margin = new Padding(4, 3, 4, 3);
            WVista_ColorizationColor_pick.Name = "WVista_ColorizationColor_pick";
            WVista_ColorizationColor_pick.Size = new Size(110, 22);
            WVista_ColorizationColor_pick.TabIndex = 2;
            // 
            // Label80
            // 
            Label80.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label80.BackColor = Color.Transparent;
            Label80.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label80.Location = new Point(44, 3);
            Label80.Name = "Label80";
            Label80.Size = new Size(468, 35);
            Label80.TabIndex = 0;
            Label80.Text = "Colorization color (main color)";
            Label80.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GroupBox49
            // 
            GroupBox49.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox49.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox49.Controls.Add(Label70);
            GroupBox49.Controls.Add(PictureBox42);
            GroupBox49.Controls.Add(WVista_theme_aero);
            GroupBox49.Controls.Add(Label72);
            GroupBox49.Controls.Add(WVista_theme_classic);
            GroupBox49.Controls.Add(Label73);
            GroupBox49.Controls.Add(WVista_theme_basic);
            GroupBox49.Controls.Add(Label74);
            GroupBox49.Controls.Add(WVista_theme_aeroopaque);
            GroupBox49.Controls.Add(Label75);
            GroupBox49.Location = new Point(3, 73);
            GroupBox49.Margin = new Padding(4, 3, 4, 3);
            GroupBox49.Name = "GroupBox49";
            GroupBox49.Padding = new Padding(1);
            GroupBox49.Size = new Size(519, 145);
            GroupBox49.TabIndex = 46;
            // 
            // Label70
            // 
            Label70.AutoEllipsis = true;
            Label70.BackColor = Color.Transparent;
            Label70.Font = new Font("Segoe UI", 9.0f);
            Label70.Location = new Point(66, 113);
            Label70.Name = "Label70";
            Label70.Size = new Size(90, 22);
            Label70.TabIndex = 39;
            Label70.Text = "Aero";
            Label70.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PictureBox42
            // 
            PictureBox42.BackColor = Color.Transparent;
            PictureBox42.Image = (Image)resources.GetObject("PictureBox42.Image");
            PictureBox42.Location = new Point(3, 3);
            PictureBox42.Name = "PictureBox42";
            PictureBox42.Size = new Size(35, 35);
            PictureBox42.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox42.TabIndex = 1;
            PictureBox42.TabStop = false;
            // 
            // WVista_theme_aero
            // 
            WVista_theme_aero.Checked = false;
            WVista_theme_aero.Font = new Font("Segoe UI", 9.0f);
            WVista_theme_aero.ForeColor = Color.White;
            WVista_theme_aero.Image = (Image)resources.GetObject("WVista_theme_aero.Image");
            WVista_theme_aero.Location = new Point(79, 44);
            WVista_theme_aero.Name = "WVista_theme_aero";
            WVista_theme_aero.ShowText = false;
            WVista_theme_aero.Size = new Size(64, 64);
            WVista_theme_aero.TabIndex = 38;
            // 
            // Label72
            // 
            Label72.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label72.BackColor = Color.Transparent;
            Label72.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label72.Location = new Point(44, 3);
            Label72.Name = "Label72";
            Label72.Size = new Size(471, 35);
            Label72.TabIndex = 0;
            Label72.Text = "Theme";
            Label72.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // WVista_theme_classic
            // 
            WVista_theme_classic.Checked = false;
            WVista_theme_classic.Font = new Font("Segoe UI", 9.0f);
            WVista_theme_classic.ForeColor = Color.White;
            WVista_theme_classic.Image = (Image)resources.GetObject("WVista_theme_classic.Image");
            WVista_theme_classic.Location = new Point(370, 44);
            WVista_theme_classic.Name = "WVista_theme_classic";
            WVista_theme_classic.ShowText = false;
            WVista_theme_classic.Size = new Size(64, 64);
            WVista_theme_classic.TabIndex = 32;
            // 
            // Label73
            // 
            Label73.AutoEllipsis = true;
            Label73.BackColor = Color.Transparent;
            Label73.Font = new Font("Segoe UI", 9.0f);
            Label73.Location = new Point(162, 113);
            Label73.Name = "Label73";
            Label73.Size = new Size(90, 22);
            Label73.TabIndex = 37;
            Label73.Text = "Aero Opaque";
            Label73.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // WVista_theme_basic
            // 
            WVista_theme_basic.Checked = false;
            WVista_theme_basic.Font = new Font("Segoe UI", 9.0f);
            WVista_theme_basic.ForeColor = Color.White;
            WVista_theme_basic.Image = (Image)resources.GetObject("WVista_theme_basic.Image");
            WVista_theme_basic.Location = new Point(274, 44);
            WVista_theme_basic.Name = "WVista_theme_basic";
            WVista_theme_basic.ShowText = false;
            WVista_theme_basic.Size = new Size(64, 64);
            WVista_theme_basic.TabIndex = 34;
            // 
            // Label74
            // 
            Label74.AutoEllipsis = true;
            Label74.BackColor = Color.Transparent;
            Label74.Font = new Font("Segoe UI", 9.0f);
            Label74.Location = new Point(261, 113);
            Label74.Name = "Label74";
            Label74.Size = new Size(90, 22);
            Label74.TabIndex = 35;
            Label74.Text = "Basic";
            Label74.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // WVista_theme_aeroopaque
            // 
            WVista_theme_aeroopaque.Checked = false;
            WVista_theme_aeroopaque.Font = new Font("Segoe UI", 9.0f);
            WVista_theme_aeroopaque.ForeColor = Color.White;
            WVista_theme_aeroopaque.Image = (Image)resources.GetObject("WVista_theme_aeroopaque.Image");
            WVista_theme_aeroopaque.Location = new Point(175, 44);
            WVista_theme_aeroopaque.Name = "WVista_theme_aeroopaque";
            WVista_theme_aeroopaque.ShowText = false;
            WVista_theme_aeroopaque.Size = new Size(64, 64);
            WVista_theme_aeroopaque.TabIndex = 36;
            // 
            // Label75
            // 
            Label75.AutoEllipsis = true;
            Label75.BackColor = Color.Transparent;
            Label75.Font = new Font("Segoe UI", 9.0f);
            Label75.Location = new Point(357, 113);
            Label75.Name = "Label75";
            Label75.Size = new Size(90, 22);
            Label75.TabIndex = 33;
            Label75.Text = "Classic";
            Label75.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TabPage9
            // 
            TabPage9.BackColor = Color.FromArgb(25, 25, 25);
            TabPage9.Controls.Add(WXP_Alert1);
            TabPage9.Controls.Add(Label76);
            TabPage9.Controls.Add(WXP_VS_ReplaceFonts);
            TabPage9.Controls.Add(WXP_VS_ReplaceMetrics);
            TabPage9.Controls.Add(WXP_VS_ReplaceColors);
            TabPage9.Controls.Add(WXP_Alert3);
            TabPage9.Controls.Add(GroupBox48);
            TabPage9.Controls.Add(GroupBox47);
            TabPage9.Location = new Point(4, 24);
            TabPage9.Name = "TabPage9";
            TabPage9.Size = new Size(529, 517);
            TabPage9.TabIndex = 6;
            TabPage9.Text = "WXP";
            // 
            // WXP_Alert1
            // 
            WXP_Alert1.AlertStyle = UI.WP.AlertBox.Style.Simple;
            WXP_Alert1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            WXP_Alert1.BackColor = Color.FromArgb(50, 50, 50);
            WXP_Alert1.CenterText = false;
            WXP_Alert1.CustomColor = Color.FromArgb(0, 81, 210);
            WXP_Alert1.Font = new Font("Segoe UI", 9.0f);
            WXP_Alert1.Image = null;
            WXP_Alert1.Location = new Point(3, 419);
            WXP_Alert1.Name = "WXP_Alert1";
            WXP_Alert1.Size = new Size(519, 28);
            WXP_Alert1.TabIndex = 53;
            WXP_Alert1.TabStop = false;
            WXP_Alert1.Text = @"External theme\visual styles require a UX-theme-patched Windows";
            // 
            // Label76
            // 
            Label76.AutoEllipsis = true;
            Label76.BackColor = Color.Transparent;
            Label76.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label76.Location = new Point(10, 265);
            Label76.Name = "Label76";
            Label76.Size = new Size(513, 20);
            Label76.TabIndex = 52;
            Label76.Text = "Make choosing any theme overwrites:";
            Label76.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // WXP_VS_ReplaceFonts
            // 
            WXP_VS_ReplaceFonts.BackColor = Color.FromArgb(25, 25, 25);
            WXP_VS_ReplaceFonts.Checked = false;
            WXP_VS_ReplaceFonts.Font = new Font("Segoe UI", 9.0f);
            WXP_VS_ReplaceFonts.ForeColor = Color.White;
            WXP_VS_ReplaceFonts.Location = new Point(381, 289);
            WXP_VS_ReplaceFonts.Name = "WXP_VS_ReplaceFonts";
            WXP_VS_ReplaceFonts.Size = new Size(129, 23);
            WXP_VS_ReplaceFonts.TabIndex = 51;
            WXP_VS_ReplaceFonts.Text = "System fonts";
            // 
            // WXP_VS_ReplaceMetrics
            // 
            WXP_VS_ReplaceMetrics.BackColor = Color.FromArgb(25, 25, 25);
            WXP_VS_ReplaceMetrics.Checked = false;
            WXP_VS_ReplaceMetrics.Font = new Font("Segoe UI", 9.0f);
            WXP_VS_ReplaceMetrics.ForeColor = Color.White;
            WXP_VS_ReplaceMetrics.Location = new Point(215, 290);
            WXP_VS_ReplaceMetrics.Name = "WXP_VS_ReplaceMetrics";
            WXP_VS_ReplaceMetrics.Size = new Size(150, 23);
            WXP_VS_ReplaceMetrics.TabIndex = 50;
            WXP_VS_ReplaceMetrics.Text = "Metrics values";
            // 
            // WXP_VS_ReplaceColors
            // 
            WXP_VS_ReplaceColors.BackColor = Color.FromArgb(25, 25, 25);
            WXP_VS_ReplaceColors.Checked = false;
            WXP_VS_ReplaceColors.Font = new Font("Segoe UI", 9.0f);
            WXP_VS_ReplaceColors.ForeColor = Color.White;
            WXP_VS_ReplaceColors.Location = new Point(19, 290);
            WXP_VS_ReplaceColors.Name = "WXP_VS_ReplaceColors";
            WXP_VS_ReplaceColors.Size = new Size(150, 23);
            WXP_VS_ReplaceColors.TabIndex = 49;
            WXP_VS_ReplaceColors.Text = "Colors in classic colors";
            // 
            // WXP_Alert3
            // 
            WXP_Alert3.AlertStyle = UI.WP.AlertBox.Style.Simple;
            WXP_Alert3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            WXP_Alert3.BackColor = Color.FromArgb(50, 50, 50);
            WXP_Alert3.CenterText = false;
            WXP_Alert3.CustomColor = Color.FromArgb(0, 81, 210);
            WXP_Alert3.Font = new Font("Segoe UI", 9.0f);
            WXP_Alert3.Image = null;
            WXP_Alert3.Location = new Point(3, 450);
            WXP_Alert3.Name = "WXP_Alert3";
            WXP_Alert3.Size = new Size(519, 64);
            WXP_Alert3.TabIndex = 48;
            WXP_Alert3.TabStop = false;
            WXP_Alert3.Text = "WinPaletter on Windows XP maybe unstable, you might always face crashes on applyi" + "ng an external theme or face other errors." + '\r' + '\n' + "Please read its documentation at Git" + "Hub.";
            // 
            // GroupBox48
            // 
            GroupBox48.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox48.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox48.Controls.Add(WXP_VS_ColorsList);
            GroupBox48.Controls.Add(PictureBox38);
            GroupBox48.Controls.Add(WXP_VS_Browse);
            GroupBox48.Controls.Add(Label71);
            GroupBox48.Controls.Add(PictureBox41);
            GroupBox48.Controls.Add(PictureBox40);
            GroupBox48.Controls.Add(Label69);
            GroupBox48.Controls.Add(Label67);
            GroupBox48.Controls.Add(WXP_VS_textbox);
            GroupBox48.Location = new Point(3, 153);
            GroupBox48.Margin = new Padding(4, 3, 4, 3);
            GroupBox48.Name = "GroupBox48";
            GroupBox48.Padding = new Padding(1);
            GroupBox48.Size = new Size(519, 102);
            GroupBox48.TabIndex = 42;
            // 
            // WXP_VS_ColorsList
            // 
            WXP_VS_ColorsList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            WXP_VS_ColorsList.BackColor = Color.FromArgb(55, 55, 55);
            WXP_VS_ColorsList.DrawMode = DrawMode.OwnerDrawVariable;
            WXP_VS_ColorsList.DropDownStyle = ComboBoxStyle.DropDownList;
            WXP_VS_ColorsList.Font = new Font("Segoe UI", 9.0f);
            WXP_VS_ColorsList.ForeColor = Color.White;
            WXP_VS_ColorsList.FormattingEnabled = true;
            WXP_VS_ColorsList.ItemHeight = 20;
            WXP_VS_ColorsList.Location = new Point(93, 72);
            WXP_VS_ColorsList.Name = "WXP_VS_ColorsList";
            WXP_VS_ColorsList.Size = new Size(420, 26);
            WXP_VS_ColorsList.TabIndex = 5;
            // 
            // PictureBox38
            // 
            PictureBox38.BackColor = Color.Transparent;
            PictureBox38.Image = (Image)resources.GetObject("PictureBox38.Image");
            PictureBox38.Location = new Point(3, 3);
            PictureBox38.Name = "PictureBox38";
            PictureBox38.Size = new Size(35, 35);
            PictureBox38.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox38.TabIndex = 1;
            PictureBox38.TabStop = false;
            // 
            // WXP_VS_Browse
            // 
            WXP_VS_Browse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            WXP_VS_Browse.BackColor = Color.FromArgb(43, 43, 43);
            WXP_VS_Browse.DrawOnGlass = false;
            WXP_VS_Browse.Font = new Font("Segoe UI", 9.0f);
            WXP_VS_Browse.ForeColor = Color.White;
            WXP_VS_Browse.Image = (Image)resources.GetObject("WXP_VS_Browse.Image");
            WXP_VS_Browse.LineColor = Color.FromArgb(184, 153, 68);
            WXP_VS_Browse.Location = new Point(479, 43);
            WXP_VS_Browse.Margin = new Padding(4, 3, 4, 3);
            WXP_VS_Browse.Name = "WXP_VS_Browse";
            WXP_VS_Browse.Size = new Size(35, 24);
            WXP_VS_Browse.TabIndex = 88;
            WXP_VS_Browse.UseVisualStyleBackColor = false;
            // 
            // Label71
            // 
            Label71.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label71.BackColor = Color.Transparent;
            Label71.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label71.Location = new Point(44, 3);
            Label71.Name = "Label71";
            Label71.Size = new Size(471, 35);
            Label71.TabIndex = 0;
            Label71.Text = "External theme";
            Label71.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox41
            // 
            PictureBox41.BackColor = Color.Transparent;
            PictureBox41.Image = (Image)resources.GetObject("PictureBox41.Image");
            PictureBox41.Location = new Point(5, 73);
            PictureBox41.Name = "PictureBox41";
            PictureBox41.Size = new Size(24, 24);
            PictureBox41.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox41.TabIndex = 4;
            PictureBox41.TabStop = false;
            // 
            // PictureBox40
            // 
            PictureBox40.BackColor = Color.Transparent;
            PictureBox40.Image = (Image)resources.GetObject("PictureBox40.Image");
            PictureBox40.Location = new Point(5, 43);
            PictureBox40.Name = "PictureBox40";
            PictureBox40.Size = new Size(24, 24);
            PictureBox40.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox40.TabIndex = 4;
            PictureBox40.TabStop = false;
            // 
            // Label69
            // 
            Label69.AutoEllipsis = true;
            Label69.BackColor = Color.Transparent;
            Label69.Font = new Font("Segoe UI", 9.0f);
            Label69.Location = new Point(32, 75);
            Label69.Name = "Label69";
            Label69.Size = new Size(55, 20);
            Label69.TabIndex = 3;
            Label69.Text = "Scheme:";
            Label69.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label67
            // 
            Label67.AutoEllipsis = true;
            Label67.BackColor = Color.Transparent;
            Label67.Font = new Font("Segoe UI", 9.0f);
            Label67.Location = new Point(32, 45);
            Label67.Name = "Label67";
            Label67.Size = new Size(55, 20);
            Label67.TabIndex = 3;
            Label67.Text = "File:";
            Label67.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // WXP_VS_textbox
            // 
            WXP_VS_textbox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            WXP_VS_textbox.BackColor = Color.FromArgb(55, 55, 55);
            WXP_VS_textbox.DrawOnGlass = false;
            WXP_VS_textbox.ForeColor = Color.White;
            WXP_VS_textbox.Location = new Point(93, 43);
            WXP_VS_textbox.MaxLength = 32767;
            WXP_VS_textbox.Multiline = false;
            WXP_VS_textbox.Name = "WXP_VS_textbox";
            WXP_VS_textbox.ReadOnly = false;
            WXP_VS_textbox.Scrollbars = ScrollBars.None;
            WXP_VS_textbox.SelectedText = "";
            WXP_VS_textbox.SelectionLength = 0;
            WXP_VS_textbox.SelectionStart = 0;
            WXP_VS_textbox.Size = new Size(382, 24);
            WXP_VS_textbox.TabIndex = 5;
            WXP_VS_textbox.TextAlign = HorizontalAlignment.Left;
            WXP_VS_textbox.UseSystemPasswordChar = false;
            WXP_VS_textbox.WordWrap = true;
            // 
            // GroupBox47
            // 
            GroupBox47.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox47.BackColor = Color.FromArgb(34, 34, 34);
            GroupBox47.Controls.Add(SeparatorVertical3);
            GroupBox47.Controls.Add(Label68);
            GroupBox47.Controls.Add(WXP_CustomTheme);
            GroupBox47.Controls.Add(WXP_Classic);
            GroupBox47.Controls.Add(Label66);
            GroupBox47.Controls.Add(Label62);
            GroupBox47.Controls.Add(PictureBox6);
            GroupBox47.Controls.Add(WXP_Luna_Blue);
            GroupBox47.Controls.Add(Label63);
            GroupBox47.Controls.Add(Label64);
            GroupBox47.Controls.Add(WXP_Luna_Silver);
            GroupBox47.Controls.Add(Label65);
            GroupBox47.Controls.Add(WXP_Luna_OliveGreen);
            GroupBox47.Location = new Point(3, 3);
            GroupBox47.Margin = new Padding(4, 3, 4, 3);
            GroupBox47.Name = "GroupBox47";
            GroupBox47.Padding = new Padding(1);
            GroupBox47.Size = new Size(519, 145);
            GroupBox47.TabIndex = 41;
            // 
            // SeparatorVertical3
            // 
            SeparatorVertical3.AlternativeLook = false;
            SeparatorVertical3.Location = new Point(312, 47);
            SeparatorVertical3.Name = "SeparatorVertical3";
            SeparatorVertical3.Size = new Size(1, 91);
            SeparatorVertical3.TabIndex = 44;
            SeparatorVertical3.TabStop = false;
            // 
            // Label68
            // 
            Label68.AutoEllipsis = true;
            Label68.BackColor = Color.Transparent;
            Label68.Font = new Font("Segoe UI", 9.0f);
            Label68.Location = new Point(315, 116);
            Label68.Name = "Label68";
            Label68.Size = new Size(90, 22);
            Label68.TabIndex = 43;
            Label68.Text = "External";
            Label68.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // WXP_CustomTheme
            // 
            WXP_CustomTheme.Checked = false;
            WXP_CustomTheme.Font = new Font("Segoe UI", 9.0f);
            WXP_CustomTheme.ForeColor = Color.White;
            WXP_CustomTheme.Image = (Image)resources.GetObject("WXP_CustomTheme.Image");
            WXP_CustomTheme.Location = new Point(328, 47);
            WXP_CustomTheme.Name = "WXP_CustomTheme";
            WXP_CustomTheme.ShowText = false;
            WXP_CustomTheme.Size = new Size(64, 64);
            WXP_CustomTheme.TabIndex = 42;
            // 
            // WXP_Classic
            // 
            WXP_Classic.Checked = false;
            WXP_Classic.Font = new Font("Segoe UI", 9.0f);
            WXP_Classic.ForeColor = Color.White;
            WXP_Classic.Image = (Image)resources.GetObject("WXP_Classic.Image");
            WXP_Classic.Location = new Point(420, 47);
            WXP_Classic.Name = "WXP_Classic";
            WXP_Classic.ShowText = false;
            WXP_Classic.Size = new Size(64, 64);
            WXP_Classic.TabIndex = 32;
            // 
            // Label66
            // 
            Label66.AutoEllipsis = true;
            Label66.BackColor = Color.Transparent;
            Label66.Font = new Font("Segoe UI", 9.0f);
            Label66.Location = new Point(407, 116);
            Label66.Name = "Label66";
            Label66.Size = new Size(90, 22);
            Label66.TabIndex = 33;
            Label66.Text = "Classic";
            Label66.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label62
            // 
            Label62.AutoEllipsis = true;
            Label62.BackColor = Color.Transparent;
            Label62.Font = new Font("Segoe UI", 9.0f);
            Label62.Location = new Point(22, 116);
            Label62.Name = "Label62";
            Label62.Size = new Size(90, 22);
            Label62.TabIndex = 39;
            Label62.Text = "Luna Blue";
            Label62.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PictureBox6
            // 
            PictureBox6.BackColor = Color.Transparent;
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(3, 3);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Size = new Size(35, 35);
            PictureBox6.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox6.TabIndex = 1;
            PictureBox6.TabStop = false;
            // 
            // WXP_Luna_Blue
            // 
            WXP_Luna_Blue.Checked = false;
            WXP_Luna_Blue.Font = new Font("Segoe UI", 9.0f);
            WXP_Luna_Blue.ForeColor = Color.White;
            WXP_Luna_Blue.Image = (Image)resources.GetObject("WXP_Luna_Blue.Image");
            WXP_Luna_Blue.Location = new Point(35, 47);
            WXP_Luna_Blue.Name = "WXP_Luna_Blue";
            WXP_Luna_Blue.ShowText = false;
            WXP_Luna_Blue.Size = new Size(64, 64);
            WXP_Luna_Blue.TabIndex = 38;
            // 
            // Label63
            // 
            Label63.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label63.BackColor = Color.Transparent;
            Label63.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label63.Location = new Point(44, 3);
            Label63.Name = "Label63";
            Label63.Size = new Size(471, 35);
            Label63.TabIndex = 0;
            Label63.Text = "Theme";
            Label63.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label64
            // 
            Label64.AutoEllipsis = true;
            Label64.BackColor = Color.Transparent;
            Label64.Font = new Font("Segoe UI", 9.0f);
            Label64.Location = new Point(118, 116);
            Label64.Name = "Label64";
            Label64.Size = new Size(90, 22);
            Label64.TabIndex = 37;
            Label64.Text = "Luna Green";
            Label64.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // WXP_Luna_Silver
            // 
            WXP_Luna_Silver.Checked = false;
            WXP_Luna_Silver.Font = new Font("Segoe UI", 9.0f);
            WXP_Luna_Silver.ForeColor = Color.White;
            WXP_Luna_Silver.Image = (Image)resources.GetObject("WXP_Luna_Silver.Image");
            WXP_Luna_Silver.Location = new Point(230, 47);
            WXP_Luna_Silver.Name = "WXP_Luna_Silver";
            WXP_Luna_Silver.ShowText = false;
            WXP_Luna_Silver.Size = new Size(64, 64);
            WXP_Luna_Silver.TabIndex = 34;
            // 
            // Label65
            // 
            Label65.AutoEllipsis = true;
            Label65.BackColor = Color.Transparent;
            Label65.Font = new Font("Segoe UI", 9.0f);
            Label65.Location = new Point(217, 116);
            Label65.Name = "Label65";
            Label65.Size = new Size(90, 22);
            Label65.TabIndex = 35;
            Label65.Text = "Luna Silver";
            Label65.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // WXP_Luna_OliveGreen
            // 
            WXP_Luna_OliveGreen.Checked = false;
            WXP_Luna_OliveGreen.Font = new Font("Segoe UI", 9.0f);
            WXP_Luna_OliveGreen.ForeColor = Color.White;
            WXP_Luna_OliveGreen.Image = (Image)resources.GetObject("WXP_Luna_OliveGreen.Image");
            WXP_Luna_OliveGreen.Location = new Point(131, 47);
            WXP_Luna_OliveGreen.Name = "WXP_Luna_OliveGreen";
            WXP_Luna_OliveGreen.ShowText = false;
            WXP_Luna_OliveGreen.Size = new Size(64, 64);
            WXP_Luna_OliveGreen.TabIndex = 36;
            // 
            // TabPage5
            // 
            TabPage5.BackColor = Color.FromArgb(25, 25, 25);
            TabPage5.Controls.Add(Button25);
            TabPage5.Controls.Add(Button22);
            TabPage5.Controls.Add(log_lbl);
            TabPage5.Controls.Add(Button14);
            TabPage5.Controls.Add(Button8);
            TabPage5.Controls.Add(TreeView1);
            TabPage5.Controls.Add(Separator1);
            TabPage5.Controls.Add(Label60);
            TabPage5.Controls.Add(PictureBox36);
            TabPage5.Location = new Point(4, 24);
            TabPage5.Name = "TabPage5";
            TabPage5.Padding = new Padding(3);
            TabPage5.Size = new Size(529, 517);
            TabPage5.TabIndex = 4;
            TabPage5.Text = "Log";
            // 
            // Button25
            // 
            Button25.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button25.BackColor = Color.FromArgb(34, 34, 34);
            Button25.DrawOnGlass = false;
            Button25.Font = new Font("Segoe UI", 9.0f);
            Button25.ForeColor = Color.White;
            Button25.Image = null;
            Button25.LineColor = Color.FromArgb(0, 81, 210);
            Button25.Location = new Point(297, 482);
            Button25.Name = "Button25";
            Button25.Size = new Size(80, 34);
            Button25.TabIndex = 22;
            Button25.Text = "Stop timer";
            Button25.UseVisualStyleBackColor = false;
            Button25.Visible = false;
            // 
            // Button22
            // 
            Button22.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button22.BackColor = Color.FromArgb(34, 34, 34);
            Button22.DrawOnGlass = false;
            Button22.Font = new Font("Segoe UI", 9.0f);
            Button22.ForeColor = Color.White;
            Button22.Image = null;
            Button22.LineColor = Color.FromArgb(0, 81, 210);
            Button22.Location = new Point(379, 482);
            Button22.Name = "Button22";
            Button22.Size = new Size(85, 34);
            Button22.TabIndex = 21;
            Button22.Text = "Export details";
            Button22.UseVisualStyleBackColor = false;
            Button22.Visible = false;
            // 
            // log_lbl
            // 
            log_lbl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            log_lbl.BackColor = Color.Transparent;
            log_lbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold);
            log_lbl.Location = new Point(6, 454);
            log_lbl.Name = "log_lbl";
            log_lbl.Padding = new Padding(5, 0, 0, 0);
            log_lbl.Size = new Size(519, 25);
            log_lbl.TabIndex = 20;
            log_lbl.Text = @"Error\s happened. Press on 'Show errors' for details";
            log_lbl.TextAlign = ContentAlignment.MiddleLeft;
            log_lbl.Visible = false;
            // 
            // Button14
            // 
            Button14.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button14.BackColor = Color.FromArgb(34, 34, 34);
            Button14.DrawOnGlass = false;
            Button14.Font = new Font("Segoe UI", 9.0f);
            Button14.ForeColor = Color.White;
            Button14.Image = null;
            Button14.LineColor = Color.FromArgb(0, 81, 210);
            Button14.Location = new Point(213, 482);
            Button14.Name = "Button14";
            Button14.Size = new Size(82, 34);
            Button14.TabIndex = 7;
            Button14.Text = "Show errors";
            Button14.UseVisualStyleBackColor = false;
            Button14.Visible = false;
            // 
            // Button8
            // 
            Button8.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button8.BackColor = Color.FromArgb(34, 34, 34);
            Button8.DrawOnGlass = false;
            Button8.Font = new Font("Segoe UI", 9.0f);
            Button8.ForeColor = Color.White;
            Button8.Image = null;
            Button8.LineColor = Color.FromArgb(0, 81, 210);
            Button8.Location = new Point(466, 482);
            Button8.Name = "Button8";
            Button8.Size = new Size(59, 34);
            Button8.TabIndex = 6;
            Button8.Text = "OK";
            Button8.UseVisualStyleBackColor = false;
            // 
            // TreeView1
            // 
            TreeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TreeView1.BackColor = Color.FromArgb(35, 35, 35);
            TreeView1.BorderStyle = BorderStyle.None;
            TreeView1.ForeColor = Color.White;
            TreeView1.FullRowSelect = true;
            TreeView1.ItemHeight = 28;
            TreeView1.Location = new Point(6, 54);
            TreeView1.Name = "TreeView1";
            TreeView1.ShowLines = false;
            TreeView1.Size = new Size(519, 397);
            TreeView1.TabIndex = 5;
            // 
            // Separator1
            // 
            Separator1.AlternativeLook = false;
            Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator1.Location = new Point(6, 47);
            Separator1.Name = "Separator1";
            Separator1.Size = new Size(519, 1);
            Separator1.TabIndex = 4;
            Separator1.TabStop = false;
            // 
            // Label60
            // 
            Label60.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label60.BackColor = Color.Transparent;
            Label60.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            Label60.Location = new Point(47, 6);
            Label60.Name = "Label60";
            Label60.Size = new Size(60, 35);
            Label60.TabIndex = 3;
            Label60.Text = "Log";
            Label60.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureBox36
            // 
            PictureBox36.BackColor = Color.Transparent;
            PictureBox36.Image = (Image)resources.GetObject("PictureBox36.Image");
            PictureBox36.Location = new Point(6, 6);
            PictureBox36.Name = "PictureBox36";
            PictureBox36.Size = new Size(35, 35);
            PictureBox36.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox36.TabIndex = 2;
            PictureBox36.TabStop = false;
            // 
            // Button19
            // 
            Button19.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button19.BackColor = Color.FromArgb(33, 33, 35);
            Button19.DrawOnGlass = false;
            Button19.Font = new Font("Segoe UI", 9.0f);
            Button19.ForeColor = Color.White;
            Button19.Image = (Image)resources.GetObject("Button19.Image");
            Button19.ImageAlign = ContentAlignment.MiddleLeft;
            Button19.LineColor = Color.FromArgb(112, 127, 79);
            Button19.Location = new Point(836, 655);
            Button19.Name = "Button19";
            Button19.Size = new Size(140, 34);
            Button19.TabIndex = 29;
            Button19.Text = "Restart Explorer";
            Button19.UseVisualStyleBackColor = false;
            // 
            // apply_btn
            // 
            apply_btn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            apply_btn.BackColor = Color.FromArgb(33, 33, 35);
            apply_btn.DrawOnGlass = false;
            apply_btn.Font = new Font("Segoe UI", 9.0f);
            apply_btn.ForeColor = Color.White;
            apply_btn.Image = (Image)resources.GetObject("apply_btn.Image");
            apply_btn.ImageAlign = ContentAlignment.MiddleLeft;
            apply_btn.LineColor = Color.FromArgb(74, 133, 186);
            apply_btn.Location = new Point(982, 655);
            apply_btn.Name = "apply_btn";
            apply_btn.Size = new Size(100, 34);
            apply_btn.TabIndex = 16;
            apply_btn.Text = "Apply";
            apply_btn.UseVisualStyleBackColor = false;
            // 
            // Button13
            // 
            Button13.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button13.BackColor = Color.FromArgb(33, 33, 35);
            Button13.DrawOnGlass = false;
            Button13.Font = new Font("Segoe UI", 9.0f);
            Button13.ForeColor = Color.White;
            Button13.Image = null;
            Button13.LineColor = Color.FromArgb(199, 49, 61);
            Button13.Location = new Point(644, 655);
            Button13.Name = "Button13";
            Button13.Size = new Size(80, 34);
            Button13.TabIndex = 26;
            Button13.Text = "Cancel";
            Button13.UseVisualStyleBackColor = false;
            // 
            // MainToolbar
            // 
            MainToolbar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            MainToolbar.BackColor = Color.FromArgb(33, 33, 35);
            MainToolbar.Controls.Add(Button40);
            MainToolbar.Controls.Add(Button39);
            MainToolbar.Controls.Add(Button36);
            MainToolbar.Controls.Add(BetaBadge);
            MainToolbar.Controls.Add(Button31);
            MainToolbar.Controls.Add(Button20);
            MainToolbar.Controls.Add(Button18);
            MainToolbar.Controls.Add(Button17);
            MainToolbar.Controls.Add(Button12);
            MainToolbar.Controls.Add(Button5);
            MainToolbar.Controls.Add(Button6);
            MainToolbar.Controls.Add(Button10);
            MainToolbar.Controls.Add(Button11);
            MainToolbar.Controls.Add(SeparatorVertical1);
            MainToolbar.Controls.Add(status_lbl);
            MainToolbar.Controls.Add(Button7);
            MainToolbar.Controls.Add(Button9);
            MainToolbar.Controls.Add(Button3);
            MainToolbar.Controls.Add(Button2);
            MainToolbar.Location = new Point(13, 11);
            MainToolbar.Margin = new Padding(4, 3, 4, 3);
            MainToolbar.Name = "MainToolbar";
            MainToolbar.Size = new Size(1070, 60);
            MainToolbar.TabIndex = 1;
            // 
            // Button40
            // 
            Button40.BackColor = Color.FromArgb(42, 42, 44);
            Button40.DrawOnGlass = false;
            Button40.Font = new Font("Segoe UI", 9.0f);
            Button40.ForeColor = Color.White;
            Button40.Image = (Image)resources.GetObject("Button40.Image");
            Button40.LineColor = Color.FromArgb(100, 86, 86);
            Button40.Location = new Point(208, 3);
            Button40.Margin = new Padding(4, 3, 4, 3);
            Button40.Name = "Button40";
            Button40.Size = new Size(40, 54);
            Button40.TabIndex = 58;
            Button40.Tag = "Generate a palette for your theme";
            Button40.UseVisualStyleBackColor = false;
            // 
            // Button39
            // 
            Button39.BackColor = Color.FromArgb(42, 42, 44);
            Button39.DrawOnGlass = false;
            Button39.Font = new Font("Segoe UI", 9.0f);
            Button39.ForeColor = Color.White;
            Button39.Image = (Image)resources.GetObject("Button39.Image");
            Button39.LineColor = Color.FromArgb(29, 100, 136);
            Button39.Location = new Point(583, 3);
            Button39.Margin = new Padding(4, 3, 4, 3);
            Button39.Name = "Button39";
            Button39.Size = new Size(40, 54);
            Button39.TabIndex = 57;
            Button39.Tag = "Help (Wiki)";
            Button39.UseVisualStyleBackColor = false;
            // 
            // Button36
            // 
            Button36.BackColor = Color.FromArgb(42, 42, 44);
            Button36.DrawOnGlass = false;
            Button36.Font = new Font("Segoe UI", 9.0f);
            Button36.ForeColor = Color.White;
            Button36.Image = (Image)resources.GetObject("Button36.Image");
            Button36.LineColor = Color.FromArgb(16, 85, 117);
            Button36.Location = new Point(372, 3);
            Button36.Margin = new Padding(4, 3, 4, 3);
            Button36.Name = "Button36";
            Button36.Size = new Size(40, 54);
            Button36.TabIndex = 56;
            Button36.Tag = "WinPaletter themes converter";
            Button36.UseVisualStyleBackColor = false;
            // 
            // BetaBadge
            // 
            BetaBadge.AlertStyle = UI.WP.AlertBox.Style.Notice;
            BetaBadge.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            BetaBadge.BackColor = Color.FromArgb(70, 91, 94);
            BetaBadge.CenterText = true;
            BetaBadge.CustomColor = Color.FromArgb(0, 81, 210);
            BetaBadge.Font = new Font("Segoe UI", 9.0f);
            BetaBadge.Image = null;
            BetaBadge.Location = new Point(1010, 19);
            BetaBadge.Name = "BetaBadge";
            BetaBadge.Size = new Size(47, 22);
            BetaBadge.TabIndex = 55;
            BetaBadge.TabStop = false;
            BetaBadge.Text = "BETA";
            BetaBadge.Visible = false;
            // 
            // Button31
            // 
            Button31.BackColor = Color.FromArgb(42, 42, 44);
            Button31.DrawOnGlass = false;
            Button31.Font = new Font("Segoe UI", 9.0f);
            Button31.ForeColor = Color.White;
            Button31.Image = (Image)resources.GetObject("Button31.Image");
            Button31.LineColor = Color.FromArgb(114, 109, 112);
            Button31.Location = new Point(501, 3);
            Button31.Margin = new Padding(4, 3, 4, 3);
            Button31.Name = "Button31";
            Button31.Size = new Size(40, 54);
            Button31.TabIndex = 23;
            Button31.Tag = "WinPaletter Store";
            Button31.UseVisualStyleBackColor = false;
            // 
            // Button20
            // 
            Button20.BackColor = Color.FromArgb(42, 42, 44);
            Button20.DrawOnGlass = false;
            Button20.Font = new Font("Segoe UI", 9.0f);
            Button20.ForeColor = Color.White;
            Button20.Image = (Image)resources.GetObject("Button20.Image");
            Button20.LineColor = Color.FromArgb(101, 134, 136);
            Button20.Location = new Point(44, 3);
            Button20.Margin = new Padding(4, 3, 4, 3);
            Button20.Name = "Button20";
            Button20.Size = new Size(40, 54);
            Button20.TabIndex = 22;
            Button20.Tag = "Create new WinPaletter theme File based on native Windows preferences";
            Button20.UseVisualStyleBackColor = false;
            // 
            // Button18
            // 
            Button18.BackColor = Color.FromArgb(42, 42, 44);
            Button18.DrawOnGlass = false;
            Button18.Font = new Font("Segoe UI", 9.0f);
            Button18.ForeColor = Color.White;
            Button18.Image = (Image)resources.GetObject("Button18.Image");
            Button18.LineColor = Color.FromArgb(58, 14, 1);
            Button18.Location = new Point(290, 3);
            Button18.Margin = new Padding(4, 3, 4, 3);
            Button18.Name = "Button18";
            Button18.Size = new Size(40, 54);
            Button18.TabIndex = 21;
            Button18.Tag = "Reload first theme";
            Button18.UseVisualStyleBackColor = false;
            // 
            // Button17
            // 
            Button17.BackColor = Color.FromArgb(42, 42, 44);
            Button17.DrawOnGlass = false;
            Button17.Font = new Font("Segoe UI", 9.0f);
            Button17.ForeColor = Color.White;
            Button17.Image = (Image)resources.GetObject("Button17.Image");
            Button17.LineColor = Color.FromArgb(1, 34, 58);
            Button17.Location = new Point(249, 3);
            Button17.Margin = new Padding(4, 3, 4, 3);
            Button17.Name = "Button17";
            Button17.Size = new Size(40, 54);
            Button17.TabIndex = 20;
            Button17.Tag = "Reload previous theme (similar to undo)";
            Button17.UseVisualStyleBackColor = false;
            // 
            // Button12
            // 
            Button12.BackColor = Color.FromArgb(42, 42, 44);
            Button12.DrawOnGlass = false;
            Button12.Font = new Font("Segoe UI", 9.0f);
            Button12.ForeColor = Color.White;
            Button12.Image = (Image)resources.GetObject("Button12.Image");
            Button12.LineColor = Color.FromArgb(23, 94, 130);
            Button12.Location = new Point(624, 3);
            Button12.Margin = new Padding(4, 3, 4, 3);
            Button12.Name = "Button12";
            Button12.Size = new Size(40, 54);
            Button12.TabIndex = 12;
            Button12.Tag = "About";
            Button12.UseVisualStyleBackColor = false;
            // 
            // Button5
            // 
            Button5.BackColor = Color.FromArgb(42, 42, 44);
            Button5.DrawOnGlass = false;
            Button5.Font = new Font("Segoe UI", 9.0f);
            Button5.ForeColor = Color.White;
            Button5.Image = (Image)resources.GetObject("Button5.Image");
            Button5.LineColor = Color.FromArgb(9, 53, 75);
            Button5.Location = new Point(460, 3);
            Button5.Margin = new Padding(4, 3, 4, 3);
            Button5.Name = "Button5";
            Button5.Size = new Size(40, 54);
            Button5.TabIndex = 10;
            Button5.Tag = "Updates";
            Button5.UseVisualStyleBackColor = false;
            // 
            // Button6
            // 
            Button6.BackColor = Color.FromArgb(42, 42, 44);
            Button6.DrawOnGlass = false;
            Button6.Font = new Font("Segoe UI", 9.0f);
            Button6.ForeColor = Color.White;
            Button6.Image = (Image)resources.GetObject("Button6.Image");
            Button6.LineColor = Color.FromArgb(101, 45, 137);
            Button6.Location = new Point(542, 3);
            Button6.Margin = new Padding(4, 3, 4, 3);
            Button6.Name = "Button6";
            Button6.Size = new Size(40, 54);
            Button6.TabIndex = 11;
            Button6.Tag = "What's new";
            Button6.UseVisualStyleBackColor = false;
            // 
            // Button10
            // 
            Button10.BackColor = Color.FromArgb(42, 42, 44);
            Button10.DrawOnGlass = false;
            Button10.Font = new Font("Segoe UI", 9.0f);
            Button10.ForeColor = Color.White;
            Button10.Image = (Image)resources.GetObject("Button10.Image");
            Button10.LineColor = Color.FromArgb(46, 38, 12);
            Button10.Location = new Point(331, 3);
            Button10.Margin = new Padding(4, 3, 4, 3);
            Button10.Name = "Button10";
            Button10.Size = new Size(40, 54);
            Button10.TabIndex = 8;
            Button10.Tag = "Edit information of current WinPaletter theme";
            Button10.UseVisualStyleBackColor = false;
            // 
            // Button11
            // 
            Button11.BackColor = Color.FromArgb(42, 42, 44);
            Button11.DrawOnGlass = false;
            Button11.Font = new Font("Segoe UI", 9.0f);
            Button11.ForeColor = Color.White;
            Button11.Image = (Image)resources.GetObject("Button11.Image");
            Button11.LineColor = Color.FromArgb(65, 73, 81);
            Button11.Location = new Point(419, 3);
            Button11.Margin = new Padding(4, 3, 4, 3);
            Button11.Name = "Button11";
            Button11.Size = new Size(40, 54);
            Button11.TabIndex = 9;
            Button11.Tag = "Settings";
            Button11.UseVisualStyleBackColor = false;
            // 
            // SeparatorVertical1
            // 
            SeparatorVertical1.AlternativeLook = false;
            SeparatorVertical1.Location = new Point(415, 5);
            SeparatorVertical1.Name = "SeparatorVertical1";
            SeparatorVertical1.Size = new Size(1, 50);
            SeparatorVertical1.TabIndex = 7;
            SeparatorVertical1.TabStop = false;
            SeparatorVertical1.Text = "SeparatorVertical1";
            // 
            // status_lbl
            // 
            status_lbl.BackColor = Color.Transparent;
            status_lbl.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold);
            status_lbl.Location = new Point(667, 5);
            status_lbl.Name = "status_lbl";
            status_lbl.Padding = new Padding(5, 0, 0, 0);
            status_lbl.Size = new Size(337, 50);
            status_lbl.TabIndex = 19;
            status_lbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Button7
            // 
            Button7.BackColor = Color.FromArgb(42, 42, 44);
            Button7.DrawOnGlass = false;
            Button7.Font = new Font("Segoe UI", 9.0f);
            Button7.ForeColor = Color.White;
            Button7.Image = (Image)resources.GetObject("Button7.Image");
            Button7.LineColor = Color.FromArgb(70, 113, 132);
            Button7.Location = new Point(126, 3);
            Button7.Margin = new Padding(4, 3, 4, 3);
            Button7.Name = "Button7";
            Button7.Size = new Size(40, 54);
            Button7.TabIndex = 3;
            Button7.Tag = "Save WinPaletter theme file";
            Button7.UseVisualStyleBackColor = false;
            // 
            // Button9
            // 
            Button9.BackColor = Color.FromArgb(42, 42, 44);
            Button9.DrawOnGlass = false;
            Button9.Font = new Font("Segoe UI", 9.0f);
            Button9.ForeColor = Color.White;
            Button9.Image = (Image)resources.GetObject("Button9.Image");
            Button9.LineColor = Color.FromArgb(76, 114, 126);
            Button9.Location = new Point(167, 3);
            Button9.Margin = new Padding(4, 3, 4, 3);
            Button9.Name = "Button9";
            Button9.Size = new Size(40, 54);
            Button9.TabIndex = 4;
            Button9.Tag = "Save WinPaletter theme file as ...";
            Button9.UseVisualStyleBackColor = false;
            // 
            // Button3
            // 
            Button3.BackColor = Color.FromArgb(42, 42, 44);
            Button3.DrawOnGlass = false;
            Button3.Font = new Font("Segoe UI", 9.0f);
            Button3.ForeColor = Color.White;
            Button3.Image = (Image)resources.GetObject("Button3.Image");
            Button3.LineColor = Color.FromArgb(115, 145, 136);
            Button3.Location = new Point(3, 3);
            Button3.Margin = new Padding(4, 3, 4, 3);
            Button3.Name = "Button3";
            Button3.Size = new Size(40, 54);
            Button3.TabIndex = 2;
            Button3.Tag = "Create new WinPaletter theme file based on the current applied preferences";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.BackColor = Color.FromArgb(42, 42, 44);
            Button2.DrawOnGlass = false;
            Button2.Font = new Font("Segoe UI", 9.0f);
            Button2.ForeColor = Color.White;
            Button2.Image = (Image)resources.GetObject("Button2.Image");
            Button2.LineColor = Color.FromArgb(161, 122, 24);
            Button2.Location = new Point(85, 3);
            Button2.Margin = new Padding(4, 3, 4, 3);
            Button2.Name = "Button2";
            Button2.Size = new Size(40, 54);
            Button2.TabIndex = 2;
            Button2.Tag = "Open a WinPaletter theme file";
            Button2.UseVisualStyleBackColor = false;
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox3.BackColor = Color.FromArgb(33, 33, 35);
            GroupBox3.Controls.Add(Button26);
            GroupBox3.Controls.Add(Button35);
            GroupBox3.Controls.Add(Button34);
            GroupBox3.Controls.Add(Button33);
            GroupBox3.Controls.Add(Button32);
            GroupBox3.Controls.Add(Button29);
            GroupBox3.Controls.Add(Button27);
            GroupBox3.Controls.Add(Button24);
            GroupBox3.Controls.Add(Button21);
            GroupBox3.Controls.Add(Button16);
            GroupBox3.Controls.Add(Button4);
            GroupBox3.Location = new Point(13, 624);
            GroupBox3.Margin = new Padding(4, 3, 4, 3);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(530, 66);
            GroupBox3.TabIndex = 28;
            // 
            // Button26
            // 
            Button26.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Button26.BackColor = Color.FromArgb(42, 42, 44);
            Button26.DrawOnGlass = false;
            Button26.Font = new Font("Segoe UI", 9.0f);
            Button26.ForeColor = Color.White;
            Button26.Image = (Image)resources.GetObject("Button26.Image");
            Button26.LineColor = Color.FromArgb(76, 106, 113);
            Button26.Location = new Point(77, 3);
            Button26.Name = "Button26";
            Button26.Size = new Size(40, 60);
            Button26.TabIndex = 37;
            Button26.Tag = "WinPaletter application theme";
            Button26.UseVisualStyleBackColor = false;
            // 
            // Button35
            // 
            Button35.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Button35.BackColor = Color.FromArgb(42, 42, 44);
            Button35.DrawOnGlass = false;
            Button35.Font = new Font("Segoe UI", 9.0f);
            Button35.ForeColor = Color.White;
            Button35.Image = (Image)resources.GetObject("Button35.Image");
            Button35.LineColor = Color.FromArgb(40, 88, 135);
            Button35.Location = new Point(282, 3);
            Button35.Name = "Button35";
            Button35.Size = new Size(40, 60);
            Button35.TabIndex = 36;
            Button35.Tag = "Wallpaper";
            Button35.UseVisualStyleBackColor = false;
            // 
            // Button34
            // 
            Button34.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Button34.BackColor = Color.FromArgb(42, 42, 44);
            Button34.DrawOnGlass = false;
            Button34.Font = new Font("Segoe UI", 9.0f);
            Button34.ForeColor = Color.White;
            Button34.Image = (Image)resources.GetObject("Button34.Image");
            Button34.LineColor = Color.FromArgb(58, 12, 27);
            Button34.Location = new Point(118, 3);
            Button34.Name = "Button34";
            Button34.Size = new Size(40, 60);
            Button34.TabIndex = 35;
            Button34.Tag = "Sounds";
            Button34.UseVisualStyleBackColor = false;
            // 
            // Button33
            // 
            Button33.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Button33.BackColor = Color.FromArgb(42, 42, 44);
            Button33.DrawOnGlass = false;
            Button33.Font = new Font("Segoe UI", 9.0f);
            Button33.ForeColor = Color.White;
            Button33.Image = (Image)resources.GetObject("Button33.Image");
            Button33.LineColor = Color.FromArgb(53, 58, 67);
            Button33.Location = new Point(159, 3);
            Button33.Name = "Button33";
            Button33.Size = new Size(40, 60);
            Button33.TabIndex = 34;
            Button33.Tag = "Screen Saver";
            Button33.UseVisualStyleBackColor = false;
            // 
            // Button32
            // 
            Button32.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Button32.BackColor = Color.FromArgb(42, 42, 44);
            Button32.DrawOnGlass = false;
            Button32.Font = new Font("Segoe UI", 9.0f);
            Button32.ForeColor = Color.White;
            Button32.Image = (Image)resources.GetObject("Button32.Image");
            Button32.LineColor = Color.FromArgb(66, 77, 82);
            Button32.Location = new Point(200, 3);
            Button32.Name = "Button32";
            Button32.Size = new Size(40, 60);
            Button32.TabIndex = 33;
            Button32.Tag = "Windows Switcher (Alt+Tab)";
            Button32.UseVisualStyleBackColor = false;
            // 
            // Button29
            // 
            Button29.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Button29.BackColor = Color.FromArgb(42, 42, 44);
            Button29.DrawOnGlass = false;
            Button29.Font = new Font("Segoe UI", 9.0f);
            Button29.ForeColor = Color.White;
            Button29.Image = (Image)resources.GetObject("Button29.Image");
            Button29.LineColor = Color.FromArgb(23, 87, 111);
            Button29.Location = new Point(241, 3);
            Button29.Name = "Button29";
            Button29.Size = new Size(40, 60);
            Button29.TabIndex = 32;
            Button29.Tag = "Windows Effects";
            Button29.UseVisualStyleBackColor = false;
            // 
            // Button27
            // 
            Button27.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Button27.BackColor = Color.FromArgb(42, 42, 44);
            Button27.DrawOnGlass = false;
            Button27.Font = new Font("Segoe UI", 9.0f);
            Button27.ForeColor = Color.White;
            Button27.Image = (Image)resources.GetObject("Button27.Image");
            Button27.LineColor = Color.FromArgb(140, 107, 1);
            Button27.Location = new Point(323, 3);
            Button27.Name = "Button27";
            Button27.Size = new Size(40, 60);
            Button27.TabIndex = 29;
            Button27.Tag = "Metrics and Fonts";
            Button27.UseVisualStyleBackColor = false;
            // 
            // Button24
            // 
            Button24.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Button24.BackColor = Color.FromArgb(42, 42, 44);
            Button24.DrawOnGlass = false;
            Button24.Font = new Font("Segoe UI", 9.0f);
            Button24.ForeColor = Color.White;
            Button24.Image = (Image)resources.GetObject("Button24.Image");
            Button24.LineColor = Color.FromArgb(70, 70, 70);
            Button24.Location = new Point(364, 3);
            Button24.Name = "Button24";
            Button24.Size = new Size(40, 60);
            Button24.TabIndex = 28;
            Button24.Tag = "Terminals ...";
            Button24.UseVisualStyleBackColor = false;
            // 
            // Button21
            // 
            Button21.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Button21.BackColor = Color.FromArgb(42, 42, 44);
            Button21.DrawOnGlass = false;
            Button21.Font = new Font("Segoe UI", 9.0f);
            Button21.ForeColor = Color.White;
            Button21.Image = (Image)resources.GetObject("Button21.Image");
            Button21.LineColor = Color.FromArgb(9, 42, 56);
            Button21.Location = new Point(405, 3);
            Button21.Name = "Button21";
            Button21.Size = new Size(40, 60);
            Button21.TabIndex = 27;
            Button21.Tag = "Cursors";
            Button21.UseVisualStyleBackColor = false;
            // 
            // Button16
            // 
            Button16.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Button16.BackColor = Color.FromArgb(42, 42, 44);
            Button16.DrawOnGlass = false;
            Button16.Font = new Font("Segoe UI", 9.0f);
            Button16.ForeColor = Color.White;
            Button16.Image = (Image)resources.GetObject("Button16.Image");
            Button16.LineColor = Color.FromArgb(72, 129, 113);
            Button16.Location = new Point(487, 3);
            Button16.Name = "Button16";
            Button16.Size = new Size(40, 60);
            Button16.TabIndex = 26;
            Button16.Tag = "LogonUI";
            Button16.UseVisualStyleBackColor = false;
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Button4.BackColor = Color.FromArgb(42, 42, 44);
            Button4.DrawOnGlass = false;
            Button4.Font = new Font("Segoe UI", 9.0f);
            Button4.ForeColor = Color.White;
            Button4.Image = (Image)resources.GetObject("Button4.Image");
            Button4.LineColor = Color.FromArgb(136, 100, 81);
            Button4.Location = new Point(446, 3);
            Button4.Name = "Button4";
            Button4.Size = new Size(40, 60);
            Button4.TabIndex = 25;
            Button4.Tag = "Classic Colors";
            Button4.UseVisualStyleBackColor = false;
            // 
            // MainFrm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 24, 26);
            ClientSize = new Size(1094, 701);
            Controls.Add(Button28);
            Controls.Add(previewContainer);
            Controls.Add(TablessControl1);
            Controls.Add(Button19);
            Controls.Add(apply_btn);
            Controls.Add(Button13);
            Controls.Add(MainToolbar);
            Controls.Add(GroupBox3);
            Font = new Font("Segoe UI", 9.0f);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(1110, 728);
            Name = "MainFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WinPaletter";
            previewContainer.ResumeLayout(false);
            tabs_preview.ResumeLayout(false);
            TabPage6.ResumeLayout(false);
            pnl_preview.ResumeLayout(false);
            Window1.ResumeLayout(false);
            Panel3.ResumeLayout(false);
            TabPage7.ResumeLayout(false);
            pnl_preview_classic.ResumeLayout(false);
            ClassicTaskbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox21).EndInit();
            TablessControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            PaletteContainer_W11.ResumeLayout(false);
            GroupBox13.ResumeLayout(false);
            GroupBox14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W11_pic9).EndInit();
            pnl8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W11_pic8).EndInit();
            pnl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W11_pic7).EndInit();
            pnl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W11_pic4).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox10).EndInit();
            pnl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W11_pic6).EndInit();
            pnl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W11_pic1).EndInit();
            pnl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W11_pic3).EndInit();
            pnl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W11_pic2).EndInit();
            pnl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W11_pic5).EndInit();
            GroupBox5.ResumeLayout(false);
            GroupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox19).EndInit();
            GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            GroupBox18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox18).EndInit();
            GroupBox24.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox20).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox17).EndInit();
            GroupBox1.ResumeLayout(false);
            GroupBox20.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox11).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            GroupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            TabPage2.ResumeLayout(false);
            Panel1.ResumeLayout(false);
            GroupBox2.ResumeLayout(false);
            GroupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W10_pic9).EndInit();
            GroupBox16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W10_pic8).EndInit();
            GroupBox25.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W10_pic7).EndInit();
            GroupBox27.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W10_pic4).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox7).EndInit();
            GroupBox28.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W10_pic6).EndInit();
            GroupBox31.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W10_pic1).EndInit();
            GroupBox34.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W10_pic3).EndInit();
            GroupBox35.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W10_pic2).EndInit();
            GroupBox36.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)W10_pic5).EndInit();
            GroupBox37.ResumeLayout(false);
            GroupBox23.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox15).EndInit();
            GroupBox38.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox16).EndInit();
            GroupBox40.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox22).EndInit();
            GroupBox42.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox26).EndInit();
            GroupBox43.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox27).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox31).EndInit();
            GroupBox44.ResumeLayout(false);
            GroupBox45.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox33).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox34).EndInit();
            GroupBox46.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox35).EndInit();
            TabPage3.ResumeLayout(false);
            PaletteContainer_W81.ResumeLayout(false);
            GroupBox17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox37).EndInit();
            GroupBox32.ResumeLayout(false);
            GroupBox39.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox28).EndInit();
            GroupBox41.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox29).EndInit();
            GroupBox15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox9).EndInit();
            GroupBox33.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox30).EndInit();
            GroupBox29.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox23).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox32).EndInit();
            TabPage4.ResumeLayout(false);
            PaletteContainer_W7.ResumeLayout(false);
            GroupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox13).EndInit();
            GroupBox22.ResumeLayout(false);
            GroupBox19.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox24).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox39).EndInit();
            GroupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox8).EndInit();
            GroupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox4).EndInit();
            GroupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            GroupBox30.ResumeLayout(false);
            GroupBox21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox12).EndInit();
            GroupBox26.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox14).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox25).EndInit();
            TabPage8.ResumeLayout(false);
            GroupBox50.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox45).EndInit();
            GroupBox49.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox42).EndInit();
            TabPage9.ResumeLayout(false);
            GroupBox48.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox38).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox41).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox40).EndInit();
            GroupBox47.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            TabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox36).EndInit();
            MainToolbar.ResumeLayout(false);
            GroupBox3.ResumeLayout(false);
            Load += new EventHandler(MainFrm_Load);
            Shown += new EventHandler(MainFrm_Shown);
            FormClosing += new FormClosingEventHandler(MainFrm_FormClosing);
            FormClosed += new FormClosedEventHandler(MainFrm_FormClosed);
            ResizeBegin += new EventHandler(MainFrm_ResizeBegin);
            ResizeEnd += new EventHandler(MainFrm_ResizeEnd);
            ResumeLayout(false);

        }
        internal UI.WP.TablessControl TablessControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal UI.WP.GroupBox MainToolbar;
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
        internal Label status_lbl;
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
        internal PictureBox PictureBox33;
        internal Label Label57;
        internal UI.Controllers.ColorItem W10_InactiveTitlebar_pick;
        internal PictureBox PictureBox34;
        internal Label Label58;
        internal UI.WP.GroupBox GroupBox46;
        internal PictureBox PictureBox35;
        internal Label Label59;
        internal UI.Controllers.ColorItem W10_ActiveTitlebar_pick;
        internal UI.WP.RadioImage Select_W7;
        internal UI.WP.RadioImage Select_W81;
        internal UI.WP.RadioImage Select_W10;
        internal UI.WP.RadioImage Select_W11;
        internal TabPage TabPage5;
        internal UI.WP.SeparatorH Separator1;
        internal Label Label60;
        internal PictureBox PictureBox36;
        internal UI.WP.Button Button8;
        internal TreeView TreeView1;
        internal UI.WP.Button Button14;
        internal Label log_lbl;
        internal Timer Timer1;
        internal UI.WP.Button Button22;
        internal SaveFileDialog SaveFileDialog3;
        internal UI.WP.Button W81_ColorizationBalance_val;
        internal UI.WP.Button W7_ColorizationGlassReflectionIntensity_val;
        internal UI.WP.Button W7_ColorizationBlurBalance_val;
        internal UI.WP.Button W7_ColorizationColorBalance_val;
        internal UI.WP.Button W7_ColorizationAfterglowBalance_val;
        internal UI.WP.Button Button25;
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
        internal UI.WP.AlertBox WXP_Alert3;
        internal UI.WP.CheckBox WXP_VS_ReplaceColors;
        internal Label Label76;
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
    }
}