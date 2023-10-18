using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.Retro;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{

    public partial class Win32UI
    {
        public Win32UI()
        {
            InitializeComponent();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Win32UI_Load(object sender, EventArgs e)
        {
            ComboBox1.PopulateThemes();
            ComboBox1.SelectedIndex = 0;
            ApplyDefaultTMValues();
            LoadTM(Program.TM);
            SetMetrics(Program.TM);
            this.DoubleBuffer();

            foreach (ColorItem ColorItem in this.GetAllControls().OfType<ColorItem>())
            {
                ColorItem.Click += ColorItem_Click;
                ColorItem.DragDrop += ColorItem_DragDrop;
            }

            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
        }

        private void Win32UI_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (ColorItem ColorItem in this.GetAllControls().OfType<ColorItem>())
            {
                ColorItem.Click -= ColorItem_Click;
                ColorItem.DragDrop -= ColorItem_DragDrop;
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ColorItem).FullName) is ColorItem)
            {
                Focus();
                BringToFront();
            }
            else
            {
                return;
            }

            base.OnDragOver(e);
        }

        public void LoadTM(Theme.Manager TM)
        {
            LoadFromTM(TM);
            ApplyRetroPreview();
        }

        public void LoadFromTM(Theme.Manager TM)
        {
            Toggle1.Checked = TM.Win32.EnableTheming;
            Toggle2.Checked = TM.Win32.EnableGradient;
            ActiveBorder_pick.BackColor = TM.Win32.ActiveBorder;
            activetitle_pick.BackColor = TM.Win32.ActiveTitle;
            AppWorkspace_pick.BackColor = TM.Win32.AppWorkspace;
            background_pick.BackColor = TM.Win32.Background;
            btnaltface_pick.BackColor = TM.Win32.ButtonAlternateFace;
            btndkshadow_pick.BackColor = TM.Win32.ButtonDkShadow;
            btnface_pick.BackColor = TM.Win32.ButtonFace;
            btnhilight_pick.BackColor = TM.Win32.ButtonHilight;
            btnlight_pick.BackColor = TM.Win32.ButtonLight;
            btnshadow_pick.BackColor = TM.Win32.ButtonShadow;
            btntext_pick.BackColor = TM.Win32.ButtonText;
            GActivetitle_pick.BackColor = TM.Win32.GradientActiveTitle;
            GInactivetitle_pick.BackColor = TM.Win32.GradientInactiveTitle;
            GrayText_pick.BackColor = TM.Win32.GrayText;
            hilighttext_pick.BackColor = TM.Win32.HilightText;
            hottracking_pick.BackColor = TM.Win32.HotTrackingColor;
            InactiveBorder_pick.BackColor = TM.Win32.InactiveBorder;
            InactiveTitle_pick.BackColor = TM.Win32.InactiveTitle;
            InactivetitleText_pick.BackColor = TM.Win32.InactiveTitleText;
            InfoText_pick.BackColor = TM.Win32.InfoText;
            InfoWindow_pick.BackColor = TM.Win32.InfoWindow;
            menu_pick.BackColor = TM.Win32.Menu;
            menubar_pick.BackColor = TM.Win32.MenuBar;
            menutext_pick.BackColor = TM.Win32.MenuText;
            Scrollbar_pick.BackColor = TM.Win32.Scrollbar;
            TitleText_pick.BackColor = TM.Win32.TitleText;
            Window_pick.BackColor = TM.Win32.Window;
            Frame_pick.BackColor = TM.Win32.WindowFrame;
            WindowText_pick.BackColor = TM.Win32.WindowText;
            hilight_pick.BackColor = TM.Win32.Hilight;
            menuhilight_pick.BackColor = TM.Win32.MenuHilight;
            desktop_pick.BackColor = TM.Win32.Desktop;
        }

        public void ApplyDefaultTMValues()
        {
            Theme.Manager DefTM;

            if (Program.W11)
            {
                DefTM = new Theme.Default().Windows11();
            }
            else if (Program.W10)
            {
                DefTM = new Theme.Default().Windows10();
            }
            else if (Program.W81)
            {
                DefTM = new Theme.Default().Windows81();
            }
            else if (Program.W7)
            {
                DefTM = new Theme.Default().Windows7();
            }
            else if (Program.WVista)
            {
                DefTM = new Theme.Default().WindowsVista();
            }
            else if (Program.WXP)
            {
                DefTM = new Theme.Default().WindowsXP();
            }
            else
            {
                DefTM = new Theme.Default().Windows11();
            }

            ActiveBorder_pick.DefaultColor = DefTM.Win32.ActiveBorder;
            activetitle_pick.DefaultColor = DefTM.Win32.ActiveTitle;
            AppWorkspace_pick.DefaultColor = DefTM.Win32.AppWorkspace;
            background_pick.DefaultColor = DefTM.Win32.Background;
            btnaltface_pick.DefaultColor = DefTM.Win32.ButtonAlternateFace;
            btndkshadow_pick.DefaultColor = DefTM.Win32.ButtonDkShadow;
            btnface_pick.DefaultColor = DefTM.Win32.ButtonFace;
            btnhilight_pick.DefaultColor = DefTM.Win32.ButtonHilight;
            btnlight_pick.DefaultColor = DefTM.Win32.ButtonLight;
            btnshadow_pick.DefaultColor = DefTM.Win32.ButtonShadow;
            btntext_pick.DefaultColor = DefTM.Win32.ButtonText;
            GActivetitle_pick.DefaultColor = DefTM.Win32.GradientActiveTitle;
            GInactivetitle_pick.DefaultColor = DefTM.Win32.GradientInactiveTitle;
            GrayText_pick.DefaultColor = DefTM.Win32.GrayText;
            hilighttext_pick.DefaultColor = DefTM.Win32.HilightText;
            hottracking_pick.DefaultColor = DefTM.Win32.HotTrackingColor;
            InactiveBorder_pick.DefaultColor = DefTM.Win32.InactiveBorder;
            InactiveTitle_pick.DefaultColor = DefTM.Win32.InactiveTitle;
            InactivetitleText_pick.DefaultColor = DefTM.Win32.InactiveTitleText;
            InfoText_pick.DefaultColor = DefTM.Win32.InfoText;
            InfoWindow_pick.DefaultColor = DefTM.Win32.InfoWindow;
            menu_pick.DefaultColor = DefTM.Win32.Menu;
            menubar_pick.DefaultColor = DefTM.Win32.MenuBar;
            menutext_pick.DefaultColor = DefTM.Win32.MenuText;
            Scrollbar_pick.DefaultColor = DefTM.Win32.Scrollbar;
            TitleText_pick.DefaultColor = DefTM.Win32.TitleText;
            Window_pick.DefaultColor = DefTM.Win32.Window;
            Frame_pick.DefaultColor = DefTM.Win32.WindowFrame;
            WindowText_pick.DefaultColor = DefTM.Win32.WindowText;
            hilight_pick.DefaultColor = DefTM.Win32.Hilight;
            menuhilight_pick.DefaultColor = DefTM.Win32.MenuHilight;
            desktop_pick.DefaultColor = DefTM.Win32.Desktop;
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            TM.Win32.EnableTheming = Toggle1.Checked;
            TM.Win32.EnableGradient = Toggle2.Checked;
            TM.Win32.ActiveBorder = ActiveBorder_pick.BackColor;
            TM.Win32.ActiveTitle = activetitle_pick.BackColor;
            TM.Win32.AppWorkspace = AppWorkspace_pick.BackColor;
            TM.Win32.Background = background_pick.BackColor;
            TM.Win32.ButtonAlternateFace = btnaltface_pick.BackColor;
            TM.Win32.ButtonDkShadow = btndkshadow_pick.BackColor;
            TM.Win32.ButtonFace = btnface_pick.BackColor;
            TM.Win32.ButtonHilight = btnhilight_pick.BackColor;
            TM.Win32.ButtonLight = btnlight_pick.BackColor;
            TM.Win32.ButtonShadow = btnshadow_pick.BackColor;
            TM.Win32.ButtonText = btntext_pick.BackColor;
            TM.Win32.GradientActiveTitle = GActivetitle_pick.BackColor;
            TM.Win32.GradientInactiveTitle = GInactivetitle_pick.BackColor;
            TM.Win32.GrayText = GrayText_pick.BackColor;
            TM.Win32.HilightText = hilighttext_pick.BackColor;
            TM.Win32.HotTrackingColor = hottracking_pick.BackColor;
            TM.Win32.InactiveBorder = InactiveBorder_pick.BackColor;
            TM.Win32.InactiveTitle = InactiveTitle_pick.BackColor;
            TM.Win32.InactiveTitleText = InactivetitleText_pick.BackColor;
            TM.Win32.InfoText = InfoText_pick.BackColor;
            TM.Win32.InfoWindow = InfoWindow_pick.BackColor;
            TM.Win32.Menu = menu_pick.BackColor;
            TM.Win32.MenuBar = menubar_pick.BackColor;
            TM.Win32.MenuText = menutext_pick.BackColor;
            TM.Win32.Scrollbar = Scrollbar_pick.BackColor;
            TM.Win32.TitleText = TitleText_pick.BackColor;
            TM.Win32.Window = Window_pick.BackColor;
            TM.Win32.WindowFrame = Frame_pick.BackColor;
            TM.Win32.WindowText = WindowText_pick.BackColor;
            TM.Win32.Hilight = hilight_pick.BackColor;
            TM.Win32.MenuHilight = menuhilight_pick.BackColor;
            TM.Win32.Desktop = desktop_pick.BackColor;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM);
            SetClassicWindowColors(Program.TM, Forms.MainFrm.ClassicWindow1);
            SetClassicWindowColors(Program.TM, Forms.MainFrm.ClassicWindow2, false);
            SetClassicButtonColors(Program.TM, Forms.MainFrm.ButtonR2);
            SetClassicButtonColors(Program.TM, Forms.MainFrm.ButtonR3);
            SetClassicButtonColors(Program.TM, Forms.MainFrm.ButtonR4);
            SetClassicPanelRaisedRColors(Program.TM, Forms.MainFrm.ClassicTaskbar);
            Close();
        }

        private void ColorItem_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
                return;

            try
            {
                if (((MouseEventArgs)e).Button == MouseButtons.Right)
                {
                    Forms.SubMenu.ShowMenu((ColorItem)sender);
                    if (Program.ColorEvent == Program.MenuEvent.Cut | Program.ColorEvent == Program.MenuEvent.Paste | Program.ColorEvent == Program.MenuEvent.Override)
                    {
                        ApplyRetroPreview();
                    }
                    return;
                }
            }
            catch
            {
            }

            var CList = new List<Control>() { (Control)sender };

            Color C = ((UI.Controllers.ColorItem)sender).BackColor;

            switch (((UI.Controllers.ColorItem)sender).ToString().ToLower() ?? "")
            {
                case var @case when @case == ("activetitle_pick".ToLower() ?? ""):
                    {
                        CList.Add(WindowR2);
                        CList.Add(WindowR3);
                        CList.Add(WindowR4);

                        var _Conditions = new Conditions() { WindowRColor1 = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                        WindowR2.Color1 = C;
                        WindowR3.Color1 = C;
                        WindowR4.Color1 = C;
                        break;
                    }

                case var case1 when case1 == ("GActivetitle_pick".ToLower() ?? ""):
                    {
                        CList.Add(WindowR2);
                        CList.Add(WindowR3);
                        CList.Add(WindowR4);

                        var _Conditions = new Conditions() { WindowRColor2 = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                        WindowR2.Color2 = C;
                        WindowR3.Color2 = C;
                        WindowR4.Color2 = C;
                        break;
                    }

                case var case2 when case2 == ("TitleText_pick".ToLower() ?? ""):
                    {
                        CList.Add(WindowR2);
                        CList.Add(WindowR3);
                        CList.Add(WindowR4);

                        var _Conditions = new Conditions() { WindowRForeColor = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                        WindowR2.ForeColor = C;
                        WindowR3.ForeColor = C;
                        WindowR4.ForeColor = C;
                        break;
                    }

                case var case3 when case3 == ("InactiveTitle_pick".ToLower() ?? ""):
                    {
                        CList.Add(WindowR1);
                        var _Conditions = new Conditions() { WindowRColor1 = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                        WindowR1.Color1 = C;
                        break;
                    }

                case var case4 when case4 == ("GInactivetitle_pick".ToLower() ?? ""):
                    {
                        CList.Add(WindowR1);
                        var _Conditions = new Conditions() { WindowRColor2 = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                        WindowR1.Color2 = C;
                        break;
                    }

                case var case5 when case5 == ("InactivetitleText_pick".ToLower() ?? ""):
                    {
                        CList.Add(WindowR1);
                        var _Conditions = new Conditions() { WindowRForeColor = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                        WindowR1.ForeColor = C;
                        break;
                    }

                case var case6 when case6 == ("ActiveBorder_pick".ToLower() ?? ""):
                    {
                        CList.Add(WindowR2);
                        CList.Add(WindowR3);
                        CList.Add(WindowR4);

                        var _Conditions = new Conditions() { WindowRBorder = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                        WindowR2.ColorBorder = C;
                        WindowR3.ColorBorder = C;
                        WindowR4.ColorBorder = C;
                        break;
                    }

                case var case7 when case7 == ("InactiveBorder_pick".ToLower() ?? ""):
                    {
                        CList.Add(WindowR1);
                        var _Conditions = new Conditions() { WindowRBorder = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                        WindowR1.ColorBorder = C;
                        break;
                    }

                case var case8 when case8 == ("Frame_pick".ToLower() ?? ""):
                    {
                        foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                            CList.Add(ButtonR);

                        CList.Add(Retro3DPreview1);

                        var _Conditions = new Conditions() { WindowRFrame = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

                        foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                            ButtonR.WindowFrame = C;

                        Retro3DPreview1.WindowFrame = C;
                        break;
                    }

                case var case9 when case9 == ("btnface_pick".ToLower() ?? ""):
                    {
                        foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                            CList.Add(WindowR);
                        foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                            CList.Add(ButtonR);

                        CList.Add(Retro3DPreview1);
                        CList.Add(PanelR2);
                        var _Conditions = new Conditions() { ButtonRFace = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

                        foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                        {
                            if (!WindowR.UseItAsMenu)
                                WindowR.BackColor = C;
                            else
                                WindowR.ButtonFace = C;
                        }
                        foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                            ButtonR.BackColor = C;

                        Retro3DPreview1.BackColor = C;
                        PanelR2.BackColor = C;
                        break;
                    }

                case var case10 when case10 == ("btndkshadow_pick".ToLower() ?? ""):
                    {
                        foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                            CList.Add(WindowR);
                        foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                            CList.Add(ButtonR);

                        CList.Add(Retro3DPreview1);
                        CList.Add(TextBoxR1);

                        var _Conditions = new Conditions() { ButtonRDkShadow = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

                        foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                            WindowR.ButtonDkShadow = C;
                        foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                            ButtonR.ButtonDkShadow = C;

                        Retro3DPreview1.ButtonDkShadow = C;
                        TextBoxR1.ButtonDkShadow = C;
                        break;
                    }

                case var case11 when case11 == ("btnhilight_pick".ToLower() ?? ""):
                    {
                        foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                            CList.Add(WindowR);
                        foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                            CList.Add(ButtonR);
                        foreach (PanelRaisedR ButtonR in pnl_preview.GetAllControls().OfType<PanelRaisedR>())
                            CList.Add(ButtonR);

                        CList.Add(Retro3DPreview1);
                        CList.Add(TextBoxR1);
                        CList.Add(PanelR1);
                        CList.Add(PanelR2);

                        var _Conditions = new Conditions() { ButtonRHilight = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

                        foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                            WindowR.ButtonHilight = C;
                        foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                            ButtonR.ButtonHilight = C;
                        foreach (PanelRaisedR ButtonR in pnl_preview.GetAllControls().OfType<PanelRaisedR>())
                            ButtonR.ButtonHilight = C;

                        TextBoxR1.ButtonHilight = C;
                        PanelR1.ButtonHilight = C;
                        PanelR2.ButtonHilight = C;
                        Retro3DPreview1.ButtonHilight = C;
                        break;
                    }

                case var case12 when case12 == ("btnlight_pick".ToLower() ?? ""):
                    {
                        foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                            CList.Add(WindowR);
                        foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                            CList.Add(ButtonR);

                        CList.Add(Retro3DPreview1);
                        CList.Add(TextBoxR1);

                        var _Conditions = new Conditions() { ButtonRLight = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

                        foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                            WindowR.ButtonLight = C;
                        foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                            ButtonR.ButtonLight = C;

                        TextBoxR1.ButtonLight = C;
                        Retro3DPreview1.ButtonLight = C;
                        break;
                    }

                case var case13 when case13 == ("btnshadow_pick".ToLower() ?? ""):
                    {
                        foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                            CList.Add(WindowR);
                        foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                            CList.Add(ButtonR);
                        foreach (PanelRaisedR ButtonR in pnl_preview.GetAllControls().OfType<PanelRaisedR>())
                            CList.Add(ButtonR);

                        CList.Add(Retro3DPreview1);
                        CList.Add(TextBoxR1);
                        CList.Add(PanelR1);

                        var _Conditions = new Conditions() { ButtonRShadow = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

                        foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                            WindowR.ButtonShadow = C;
                        foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                            ButtonR.ButtonShadow = C;
                        foreach (PanelRaisedR ButtonR in pnl_preview.GetAllControls().OfType<PanelRaisedR>())
                            ButtonR.ButtonShadow = C;

                        Retro3DPreview1.ButtonShadow = C;
                        TextBoxR1.ButtonShadow = C;
                        PanelR1.ButtonShadow = C;
                        break;
                    }

                case var case14 when case14 == ("btntext_pick".ToLower() ?? ""):
                    {
                        foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                            CList.Add(WindowR);
                        foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                            CList.Add(ButtonR);

                        CList.Add(Retro3DPreview1);
                        var _Conditions = new Conditions() { ButtonRText = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

                        foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                            WindowR.ButtonText = C;
                        foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                            ButtonR.ForeColor = C;

                        Retro3DPreview1.ForeColor = C;
                        break;
                    }

                case var case15 when case15 == ("AppWorkspace_pick".ToLower() ?? ""):
                    {
                        CList.Add(programcontainer);
                        var _Conditions = new Conditions() { RetroAppWorkspace = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                        programcontainer.BackColor = C;
                        break;
                    }

                case var case16 when case16 == ("background_pick".ToLower() ?? ""):
                    {
                        CList.Add(pnl_preview);
                        var _Conditions = new Conditions() { RetroBackground = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                        pnl_preview.BackColor = C;
                        break;
                    }

                case var case17 when case17 == ("menu_pick".ToLower() ?? ""):
                    {
                        CList.Add(Menu_Window);

                        if (!Toggle1.Checked)
                            CList.Add(PanelR1);
                        if (!Toggle1.Checked)
                            CList.Add(menucontainer0);

                        var _Conditions = new Conditions() { RetroBackground = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                        Menu_Window.BackColor = C;
                        Menu_Window.Invalidate();

                        if (!Toggle1.Checked)
                            PanelR1.BackColor = C;
                        if (!Toggle1.Checked)
                            menucontainer0.BackColor = C;
                        break;
                    }

                case var case18 when case18 == ("menubar_pick".ToLower() ?? ""):
                    {
                        if (Toggle1.Checked)
                        {
                            CList.Add(menucontainer0);
                            var _Conditions = new Conditions() { RetroBackground = true };
                            C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                            menucontainer0.BackColor = C;
                        }
                        else
                        {
                            C = Forms.ColorPickerDlg.Pick(CList);
                            ((UI.Controllers.ColorItem)sender).BackColor = C;
                        }

                        break;
                    }

                case var case19 when case19 == ("hilight_pick".ToLower() ?? ""):
                    {
                        if (Toggle1.Checked)
                        {
                            CList.Add(highlight);
                            CList.Add(PanelR1);
                            var _Conditions = new Conditions() { ButtonRShadow = true, RetroBackground = true, RetroHighlight17BitFixer = true };
                            C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                            highlight.BackColor = C;
                            PanelR1.ButtonShadow = C;
                        }
                        else
                        {
                            CList.Add(highlight);
                            CList.Add(menuhilight);
                            var _Conditions = new Conditions() { RetroBackground = true };
                            C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                            highlight.BackColor = C;
                            menuhilight.BackColor = C;
                        }

                        break;
                    }


                case var case20 when case20 == ("menuhilight_pick".ToLower() ?? ""):
                    {
                        if (Toggle1.Checked)
                        {
                            CList.Add(menuhilight);
                            CList.Add(PanelR1);
                            var _Conditions = new Conditions() { RetroBackground = true };
                            C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                            menuhilight.BackColor = C;
                            PanelR1.BackColor = C;
                        }
                        else
                        {
                            C = Forms.ColorPickerDlg.Pick(CList);
                            ((UI.Controllers.ColorItem)sender).BackColor = C;
                        }

                        break;
                    }

                case var case21 when case21 == ("menutext_pick".ToLower() ?? ""):
                    {
                        CList.Add(LabelR1);
                        if (!Toggle1.Checked)
                            CList.Add(LabelR3);
                        CList.Add(LabelR6);

                        C = Forms.ColorPickerDlg.Pick(CList);

                        LabelR1.ForeColor = C;
                        if (!Toggle1.Checked)
                            LabelR3.ForeColor = C;
                        LabelR6.ForeColor = C;
                        break;
                    }

                case var case22 when case22 == ("hilighttext_pick".ToLower() ?? ""):
                    {
                        CList.Add(LabelR5);
                        if (Toggle1.Checked)
                            CList.Add(LabelR3);

                        C = Forms.ColorPickerDlg.Pick(CList);
                        LabelR5.ForeColor = C;
                        if (Toggle1.Checked)
                            LabelR3.ForeColor = C;
                        break;
                    }

                case var case23 when case23 == ("GrayText_pick".ToLower() ?? ""):
                    {
                        CList.Add(LabelR2);
                        CList.Add(LabelR9);

                        C = Forms.ColorPickerDlg.Pick(CList);
                        LabelR2.ForeColor = C;
                        LabelR9.ForeColor = C;
                        break;
                    }

                case var case24 when case24 == ("Window_pick".ToLower() ?? ""):
                    {
                        CList.Add(TextBoxR1);
                        var _Conditions = new Conditions() { RetroBackground = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                        TextBoxR1.BackColor = C;
                        break;
                    }

                case var case25 when case25 == ("WindowText_pick".ToLower() ?? ""):
                    {
                        CList.Add(TextBoxR1);
                        CList.Add(LabelR4);

                        var _Conditions = new Conditions() { WindowRText = true, WindowRForeColor = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                        TextBoxR1.ForeColor = C;
                        LabelR4.ForeColor = C;
                        break;
                    }

                case var case26 when case26 == ("InfoWindow_pick".ToLower() ?? ""):
                    {
                        CList.Add(LabelR13);
                        var _Conditions = new Conditions() { RetroBackground = true };
                        C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                        LabelR13.BackColor = C;
                        break;
                    }

                case var case27 when case27 == ("InfoText_pick".ToLower() ?? ""):
                    {
                        CList.Add(LabelR13);
                        C = Forms.ColorPickerDlg.Pick(CList);
                        LabelR13.ForeColor = C;
                        break;
                    }

                case var case28 when case28 == ("Scrollbar_pick".ToLower() ?? ""):
                    {
                        C = Forms.ColorPickerDlg.Pick(CList);
                        break;
                    }

                default:
                    {
                        C = Forms.ColorPickerDlg.Pick(CList);
                        break;
                    }


            } ((ColorItem)sender).BackColor = C;

            foreach (var Ctrl in CList)
                Ctrl.Refresh();

            CList.Clear();

            WindowR1.Invalidate();
            WindowR2.Invalidate();
            WindowR3.Invalidate();
            WindowR4.Invalidate();

            Refresh17BitPreference();
        }

        private void ColorItem_DragDrop(object sender, DragEventArgs e)
        {

            WindowR2.Color1 = activetitle_pick.BackColor;
            WindowR3.Color1 = activetitle_pick.BackColor;
            WindowR4.Color1 = activetitle_pick.BackColor;
            WindowR2.Color2 = GActivetitle_pick.BackColor;
            WindowR3.Color2 = GActivetitle_pick.BackColor;
            WindowR4.Color2 = GActivetitle_pick.BackColor;
            WindowR2.ForeColor = TitleText_pick.BackColor;
            WindowR3.ForeColor = TitleText_pick.BackColor;
            WindowR4.ForeColor = TitleText_pick.BackColor;
            WindowR2.ColorBorder = ActiveBorder_pick.BackColor;
            WindowR3.ColorBorder = ActiveBorder_pick.BackColor;
            WindowR4.ColorBorder = ActiveBorder_pick.BackColor;

            WindowR1.Color1 = InactiveTitle_pick.BackColor;
            WindowR1.Color2 = GInactivetitle_pick.BackColor;
            WindowR1.ForeColor = InactivetitleText_pick.BackColor;
            WindowR1.ColorBorder = InactiveBorder_pick.BackColor;

            Retro3DPreview1.WindowFrame = Frame_pick.BackColor;
            Retro3DPreview1.BackColor = btnface_pick.BackColor;
            Retro3DPreview1.ButtonDkShadow = btndkshadow_pick.BackColor;
            Retro3DPreview1.ButtonHilight = btnhilight_pick.BackColor;
            Retro3DPreview1.ButtonLight = btnlight_pick.BackColor;
            Retro3DPreview1.ButtonShadow = btnshadow_pick.BackColor;
            Retro3DPreview1.ForeColor = btntext_pick.BackColor;

            PanelR2.BackColor = btnface_pick.BackColor;
            PanelR2.ButtonHilight = btnhilight_pick.BackColor;

            TextBoxR1.BackColor = Window_pick.BackColor;
            TextBoxR1.ForeColor = WindowText_pick.BackColor;
            TextBoxR1.ButtonDkShadow = btndkshadow_pick.BackColor;
            TextBoxR1.ButtonHilight = btnhilight_pick.BackColor;
            TextBoxR1.ButtonLight = btnlight_pick.BackColor;
            TextBoxR1.ButtonShadow = btnshadow_pick.BackColor;

            PanelR1.ButtonHilight = btnhilight_pick.BackColor;
            PanelR1.ButtonShadow = btnshadow_pick.BackColor;

            programcontainer.BackColor = AppWorkspace_pick.BackColor;
            pnl_preview.BackColor = background_pick.BackColor;
            Menu_Window.BackColor = menu_pick.BackColor;
            Menu_Window.Invalidate();

            if (Toggle1.Checked)
            {
                highlight.BackColor = hilight_pick.BackColor;
                menuhilight.BackColor = menuhilight_pick.BackColor;
                LabelR3.ForeColor = hilighttext_pick.BackColor;
                PanelR1.ButtonShadow = hilight_pick.BackColor;
                PanelR1.BackColor = menuhilight_pick.BackColor;
                menucontainer0.BackColor = menubar_pick.BackColor;
            }
            else
            {
                highlight.BackColor = hilight_pick.BackColor;
                menuhilight.BackColor = hilight_pick.BackColor;
                LabelR3.ForeColor = menutext_pick.BackColor;
                PanelR1.BackColor = menu_pick.BackColor;
                menucontainer0.BackColor = menu_pick.BackColor;
            }

            LabelR1.ForeColor = menutext_pick.BackColor;
            LabelR2.ForeColor = GrayText_pick.BackColor;

            LabelR4.ForeColor = WindowText_pick.BackColor;
            LabelR5.ForeColor = hilighttext_pick.BackColor;
            LabelR6.ForeColor = menutext_pick.BackColor;

            LabelR9.ForeColor = GrayText_pick.BackColor;

            LabelR13.BackColor = InfoWindow_pick.BackColor;
            LabelR13.ForeColor = InfoText_pick.BackColor;

            foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
            {
                if (!WindowR.UseItAsMenu)
                    WindowR.BackColor = btnface_pick.BackColor;
                else
                    WindowR.ButtonFace = btnface_pick.BackColor;

                WindowR.ButtonDkShadow = btndkshadow_pick.BackColor;
                WindowR.ButtonHilight = btnhilight_pick.BackColor;
                WindowR.ButtonLight = btnlight_pick.BackColor;
                WindowR.ButtonShadow = btnshadow_pick.BackColor;
                WindowR.ButtonText = btntext_pick.BackColor;
            }

            foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
            {
                ButtonR.WindowFrame = Frame_pick.BackColor;
                ButtonR.BackColor = btnface_pick.BackColor;
                ButtonR.ButtonDkShadow = btndkshadow_pick.BackColor;
                ButtonR.ButtonHilight = btnhilight_pick.BackColor;
                ButtonR.ButtonLight = btnlight_pick.BackColor;
                ButtonR.ButtonShadow = btnshadow_pick.BackColor;
                ButtonR.ForeColor = btntext_pick.BackColor;
            }

            foreach (PanelRaisedR PanelRaisedR in pnl_preview.GetAllControls().OfType<PanelRaisedR>())
            {
                PanelRaisedR.ButtonHilight = btnhilight_pick.BackColor;
                PanelRaisedR.ButtonShadow = btnshadow_pick.BackColor;
            }

            WindowR1.Invalidate();
            WindowR2.Invalidate();
            WindowR3.Invalidate();

            Refresh17BitPreference();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (OpenThemeDialog.ShowDialog() == DialogResult.OK)
            {
                Toggle1.Checked = false;
                using (var _Def = Theme.Default.From(Program.PreviewStyle))
                {
                    LoadFromWin9xTheme(OpenThemeDialog.FileName, _Def.Win32);
                }

            }
        }

        public void LoadFromWin9xTheme(string File, Theme.Structures.Win32UI _DefWin32)
        {
            if (System.IO.File.Exists(File))
            {

                using (var _ini = new INI(File))
                {
                    string Section = @"Control Panel\Colors";

                    TitleText_pick.BackColor = _ini.Read(Section, "TitleText", _DefWin32.TitleText.ToWin32Reg()).FromWin32RegToColor();
                    InactivetitleText_pick.BackColor = _ini.Read(Section, "InactiveTitleText", _DefWin32.InactiveTitleText.ToWin32Reg()).FromWin32RegToColor();
                    ActiveBorder_pick.BackColor = _ini.Read(Section, "ActiveBorder", _DefWin32.ActiveBorder.ToWin32Reg()).FromWin32RegToColor();
                    InactiveBorder_pick.BackColor = _ini.Read(Section, "InactiveBorder", _DefWin32.InactiveBorder.ToWin32Reg()).FromWin32RegToColor();
                    activetitle_pick.BackColor = _ini.Read(Section, "ActiveTitle", _DefWin32.ActiveTitle.ToWin32Reg()).FromWin32RegToColor();
                    InactiveTitle_pick.BackColor = _ini.Read(Section, "InactiveTitle", _DefWin32.InactiveTitle.ToWin32Reg()).FromWin32RegToColor();

                    var GA = _ini.Read(Section, "GradientActiveTitle").FromWin32RegToColor();
                    var GI = _ini.Read(Section, "GradientInactiveTitle").FromWin32RegToColor();

                    if (GA != Color.Empty)
                    {
                        GActivetitle_pick.BackColor = GA;
                    }
                    else
                    {
                        GActivetitle_pick.BackColor = activetitle_pick.BackColor;
                    }

                    if (GI != Color.Empty)
                    {
                        GInactivetitle_pick.BackColor = GA;
                    }
                    else
                    {
                        GInactivetitle_pick.BackColor = InactiveTitle_pick.BackColor;
                    }

                    btnface_pick.BackColor = _ini.Read(Section, "ButtonFace", _DefWin32.ButtonFace.ToWin32Reg()).FromWin32RegToColor();
                    btnshadow_pick.BackColor = _ini.Read(Section, "ButtonShadow", _DefWin32.ButtonShadow.ToWin32Reg()).FromWin32RegToColor();
                    btntext_pick.BackColor = _ini.Read(Section, "ButtonText", _DefWin32.ButtonText.ToWin32Reg()).FromWin32RegToColor();
                    btnhilight_pick.BackColor = _ini.Read(Section, "ButtonHilight", _DefWin32.ButtonHilight.ToWin32Reg()).FromWin32RegToColor();
                    btndkshadow_pick.BackColor = _ini.Read(Section, "ButtonDkShadow", _DefWin32.ButtonDkShadow.ToWin32Reg()).FromWin32RegToColor();
                    btnlight_pick.BackColor = _ini.Read(Section, "ButtonLight", _DefWin32.ButtonLight.ToWin32Reg()).FromWin32RegToColor();
                    btnaltface_pick.BackColor = _ini.Read(Section, "ButtonAlternateFace", _DefWin32.ButtonAlternateFace.ToWin32Reg()).FromWin32RegToColor();
                    background_pick.BackColor = _ini.Read(Section, "Background", _DefWin32.Background.ToWin32Reg()).FromWin32RegToColor();
                    hilight_pick.BackColor = _ini.Read(Section, "Hilight", _DefWin32.Hilight.ToWin32Reg()).FromWin32RegToColor();
                    hilighttext_pick.BackColor = _ini.Read(Section, "HilightText", _DefWin32.HilightText.ToWin32Reg()).FromWin32RegToColor();
                    Window_pick.BackColor = _ini.Read(Section, "Window", _DefWin32.Window.ToWin32Reg()).FromWin32RegToColor();
                    WindowText_pick.BackColor = _ini.Read(Section, "WindowText", _DefWin32.WindowText.ToWin32Reg()).FromWin32RegToColor();
                    Scrollbar_pick.BackColor = _ini.Read(Section, "ScrollBar", _DefWin32.Scrollbar.ToWin32Reg()).FromWin32RegToColor();
                    menu_pick.BackColor = _ini.Read(Section, "Menu", _DefWin32.Menu.ToWin32Reg()).FromWin32RegToColor();
                    Frame_pick.BackColor = _ini.Read(Section, "WindowFrame", _DefWin32.WindowFrame.ToWin32Reg()).FromWin32RegToColor();
                    menutext_pick.BackColor = _ini.Read(Section, "MenuText", _DefWin32.MenuText.ToWin32Reg()).FromWin32RegToColor();
                    AppWorkspace_pick.BackColor = _ini.Read(Section, "AppWorkspace", _DefWin32.AppWorkspace.ToWin32Reg()).FromWin32RegToColor();
                    GrayText_pick.BackColor = _ini.Read(Section, "GrayText", _DefWin32.GrayText.ToWin32Reg()).FromWin32RegToColor();
                    InfoText_pick.BackColor = _ini.Read(Section, "InfoText", _DefWin32.InfoText.ToWin32Reg()).FromWin32RegToColor();
                    InfoWindow_pick.BackColor = _ini.Read(Section, "InfoWindow", _DefWin32.InfoWindow.ToWin32Reg()).FromWin32RegToColor();
                    hottracking_pick.BackColor = _ini.Read(Section, "HotTrackingColor", _DefWin32.HotTrackingColor.ToWin32Reg()).FromWin32RegToColor();
                    menubar_pick.BackColor = _ini.Read(Section, "MenuBar", _DefWin32.MenuBar.ToWin32Reg()).FromWin32RegToColor();
                    menuhilight_pick.BackColor = _ini.Read(Section, "MenuHilight", _DefWin32.MenuHilight.ToWin32Reg()).FromWin32RegToColor();
                    desktop_pick.BackColor = _ini.Read(Section, "Desktop", _DefWin32.Desktop.ToWin32Reg()).FromWin32RegToColor();
                }

            }

            ApplyRetroPreview();
        }

        public void LoadFromWinThemeString(string String, string ThemeName)
        {
            if (string.IsNullOrWhiteSpace(String))
            {
                return;
            }

            if (!String.Contains("|"))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(ThemeName))
            {
                return;
            }

            var ls = new List<Color>();
            ls.Clear();

            var AllThemes = String.CList();
            string SelectedTheme = "";
            var SelectedThemeList = new List<string>();

            bool Found = false;

            foreach (string th in AllThemes)
            {
                if ((th.Split('|')[0].ToLower() ?? "") == (ThemeName.ToLower() ?? ""))
                {
                    SelectedTheme = th.Replace("|", "\r\n");
                    Found = true;
                    break;
                }
            }

            if (!Found)
            {
                return;
            }

            SelectedThemeList = SelectedTheme.CList();

            bool FoundGradientActive = false;
            bool FoundGradientInactive = false;

            foreach (string x in SelectedThemeList)
            {

                if (x.StartsWith("activetitle=", Program._ignore))
                {
                    activetitle_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                    if (!FoundGradientActive)
                        GActivetitle_pick.BackColor = activetitle_pick.BackColor;
                }

                if (x.StartsWith("gradientactivetitle=", Program._ignore))
                {
                    GActivetitle_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                    FoundGradientActive = true;
                }

                if (x.StartsWith("inactivetitle=", Program._ignore))
                {
                    InactiveTitle_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                    if (!FoundGradientInactive)
                        GInactivetitle_pick.BackColor = InactiveTitle_pick.BackColor;

                }

                if (x.StartsWith("gradientinactivetitle=", Program._ignore))
                {
                    GInactivetitle_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                    FoundGradientInactive = true;
                }

                if (x.StartsWith("background=", Program._ignore))
                    background_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("hilight=", Program._ignore))
                    hilight_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("hilighttext=", Program._ignore))
                    hilighttext_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("titletext=", Program._ignore))
                    TitleText_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("window=", Program._ignore))
                    Window_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("windowtext=", Program._ignore))
                    WindowText_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("scrollbar=", Program._ignore))
                    Scrollbar_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("menu=", Program._ignore))
                    menu_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("windowframe=", Program._ignore))
                    Frame_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("menutext=", Program._ignore))
                    menutext_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("activeborder=", Program._ignore))
                    ActiveBorder_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("inactiveborder=", Program._ignore))
                    InactiveBorder_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("appworkspace=", Program._ignore))
                    AppWorkspace_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("buttonface=", Program._ignore))
                    btnface_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("buttonshadow=", Program._ignore))
                    btnshadow_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("graytext=", Program._ignore))
                    GrayText_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("buttontext=", Program._ignore))
                    btntext_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("inactivetitletext=", Program._ignore))
                    InactivetitleText_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("buttonhilight=", Program._ignore))
                    btnhilight_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("buttondkshadow=", Program._ignore))
                    btndkshadow_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("buttonlight=", Program._ignore))
                    btnlight_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("infotext=", Program._ignore))
                    InfoText_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("infowindow=", Program._ignore))
                    InfoWindow_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("hottrackingcolor=", Program._ignore))
                    hottracking_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("buttonalternateface=", Program._ignore))
                    btnaltface_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("menubar=", Program._ignore))
                    menubar_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("menuhilight=", Program._ignore))
                    menuhilight_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("desktop=", Program._ignore))
                    desktop_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
            }

            ApplyRetroPreview();
        }

        public void SetMetrics(Theme.Manager TM)
        {
            PanelR2.Width = TM.MetricsFonts.ScrollWidth;
            menucontainer0.Height = TM.MetricsFonts.MenuHeight;

            menucontainer0.Height = Math.Max(TM.MetricsFonts.MenuHeight, Forms.Metrics_Fonts.GetTitleTextHeight(TM.MetricsFonts.MenuFont));

            LabelR1.Font = TM.MetricsFonts.MenuFont;
            LabelR2.Font = TM.MetricsFonts.MenuFont;
            LabelR3.Font = TM.MetricsFonts.MenuFont;

            LabelR9.Font = TM.MetricsFonts.MenuFont;
            LabelR5.Font = TM.MetricsFonts.MenuFont;
            LabelR6.Font = TM.MetricsFonts.MenuFont;

            menucontainer1.Height = Forms.Metrics_Fonts.GetTitleTextHeight(TM.MetricsFonts.MenuFont) + 3;
            highlight.Height = menucontainer1.Height + 1;
            menucontainer3.Height = menucontainer1.Height + 1;
            Menu_Window.Height = menucontainer1.Height + highlight.Height + menucontainer3.Height + Menu_Window.Padding.Top + Menu_Window.Padding.Bottom;

            LabelR4.Font = TM.MetricsFonts.MessageFont;

            LabelR1.Width = (int)Math.Round(LabelR1.Text.Measure(TM.MetricsFonts.MenuFont).Width + 5f);
            LabelR2.Width = (int)Math.Round(LabelR2.Text.Measure(TM.MetricsFonts.MenuFont).Width + 5f);
            PanelR1.Width = (int)Math.Round(LabelR3.Text.Measure(TM.MetricsFonts.MenuFont).Width + 5f + PanelR1.Padding.Left + PanelR1.Padding.Right);

            int TitleTextH, TitleTextH_9, TitleTextH_Sum;
            TitleTextH = (int)Math.Round("ABCabc0123xYz.#".Measure(TM.MetricsFonts.CaptionFont).Height);
            TitleTextH_9 = (int)Math.Round("ABCabc0123xYz.#".Measure(new Font(TM.MetricsFonts.CaptionFont.Name, 9f, Font.Style)).Height);
            TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5);

            int iP = 3 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth;
            int iT = 4 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + TM.MetricsFonts.CaptionHeight + TitleTextH_Sum;
            var _Padding = new Padding(iP, iT, iP, iP);

            foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
            {
                if (!WindowR.UseItAsMenu)
                {
                    SetClassicWindowMetrics(TM, WindowR);
                    WindowR.Padding = _Padding;
                }
            }

            WindowR3.Height = 85 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + WindowR3.GetTitleTextHeight();
            WindowR2.Height = 120 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + WindowR2.GetTitleTextHeight() + TM.MetricsFonts.MenuHeight;

            Menu_Window.Top = WindowR2.Top + menucontainer0.Top + menucontainer0.Height;
            Menu_Window.Left = Math.Min(WindowR2.Left + menucontainer0.Left + PanelR1.Left + (+3), WindowR2.Right - TM.MetricsFonts.PaddedBorderWidth - TM.MetricsFonts.BorderWidth);

            WindowR3.Top = WindowR2.Top + TextBoxR1.Top + TextBoxR1.Font.Height + 10;
            WindowR3.Left = WindowR2.Left + TextBoxR1.Left + 15;

            LabelR13.Top = WindowR4.Top + WindowR4.Metrics_CaptionHeight + 2;
            LabelR13.Left = WindowR4.Right - WindowR4.Metrics_CaptionWidth - 2;

            RetroShadow1.Visible = TM.WindowsEffects.WindowShadow;

            ButtonR1.FocusRectWidth = (int)TM.WindowsEffects.FocusRectWidth;
            ButtonR1.FocusRectHeight = (int)TM.WindowsEffects.FocusRectHeight;
            ButtonR1.Refresh();
        }

        public void ApplyRetroPreview()
        {
            WindowR1.ColorGradient = Toggle2.Checked;
            WindowR1.Color1 = InactiveTitle_pick.BackColor;
            WindowR1.Color2 = GInactivetitle_pick.BackColor;
            WindowR1.ForeColor = InactivetitleText_pick.BackColor;
            WindowR1.ColorBorder = InactiveBorder_pick.BackColor;

            WindowR2.ColorGradient = Toggle2.Checked;
            WindowR3.ColorGradient = Toggle2.Checked;
            WindowR4.ColorGradient = Toggle2.Checked;

            WindowR2.Color1 = activetitle_pick.BackColor;
            WindowR3.Color1 = activetitle_pick.BackColor;
            WindowR4.Color1 = activetitle_pick.BackColor;

            WindowR2.Color2 = GActivetitle_pick.BackColor;
            WindowR3.Color2 = GActivetitle_pick.BackColor;
            WindowR4.Color2 = GActivetitle_pick.BackColor;

            WindowR2.ForeColor = TitleText_pick.BackColor;
            WindowR3.ForeColor = TitleText_pick.BackColor;
            WindowR4.ForeColor = TitleText_pick.BackColor;

            WindowR2.ColorBorder = ActiveBorder_pick.BackColor;
            WindowR3.ColorBorder = ActiveBorder_pick.BackColor;
            WindowR4.ColorBorder = ActiveBorder_pick.BackColor;

            foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
            {
                if (!WindowR.UseItAsMenu)
                    WindowR.BackColor = btnface_pick.BackColor;

                WindowR.ButtonDkShadow = btndkshadow_pick.BackColor;
                WindowR.ButtonHilight = btnhilight_pick.BackColor;
                WindowR.ButtonLight = btnlight_pick.BackColor;
                WindowR.ButtonShadow = btnshadow_pick.BackColor;
                WindowR.ButtonText = btntext_pick.BackColor;
            }

            foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
            {
                ButtonR.BackColor = btnface_pick.BackColor;
                ButtonR.WindowFrame = Frame_pick.BackColor;
                ButtonR.ButtonDkShadow = btndkshadow_pick.BackColor;
                ButtonR.ButtonHilight = btnhilight_pick.BackColor;
                ButtonR.ButtonLight = btnlight_pick.BackColor;
                ButtonR.ButtonShadow = btnshadow_pick.BackColor;
                ButtonR.ForeColor = btntext_pick.BackColor;
            }

            foreach (PanelRaisedR PanelRaisedR in pnl_preview.GetAllControls().OfType<PanelRaisedR>())
            {
                PanelRaisedR.ButtonHilight = btnhilight_pick.BackColor;
                PanelRaisedR.ButtonShadow = btnshadow_pick.BackColor;
            }

            Retro3DPreview1.BackColor = btnface_pick.BackColor;
            Retro3DPreview1.ButtonDkShadow = btndkshadow_pick.BackColor;
            Retro3DPreview1.WindowFrame = Frame_pick.BackColor;
            Retro3DPreview1.ButtonHilight = btnhilight_pick.BackColor;
            Retro3DPreview1.ButtonLight = btnlight_pick.BackColor;
            Retro3DPreview1.ButtonShadow = btnshadow_pick.BackColor;
            Retro3DPreview1.ForeColor = btntext_pick.BackColor;
            Retro3DPreview1.Refresh();

            TextBoxR1.BackColor = Window_pick.BackColor;
            TextBoxR1.ForeColor = WindowText_pick.BackColor;
            TextBoxR1.ButtonDkShadow = btndkshadow_pick.BackColor;
            TextBoxR1.ButtonHilight = btnhilight_pick.BackColor;
            TextBoxR1.ButtonLight = btnlight_pick.BackColor;
            TextBoxR1.ButtonShadow = btnshadow_pick.BackColor;
            TextBoxR1.Refresh();

            Menu_Window.ButtonFace = btnface_pick.BackColor;
            Menu_Window.ButtonDkShadow = btndkshadow_pick.BackColor;
            Menu_Window.ButtonHilight = btnhilight_pick.BackColor;
            Menu_Window.ButtonLight = btnlight_pick.BackColor;
            Menu_Window.ButtonShadow = btnshadow_pick.BackColor;
            Menu_Window.BackColor = menu_pick.BackColor;
            Menu_Window.Refresh();

            PanelR1.ButtonHilight = btnhilight_pick.BackColor;
            PanelR1.ButtonShadow = btnshadow_pick.BackColor;
            PanelR1.BackColor = menu_pick.BackColor;

            PanelR2.ButtonHilight = btnhilight_pick.BackColor;
            PanelR2.BackColor = btnface_pick.BackColor;

            programcontainer.BackColor = AppWorkspace_pick.BackColor;

            pnl_preview.BackColor = background_pick.BackColor;

            menucontainer0.BackColor = menubar_pick.BackColor;

            highlight.BackColor = hilight_pick.BackColor;

            menuhilight.BackColor = menuhilight_pick.BackColor;

            LabelR6.ForeColor = menutext_pick.BackColor;

            LabelR1.ForeColor = menutext_pick.BackColor;

            LabelR5.ForeColor = hilighttext_pick.BackColor;

            LabelR2.ForeColor = GrayText_pick.BackColor;

            LabelR9.ForeColor = GrayText_pick.BackColor;

            LabelR4.ForeColor = WindowText_pick.BackColor;

            LabelR13.BackColor = InfoWindow_pick.BackColor;
            LabelR13.ForeColor = InfoText_pick.BackColor;

            Refresh17BitPreference();

            RetroShadow1.Refresh();
        }

        private void Toggle1_CheckedChanged(object sender, EventArgs e)
        {
            Refresh17BitPreference();
        }

        public void Refresh17BitPreference()
        {

            if (Toggle1.Checked)
            {
                // Theming Enabled (Menus Has colors and borders)
                Menu_Window.Flat = true;
                PanelR1.Flat = true;
                PanelR1.BackColor = menuhilight_pick.BackColor;
                PanelR1.ButtonShadow = hilight_pick.BackColor;

                menuhilight.BackColor = menuhilight_pick.BackColor;  // Filling of selected item

                highlight.BackColor = hilight_pick.BackColor;        // Outer Border of selected item

                menucontainer0.BackColor = menubar_pick.BackColor;

                LabelR3.ForeColor = hilighttext_pick.BackColor;
            }
            else
            {
                // Theming Disabled (Menus are retro 3D)
                Menu_Window.Flat = false;
                PanelR1.Flat = false;
                PanelR1.BackColor = menu_pick.BackColor;
                PanelR1.ButtonShadow = btnshadow_pick.BackColor;

                menuhilight.BackColor = hilight_pick.BackColor;      // Both will have same color

                highlight.BackColor = hilight_pick.BackColor;        // Both will have same color

                menucontainer0.BackColor = menu_pick.BackColor;

                LabelR3.ForeColor = menutext_pick.BackColor;
            }

            Menu_Window.Refresh();
            PanelR1.Refresh();
            menuhilight.Refresh();
            highlight.Refresh();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var TMx = new Theme.Manager(Theme.Manager.Source.File, OpenFileDialog1.FileName);
                LoadTM(TMx);
                TMx.Dispose();
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            var TMx = new Theme.Manager(Theme.Manager.Source.Registry);
            LoadTM(TMx);
            TMx.Dispose();
        }

        private void Toggle2_CheckedChanged(object sender, EventArgs e)
        {
            WindowR1.ColorGradient = Toggle2.Checked;
            WindowR2.ColorGradient = Toggle2.Checked;
            WindowR3.ColorGradient = Toggle2.Checked;
            WindowR4.ColorGradient = Toggle2.Checked;

            WindowR1.Invalidate();
            WindowR2.Invalidate();
            WindowR3.Invalidate();
            WindowR4.Invalidate();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var TMx = new Theme.Manager(Theme.Manager.Source.Registry);
            ApplyToTM(TMx);
            ApplyToTM(Program.TM);

            SetClassicWindowColors(Program.TM, Forms.MainFrm.ClassicWindow1);
            SetClassicWindowColors(Program.TM, Forms.MainFrm.ClassicWindow2, false);
            SetClassicButtonColors(Program.TM, Forms.MainFrm.ButtonR2);
            SetClassicButtonColors(Program.TM, Forms.MainFrm.ButtonR3);
            SetClassicButtonColors(Program.TM, Forms.MainFrm.ButtonR4);
            SetClassicPanelRaisedRColors(Program.TM, Forms.MainFrm.ClassicTaskbar);

            try
            {
                TMx.Win32.Apply();
                TMx.Win32.Update_UPM_DEFAULT();
            }
            catch
            {
            }
            TMx.Dispose();
            Cursor = Cursors.Default;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                var s = new List<string>();
                s.Clear();
                s.Add("; " + string.Format(Program.Lang.OldMSTheme_Copyrights, DateTime.Now.Year));
                s.Add("; " + string.Format(Program.Lang.OldMSTheme_ProgrammedBy, Application.CompanyName));
                s.Add("; " + string.Format(Program.Lang.OldMSTheme_CreatedFromAppVer, Program.TM.Info.AppVersion));
                s.Add("; " + string.Format(Program.Lang.OldMSTheme_CreatedBy, Program.TM.Info.Author));
                s.Add("; " + string.Format(Program.Lang.OldMSTheme_ThemeName, Program.TM.Info.ThemeName));
                s.Add("; " + string.Format(Program.Lang.OldMSTheme_ThemeVersion, Program.TM.Info.ThemeVersion));
                s.Add("");

                s.Add(string.Format(@"[Control Panel\Colors]"));

                {
                    var temp = activetitle_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "ActiveTitle", temp.R, temp.G, temp.B));
                }
                {
                    var temp1 = background_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "Background", temp1.R, temp1.G, temp1.B));
                }
                {
                    var temp2 = hilight_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "Hilight", temp2.R, temp2.G, temp2.B));
                }
                {
                    var temp3 = hilighttext_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "HilightText", temp3.R, temp3.G, temp3.B));
                }
                {
                    var temp4 = TitleText_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "TitleText", temp4.R, temp4.G, temp4.B));
                }
                {
                    var temp5 = Window_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "Window", temp5.R, temp5.G, temp5.B));
                }
                {
                    var temp6 = WindowText_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "WindowText", temp6.R, temp6.G, temp6.B));
                }
                {
                    var temp7 = Scrollbar_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "Scrollbar", temp7.R, temp7.G, temp7.B));
                }
                {
                    var temp8 = InactiveTitle_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "InactiveTitle", temp8.R, temp8.G, temp8.B));
                }
                {
                    var temp9 = menu_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "Menu", temp9.R, temp9.G, temp9.B));
                }
                {
                    var temp10 = Frame_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "WindowFrame", temp10.R, temp10.G, temp10.B));
                }
                {
                    var temp11 = menutext_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "MenuText", temp11.R, temp11.G, temp11.B));
                }
                {
                    var temp12 = ActiveBorder_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "ActiveBorder", temp12.R, temp12.G, temp12.B));
                }
                {
                    var temp13 = InactiveBorder_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "InactiveBorder", temp13.R, temp13.G, temp13.B));
                }
                {
                    var temp14 = AppWorkspace_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "AppWorkspace", temp14.R, temp14.G, temp14.B));
                }
                {
                    var temp15 = btnface_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "ButtonFace", temp15.R, temp15.G, temp15.B));
                }
                {
                    var temp16 = btnshadow_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "ButtonShadow", temp16.R, temp16.G, temp16.B));
                }
                {
                    var temp17 = GrayText_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "GrayText", temp17.R, temp17.G, temp17.B));
                }
                {
                    var temp18 = btntext_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "ButtonText", temp18.R, temp18.G, temp18.B));
                }
                {
                    var temp19 = InactivetitleText_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "InactiveTitleText", temp19.R, temp19.G, temp19.B));
                }
                {
                    var temp20 = btnhilight_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "ButtonHilight", temp20.R, temp20.G, temp20.B));
                }
                {
                    var temp21 = btndkshadow_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "ButtonDkShadow", temp21.R, temp21.G, temp21.B));
                }
                {
                    var temp22 = btnlight_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "ButtonLight", temp22.R, temp22.G, temp22.B));
                }
                {
                    var temp23 = InfoText_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "InfoText", temp23.R, temp23.G, temp23.B));
                }
                {
                    var temp24 = InfoWindow_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "InfoWindow", temp24.R, temp24.G, temp24.B));
                }
                {
                    var temp25 = GActivetitle_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "GradientActiveTitle", temp25.R, temp25.G, temp25.B));
                }
                {
                    var temp26 = GInactivetitle_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "GradientInactiveTitle", temp26.R, temp26.G, temp26.B));
                }
                {
                    var temp27 = btnaltface_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "ButtonAlternateFace", temp27.R, temp27.G, temp27.B));
                }
                {
                    var temp28 = hottracking_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "HotTrackingColor", temp28.R, temp28.G, temp28.B));
                }
                {
                    var temp29 = menuhilight_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "MenuHilight", temp29.R, temp29.G, temp29.B));
                }
                {
                    var temp30 = menubar_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "MenuBar", temp30.R, temp30.G, temp30.B));
                }
                {
                    var temp31 = desktop_pick.BackColor;
                    s.Add(string.Format("{0}={1} {2} {3}", "Desktop", temp31.R, temp31.G, temp31.B));
                }
                s.Add("");

                s.Add(string.Format("[MasterThemeSelector]"));
                s.Add(string.Format("MTSM=DABJDKT"));

                s.Add("");
                s.Add(@"[Control Panel\Desktop]");
                s.Add("Wallpaper=");
                s.Add("TileWallpaper=0");
                s.Add("WallpaperStyle=10");
                s.Add("Pattern=");
                s.Add("");

                s.Add("[VisualStyles]");
                s.Add("Path=");
                s.Add("ColorStyle=@themeui.dll,-854");
                s.Add("Size=@themeui.dll,-2019");
                s.Add("Transparency=0");
                s.Add("");

                try
                {
                    System.IO.File.WriteAllText(SaveFileDialog2.FileName, s.CString());
                }
                catch (Exception ex)
                {
                    Forms.BugReport.ThrowError(ex);
                }

            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Forms.VS2Win32UI.ShowDialog();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ComboBox1.SelectedItem.ToString()))
                return;

            bool condition0 = ComboBox1.SelectedIndex <= 3;
            bool condition1 = ComboBox1.SelectedItem.ToString().StartsWith("Windows Classic (3.1)");
            bool condition2 = ComboBox1.SelectedItem.ToString().StartsWith("Windows 3.1 - ");

            Toggle1.Checked = condition0 | condition1 | condition2;

            LoadFromWinThemeString(Properties.Resources.RetroThemesDB, ComboBox1.SelectedItem.ToString());
        }

        private void Menu_Window_SizeChanged(object sender, EventArgs e)
        {
            RetroShadow1.Size = Menu_Window.Size;
            RetroShadow1.Location = Menu_Window.Location + (Size)new Point(6, 5);

            var b = new Bitmap(RetroShadow1.Width, RetroShadow1.Height);
            var g = Graphics.FromImage(b);
            g.DrawGlow(new Rectangle(5, 5, b.Width - 10 - 1, b.Height - 10 - 1), Color.FromArgb(128, 0, 0, 0));
            g.Save();
            RetroShadow1.Image = b;
            g.Dispose();

            RetroShadow1.BringToFront();
            Menu_Window.BringToFront();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            btndkshadow_pick.BackColor = btnface_pick.BackColor;
            btnshadow_pick.BackColor = btnface_pick.BackColor;
            btnhilight_pick.BackColor = btnface_pick.BackColor;
            btnlight_pick.BackColor = btnface_pick.BackColor;
            ApplyRetroPreview();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            btndkshadow_pick.BackColor = btnshadow_pick.BackColor;
            btnhilight_pick.BackColor = btnshadow_pick.BackColor;
            btnlight_pick.BackColor = btnface_pick.BackColor;
            btnshadow_pick.BackColor = btnface_pick.BackColor;
            ApplyRetroPreview();
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Edit-Windows-classic-colors");
        }

    }
}