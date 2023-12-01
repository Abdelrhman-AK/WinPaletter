using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Theme;
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
            ApplyStyle(this);
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
            using (Theme.Manager DefTM = Theme.Default.Get())
            {
                ActiveBorder_pick.DefaultBackColor = DefTM.Win32.ActiveBorder;
                activetitle_pick.DefaultBackColor = DefTM.Win32.ActiveTitle;
                AppWorkspace_pick.DefaultBackColor = DefTM.Win32.AppWorkspace;
                background_pick.DefaultBackColor = DefTM.Win32.Background;
                btnaltface_pick.DefaultBackColor = DefTM.Win32.ButtonAlternateFace;
                btndkshadow_pick.DefaultBackColor = DefTM.Win32.ButtonDkShadow;
                btnface_pick.DefaultBackColor = DefTM.Win32.ButtonFace;
                btnhilight_pick.DefaultBackColor = DefTM.Win32.ButtonHilight;
                btnlight_pick.DefaultBackColor = DefTM.Win32.ButtonLight;
                btnshadow_pick.DefaultBackColor = DefTM.Win32.ButtonShadow;
                btntext_pick.DefaultBackColor = DefTM.Win32.ButtonText;
                GActivetitle_pick.DefaultBackColor = DefTM.Win32.GradientActiveTitle;
                GInactivetitle_pick.DefaultBackColor = DefTM.Win32.GradientInactiveTitle;
                GrayText_pick.DefaultBackColor = DefTM.Win32.GrayText;
                hilighttext_pick.DefaultBackColor = DefTM.Win32.HilightText;
                hottracking_pick.DefaultBackColor = DefTM.Win32.HotTrackingColor;
                InactiveBorder_pick.DefaultBackColor = DefTM.Win32.InactiveBorder;
                InactiveTitle_pick.DefaultBackColor = DefTM.Win32.InactiveTitle;
                InactivetitleText_pick.DefaultBackColor = DefTM.Win32.InactiveTitleText;
                InfoText_pick.DefaultBackColor = DefTM.Win32.InfoText;
                InfoWindow_pick.DefaultBackColor = DefTM.Win32.InfoWindow;
                menu_pick.DefaultBackColor = DefTM.Win32.Menu;
                menubar_pick.DefaultBackColor = DefTM.Win32.MenuBar;
                menutext_pick.DefaultBackColor = DefTM.Win32.MenuText;
                Scrollbar_pick.DefaultBackColor = DefTM.Win32.Scrollbar;
                TitleText_pick.DefaultBackColor = DefTM.Win32.TitleText;
                Window_pick.DefaultBackColor = DefTM.Win32.Window;
                Frame_pick.DefaultBackColor = DefTM.Win32.WindowFrame;
                WindowText_pick.DefaultBackColor = DefTM.Win32.WindowText;
                hilight_pick.DefaultBackColor = DefTM.Win32.Hilight;
                menuhilight_pick.DefaultBackColor = DefTM.Win32.MenuHilight;
                desktop_pick.DefaultBackColor = DefTM.Win32.Desktop;
            }
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
            Forms.MainFrm.Window1.Refresh();
            Forms.MainFrm.Window2.Refresh();
            Close();
        }

        private void ColorItem_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs) return;

            try
            {
                if (((MouseEventArgs)e).Button == MouseButtons.Right)
                {
                    Forms.SubMenu.ShowMenu((ColorItem)sender);
                    if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                    {
                        ApplyRetroPreview();
                    }
                    return;
                }
            }
            catch { }

            List<Control> CList = new();
            CList.Add((Control)sender);

            Color C = ((UI.Controllers.ColorItem)sender).BackColor;
            string CtrlName = ((UI.Controllers.ColorItem)sender).Name.ToString().ToLower();

            if (CtrlName == "activetitle_pick".ToLower())
            {
                CList.Add(WindowR2);
                CList.Add(WindowR3);
                CList.Add(WindowR4);

                Conditions _conditions = new() { WindowRColor1 = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                WindowR2.Color1 = C;
                WindowR3.Color1 = C;
                WindowR4.Color1 = C;
            }

            else if (CtrlName == "GActivetitle_pick".ToLower())
            {
                CList.Add(WindowR2);
                CList.Add(WindowR3);
                CList.Add(WindowR4);

                Conditions _conditions = new() { WindowRColor2 = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                WindowR2.Color2 = C;
                WindowR3.Color2 = C;
                WindowR4.Color2 = C;
            }

            else if (CtrlName == "TitleText_pick".ToLower())
            {
                CList.Add(WindowR2);
                CList.Add(WindowR3);
                CList.Add(WindowR4);

                Conditions _conditions = new() { WindowRForeColor = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                WindowR2.ForeColor = C;
                WindowR3.ForeColor = C;
                WindowR4.ForeColor = C;
            }

            else if (CtrlName == "InactiveTitle_pick".ToLower())
            {
                CList.Add(WindowR1);
                Conditions _conditions = new() { WindowRColor1 = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                WindowR1.Color1 = C;
            }

            else if (CtrlName == "GInactivetitle_pick".ToLower())
            {
                CList.Add(WindowR1);
                Conditions _conditions = new() { WindowRColor2 = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                WindowR1.Color2 = C;
            }

            else if (CtrlName == "InactivetitleText_pick".ToLower())
            {
                CList.Add(WindowR1);
                Conditions _conditions = new() { WindowRForeColor = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                WindowR1.ForeColor = C;
            }

            else if (CtrlName == "ActiveBorder_pick".ToLower())
            {
                CList.Add(WindowR2);
                CList.Add(WindowR3);
                CList.Add(WindowR4);

                Conditions _conditions = new() { WindowRBorder = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                WindowR2.ColorBorder = C;
                WindowR3.ColorBorder = C;
                WindowR4.ColorBorder = C;
            }

            else if (CtrlName == "InactiveBorder_pick".ToLower())
            {
                CList.Add(WindowR1);
                Conditions _conditions = new() { WindowRBorder = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                WindowR1.ColorBorder = C;
            }

            else if (CtrlName == "Frame_pick".ToLower())
            {
                foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                    CList.Add(ButtonR);

                CList.Add(Retro3DPreview1);

                Conditions _conditions = new() { WindowRFrame = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);

                foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                    ButtonR.WindowFrame = C;

                Retro3DPreview1.WindowFrame = C;
            }

            else if (CtrlName == "btnface_pick".ToLower())
            {
                foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                    CList.Add(WindowR);
                foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                    CList.Add(ButtonR);

                CList.Add(Retro3DPreview1);
                CList.Add(PanelR2);
                Conditions _conditions = new() { ButtonRFace = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);

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
            }

            else if (CtrlName == "btndkshadow_pick".ToLower())
            {
                foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                    CList.Add(WindowR);
                foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                    CList.Add(ButtonR);

                CList.Add(Retro3DPreview1);
                CList.Add(TextBoxR1);

                Conditions _conditions = new() { ButtonRDkShadow = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);

                foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                    WindowR.ButtonDkShadow = C;
                foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                    ButtonR.ButtonDkShadow = C;

                Retro3DPreview1.ButtonDkShadow = C;
                TextBoxR1.ButtonDkShadow = C;
            }

            else if (CtrlName == "btnhilight_pick".ToLower())
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

                Conditions _conditions = new() { ButtonRHilight = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);

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
            }

            else if (CtrlName == "btnlight_pick".ToLower())
            {
                foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                    CList.Add(WindowR);
                foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                    CList.Add(ButtonR);

                CList.Add(Retro3DPreview1);
                CList.Add(TextBoxR1);

                Conditions _conditions = new() { ButtonRLight = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);

                foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                    WindowR.ButtonLight = C;
                foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                    ButtonR.ButtonLight = C;

                TextBoxR1.ButtonLight = C;
                Retro3DPreview1.ButtonLight = C;
            }

            else if (CtrlName == "btnshadow_pick".ToLower())
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

                Conditions _conditions = new() { ButtonRShadow = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);

                foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                    WindowR.ButtonShadow = C;
                foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                    ButtonR.ButtonShadow = C;
                foreach (PanelRaisedR ButtonR in pnl_preview.GetAllControls().OfType<PanelRaisedR>())
                    ButtonR.ButtonShadow = C;

                Retro3DPreview1.ButtonShadow = C;
                TextBoxR1.ButtonShadow = C;
                PanelR1.ButtonShadow = C;
            }

            else if (CtrlName == "btntext_pick".ToLower())
            {
                foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                    CList.Add(WindowR);
                foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                    CList.Add(ButtonR);

                CList.Add(Retro3DPreview1);
                Conditions _conditions = new() { ButtonRText = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);

                foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
                    WindowR.ButtonText = C;
                foreach (ButtonR ButtonR in pnl_preview.GetAllControls().OfType<ButtonR>())
                    ButtonR.ForeColor = C;

                Retro3DPreview1.ForeColor = C;
            }

            else if (CtrlName == "AppWorkspace_pick".ToLower())
            {
                CList.Add(programcontainer);
                Conditions _conditions = new() { RetroAppWorkspace = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                programcontainer.BackColor = C;
            }

            else if (CtrlName == "background_pick".ToLower())
            {
                CList.Add(pnl_preview);
                Conditions _conditions = new() { RetroBackground = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                pnl_preview.BackColor = C;
            }

            else if (CtrlName == "menu_pick".ToLower())
            {
                CList.Add(Menu_Window);

                if (!Toggle1.Checked)
                    CList.Add(PanelR1);
                if (!Toggle1.Checked)
                    CList.Add(menucontainer0);

                Conditions _conditions = new() { RetroBackground = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                Menu_Window.BackColor = C;
                Menu_Window.Invalidate();

                if (!Toggle1.Checked)
                    PanelR1.BackColor = C;
                if (!Toggle1.Checked)
                    menucontainer0.BackColor = C;
            }

            else if (CtrlName == "menubar_pick".ToLower())
            {
                if (Toggle1.Checked)
                {
                    CList.Add(menucontainer0);
                    Conditions _conditions = new() { RetroBackground = true };
                    C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                    menucontainer0.BackColor = C;
                }
                else
                {
                    C = Forms.ColorPickerDlg.Pick(CList);
                    ((UI.Controllers.ColorItem)sender).BackColor = C;
                }
            }

            else if (CtrlName == "hilight_pick".ToLower())
            {
                if (Toggle1.Checked)
                {
                    CList.Add(highlight);
                    CList.Add(PanelR1);
                    Conditions _conditions = new() { ButtonRShadow = true, RetroBackground = true, RetroHighlight17BitFixer = true };
                    C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                    highlight.BackColor = C;
                    PanelR1.ButtonShadow = C;
                }
                else
                {
                    CList.Add(highlight);
                    CList.Add(menuhilight);
                    Conditions _conditions = new() { RetroBackground = true };
                    C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                    highlight.BackColor = C;
                    menuhilight.BackColor = C;
                }

            }

            else if (CtrlName == "menuhilight_pick".ToLower())
            {
                if (Toggle1.Checked)
                {
                    CList.Add(menuhilight);
                    CList.Add(PanelR1);
                    Conditions _conditions = new() { RetroBackground = true };
                    C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                    menuhilight.BackColor = C;
                    PanelR1.BackColor = C;
                }
                else
                {
                    C = Forms.ColorPickerDlg.Pick(CList);
                    ((UI.Controllers.ColorItem)sender).BackColor = C;
                }

            }

            else if (CtrlName == "menutext_pick".ToLower())
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
            }

            else if (CtrlName == "hilighttext_pick".ToLower())
            {
                CList.Add(LabelR5);
                if (Toggle1.Checked)
                    CList.Add(LabelR3);

                C = Forms.ColorPickerDlg.Pick(CList);
                LabelR5.ForeColor = C;
                if (Toggle1.Checked)
                    LabelR3.ForeColor = C;
            }

            else if (CtrlName == "GrayText_pick".ToLower())
            {
                CList.Add(LabelR2);
                CList.Add(LabelR9);

                C = Forms.ColorPickerDlg.Pick(CList);
                LabelR2.ForeColor = C;
                LabelR9.ForeColor = C;
            }

            else if (CtrlName == "Window_pick".ToLower())
            {
                CList.Add(TextBoxR1);
                Conditions _conditions = new() { RetroBackground = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                TextBoxR1.BackColor = C;
            }

            else if (CtrlName == "WindowText_pick".ToLower())
            {
                CList.Add(TextBoxR1);
                CList.Add(LabelR4);

                Conditions _conditions = new() { WindowRText = true, WindowRForeColor = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                TextBoxR1.ForeColor = C;
                LabelR4.ForeColor = C;
            }

            else if (CtrlName == "InfoWindow_pick".ToLower())
            {
                CList.Add(LabelR13);
                Conditions _conditions = new() { RetroBackground = true };
                C = Forms.ColorPickerDlg.Pick(CList, _conditions);
                LabelR13.BackColor = C;
            }

            else if (CtrlName == "InfoText_pick".ToLower())
            {
                CList.Add(LabelR13);
                C = Forms.ColorPickerDlg.Pick(CList);
                LabelR13.ForeColor = C;
            }

            else if (CtrlName == "Scrollbar_pick".ToLower())
            {
                C = Forms.ColorPickerDlg.Pick(CList);
            }

            else
            {
                C = Forms.ColorPickerDlg.Pick(CList);
            }

            ((ColorItem)sender).BackColor = C;

            foreach (Control Ctrl in CList) { Ctrl.Refresh(); }
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
                using (Manager _Def = Theme.Default.Get(Program.PreviewStyle))
                {
                    LoadFromWin9xTheme(OpenThemeDialog.FileName, _Def.Win32);
                }

            }
        }

        public void LoadFromWin9xTheme(string File, Theme.Structures.Win32UI _DefWin32)
        {
            if (System.IO.File.Exists(File))
            {

                using (INI _ini = new(File))
                {
                    string Section = @"Control Panel\Colors";

                    TitleText_pick.BackColor = _ini.Read(Section, "TitleText", _DefWin32.TitleText.ToWin32Reg()).FromWin32RegToColor();
                    InactivetitleText_pick.BackColor = _ini.Read(Section, "InactiveTitleText", _DefWin32.InactiveTitleText.ToWin32Reg()).FromWin32RegToColor();
                    ActiveBorder_pick.BackColor = _ini.Read(Section, "ActiveBorder", _DefWin32.ActiveBorder.ToWin32Reg()).FromWin32RegToColor();
                    InactiveBorder_pick.BackColor = _ini.Read(Section, "InactiveBorder", _DefWin32.InactiveBorder.ToWin32Reg()).FromWin32RegToColor();
                    activetitle_pick.BackColor = _ini.Read(Section, "ActiveTitle", _DefWin32.ActiveTitle.ToWin32Reg()).FromWin32RegToColor();
                    InactiveTitle_pick.BackColor = _ini.Read(Section, "InactiveTitle", _DefWin32.InactiveTitle.ToWin32Reg()).FromWin32RegToColor();

                    Color GA = _ini.Read(Section, "GradientActiveTitle").FromWin32RegToColor();
                    Color GI = _ini.Read(Section, "GradientInactiveTitle").FromWin32RegToColor();

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

            List<string> ls = new();
            ls.Clear();

            List<string> AllThemes = String.CList();
            string SelectedTheme = string.Empty;
            List<string> SelectedThemeList = new();

            bool Found = false;

            foreach (string th in AllThemes)
            {
                if ((th.Split('|')[0].ToLower() ?? string.Empty) == (ThemeName.ToLower() ?? string.Empty))
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

                if (x.StartsWith("activetitle=", StringComparison.OrdinalIgnoreCase))
                {
                    activetitle_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                    if (!FoundGradientActive)
                        GActivetitle_pick.BackColor = activetitle_pick.BackColor;
                }

                if (x.StartsWith("gradientactivetitle=", StringComparison.OrdinalIgnoreCase))
                {
                    GActivetitle_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                    FoundGradientActive = true;
                }

                if (x.StartsWith("inactivetitle=", StringComparison.OrdinalIgnoreCase))
                {
                    InactiveTitle_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                    if (!FoundGradientInactive)
                        GInactivetitle_pick.BackColor = InactiveTitle_pick.BackColor;

                }

                if (x.StartsWith("gradientinactivetitle=", StringComparison.OrdinalIgnoreCase))
                {
                    GInactivetitle_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                    FoundGradientInactive = true;
                }

                if (x.StartsWith("background=", StringComparison.OrdinalIgnoreCase))
                    background_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("hilight=", StringComparison.OrdinalIgnoreCase))
                    hilight_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("hilighttext=", StringComparison.OrdinalIgnoreCase))
                    hilighttext_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("titletext=", StringComparison.OrdinalIgnoreCase))
                    TitleText_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("window=", StringComparison.OrdinalIgnoreCase))
                    Window_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("windowtext=", StringComparison.OrdinalIgnoreCase))
                    WindowText_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("scrollbar=", StringComparison.OrdinalIgnoreCase))
                    Scrollbar_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("menu=", StringComparison.OrdinalIgnoreCase))
                    menu_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("windowframe=", StringComparison.OrdinalIgnoreCase))
                    Frame_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("menutext=", StringComparison.OrdinalIgnoreCase))
                    menutext_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("activeborder=", StringComparison.OrdinalIgnoreCase))
                    ActiveBorder_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("inactiveborder=", StringComparison.OrdinalIgnoreCase))
                    InactiveBorder_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("appworkspace=", StringComparison.OrdinalIgnoreCase))
                    AppWorkspace_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("buttonface=", StringComparison.OrdinalIgnoreCase))
                    btnface_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("buttonshadow=", StringComparison.OrdinalIgnoreCase))
                    btnshadow_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("graytext=", StringComparison.OrdinalIgnoreCase))
                    GrayText_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("buttontext=", StringComparison.OrdinalIgnoreCase))
                    btntext_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("inactivetitletext=", StringComparison.OrdinalIgnoreCase))
                    InactivetitleText_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("buttonhilight=", StringComparison.OrdinalIgnoreCase))
                    btnhilight_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("buttondkshadow=", StringComparison.OrdinalIgnoreCase))
                    btndkshadow_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("buttonlight=", StringComparison.OrdinalIgnoreCase))
                    btnlight_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("infotext=", StringComparison.OrdinalIgnoreCase))
                    InfoText_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("infowindow=", StringComparison.OrdinalIgnoreCase))
                    InfoWindow_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("hottrackingcolor=", StringComparison.OrdinalIgnoreCase))
                    hottracking_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("buttonalternateface=", StringComparison.OrdinalIgnoreCase))
                    btnaltface_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("menubar=", StringComparison.OrdinalIgnoreCase))
                    menubar_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("menuhilight=", StringComparison.OrdinalIgnoreCase))
                    menuhilight_pick.BackColor = x.Split('=')[1].FromWin32RegToColor();
                if (x.StartsWith("desktop=", StringComparison.OrdinalIgnoreCase))
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

            int iP = 3 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth;
            int iT = 4 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + TM.MetricsFonts.CaptionHeight + GetTitlebarTextHeight(TM.MetricsFonts.CaptionFont);
            Padding _Padding = new(iP, iT, iP, iP);

            foreach (WindowR WindowR in pnl_preview.GetAllControls().OfType<WindowR>())
            {
                if (!WindowR.UseItAsMenu)
                {
                    SetClassicWindowMetrics(TM, WindowR);
                    WindowR.Padding = _Padding;
                }
            }

            WindowR3.Height = 85 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + GetTitlebarTextHeight(WindowR3.Font);
            WindowR2.Height = 120 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + GetTitlebarTextHeight(WindowR2.Font) + TM.MetricsFonts.MenuHeight;

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
                Theme.Manager TMx = new(Theme.Manager.Source.File, OpenFileDialog1.FileName);
                LoadTM(TMx);
                TMx.Dispose();
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Theme.Manager TMx = new(Theme.Manager.Source.Registry);
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
            Theme.Manager TMx = new(Theme.Manager.Source.Registry);
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
                TMx.Win32.Broadcast_UPM_ToDefUsers();
            }
            catch { }
            TMx.Dispose();
            Cursor = Cursors.Default;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                List<string> s = new();
                s.Clear();
                s.Add($"; {(string.Format(Program.Lang.OldMSTheme_Copyrights, DateTime.Now.Year))}");
                s.Add($"; {(string.Format(Program.Lang.OldMSTheme_ProgrammedBy, Application.CompanyName))}");
                s.Add($"; {(string.Format(Program.Lang.OldMSTheme_CreatedFromAppVer, Program.TM.Info.AppVersion))}");
                s.Add($"; {(string.Format(Program.Lang.OldMSTheme_CreatedBy, Program.TM.Info.Author))}");
                s.Add($"; {(string.Format(Program.Lang.OldMSTheme_ThemeName, Program.TM.Info.ThemeName))}");
                s.Add($"; {(string.Format(Program.Lang.OldMSTheme_ThemeVersion, Program.TM.Info.ThemeVersion))}");
                s.Add(string.Empty);

                s.Add(string.Format(@"[Control Panel\Colors]"));

                {
                    Color temp = activetitle_pick.BackColor;
                    s.Add($"{"ActiveTitle"}={temp.R} {temp.G} {temp.B}");
                }
                {
                    Color temp1 = background_pick.BackColor;
                    s.Add($"{"Background"}={temp1.R} {temp1.G} {temp1.B}");
                }
                {
                    Color temp2 = hilight_pick.BackColor;
                    s.Add($"{"Hilight"}={temp2.R} {temp2.G} {temp2.B}");
                }
                {
                    Color temp3 = hilighttext_pick.BackColor;
                    s.Add($"{"HilightText"}={temp3.R} {temp3.G} {temp3.B}");
                }
                {
                    Color temp4 = TitleText_pick.BackColor;
                    s.Add($"{"TitleText"}={temp4.R} {temp4.G} {temp4.B}");
                }
                {
                    Color temp5 = Window_pick.BackColor;
                    s.Add($"{"Window"}={temp5.R} {temp5.G} {temp5.B}");
                }
                {
                    Color temp6 = WindowText_pick.BackColor;
                    s.Add($"{"WindowText"}={temp6.R} {temp6.G} {temp6.B}");
                }
                {
                    Color temp7 = Scrollbar_pick.BackColor;
                    s.Add($"{"Scrollbar"}={temp7.R} {temp7.G} {temp7.B}");
                }
                {
                    Color temp8 = InactiveTitle_pick.BackColor;
                    s.Add($"{"InactiveTitle"}={temp8.R} {temp8.G} {temp8.B}");
                }
                {
                    Color temp9 = menu_pick.BackColor;
                    s.Add($"{"Menu"}={temp9.R} {temp9.G} {temp9.B}");
                }
                {
                    Color temp10 = Frame_pick.BackColor;
                    s.Add($"{"WindowFrame"}={temp10.R} {temp10.G} {temp10.B}");
                }
                {
                    Color temp11 = menutext_pick.BackColor;
                    s.Add($"{"MenuText"}={temp11.R} {temp11.G} {temp11.B}");
                }
                {
                    Color temp12 = ActiveBorder_pick.BackColor;
                    s.Add($"{"ActiveBorder"}={temp12.R} {temp12.G} {temp12.B}");
                }
                {
                    Color temp13 = InactiveBorder_pick.BackColor;
                    s.Add($"{"InactiveBorder"}={temp13.R} {temp13.G} {temp13.B}");
                }
                {
                    Color temp14 = AppWorkspace_pick.BackColor;
                    s.Add($"{"AppWorkspace"}={temp14.R} {temp14.G} {temp14.B}");
                }
                {
                    Color temp15 = btnface_pick.BackColor;
                    s.Add($"{"ButtonFace"}={temp15.R} {temp15.G} {temp15.B}");
                }
                {
                    Color temp16 = btnshadow_pick.BackColor;
                    s.Add($"{"ButtonShadow"}={temp16.R} {temp16.G} {temp16.B}");
                }
                {
                    Color temp17 = GrayText_pick.BackColor;
                    s.Add($"{"GrayText"}={temp17.R} {temp17.G} {temp17.B}");
                }
                {
                    Color temp18 = btntext_pick.BackColor;
                    s.Add($"{"ButtonText"}={temp18.R} {temp18.G} {temp18.B}");
                }
                {
                    Color temp19 = InactivetitleText_pick.BackColor;
                    s.Add($"{"InactiveTitleText"}={temp19.R} {temp19.G} {temp19.B}");
                }
                {
                    Color temp20 = btnhilight_pick.BackColor;
                    s.Add($"{"ButtonHilight"}={temp20.R} {temp20.G} {temp20.B}");
                }
                {
                    Color temp21 = btndkshadow_pick.BackColor;
                    s.Add($"{"ButtonDkShadow"}={temp21.R} {temp21.G} {temp21.B}");
                }
                {
                    Color temp22 = btnlight_pick.BackColor;
                    s.Add($"{"ButtonLight"}={temp22.R} {temp22.G} {temp22.B}");
                }
                {
                    Color temp23 = InfoText_pick.BackColor;
                    s.Add($"{"InfoText"}={temp23.R} {temp23.G} {temp23.B}");
                }
                {
                    Color temp24 = InfoWindow_pick.BackColor;
                    s.Add($"{"InfoWindow"}={temp24.R} {temp24.G} {temp24.B}");
                }
                {
                    Color temp25 = GActivetitle_pick.BackColor;
                    s.Add($"{"GradientActiveTitle"}={temp25.R} {temp25.G} {temp25.B}");
                }
                {
                    Color temp26 = GInactivetitle_pick.BackColor;
                    s.Add($"{"GradientInactiveTitle"}={temp26.R} {temp26.G} {temp26.B}");
                }
                {
                    Color temp27 = btnaltface_pick.BackColor;
                    s.Add($"{"ButtonAlternateFace"}={temp27.R} {temp27.G} {temp27.B}");
                }
                {
                    Color temp28 = hottracking_pick.BackColor;
                    s.Add($"{"HotTrackingColor"}={temp28.R} {temp28.G} {temp28.B}");
                }
                {
                    Color temp29 = menuhilight_pick.BackColor;
                    s.Add($"{"MenuHilight"}={temp29.R} {temp29.G} {temp29.B}");
                }
                {
                    Color temp30 = menubar_pick.BackColor;
                    s.Add($"{"MenuBar"}={temp30.R} {temp30.G} {temp30.B}");
                }
                {
                    Color temp31 = desktop_pick.BackColor;
                    s.Add($"{"Desktop"}={temp31.R} {temp31.G} {temp31.B}");
                }
                s.Add(string.Empty);

                s.Add(string.Format("[MasterThemeSelector]"));
                s.Add(string.Format("MTSM=DABJDKT"));

                s.Add(string.Empty);
                s.Add(@"[Control Panel\Desktop]");
                s.Add("Wallpaper=");
                s.Add("TileWallpaper=0");
                s.Add("WallpaperStyle=10");
                s.Add("Pattern=");
                s.Add(string.Empty);

                s.Add("[VisualStyles]");
                s.Add("Path=");
                s.Add("ColorStyle=@themeui.dll,-854");
                s.Add("Size=@themeui.dll,-2019");
                s.Add("Transparency=0");
                s.Add(string.Empty);

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

            using (Bitmap b = new(RetroShadow1.Width, RetroShadow1.Height))
            using (Graphics G = Graphics.FromImage(b))
            {
                G.Clear(Color.Transparent);
                G.DrawGlow(new(5, 5, b.Width - 10 - 1, b.Height - 10 - 1), Color.FromArgb(128, 0, 0, 0));
                G.Save();
                G.Dispose();
                RetroShadow1.Image = new Bitmap(b);
            }

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
            Process.Start($"{Properties.Resources.Link_Wiki}/Edit-Windows-classic-colors");
        }
    }
}