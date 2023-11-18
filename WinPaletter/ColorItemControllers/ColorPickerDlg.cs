using Cyotek.Windows.Forms;
using Devcorp.Controls.VisualStyles;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.UI.Simulation;
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
        private bool _EnableAlpha;

        #region Form Shadow
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                if (!DWMAPI.IsCompositionEnabled())
                {
                    cp.ClassStyle = cp.ClassStyle | DWMAPI.CS_DROPSHADOW;
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
                case DWMAPI.WM_NCPAINT:
                    {
                        int val = 2;
                        if (DWMAPI.IsCompositionEnabled())
                        {
                            DWMAPI.DwmSetWindowAttribute(Handle, Program.Style.RoundedCorners ? 2 : 1, ref val, 4);
                            DWMAPI.MARGINS bla = new();
                            {
                                bla.bottomHeight = 1;
                                bla.leftWidth = 1;
                                bla.rightWidth = 1;
                                bla.topHeight = 1;
                            }
                            DWMAPI.DwmExtendFrameIntoClientArea(Handle, ref bla);
                        }

                        break;
                    }
            }

            base.WndProc(ref m);
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
            ApplyStyle(this);
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

            foreach (Color c in TM.Colors())
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
            if (!Program.Settings.NerdStats.Classic_Color_Picker)
            {
                var c = Ctrl[0].BackColor;
                ColorEditorManager1.Color = Ctrl[0].BackColor;
                InitColor = Ctrl[0].BackColor;
                _EnableAlpha = EnableAlpha;
                ColorControls_List = Ctrl;

                ColorEditorManager1.ColorEditor.ShowAlphaChannel = _EnableAlpha;

                if (Ctrl[0] is UI.Controllers.ColorItem)
                {
                    {
                        var ctrl = (UI.Controllers.ColorItem)Ctrl[0];
                        GetColorsHistory((UI.Controllers.ColorItem)Ctrl[0]);
                        ctrl.PauseColorsHistory = true;
                        ctrl.ColorPickerOpened = true;
                        ctrl.Refresh();
                    }
                }

                if (Conditions is null)
                    _Conditions = new Conditions();
                else
                    _Conditions = Conditions;

                Location = Ctrl[0].PointToScreen(Point.Empty) + (Size)new Point(-Width + Ctrl[0].Width + 5, Ctrl[0].Height - 1);
                if (Location.Y + Height > Program.Computer.Screen.Bounds.Height)
                    Location = new Point(Location.X, Program.Computer.Screen.Bounds.Height - Height);
                if (Location.Y < 0)
                    Location = new Point(Location.X, 0);
                if (Location.X + Width > Program.Computer.Screen.Bounds.Width)
                    Location = new Point(Program.Computer.Screen.Bounds.Width - Width, Location.Y);
                if (Location.X < 0)
                    Location = new Point(0, Location.Y);

                if (ShowDialog() == DialogResult.OK)
                    c = ColorEditorManager1.Color;

                if (Ctrl[0] is UI.Controllers.ColorItem)
                {
                    {
                        var ColorItem = (UI.Controllers.ColorItem)Ctrl[0];
                        ColorItem.Refresh();
                        ColorItem.PauseColorsHistory = false;
                        ColorItem.ColorPickerOpened = false;
                        ColorItem.UpdateColorsHistory();
                    }
                }

                if (_EnableAlpha)
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
            int steps = 25;
            int delay = 1;

            Color color = _EnableAlpha ? ColorEditorManager1.Color : Color.FromArgb(255, ColorEditorManager1.Color);

            foreach (Control control in ColorControls_List)
            {
                if (control is UI.Simulation.Window)
                {
                    var Window = (UI.Simulation.Window)control;

                    if (!_Conditions.Win7)
                    {
                        if (_Conditions.Window_ActiveTitlebar)
                            Window.AccentColor_Active = color;

                        else if (_Conditions.Window_InactiveTitlebar)
                            Window.AccentColor_Inactive = color;

                        else
                            Window.AccentColor_Active = color;

                    }

                    else if (_Conditions.Color1)
                    {
                        Window.AccentColor_Active = color;
                        Window.AccentColor_Inactive = color;
                    }

                    else if (_Conditions.Color2)
                    {
                        Window.AccentColor2_Active = color;
                        Window.AccentColor2_Inactive = color;
                    }

                    Window.Refresh();
                }

                else if (control is UI.Simulation.WinElement)
                {
                    {
                        var WinElement = (UI.Simulation.WinElement)control;

                        if (WinElement.Style == UI.Simulation.WinElement.Styles.Taskbar11 | WinElement.Style == UI.Simulation.WinElement.Styles.Taskbar10)
                        {
                            if (_Conditions.AppUnderlineOnly)
                            {
                                FluentTransitions.Transition.With(WinElement, nameof(WinElement.AppUnderline), color.Light()).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                            }

                            else if (_Conditions.AppUnderlineWithTaskbar)
                            {
                                FluentTransitions.Transition.With(WinElement, nameof(WinElement.Background), color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                                FluentTransitions.Transition.With(WinElement, nameof(WinElement.AppUnderline), color.Light()).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                            }

                            else if (_Conditions.AppBackgroundOnly)
                            {
                                FluentTransitions.Transition.With(WinElement, nameof(WinElement.AppBackground), color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                            }
                            else if (_Conditions.StartColorOnly)
                            {
                                FluentTransitions.Transition.With(WinElement, nameof(WinElement.StartColor), color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                            }
                            else
                            {
                                if (_Conditions.Background)
                                {
                                    WinElement.Background = color;
                                }
                                else if (_Conditions.Background2)
                                {
                                    WinElement.Background2 = color;
                                }
                                else
                                {
                                    FluentTransitions.Transition.With(WinElement, nameof(WinElement.Background), color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                                }
                            }
                        }

                        else if (WinElement.Style == UI.Simulation.WinElement.Styles.ActionCenter11 & _Conditions.ActionCenterBtn)
                        {
                            FluentTransitions.Transition.With(WinElement, nameof(WinElement.ActionCenterButton_Normal), color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                        }

                        else if (WinElement.Style == UI.Simulation.WinElement.Styles.ActionCenter10 & _Conditions.ActionCenterLink)
                        {
                            FluentTransitions.Transition.With(WinElement, nameof(WinElement.LinkColor), color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                        }

                        else
                        {
                            if (_Conditions.Background)
                            {
                                WinElement.Background = color;
                            }
                            else if (_Conditions.Background2)
                            {
                                WinElement.Background2 = color;
                            }
                            else
                            {
                                FluentTransitions.Transition.With(WinElement, nameof(WinElement.Background), color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                            }

                        }

                        WinElement.Refresh();
                    }
                }

                else if (control is UI.Controllers.StoreItem)
                {
                    var StoreItem = (UI.Controllers.StoreItem)control;
                    if (_Conditions.Background)
                    {
                        StoreItem.TM.Info.Color1 = color;
                    }
                    else if (_Conditions.Background2)
                    {
                        StoreItem.TM.Info.Color2 = color;

                    }
                    StoreItem.Invalidate();
                }

                else if (control is Label)
                {
                    if (_Conditions.RetroAppWorkspace | _Conditions.RetroBackground)
                    {
                        control.BackColor = Color.FromArgb(255, color);
                    }
                    else
                    {
                        FluentTransitions.Transition.With(control, nameof(control.ForeColor), Color.FromArgb(control.ForeColor.A, color)).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                    }
                }

                else if (control is UI.Retro.WindowR)
                {
                    var WindowR = (UI.Retro.WindowR)control;
                    if (_Conditions.WindowRColor1)
                        WindowR.Color1 = color;

                    if (_Conditions.WindowRColor2)
                        WindowR.Color2 = color;

                    if (_Conditions.WindowRForeColor)
                        WindowR.ForeColor = color;

                    if (_Conditions.WindowRBorder)
                        WindowR.ColorBorder = color;

                    if (_Conditions.ButtonRShadow)
                        WindowR.ButtonShadow = color;

                    if (_Conditions.ButtonRDkShadow)
                        WindowR.ButtonDkShadow = color;

                    if (_Conditions.ButtonRHilight)
                        WindowR.ButtonHilight = color;

                    if (_Conditions.ButtonRLight)
                        WindowR.ButtonLight = color;

                    if (_Conditions.ButtonRFace)
                    {
                        if (!WindowR.UseItAsMenu)
                        {
                            WindowR.BackColor = color;
                        }
                        else
                        {
                            WindowR.ButtonFace = color;
                        }
                    }

                    if (_Conditions.RetroBackground)
                        WindowR.BackColor = color;

                    WindowR.Refresh();
                }

                else if (control is UI.Retro.Preview3D)
                {
                    var Preview3D = (UI.Retro.Preview3D)control;
                    if (_Conditions.ButtonRFace)
                        Preview3D.BackColor = color;

                    if (_Conditions.WindowRFrame)
                        Preview3D.WindowFrame = color;

                    if (_Conditions.ButtonRText)
                        Preview3D.ForeColor = color;

                    if (_Conditions.ButtonRShadow)
                        Preview3D.ButtonShadow = color;

                    if (_Conditions.ButtonRDkShadow)
                        Preview3D.ButtonDkShadow = color;

                    if (_Conditions.ButtonRHilight)
                        Preview3D.ButtonHilight = color;

                    if (_Conditions.ButtonRLight)
                        Preview3D.ButtonLight = color;

                    Preview3D.Refresh();
                }

                else if (control is UI.Retro.TextBoxR)
                {
                    var TextBoxR = (UI.Retro.TextBoxR)control;
                    if (_Conditions.WindowRForeColor)
                        TextBoxR.ForeColor = color;

                    if (_Conditions.ButtonRShadow)
                        TextBoxR.ButtonShadow = color;

                    if (_Conditions.ButtonRDkShadow)
                        TextBoxR.ButtonDkShadow = color;

                    if (_Conditions.ButtonRHilight)
                        TextBoxR.ButtonHilight = color;

                    if (_Conditions.ButtonRLight)
                        TextBoxR.ButtonLight = color;

                    if (_Conditions.ButtonRFace)
                        TextBoxR.BackColor = color;

                    if (_Conditions.RetroBackground)
                        TextBoxR.BackColor = color;

                    TextBoxR.Refresh();
                }

                else if (control is UI.Retro.ButtonR)
                {
                    var ButtonR = (UI.Retro.ButtonR)control;
                    if (_Conditions.ButtonRFace)
                        ButtonR.BackColor = color;

                    if (_Conditions.WindowRFrame)
                        ButtonR.WindowFrame = color;

                    if (_Conditions.ButtonRText)
                        ButtonR.ForeColor = color;

                    if (_Conditions.ButtonRShadow)
                        ButtonR.ButtonShadow = color;

                    if (_Conditions.ButtonRDkShadow)
                        ButtonR.ButtonDkShadow = color;

                    if (_Conditions.ButtonRHilight)
                        ButtonR.ButtonHilight = color;

                    if (_Conditions.ButtonRLight)
                        ButtonR.ButtonLight = color;

                    ButtonR.Refresh();
                }

                else if (control is UI.Retro.ScrollBarR)
                {
                    var ScrollBarR = (UI.Retro.ScrollBarR)control;

                    if (_Conditions.ButtonRHilight)
                        ScrollBarR.ButtonHilight = color;

                    else
                        ScrollBarR.BackColor = color;

                    ScrollBarR.Refresh();
                }

                else if (control is Panel)
                {
                    if (_Conditions.RetroHighlight17BitFixer && control is UI.Retro.PanelR)
                    {
                        ((UI.Retro.PanelR)control).ButtonShadow = color;
                    }

                    else if (control is not UI.WP.GroupBox && control is not UI.Controllers.ColorItem)
                    {
                        if (_Conditions.RetroAppWorkspace | _Conditions.RetroBackground)
                            control.BackColor = color;

                        if (control is UI.Retro.PanelR)
                        {
                            if (_Conditions.ButtonRHilight)
                                ((UI.Retro.PanelR)control).ButtonHilight = color;

                            if (_Conditions.ButtonRShadow)
                                ((UI.Retro.PanelR)control).ButtonShadow = color;
                        }

                        if (control is Panel)
                        {
                            control.BackColor = color;
                        }
                    }

                    else if (control is UI.Controllers.ColorItem)
                    {
                        FluentTransitions.Transition.With(control, nameof(control.BackColor), color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                    }

                    control.Refresh();
                }

                else if (control is UI.Retro.TextBoxR)
                {
                    var TextBoxR = (UI.Retro.TextBoxR)control;

                    if (_Conditions.WindowRText)
                    {
                        TextBoxR.ForeColor = Color.FromArgb(control.ForeColor.A, color);
                    }

                    else
                    {
                        TextBoxR.BackColor = Color.FromArgb(control.BackColor.A, color);
                    }

                    TextBoxR.Refresh();
                }

                else if (control is UI.Simulation.WinTerminal)
                {
                    var WinTerminal = (UI.Simulation.WinTerminal)control;

                    if (_Conditions.Terminal_Back)
                    {
                        WinTerminal.Color_Background = Color.FromArgb(255, color);
                    }

                    else if (_Conditions.Terminal_Fore)
                    {
                        WinTerminal.Color_Foreground = Color.FromArgb(255, color);
                    }

                    else if (_Conditions.Terminal_Selection)
                    {
                        WinTerminal.Color_Selection = Color.FromArgb(255, color);
                    }

                    else if (_Conditions.Terminal_Cursor)
                    {
                        WinTerminal.Color_Cursor = Color.FromArgb(255, color);
                    }

                    else if (_Conditions.Terminal_TabColor)
                    {
                        WinTerminal.TabColor = Color.FromArgb(255, color);
                    }

                    else if (_Conditions.Terminal_TabActive)
                    {
                        WinTerminal.Color_TabFocused = Color.FromArgb(255, color);
                    }

                    else if (_Conditions.Terminal_TabInactive)
                    {
                        WinTerminal.Color_TabUnFocused = Color.FromArgb(255, color);
                    }

                    else if (_Conditions.Terminal_TitlebarActive)
                    {
                        WinTerminal.Color_Titlebar = Color.FromArgb(255, color);
                    }

                    else if (_Conditions.Terminal_TitlebarInactive)
                    {
                        WinTerminal.Color_Titlebar_Unfocused = Color.FromArgb(255, color);
                    }

                    WinTerminal.Refresh();
                }

                else if (control is UI.Simulation.WinCMD)
                {
                    var WinCMD = (UI.Simulation.WinCMD)control;
                    if (_Conditions.CMD_ColorTable00)
                    {
                        WinCMD.CMD_ColorTable00 = Color.FromArgb(255, color);
                    }

                    else if (_Conditions.CMD_ColorTable01)
                    {
                        WinCMD.CMD_ColorTable01 = Color.FromArgb(255, color);
                    }

                    else if (_Conditions.CMD_ColorTable02)
                    {
                        WinCMD.CMD_ColorTable02 = Color.FromArgb(255, color);
                    }


                    else if (_Conditions.CMD_ColorTable03)
                    {
                        WinCMD.CMD_ColorTable03 = Color.FromArgb(255, color);
                    }


                    else if (_Conditions.CMD_ColorTable04)
                    {
                        WinCMD.CMD_ColorTable04 = Color.FromArgb(255, color);
                    }


                    else if (_Conditions.CMD_ColorTable05)
                    {
                        WinCMD.CMD_ColorTable05 = Color.FromArgb(255, color);
                    }


                    else if (_Conditions.CMD_ColorTable06)
                    {
                        WinCMD.CMD_ColorTable06 = Color.FromArgb(255, color);
                    }


                    else if (_Conditions.CMD_ColorTable07)
                    {
                        WinCMD.CMD_ColorTable07 = Color.FromArgb(255, color);
                    }


                    else if (_Conditions.CMD_ColorTable08)
                    {
                        WinCMD.CMD_ColorTable08 = Color.FromArgb(255, color);
                    }


                    else if (_Conditions.CMD_ColorTable09)
                    {
                        WinCMD.CMD_ColorTable09 = Color.FromArgb(255, color);
                    }


                    else if (_Conditions.CMD_ColorTable10)
                    {
                        WinCMD.CMD_ColorTable10 = Color.FromArgb(255, color);
                    }


                    else if (_Conditions.CMD_ColorTable11)
                    {
                        WinCMD.CMD_ColorTable11 = Color.FromArgb(255, color);
                    }


                    else if (_Conditions.CMD_ColorTable12)
                    {
                        WinCMD.CMD_ColorTable12 = Color.FromArgb(255, color);
                    }


                    else if (_Conditions.CMD_ColorTable13)
                    {
                        WinCMD.CMD_ColorTable13 = Color.FromArgb(255, color);
                    }


                    else if (_Conditions.CMD_ColorTable14)
                    {
                        WinCMD.CMD_ColorTable14 = Color.FromArgb(255, color);
                    }


                    else if (_Conditions.CMD_ColorTable15)
                    {
                        WinCMD.CMD_ColorTable15 = Color.FromArgb(255, color);
                    }
                    else
                    {
                        WinCMD.CMD_ColorTable00 = Color.FromArgb(255, color);
                    }

                    WinCMD.Refresh();
                }

                else if (control is UI.Controllers.CursorControl)
                {
                    var CursorControl = (UI.Controllers.CursorControl)control;

                    if (_Conditions.CursorBack1)
                    {
                        CursorControl.Prop_PrimaryColor1 = color;
                    }
                    else if (_Conditions.CursorBack2)
                    {
                        CursorControl.Prop_PrimaryColor2 = color;
                    }

                    else if (_Conditions.CursorLine1)
                    {
                        CursorControl.Prop_SecondaryColor1 = color;
                    }
                    else if (_Conditions.CursorLine2)
                    {
                        CursorControl.Prop_SecondaryColor2 = color;
                    }

                    else if (_Conditions.CursorCircle1)
                    {
                        CursorControl.Prop_LoadingCircleBack1 = color;
                    }
                    else if (_Conditions.CursorCircle2)
                    {
                        CursorControl.Prop_LoadingCircleBack2 = color;
                    }

                    else if (_Conditions.CursorCircleHot1)
                    {
                        CursorControl.Prop_LoadingCircleHot1 = color;
                    }
                    else if (_Conditions.CursorCircleHot2)
                    {
                        CursorControl.Prop_LoadingCircleHot2 = color;
                    }

                    else if (_Conditions.CursorShadow)
                    {
                        CursorControl.Prop_Shadow_Color = color;

                    }

                    CursorControl.Refresh();
                }

                else
                {
                    try { FluentTransitions.Transition.With(control, nameof(control.ForeColor), Color.FromArgb(255, color)).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
                    catch
                    {
                        try { control.BackColor = Color.FromArgb(255, color); }
                        catch { }
                    }
                }
            }

            if ((OS.WVista || OS.W7 || OS.W8 || OS.W81) && Program.Settings.Miscellaneous.Win7LivePreview)
            {
                if (_Conditions.LivePreview_Colorization)
                {
                    UpdateDWMPreview(ColorEditorManager1.Color, Program.TM.Windows7.ColorizationAfterglow);
                }

                if (_Conditions.LivePreview_AfterGlow)
                {
                    UpdateDWMPreview(Program.TM.Windows7.ColorizationColor, ColorEditorManager1.Color);
                }
            }
        }

        #region DWM Windows 7 Live Preview
        public static void UpdateDWMPreview(Color Color1, Color Color2)
        {
            Task.Run(() =>
            {
                try
                {
                    using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                    {
                        if (DWMAPI.IsCompositionEnabled())
                        {
                            var DCP = new DWMAPI.DWM_COLORIZATION_PARAMS()
                            {
                                clrColor = (uint)Color1.ToArgb(),
                                clrAfterGlow = (uint)Color2.ToArgb()
                            };

                            if (Program.PreviewStyle == WindowStyle.W81)
                            {
                                DCP.nIntensity = (uint)Program.TM.Windows81.ColorizationColorBalance;
                                DWMAPI.DwmSetColorizationParameters(ref DCP, false);
                            }

                            else if (Program.PreviewStyle == WindowStyle.W7)
                            {
                                DCP.nIntensity = (uint)Program.TM.Windows7.ColorizationColorBalance;

                                DCP.clrAfterGlowBalance = (uint)Program.TM.Windows7.ColorizationAfterglowBalance;
                                DCP.clrBlurBalance = (uint)Program.TM.Windows7.ColorizationBlurBalance;
                                DCP.clrGlassReflectionIntensity = (uint)Program.TM.Windows7.ColorizationGlassReflectionIntensity;
                                DCP.fOpaque = Program.TM.Windows7.Theme == Theme.Structures.Windows7.Themes.AeroOpaque;
                                DWMAPI.DwmSetColorizationParameters(ref DCP, false);
                            }
                        }
                        wic.Undo();
                    }
                }
                catch { }
            });
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
                        img = Program.Wallpaper_Unscaled;
                        break;
                    }

                case false:
                    {
                        img = Bitmap_Mgr.Load(TextBox1.Text);
                        break;
                    }

            }

            if (CheckBox2.Checked)
                img = img.Resize(Forms.MainFrm.pnl_preview.Size);

            if (img is not null)
            {
                Label4.Text = Program.Lang.Extracting;
                Program.Animator.HideSync(Button6, true);
                Program.Animator.HideSync(ImgPaletteContainer, true);
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
            Program.Animator.ShowSync(ImgPaletteContainer, true);
            Program.Animator.ShowSync(Button6, true);
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
                        GetColorsFromPalette(Program.TM);
                        break;
                    }
                case 1:
                    {
                        GetColorsFromPalette(Theme.Default.Windows11());
                        break;
                    }
                case 2:
                    {
                        GetColorsFromPalette(Theme.Default.Windows10());
                        break;
                    }
                case 3:
                    {
                        GetColorsFromPalette(Theme.Default.Windows81());
                        break;
                    }
                case 4:
                    {
                        GetColorsFromPalette(Theme.Default.WindowsVista());
                        break;
                    }
                case 5:
                    {
                        GetColorsFromPalette(Theme.Default.WindowsXP());
                        break;
                    }
                case 6:
                    {
                        GetColorsFromPalette(Theme.Default.Windows7());
                        break;
                    }

                default:
                    {
                        GetColorsFromPalette(Program.TM);
                        break;
                    }
            }
        }

        private void ColorPickerDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult != DialogResult.OK & (OS.WVista | OS.W7 | OS.W8 | OS.W81) & Program.Settings.Miscellaneous.Win7LivePreview)
            {
                if (_Conditions.LivePreview_Colorization)
                {
                    UpdateDWMPreview(InitColor, Program.TM.Windows7.ColorizationAfterglow);
                }

                if (_Conditions.LivePreview_AfterGlow)
                {
                    UpdateDWMPreview(Program.TM.Windows7.ColorizationColor, InitColor);
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
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar1.Maximum), Trackbar1.Minimum).ToString();
            Trackbar1.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
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
                        MsgBox(Program.Lang.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                else if (System.IO.Path.GetExtension(TextBox2.Text).ToLower() == ".msstyles")
                {
                    try
                    {
                        System.IO.File.WriteAllText(PathsExt.appData + @"\VisualStyles\Luna\win32uischeme.theme", string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", TextBox2.Text, "\r\n"));

                        var vs = new VisualStyleFile(PathsExt.appData + @"\VisualStyles\Luna\win32uischeme.theme");

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
                        MsgBox(Program.Lang.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MsgBox(Program.Lang.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public bool LivePreview_Colorization { get; set; } = false;
        public bool LivePreview_AfterGlow { get; set; } = false;
        public bool Win7 { get; set; } = false;
        public bool Color1 { get; set; } = false;
        public bool Color2 { get; set; } = false;
        public bool Background { get; set; } = false;
        public bool Background2 { get; set; } = false;
        public bool Window_InactiveTitlebar { get; set; } = false;
        public bool Window_ActiveTitlebar { get; set; } = false;
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