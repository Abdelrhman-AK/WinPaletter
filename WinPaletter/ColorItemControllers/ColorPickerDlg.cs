using Cyotek.Windows.Forms;
using Devcorp.Controls.VisualStyles;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{

    public partial class ColorPickerDlg
    {
        private Color InitColor;
        private Image img;
        private readonly List<Control> ChildControls_List = new List<Control>();
        private List<Control> ColorControls_List = new List<Control>();
        private readonly List<Form> Forms_List = new List<Form>();
        private List<Color> Colors_List = new List<Color>();
        private Conditions _Conditions = new Conditions();
        private readonly int _Speed = 40;

        #region Form Shadow

        private bool aeroEnabled;

        protected override CreateParams CreateParams
        {
            get
            {
                CheckAeroEnabled();
                var cp = base.CreateParams;
                if (!aeroEnabled)
                {
                    cp.ClassStyle = cp.ClassStyle | Dwmapi.CS_DROPSHADOW;
                    cp.ExStyle = cp.ExStyle | 33554432;
                    return cp;
                }
                else
                {
                    return cp;
                }
            }
        }

        public ColorPickerDlg()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Dwmapi.WM_NCPAINT:
                    {
                        int val = 2;
                        if (aeroEnabled)
                        {
                            Dwmapi.DwmSetWindowAttribute(Handle, WPStyle.GetRoundedCorners() ? 2 : 1, ref val, 4);
                            Dwmapi.MARGINS bla = new();
                            {
                                bla.bottomHeight = 1;
                                bla.leftWidth = 1;
                                bla.rightWidth = 1;
                                bla.topHeight = 1;
                            }
                            Dwmapi.DwmExtendFrameIntoClientArea(Handle, ref bla);
                        }

                        break;
                    }
            }

            base.WndProc(ref m);
        }

        private void CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                var Com = default(bool);
                Dwmapi.DwmIsCompositionEnabled(ref Com);
                aeroEnabled = Com;
            }
            else
            {
                aeroEnabled = false;
            }
        }
        #endregion

        private Point newPoint = new Point();
        private Point xPoint = new Point();

        private void ColorPicker_MouseDown(object sender, MouseEventArgs e)
        {
            xPoint = MousePosition - (Size)Location;
        }

        private void ColorPicker_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newPoint = MousePosition - (Size)xPoint;
                Location = newPoint;
            }
        }

        private void ColorPicker_FormClosing(object sender, FormClosingEventArgs e)
        {
            User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_HIDE | User32.AnimateWindowFlags.AW_BLEND);
        }

        private void ColorPicker_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            ComboBox1.PopulateThemes();

            User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_ACTIVATE | User32.AnimateWindowFlags.AW_BLEND);
            Invalidate();
        }

        public void GetColorsHistory(UI.Controllers.ColorItem ColorItem)
        {
            FlowLayoutPanel1.SuspendLayout();

            foreach (UI.Controllers.ColorItem c in FlowLayoutPanel1.Controls.OfType<UI.Controllers.ColorItem>())
            {
                c.Click -= MiniColorItem_click;
                c.Dispose();
                FlowLayoutPanel1.Controls.Remove(c);
            }

            FlowLayoutPanel1.Controls.Clear();

            foreach (Color c in ColorItem.ColorsHistory)
            {

                UI.Controllers.ColorItem MiniColorItem = new UI.Controllers.ColorItem();
                MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                MiniColorItem.AllowDrop = false;
                MiniColorItem.PauseColorsHistory = true;
                MiniColorItem.BackColor = c;
                MiniColorItem.DefaultColor = MiniColorItem.BackColor;

                FlowLayoutPanel1.Controls.Add(MiniColorItem);
                MiniColorItem.Click += MiniColorItem_click;
            }

            FlowLayoutPanel1.ResumeLayout();
        }


        public void GetColorsFromPalette(Theme.Manager TM)
        {
            PaletteContainer.SuspendLayout();

            foreach (UI.Controllers.ColorItem c in PaletteContainer.Controls.OfType<UI.Controllers.ColorItem>())
            {
                c.Click -= MiniColorItem_click;
                c.Dispose();
                PaletteContainer.Controls.Remove(c);
            }

            PaletteContainer.Controls.Clear();

            foreach (Color c in TM.ListColors())
            {

                UI.Controllers.ColorItem MiniColorItem = new UI.Controllers.ColorItem();
                MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                MiniColorItem.AllowDrop = false;
                MiniColorItem.PauseColorsHistory = true;
                MiniColorItem.BackColor = c;
                MiniColorItem.DefaultColor = MiniColorItem.BackColor;

                PaletteContainer.Controls.Add(MiniColorItem);
                MiniColorItem.Click += MiniColorItem_click;
            }

            PaletteContainer.ResumeLayout();
        }

        private void ScreenColorPicker1_MouseDown(object sender, MouseEventArgs e)
        {
            Forms_List.Clear();
            ChildControls_List.Clear();

            foreach (Control ctrl in Controls)
            {
                if (!(ctrl is ScreenColorPicker) & ctrl.Visible)
                {
                    ctrl.Visible = false;
                    ChildControls_List.Add(ctrl);
                }
            }

            for (int ix = Application.OpenForms.Count - 1; ix >= 0; ix -= 1)
            {
                if (Application.OpenForms[ix].Visible & Application.OpenForms[ix] != this)
                    Forms_List.Add(Application.OpenForms[ix]);
            }
            for (int ix = 0, loopTo = Forms_List.Count - 1; ix <= loopTo; ix++)
                Forms_List[ix].Visible = false;

            AllowTransparency = true;
            TransparencyKey = BackColor;
        }

        private void ScreenColorPicker1_MouseUp(object sender, MouseEventArgs e)
        {

            foreach (Control ctrl in ChildControls_List)
                ctrl.Visible = true;
            for (int ix = 0, loopTo = Forms_List.Count - 1; ix <= loopTo; ix++)
                Forms_List[ix].Visible = true;
            Forms_List.Clear();
            ChildControls_List.Clear();

            AllowTransparency = false;
            TransparencyKey = default;
        }

        public Color Pick(List<Control> Ctrl, Conditions Conditions = null, bool EnableAlpha = false)
        {
            if (!My.Env.Settings.Miscellaneous.Classic_Color_Picker)
            {
                var c = Ctrl[0].BackColor;
                ColorEditorManager1.Color = Ctrl[0].BackColor;
                InitColor = Ctrl[0].BackColor;

                ColorControls_List = Ctrl;

                if (Ctrl[0] is UI.Controllers.ColorItem)
                {
                    {
                        var temp = (UI.Controllers.ColorItem)Ctrl[0];
                        GetColorsHistory((UI.Controllers.ColorItem)Ctrl[0]);
                        temp.PauseColorsHistory = true;
                        temp.ColorPickerOpened = true;
                        temp.Refresh();
                    }
                }

                if (Conditions is null)
                    _Conditions = new Conditions();
                else
                    _Conditions = Conditions;

                Location = Ctrl[0].PointToScreen(Point.Empty) + (Size)new Point(-Width + Ctrl[0].Width + 5, Ctrl[0].Height - 1);
                if (Location.Y + Height > My.MyProject.Computer.Screen.Bounds.Height)
                    Location = new Point(Location.X, My.MyProject.Computer.Screen.Bounds.Height - Height);
                if (Location.Y < 0)
                    Location = new Point(Location.X, 0);
                if (Location.X + Width > My.MyProject.Computer.Screen.Bounds.Width)
                    Location = new Point(My.MyProject.Computer.Screen.Bounds.Width - Width, Location.Y);
                if (Location.X < 0)
                    Location = new Point(0, Location.Y);

                if (ShowDialog() == DialogResult.OK)
                    c = ColorEditorManager1.Color;

                if (Ctrl[0] is UI.Controllers.ColorItem)
                {
                    {
                        var temp1 = (UI.Controllers.ColorItem)Ctrl[0];
                        temp1.Refresh();
                        temp1.PauseColorsHistory = false;
                        temp1.ColorPickerOpened = false;
                        temp1.UpdateColorsHistory();
                    }
                }

                if (EnableAlpha)
                {
                    return c;
                }
                else
                {
                    return Color.FromArgb(255, c);
                }
            }

            else
            {
                var c = Color.FromArgb(Ctrl[0].BackColor.A, Ctrl[0].BackColor);
                using (var CCP = new ColorDialog() { AllowFullOpen = true, AnyColor = true, Color = c, FullOpen = true, SolidColorOnly = false })
                {
                    if (CCP.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            Ctrl[0].BackColor = CCP.Color;
                        }
                        catch
                        {
                        }
                        return CCP.Color;
                    }
                    else
                    {
                        try
                        {
                            Ctrl[0].BackColor = c;
                        }
                        catch
                        {
                        }
                        return c;
                    }
                }

            }

        }

        private void Change_Color_Preview(object sender, EventArgs e)
        {
            int steps = 30;
            int delay = 1;

            foreach (Control ctrl in ColorControls_List)
            {

                if (ctrl is UI.Simulation.Window)
                {
                    {
                        var temp = (UI.Simulation.Window)ctrl;
                        if (!_Conditions.Win7)
                        {
                            if (_Conditions.Window_ActiveTitlebar)
                            {
                                temp.AccentColor_Active = ColorEditorManager1.Color;
                            }

                            if (_Conditions.Window_InactiveTitlebar)
                            {
                                temp.AccentColor_Inactive = ColorEditorManager1.Color;
                            }
                            else
                            {
                                temp.AccentColor_Active = ColorEditorManager1.Color;
                            }
                        }


                        else if (_Conditions.Color1)
                        {
                            temp.AccentColor_Active = ColorEditorManager1.Color;
                            temp.AccentColor_Inactive = ColorEditorManager1.Color;
                        }
                        else if (_Conditions.Color2)
                        {
                            temp.AccentColor2_Active = ColorEditorManager1.Color;
                            temp.AccentColor2_Inactive = ColorEditorManager1.Color;

                        }

                        temp.Refresh();
                    }
                }

                else if (ctrl is UI.Simulation.WinElement)
                {
                    {
                        var temp1 = (UI.Simulation.WinElement)ctrl;

                        if (temp1.Style == UI.Simulation.WinElement.Styles.Taskbar11 | temp1.Style == UI.Simulation.WinElement.Styles.Taskbar10)
                        {
                            if (_Conditions.AppUnderlineOnly)
                            {
                                Visual.FadeColor((UI.Simulation.WinElement)ctrl, "AppUnderline", temp1.AppUnderline, Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color).Light(), steps, delay);
                                temp1.Refresh();
                            }

                            else if (_Conditions.AppUnderlineWithTaskbar)
                            {
                                Visual.FadeColor((UI.Simulation.WinElement)ctrl, "BackColor", temp1.BackColor, Color.FromArgb(temp1.BackColor.A, ColorEditorManager1.Color), steps, delay);
                                Visual.FadeColor((UI.Simulation.WinElement)ctrl, "AppUnderline", temp1.AppUnderline, Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color).Light(), steps, delay);
                                temp1.Refresh();
                            }

                            else if (_Conditions.AppBackgroundOnly)
                            {

                                Visual.FadeColor((UI.Simulation.WinElement)ctrl, "AppBackground", temp1.AppBackground, Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color), steps, delay);
                                temp1.Refresh();
                            }
                            else if (_Conditions.StartColorOnly)
                            {

                                Visual.FadeColor((UI.Simulation.WinElement)ctrl, "StartColor", temp1.StartColor, Color.FromArgb(255, ColorEditorManager1.Color), steps, delay);
                                temp1.Refresh();
                            }
                            else
                            {
                                if (_Conditions.BackColor1)
                                {
                                    temp1.BackColor = Color.FromArgb(temp1.BackColor.A, ColorEditorManager1.Color);
                                }
                                else if (_Conditions.BackColor2)
                                {
                                    temp1.Background2 = Color.FromArgb(temp1.Background2.A, ColorEditorManager1.Color);
                                }
                                else
                                {
                                    Visual.FadeColor((UI.Simulation.WinElement)ctrl, "BackColor", temp1.BackColor, Color.FromArgb(temp1.BackColor.A, ColorEditorManager1.Color), steps, delay);
                                }
                                temp1.Refresh();
                            }
                        }

                        else if (temp1.Style == UI.Simulation.WinElement.Styles.ActionCenter11 & _Conditions.ActionCenterBtn)
                        {
                            Visual.FadeColor((UI.Simulation.WinElement)ctrl, "ActionCenterButton_Normal", temp1.ActionCenterButton_Normal, Color.FromArgb(255, ColorEditorManager1.Color), steps, delay);
                            temp1.Refresh();
                        }

                        else if (temp1.Style == UI.Simulation.WinElement.Styles.ActionCenter10 & _Conditions.ActionCenterLink)
                        {
                            Visual.FadeColor((UI.Simulation.WinElement)ctrl, "LinkColor", temp1.LinkColor, Color.FromArgb(temp1.BackColor.A, ColorEditorManager1.Color), steps, delay);
                            temp1.Refresh();
                        }

                        else
                        {
                            if (_Conditions.BackColor1)
                            {
                                temp1.BackColor = Color.FromArgb(temp1.BackColor.A, ColorEditorManager1.Color);
                            }
                            else if (_Conditions.BackColor2)
                            {
                                temp1.Background2 = Color.FromArgb(temp1.Background2.A, ColorEditorManager1.Color);
                            }
                            else
                            {
                                Visual.FadeColor((UI.Simulation.WinElement)ctrl, "BackColor", temp1.BackColor, Color.FromArgb(temp1.BackColor.A, ColorEditorManager1.Color), steps, delay);
                            }

                            temp1.Refresh();
                        }
                    }
                }

                else if (ctrl is UI.Controllers.StoreItem)
                {
                    {
                        var temp2 = (UI.Controllers.StoreItem)ctrl;
                        if (_Conditions.BackColor1)
                        {
                            temp2.TM.Info.Color1 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }
                        else if (_Conditions.BackColor2)
                        {
                            temp2.TM.Info.Color2 = Color.FromArgb(255, ColorEditorManager1.Color);

                        }
                        temp2.Invalidate();
                    }
                }

                else if (ctrl is Label)
                {
                    if (_Conditions.RetroAppWorkspace | _Conditions.RetroBackground)
                    {
                        ctrl.BackColor = Color.FromArgb(255, ColorEditorManager1.Color);
                    }
                    else
                    {
                        Visual.FadeColor(ctrl, "Forecolor", ctrl.ForeColor, Color.FromArgb(ctrl.ForeColor.A, ColorEditorManager1.Color), steps, delay);
                    }
                }

                else if (ctrl is UI.Retro.WindowR)
                {
                    {
                        var temp3 = (UI.Retro.WindowR)ctrl;
                        if (_Conditions.WindowRColor1)
                            temp3.Color1 = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.WindowRColor2)
                            temp3.Color2 = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.WindowRForeColor)
                            temp3.ForeColor = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.WindowRBorder)
                            temp3.ColorBorder = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRShadow)
                            temp3.ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRDkShadow)
                            temp3.ButtonDkShadow = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRHilight)
                            temp3.ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRLight)
                            temp3.ButtonLight = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRFace)
                        {
                            if (!temp3.UseItAsMenu)
                            {
                                temp3.BackColor = Color.FromArgb(255, ColorEditorManager1.Color);
                            }
                            else
                            {
                                temp3.ButtonFace = Color.FromArgb(255, ColorEditorManager1.Color);
                            }
                        }
                        if (_Conditions.RetroBackground)
                            temp3.BackColor = Color.FromArgb(255, ColorEditorManager1.Color);
                        temp3.Refresh();
                    }
                }

                else if (ctrl is UI.Retro.Preview3D)
                {
                    {
                        var temp4 = (UI.Retro.Preview3D)ctrl;
                        if (_Conditions.ButtonRFace)
                            temp4.BackColor = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.WindowRFrame)
                            temp4.WindowFrame = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRText)
                            temp4.ForeColor = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRShadow)
                            temp4.ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRDkShadow)
                            temp4.ButtonDkShadow = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRHilight)
                            temp4.ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRLight)
                            temp4.ButtonLight = Color.FromArgb(255, ColorEditorManager1.Color);
                        temp4.Refresh();
                    }
                }

                else if (ctrl is UI.Retro.TextBoxR)
                {
                    {
                        var temp5 = (UI.Retro.TextBoxR)ctrl;

                        if (_Conditions.WindowRForeColor)
                            temp5.ForeColor = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRShadow)
                            temp5.ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRDkShadow)
                            temp5.ButtonDkShadow = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRHilight)
                            temp5.ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRLight)
                            temp5.ButtonLight = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRFace)
                            temp5.BackColor = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.RetroBackground)
                            temp5.BackColor = Color.FromArgb(255, ColorEditorManager1.Color);

                        temp5.Refresh();
                    }
                }

                else if (ctrl is UI.Retro.ButtonR)
                {
                    {
                        var temp6 = (UI.Retro.ButtonR)ctrl;
                        if (_Conditions.ButtonRFace)
                            temp6.BackColor = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.WindowRFrame)
                            temp6.WindowFrame = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRText)
                            temp6.ForeColor = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRShadow)
                            temp6.ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRDkShadow)
                            temp6.ButtonDkShadow = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRHilight)
                            temp6.ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (_Conditions.ButtonRLight)
                            temp6.ButtonLight = Color.FromArgb(255, ColorEditorManager1.Color);
                        temp6.Refresh();
                    }
                }

                else if (ctrl is UI.Retro.ScrollBarR)
                {
                    {
                        var temp7 = (UI.Retro.ScrollBarR)ctrl;
                        if (_Conditions.ButtonRHilight)
                            temp7.ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color);
                        else
                            temp7.BackColor = Color.FromArgb(255, ColorEditorManager1.Color);
                        temp7.Refresh();
                    }
                }

                else if (ctrl is Panel)
                {
                    if (_Conditions.RetroHighlight17BitFixer & ctrl is UI.Retro.PanelR)
                    {
                        ((UI.Retro.PanelR)ctrl).ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color);
                    }
                    else if (!(ctrl is UI.WP.GroupBox) & !(ctrl is UI.Controllers.ColorItem))
                    {
                        if (_Conditions.RetroAppWorkspace | _Conditions.RetroBackground)
                            ctrl.BackColor = Color.FromArgb(255, ColorEditorManager1.Color);
                        if (ctrl is UI.Retro.PanelR)
                        {
                            if (_Conditions.ButtonRHilight)
                                ((UI.Retro.PanelR)ctrl).ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color);
                            if (_Conditions.ButtonRShadow)
                                ((UI.Retro.PanelR)ctrl).ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color);
                        }
                        if (ctrl is Panel)
                        {
                            ctrl.BackColor = Color.FromArgb(255, ColorEditorManager1.Color);
                        }
                    }

                    if (ctrl is UI.Controllers.ColorItem)
                    {
                        Visual.FadeColor(ctrl, "BackColor", ctrl.BackColor, ColorEditorManager1.Color, steps, delay);
                    }
                    ctrl.Refresh();
                }

                else if (ctrl is UI.Retro.TextBoxR)
                {
                    {
                        var temp8 = (UI.Retro.TextBoxR)ctrl;
                        if (_Conditions.WindowRText)
                        {
                            ctrl.ForeColor = Color.FromArgb(ctrl.ForeColor.A, ColorEditorManager1.Color);
                        }
                        else
                        {
                            ctrl.BackColor = Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color);
                        }
                        ctrl.Refresh();
                    }
                }

                else if (ctrl is UI.Simulation.WinTerminal)
                {
                    {
                        var temp9 = (UI.Simulation.WinTerminal)ctrl;

                        if (_Conditions.Terminal_Back)
                        {
                            temp9.Color_Background = Color.FromArgb(255, ColorEditorManager1.Color);
                        }

                        else if (_Conditions.Terminal_Fore)
                        {
                            temp9.Color_Foreground = Color.FromArgb(255, ColorEditorManager1.Color);
                        }

                        else if (_Conditions.Terminal_Selection)
                        {
                            temp9.Color_Selection = Color.FromArgb(255, ColorEditorManager1.Color);
                        }

                        else if (_Conditions.Terminal_Cursor)
                        {
                            temp9.Color_Cursor = Color.FromArgb(255, ColorEditorManager1.Color);
                        }

                        else if (_Conditions.Terminal_TabColor)
                        {
                            temp9.TabColor = Color.FromArgb(255, ColorEditorManager1.Color);
                        }

                        else if (_Conditions.Terminal_TabActive)
                        {
                            temp9.Color_TabFocused = Color.FromArgb(255, ColorEditorManager1.Color);
                        }

                        else if (_Conditions.Terminal_TabInactive)
                        {
                            temp9.Color_TabUnFocused = Color.FromArgb(255, ColorEditorManager1.Color);
                        }

                        else if (_Conditions.Terminal_TitlebarActive)
                        {
                            temp9.Color_Titlebar = Color.FromArgb(255, ColorEditorManager1.Color);
                        }

                        else if (_Conditions.Terminal_TitlebarInactive)
                        {
                            temp9.Color_Titlebar_Unfocused = Color.FromArgb(255, ColorEditorManager1.Color);
                        }

                        temp9.Refresh();
                    }
                }

                else if (ctrl is UI.Simulation.WinCMD)
                {
                    {
                        var temp10 = (UI.Simulation.WinCMD)ctrl;
                        if (_Conditions.CMD_ColorTable00)
                        {
                            temp10.CMD_ColorTable00 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }

                        else if (_Conditions.CMD_ColorTable01)
                        {
                            temp10.CMD_ColorTable01 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }

                        else if (_Conditions.CMD_ColorTable02)
                        {
                            temp10.CMD_ColorTable02 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }


                        else if (_Conditions.CMD_ColorTable03)
                        {
                            temp10.CMD_ColorTable03 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }


                        else if (_Conditions.CMD_ColorTable04)
                        {
                            temp10.CMD_ColorTable04 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }


                        else if (_Conditions.CMD_ColorTable05)
                        {
                            temp10.CMD_ColorTable05 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }


                        else if (_Conditions.CMD_ColorTable06)
                        {
                            temp10.CMD_ColorTable06 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }


                        else if (_Conditions.CMD_ColorTable07)
                        {
                            temp10.CMD_ColorTable07 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }


                        else if (_Conditions.CMD_ColorTable08)
                        {
                            temp10.CMD_ColorTable08 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }


                        else if (_Conditions.CMD_ColorTable09)
                        {
                            temp10.CMD_ColorTable09 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }


                        else if (_Conditions.CMD_ColorTable10)
                        {
                            temp10.CMD_ColorTable10 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }


                        else if (_Conditions.CMD_ColorTable11)
                        {
                            temp10.CMD_ColorTable11 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }


                        else if (_Conditions.CMD_ColorTable12)
                        {
                            temp10.CMD_ColorTable12 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }


                        else if (_Conditions.CMD_ColorTable13)
                        {
                            temp10.CMD_ColorTable13 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }


                        else if (_Conditions.CMD_ColorTable14)
                        {
                            temp10.CMD_ColorTable14 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }


                        else if (_Conditions.CMD_ColorTable15)
                        {
                            temp10.CMD_ColorTable15 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }
                        else
                        {
                            temp10.CMD_ColorTable00 = Color.FromArgb(255, ColorEditorManager1.Color);
                        }

                        temp10.Refresh();
                    }
                }

                else if (ctrl is CursorControl)
                {
                    {
                        var temp11 = (CursorControl)ctrl;
                        if (_Conditions.CursorBack1)
                        {
                            temp11.Prop_PrimaryColor1 = ColorEditorManager1.Color;
                        }
                        else if (_Conditions.CursorBack2)
                        {
                            temp11.Prop_PrimaryColor2 = ColorEditorManager1.Color;
                        }

                        else if (_Conditions.CursorLine1)
                        {
                            temp11.Prop_SecondaryColor1 = ColorEditorManager1.Color;
                        }
                        else if (_Conditions.CursorLine2)
                        {
                            temp11.Prop_SecondaryColor2 = ColorEditorManager1.Color;
                        }

                        else if (_Conditions.CursorCircle1)
                        {
                            temp11.Prop_LoadingCircleBack1 = ColorEditorManager1.Color;
                        }
                        else if (_Conditions.CursorCircle2)
                        {
                            temp11.Prop_LoadingCircleBack2 = ColorEditorManager1.Color;
                        }

                        else if (_Conditions.CursorCircleHot1)
                        {
                            temp11.Prop_LoadingCircleHot1 = ColorEditorManager1.Color;
                        }
                        else if (_Conditions.CursorCircleHot2)
                        {
                            temp11.Prop_LoadingCircleHot2 = ColorEditorManager1.Color;
                        }
                        else if (_Conditions.CursorShadow)
                        {
                            temp11.Prop_Shadow_Color = ColorEditorManager1.Color;

                        }

                        temp11.Refresh();
                    }
                }

                else
                {
                    try
                    {
                        Visual.FadeColor(ctrl, "backcolor", ctrl.BackColor, Color.FromArgb(255, ColorEditorManager1.Color), steps, delay);
                    }
                    catch
                    {
                        try
                        {
                            ctrl.BackColor = Color.FromArgb(255, ColorEditorManager1.Color);
                        }
                        catch
                        {
                        }
                    }
                }

            }

            if ((My.Env.WVista | My.Env.W7 | My.Env.W8 | My.Env.W81) & My.Env.Settings.Miscellaneous.Win7LivePreview)
            {
                if (_Conditions.Win7LivePreview_Colorization)
                {
                    UpdateWin7Preview(ColorEditorManager1.Color, My.Env.TM.Windows7.ColorizationAfterglow);
                }

                if (_Conditions.Win7LivePreview_AfterGlow)
                {
                    UpdateWin7Preview(My.Env.TM.Windows7.ColorizationColor, ColorEditorManager1.Color);
                }
            }
        }

        #region DWM Windows 7 Live Preview
        public static void UpdateWin7Preview(Color Color1, Color Color2)
        {
            try
            {
                var Com = default(bool);
                Dwmapi.DwmIsCompositionEnabled(ref Com);

                if (Com)
                {
                    var temp = new Dwmapi.DWM_COLORIZATION_PARAMS()
                    {
                        clrColor = Color1.ToArgb(),
                        clrAfterGlow = Color2.ToArgb()
                    };

                    if (My.Env.PreviewStyle == WindowStyle.W81)
                    {
                        temp.nIntensity = My.Env.TM.Windows81.ColorizationColorBalance;
                    }

                    else if (My.Env.PreviewStyle == WindowStyle.W7)
                    {
                        temp.nIntensity = My.Env.TM.Windows7.ColorizationColorBalance;

                        temp.clrAfterGlowBalance = My.Env.TM.Windows7.ColorizationAfterglowBalance;
                        temp.clrBlurBalance = My.Env.TM.Windows7.ColorizationBlurBalance;
                        temp.clrGlassReflectionIntensity = My.Env.TM.Windows7.ColorizationGlassReflectionIntensity;
                        temp.fOpaque = My.Env.TM.Windows7.Theme == Theme.Structures.Windows7.Themes.AeroOpaque;
                    }

                    else if (My.Env.PreviewStyle == WindowStyle.WVista)
                    {
                        temp.clrColor = Color.FromArgb(My.Env.TM.WindowsVista.Alpha, My.Env.TM.WindowsVista.ColorizationColor).ToArgb();
                        temp.clrAfterGlowBalance = Color.FromArgb(My.Env.TM.WindowsVista.Alpha, My.Env.TM.WindowsVista.ColorizationColor).ToArgb();

                        // temp.nIntensity = My.Manager.WindowsVista.ColorizationColorBalance
                        // temp.clrBlurBalance = My.Manager.WindowsVista.ColorizationBlurBalance
                        // temp.clrGlassReflectionIntensity = My.Manager.WindowsVista.ColorizationGlassReflectionIntensity
                        temp.fOpaque = My.Env.TM.WindowsVista.Theme == Theme.Structures.Windows7.Themes.AeroOpaque;
                    }

                    Dwmapi.DwmSetColorizationParameters(ref temp, false);
                }
            }
            catch
            {
            }
        }


        #endregion

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            switch (RadioButton1.Checked)
            {
                case true:
                    {
                        img = My.Env.Wallpaper_Unscaled;
                        break;
                    }

                case false:
                    {
                        img = Bitmap_Mgr.Load(TextBox1.Text);
                        break;
                    }

            }

            if (CheckBox2.Checked)
                img = img.Resize(My.MyProject.Forms.MainFrm.pnl_preview.Size);

            if (img is not null)
            {
                Label4.Text = My.Env.Lang.Extracting;
                My.Env.Animator.HideSync(Button6, true);
                My.Env.Animator.HideSync(ImgPaletteContainer, true);
                ProgressBar1.Visible = true;
                Colors_List.Clear();

                try
                {
                    BackgroundWorker1.CancelAsync();
                    BackgroundWorker1.RunWorkerAsync();
                }
                catch
                {
                }
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (img is not null)
            {
                var ColorThiefX = new ColorThiefDotNet.ColorThief();
                var Colors = ColorThiefX.GetPalette((Bitmap)img, Trackbar1.Value, Trackbar2.Value, CheckBox1.Checked);
                foreach (ColorThiefDotNet.QuantizedColor C in Colors)
                    Colors_List.Add(Color.FromArgb(255, C.Color.R, C.Color.G, C.Color.B));
                GC.Collect();
                GC.SuppressFinalize(Colors);
                GC.SuppressFinalize(ColorThiefX);
                img.Dispose();
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            foreach (UI.Controllers.ColorItem ctrl in ImgPaletteContainer.Controls.OfType<UI.Controllers.ColorItem>())
            {
                ctrl.Click -= MiniColorItem_click;
                ctrl.Dispose();
            }
            ImgPaletteContainer.Controls.Clear();

            Colors_List = Colors_List.Distinct().ToList();
            Colors_List.Sort(new RGBColorComparer());

            foreach (Color C in Colors_List)
            {
                UI.Controllers.ColorItem MiniColorItem = new UI.Controllers.ColorItem();
                MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                MiniColorItem.AllowDrop = false;
                MiniColorItem.PauseColorsHistory = true;
                MiniColorItem.BackColor = Color.FromArgb(255, C);
                MiniColorItem.DefaultColor = MiniColorItem.BackColor;

                ImgPaletteContainer.Controls.Add(MiniColorItem);
                MiniColorItem.Click += MiniColorItem_click;
            }

            ProgressBar1.Visible = false;
            Colors_List.Clear();
            My.Env.Animator.ShowSync(ImgPaletteContainer, true);
            My.Env.Animator.ShowSync(Button6, true);
        }

        private void MiniColorItem_click(object sender, EventArgs e)
        {
            ColorEditorManager1.Color = ((UI.Controllers.ColorItem)sender).BackColor;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            ColorWheel1.Visible = false;
            ColorGrid1.Visible = false;
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Text = OpenFileDialog1.FileName;
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            if (OpenFileDialog2.ShowDialog() == DialogResult.OK)
            {
                ColorGrid1.Colors = ColorCollection.LoadPalette(OpenFileDialog2.FileName);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (OpenThemeDialog.ShowDialog() == DialogResult.OK)
            {
                TextBox2.Text = OpenThemeDialog.FileName;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ComboBox2.SelectedIndex)
            {
                case 0:
                    {
                        GetColorsFromPalette(My.Env.TM);
                        break;
                    }
                case 1:
                    {
                        GetColorsFromPalette(new Theme.Default().Windows11());
                        break;
                    }
                case 2:
                    {
                        GetColorsFromPalette(new Theme.Default().Windows10());
                        break;
                    }
                case 3:
                    {
                        GetColorsFromPalette(new Theme.Default().Windows81());
                        break;
                    }
                case 4:
                    {
                        GetColorsFromPalette(new Theme.Default().WindowsVista());
                        break;
                    }
                case 5:
                    {
                        GetColorsFromPalette(new Theme.Default().WindowsXP());
                        break;
                    }
                case 6:
                    {
                        GetColorsFromPalette(new Theme.Default().Windows7());
                        break;
                    }

                default:
                    {
                        GetColorsFromPalette(My.Env.TM);
                        break;
                    }
            }
        }

        private void ColorPickerDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult != DialogResult.OK & (My.Env.WVista | My.Env.W7 | My.Env.W8 | My.Env.W81) & My.Env.Settings.Miscellaneous.Win7LivePreview)
            {
                if (_Conditions.Win7LivePreview_Colorization)
                {
                    UpdateWin7Preview(InitColor, My.Env.TM.Windows7.ColorizationAfterglow);
                }

                if (_Conditions.Win7LivePreview_AfterGlow)
                {
                    UpdateWin7Preview(My.Env.TM.Windows7.ColorizationColor, InitColor);
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem is null)
                return;


            foreach (UI.Controllers.ColorItem c in ThemePaletteContainer.Controls.OfType<UI.Controllers.ColorItem>())
            {
                c.Click -= MiniColorItem_click;
                c.Dispose();
                ThemePaletteContainer.Controls.Remove(c);
            }

            ThemePaletteContainer.Controls.Clear();

            try
            {
                if (!string.IsNullOrWhiteSpace(ComboBox1.SelectedItem.ToString()))
                {
                    foreach (Color C in Theme.Manager.GetPaletteFromString(Properties.Resources.RetroThemesDB, ComboBox1.SelectedItem.ToString()))
                    {
                        UI.Controllers.ColorItem MiniColorItem = new UI.Controllers.ColorItem();
                        MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                        MiniColorItem.AllowDrop = false;
                        MiniColorItem.PauseColorsHistory = true;
                        MiniColorItem.BackColor = Color.FromArgb(255, C);
                        MiniColorItem.DefaultColor = MiniColorItem.BackColor;

                        ThemePaletteContainer.Controls.Add(MiniColorItem);
                        MiniColorItem.Click += MiniColorItem_click;
                    }
                }
            }
            catch
            {

            }
        }

        private void Ttl_h_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar1.Maximum), Trackbar1.Minimum).ToString();
            Trackbar1.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar2.Maximum), Trackbar2.Minimum).ToString();
            Trackbar2.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Trackbar1_Scroll(object sender)
        {
            val1.Text = ((UI.WP.Trackbar)sender).Value.ToString();
        }

        private void Trackbar2_Scroll(object sender)
        {
            val2.Text = ((UI.WP.Trackbar)sender).Value.ToString();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(TextBox2.Text))
            {

                foreach (UI.Controllers.ColorItem c in ThemePaletteContainer.Controls.OfType<UI.Controllers.ColorItem>())
                {
                    c.Click -= MiniColorItem_click;
                    c.Dispose();
                    ThemePaletteContainer.Controls.Remove(c);
                }

                ThemePaletteContainer.Controls.Clear();

                if (System.IO.Path.GetExtension(TextBox2.Text).ToLower() == ".theme")
                {
                    try
                    {
                        foreach (Color C in Theme.Manager.GetPaletteFromMSTheme(TextBox2.Text))
                        {
                            UI.Controllers.ColorItem MiniColorItem = new UI.Controllers.ColorItem();
                            MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                            MiniColorItem.AllowDrop = false;
                            MiniColorItem.PauseColorsHistory = true;
                            MiniColorItem.BackColor = Color.FromArgb(255, C);
                            MiniColorItem.DefaultColor = MiniColorItem.BackColor;

                            ThemePaletteContainer.Controls.Add(MiniColorItem);
                            MiniColorItem.Click += MiniColorItem_click;
                        }
                    }
                    catch
                    {
                        WPStyle.MsgBox(My.Env.Lang.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                else if (System.IO.Path.GetExtension(TextBox2.Text).ToLower() == ".msstyles")
                {
                    try
                    {
                        System.IO.File.WriteAllText(My.Env.PATH_appData + @"\VisualStyles\Luna\win32uischeme.theme", string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", TextBox2.Text, "\r\n"));

                        var vs = new VisualStyleFile(My.Env.PATH_appData + @"\VisualStyles\Luna\win32uischeme.theme");

                        foreach (var field in typeof(VisualStyleMetricColors).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                        {
                            if (field.FieldType.Name.ToLower() == "color")
                            {

                                UI.Controllers.ColorItem MiniColorItem = new UI.Controllers.ColorItem();
                                MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                                MiniColorItem.AllowDrop = false;
                                MiniColorItem.PauseColorsHistory = true;
                                MiniColorItem.BackColor = (Color)field.GetValue(vs.Metrics.Colors);
                                MiniColorItem.DefaultColor = MiniColorItem.BackColor;

                                ThemePaletteContainer.Controls.Add(MiniColorItem);
                                MiniColorItem.Click += MiniColorItem_click;

                            }
                        }
                    }
                    catch
                    {
                        WPStyle.MsgBox(My.Env.Lang.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    WPStyle.MsgBox(My.Env.Lang.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }

    public class Conditions
    {

        public Conditions()
        {
        }

        public bool Terminal_Back { get; set; } = false;
        public bool Terminal_Fore { get; set; } = false;
        public bool Terminal_Selection { get; set; } = false;
        public bool Terminal_Cursor { get; set; } = false;
        public bool Terminal_TabColor { get; set; } = false;
        public bool Terminal_TabActive { get; set; } = false;
        public bool Terminal_TabInactive { get; set; } = false;
        public bool Terminal_TitlebarActive { get; set; } = false;
        public bool Terminal_TitlebarInactive { get; set; } = false;

        public bool CMD_ColorTable00 { get; set; } = false;
        public bool CMD_ColorTable01 { get; set; } = false;
        public bool CMD_ColorTable02 { get; set; } = false;
        public bool CMD_ColorTable03 { get; set; } = false;
        public bool CMD_ColorTable04 { get; set; } = false;
        public bool CMD_ColorTable05 { get; set; } = false;
        public bool CMD_ColorTable06 { get; set; } = false;
        public bool CMD_ColorTable07 { get; set; } = false;
        public bool CMD_ColorTable08 { get; set; } = false;
        public bool CMD_ColorTable09 { get; set; } = false;
        public bool CMD_ColorTable10 { get; set; } = false;
        public bool CMD_ColorTable11 { get; set; } = false;
        public bool CMD_ColorTable12 { get; set; } = false;
        public bool CMD_ColorTable13 { get; set; } = false;
        public bool CMD_ColorTable14 { get; set; } = false;
        public bool CMD_ColorTable15 { get; set; } = false;

        public bool Win7LivePreview_Colorization { get; set; } = false;
        public bool Win7LivePreview_AfterGlow { get; set; } = false;
        public bool Win7 { get; set; } = false;
        public bool Color1 { get; set; } = false;
        public bool Color2 { get; set; } = false;
        public bool BackColor1 { get; set; } = false;
        public bool BackColor2 { get; set; } = false;
        public bool Window_InactiveTitlebar { get; set; } = false;
        public bool Window_ActiveTitlebar { get; set; } = true;
        public bool AppUnderlineOnly { get; set; } = false;
        public bool AppUnderlineWithTaskbar { get; set; } = false;
        public bool AppBackgroundOnly { get; set; } = false;
        public bool StartColorOnly { get; set; } = false;
        public bool ActionCenterBtn { get; set; } = false;
        public bool ActionCenterLink { get; set; } = false;
        public bool WindowRColor1 { get; set; } = false;
        public bool WindowRColor2 { get; set; } = false;
        public bool WindowRForeColor { get; set; } = false;
        public bool WindowRBorder { get; set; } = false;
        public bool WindowRFrame { get; set; } = false;
        public bool ButtonRFace { get; set; } = false;
        public bool ButtonRDkShadow { get; set; } = false;
        public bool ButtonRShadow { get; set; } = false;
        public bool ButtonRHilight { get; set; } = false;
        public bool ButtonRLight { get; set; } = false;
        public bool ButtonRText { get; set; } = false;
        public bool RetroAppWorkspace { get; set; } = false;
        public bool RetroBackground { get; set; } = false;
        public bool WindowRText { get; set; } = false;
        public bool RetroHighlight17BitFixer { get; set; } = false;
        public bool CursorBack1 { get; set; } = false;
        public bool CursorBack2 { get; set; } = false;
        public bool CursorLine1 { get; set; } = false;
        public bool CursorLine2 { get; set; } = false;
        public bool CursorCircle1 { get; set; } = false;
        public bool CursorCircle2 { get; set; } = false;
        public bool CursorCircleHot1 { get; set; } = false;
        public bool CursorCircleHot2 { get; set; } = false;
        public bool CursorShadow { get; set; } = false;

    }
}