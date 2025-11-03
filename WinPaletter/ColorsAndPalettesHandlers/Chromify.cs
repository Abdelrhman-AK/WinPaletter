using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Theme;
using WinPaletter.Theme.Structures;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.Simulation;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{
    public partial class Chromify : Form
    {
        List<Color> Palette = null;
        Theme.Manager TM;
        private readonly List<CursorControl> AnimateList = [];
        private float Angle = 180f;
        private float Increment = 5f;
        private int Cycles = 0;

        public Chromify()
        {
            InitializeComponent();
        }

        private void Chromify_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            NativeMethods.Helpers.RemoveFormTitlebarTextAndIcon(Handle);
            Icon = FormsExtensions.Icon<MainForm>();

            labelAlt1.Text = Text;
            next_btn.Text = Program.Lang.Strings.General.Next;

            progressBar1.Maximum = (tablessControl1.TabCount - 1) * 100;

            foreach (CursorControl i in Cursors_Container.Controls.OfType<CursorControl>().Where(i => i.Prop_Cursor == Paths.CursorType.AppLoading | i.Prop_Cursor == Paths.CursorType.Busy))
            {
                AnimateList.Add(i);
            }

            Forms.GlassWindow.Show();
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            if (tablessControl1.SelectedIndex == tablessControl1.TabCount - 1)
            {


                DialogResult = DialogResult.OK;
                Close();
                return;
            }
            else if (tablessControl1.SelectedIndex == 2)
            {
                // Start Magic

                TM = Default.Get();

                TM.Wallpaper.WallpaperType = Theme.Structures.Wallpaper.WallpaperTypes.Picture;
                TM.Wallpaper.ImageFile = textBox1.Text;

                TM.Windows12.ApplyAccentOnTaskbar = Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
                TM.Windows12.ApplyAccentOnTitlebars = true;
                TM.Windows12.WinMode_Light = Program.TM.Windows12.WinMode_Light;
                TM.Windows12.AppMode_Light = Program.TM.Windows12.AppMode_Light;

                TM.Windows11.ApplyAccentOnTaskbar = Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
                TM.Windows11.ApplyAccentOnTitlebars = true;
                TM.Windows11.WinMode_Light = Program.TM.Windows11.WinMode_Light;
                TM.Windows11.AppMode_Light = Program.TM.Windows11.AppMode_Light;

                TM.Windows10.ApplyAccentOnTaskbar = Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
                TM.Windows10.ApplyAccentOnTitlebars = true;
                TM.Windows10.WinMode_Light = Program.TM.Windows10.WinMode_Light;
                TM.Windows10.AppMode_Light = Program.TM.Windows10.AppMode_Light;

                TM.LogonUI12.ImageFile = textBox2.Text;
                TM.LogonUI11.ImageFile = textBox2.Text;
                TM.LogonUI10.ImageFile = textBox2.Text;

                TM.LogonUI7.Enabled = true;
                TM.LogonUI7.Mode = Theme.Structures.LogonUI7.Sources.CustomImage;
                TM.LogonUI7.ImagePath = textBox2.Text;

                TM.CommandPrompt.Enabled = true;
                TM.PowerShellx86.Enabled = true;
                TM.PowerShellx64.Enabled = true;

                TM.Cursors.Enabled = true;
                TM.Cursors.Cursor_Arrow.UseFromFile = false;
                TM.Cursors.Cursor_Help.UseFromFile = false;
                TM.Cursors.Cursor_AppLoading.UseFromFile = false;
                TM.Cursors.Cursor_Busy.UseFromFile = false;
                TM.Cursors.Cursor_Move.UseFromFile = false;
                TM.Cursors.Cursor_NS.UseFromFile = false;
                TM.Cursors.Cursor_EW.UseFromFile = false;
                TM.Cursors.Cursor_NESW.UseFromFile = false;
                TM.Cursors.Cursor_NWSE.UseFromFile = false;
                TM.Cursors.Cursor_Up.UseFromFile = false;
                TM.Cursors.Cursor_Pen.UseFromFile = false;
                TM.Cursors.Cursor_None.UseFromFile = false;
                TM.Cursors.Cursor_Link.UseFromFile = false;
                TM.Cursors.Cursor_Pin.UseFromFile = false;
                TM.Cursors.Cursor_Person.UseFromFile = false;
                TM.Cursors.Cursor_IBeam.UseFromFile = false;
                TM.Cursors.Cursor_Cross.UseFromFile = false;

                foreach (PropertyInfo prop in typeof(Theme.Structures.Windows10x).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
                {
                    prop.SetValue(TM.Windows12, ((Color)prop.GetValue(TM.Windows12)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Windows11, ((Color)prop.GetValue(TM.Windows11)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Windows10, ((Color)prop.GetValue(TM.Windows10)).GetNearestColorFromPalette(Palette));
                }

                foreach (PropertyInfo prop in typeof(Theme.Structures.Windows81).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
                {
                    prop.SetValue(TM.Windows81, ((Color)prop.GetValue(TM.Windows81)).GetNearestColorFromPalette(Palette));
                }

                foreach (PropertyInfo prop in typeof(Theme.Structures.Windows7).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
                {
                    prop.SetValue(TM.Windows7, ((Color)prop.GetValue(TM.Windows7)).GetNearestColorFromPalette(Palette));
                }

                foreach (PropertyInfo prop in typeof(Theme.Structures.WindowsVista).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
                {
                    prop.SetValue(TM.WindowsVista, ((Color)prop.GetValue(TM.WindowsVista)).GetNearestColorFromPalette(Palette));
                }

                foreach (PropertyInfo prop in typeof(Theme.Structures.Win32UI).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
                {
                    prop.SetValue(TM.Win32, ((Color)prop.GetValue(TM.Win32)).GetNearestColorFromPalette(Palette));
                }

                foreach (PropertyInfo prop in typeof(Theme.Structures.Console).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
                {
                    prop.SetValue(TM.CommandPrompt, ((Color)prop.GetValue(TM.CommandPrompt)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.PowerShellx86, ((Color)prop.GetValue(TM.PowerShellx86)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.PowerShellx64, ((Color)prop.GetValue(TM.PowerShellx64)).GetNearestColorFromPalette(Palette));
                }

                foreach (PropertyInfo prop in typeof(Theme.Structures.Cursor).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
                {
                    prop.SetValue(TM.Cursors.Cursor_Arrow, ((Color)prop.GetValue(TM.Cursors.Cursor_Arrow)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_Help, ((Color)prop.GetValue(TM.Cursors.Cursor_Help)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_AppLoading, ((Color)prop.GetValue(TM.Cursors.Cursor_AppLoading)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_Busy, ((Color)prop.GetValue(TM.Cursors.Cursor_Busy)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_Move, ((Color)prop.GetValue(TM.Cursors.Cursor_Move)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_NS, ((Color)prop.GetValue(TM.Cursors.Cursor_NS)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_EW, ((Color)prop.GetValue(TM.Cursors.Cursor_EW)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_NESW, ((Color)prop.GetValue(TM.Cursors.Cursor_NESW)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_NWSE, ((Color)prop.GetValue(TM.Cursors.Cursor_NWSE)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_Up, ((Color)prop.GetValue(TM.Cursors.Cursor_Up)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_Pen, ((Color)prop.GetValue(TM.Cursors.Cursor_Pen)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_None, ((Color)prop.GetValue(TM.Cursors.Cursor_None)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_Link, ((Color)prop.GetValue(TM.Cursors.Cursor_Link)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_Pin, ((Color)prop.GetValue(TM.Cursors.Cursor_Pin)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_Person, ((Color)prop.GetValue(TM.Cursors.Cursor_Person)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_IBeam, ((Color)prop.GetValue(TM.Cursors.Cursor_IBeam)).GetNearestColorFromPalette(Palette));
                    prop.SetValue(TM.Cursors.Cursor_Cross, ((Color)prop.GetValue(TM.Cursors.Cursor_Cross)).GetNearestColorFromPalette(Palette));
                }

                Adjust_Preview(TM);
                LoadCursorsFromTM(TM);
                ApplyCMDPreview(CMD1, TM.CommandPrompt, false);
                ApplyCMDPreview(CMD2, TM.PowerShellx86, true);
                ApplyCMDPreview(CMD3, TM.PowerShellx64, true);
            }

            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = tablessControl1.SelectedIndex + 1 > tablessControl1.TabCount - 1 ? tablessControl1.TabCount - 1 : tablessControl1.SelectedIndex + 1;
            Program.Animator.ShowSync(tablessControl1);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            next_btn.Enabled = true;
            next_btn.Text = tablessControl1.SelectedIndex >= tablessControl1.TabCount - 2 ? Program.Lang.Strings.General.Finish : Program.Lang.Strings.General.Next;

            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = tablessControl1.SelectedIndex - 1 < 0 ? 0 : tablessControl1.SelectedIndex - 1;
            Program.Animator.ShowSync(tablessControl1);
        }

        private void tablessControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            back_btn.Enabled = tablessControl1.SelectedIndex > 0;
            next_btn.Text = tablessControl1.SelectedIndex >= tablessControl1.TabCount - 2 ? Program.Lang.Strings.General.Finish : Program.Lang.Strings.General.Next;

            progressBar1.Value = tablessControl1.SelectedIndex * 100;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void Chromify_FormClosed(object sender, FormClosedEventArgs e)
        {
            Forms.GlassWindow.Close();
        }

        #region Preview Methods

        /// <summary>
        /// Adjusts the preview of the store form with the specified theme manager.
        /// </summary>
        /// <param name="TM"></param>
        public void Adjust_Preview(Manager TM)
        {
            windowsDesktop1.WindowStyle = Program.WindowStyle;
            windowsDesktop1.BackgroundImage = Program.FetchSuitableWallpaper(TM, Program.WindowStyle);
            windowsDesktop1.LoadFromTM(TM);
            windowsDesktop1.LoadClassicColors(TM.Win32);
            retroDesktopColors1.LoadColors(TM);
            retroDesktopColors1.LoadMetrics(TM);
        }

        /// <summary>
        /// Apply Command Prompt preview with the specified console settings.
        /// </summary>
        /// <param name="CMD"></param>
        /// <param name="Console"></param>
        /// <param name="PS"></param>
        public void ApplyCMDPreview(WinCMD CMD, Theme.Structures.Console Console, bool PS)
        {
            CMD.CMD_ColorTable00 = Console.ColorTable00;
            CMD.CMD_ColorTable01 = Console.ColorTable01;
            CMD.CMD_ColorTable02 = Console.ColorTable02;
            CMD.CMD_ColorTable03 = Console.ColorTable03;
            CMD.CMD_ColorTable04 = Console.ColorTable04;
            CMD.CMD_ColorTable05 = Console.ColorTable05;
            CMD.CMD_ColorTable06 = Console.ColorTable06;
            CMD.CMD_ColorTable07 = Console.ColorTable07;
            CMD.CMD_ColorTable08 = Console.ColorTable08;
            CMD.CMD_ColorTable09 = Console.ColorTable09;
            CMD.CMD_ColorTable10 = Console.ColorTable10;
            CMD.CMD_ColorTable11 = Console.ColorTable11;
            CMD.CMD_ColorTable12 = Console.ColorTable12;
            CMD.CMD_ColorTable13 = Console.ColorTable13;
            CMD.CMD_ColorTable14 = Console.ColorTable14;
            CMD.CMD_ColorTable15 = Console.ColorTable15;
            CMD.CMD_PopupForeground = Console.PopupForeground;
            CMD.CMD_PopupBackground = Console.PopupBackground;
            CMD.CMD_ScreenColorsForeground = Console.ScreenColorsForeground;
            CMD.CMD_ScreenColorsBackground = Console.ScreenColorsBackground;

            if (!Console.FontRaster)
            {
                GDI32.LogFont logFont = new()
                {
                    lfFaceName = Console.FaceName,
                    lfHeight = -Console.PixelHeight,
                    lfWidth = Console.PixelWidth,
                    lfWeight = Console.FontWeight
                };

                try
                {
                    CMD.Font = System.Drawing.Font.FromLogFont(logFont);
                }
                catch
                {

                }
            }

            CMD.PowerShell = PS;
            CMD.Raster = Console.FontRaster;

            string key = $"{Console.PixelWidth}x{Console.PixelHeight}";
            if (key == "4x6") CMD.RasterSize = WinCMD.Raster_Sizes._4x6;
            else if (key == "6x8") CMD.RasterSize = WinCMD.Raster_Sizes._6x8;
            else if (key == "6x9") CMD.RasterSize = WinCMD.Raster_Sizes._6x8;
            else if (key == "8x8") CMD.RasterSize = WinCMD.Raster_Sizes._8x8;
            else if (key == "8x9") CMD.RasterSize = WinCMD.Raster_Sizes._8x8;
            else if (key == "16x8") CMD.RasterSize = WinCMD.Raster_Sizes._16x8;
            else if (key == "5x12") CMD.RasterSize = WinCMD.Raster_Sizes._5x12;
            else if (key == "7x12") CMD.RasterSize = WinCMD.Raster_Sizes._7x12;
            else if (key == "8x12") CMD.RasterSize = WinCMD.Raster_Sizes._8x12;
            else if (key == "16x12") CMD.RasterSize = WinCMD.Raster_Sizes._16x12;
            else if (key == "12x16") CMD.RasterSize = WinCMD.Raster_Sizes._12x16;
            else if (key == "10x18") CMD.RasterSize = WinCMD.Raster_Sizes._10x18;
            else CMD.RasterSize = WinCMD.Raster_Sizes._8x12; // default

            CMD.Refresh();
        }

        /// <summary>
        /// Load cursors preview from the specified theme manager.
        /// </summary>
        /// <param name="TM"></param>
        public void LoadCursorsFromTM(Manager TM)
        {
            CursorTM_to_Cursor(Arrow, TM.Cursors.Cursor_Arrow);
            CursorTM_to_Cursor(Help, TM.Cursors.Cursor_Help);
            CursorTM_to_Cursor(AppLoading, TM.Cursors.Cursor_AppLoading);
            CursorTM_to_Cursor(Busy, TM.Cursors.Cursor_Busy);
            CursorTM_to_Cursor(Move_Cur, TM.Cursors.Cursor_Move);
            CursorTM_to_Cursor(NS, TM.Cursors.Cursor_NS);
            CursorTM_to_Cursor(EW, TM.Cursors.Cursor_EW);
            CursorTM_to_Cursor(NESW, TM.Cursors.Cursor_NESW);
            CursorTM_to_Cursor(NWSE, TM.Cursors.Cursor_NWSE);
            CursorTM_to_Cursor(Up, TM.Cursors.Cursor_Up);
            CursorTM_to_Cursor(Pen, TM.Cursors.Cursor_Pen);
            CursorTM_to_Cursor(None, TM.Cursors.Cursor_None);
            CursorTM_to_Cursor(Link, TM.Cursors.Cursor_Link);
            CursorTM_to_Cursor(Pin, TM.Cursors.Cursor_Pin);
            CursorTM_to_Cursor(Person, TM.Cursors.Cursor_Person);
            CursorTM_to_Cursor(IBeam, TM.Cursors.Cursor_IBeam);
            CursorTM_to_Cursor(Cross, TM.Cursors.Cursor_Cross);

            foreach (CursorControl i in Cursors_Container.Controls)
            {
                i.Prop_Scale = TM.Cursors.Size / 32f;
                i.Width = (int)(32f * i.Prop_Scale + 32f);
                i.Height = i.Width;
                i.Refresh();
            }
        }

        /// <summary>
        /// Converts the specified theme manager cursor to the specified cursor control.
        /// </summary>
        /// <param name="CursorControl"></param>
        /// <param name="Cursor"></param>
        public void CursorTM_to_Cursor(CursorControl CursorControl, Theme.Structures.Cursor Cursor)
        {
            CursorControl.Prop_ArrowStyle = Cursor.ArrowStyle;
            CursorControl.Prop_CircleStyle = Cursor.CircleStyle;
            CursorControl.Prop_PrimaryColor1 = Cursor.PrimaryColor1;
            CursorControl.Prop_PrimaryColor2 = Cursor.PrimaryColor2;
            CursorControl.Prop_PrimaryColorGradient = Cursor.PrimaryColorGradient;
            CursorControl.Prop_PrimaryColorGradientMode = Cursor.PrimaryColorGradientMode;
            CursorControl.Prop_PrimaryNoise = Cursor.PrimaryColorNoise;
            CursorControl.Prop_PrimaryNoiseOpacity = Cursor.PrimaryColorNoiseOpacity;
            CursorControl.Prop_SecondaryColor1 = Cursor.SecondaryColor1;
            CursorControl.Prop_SecondaryColor2 = Cursor.SecondaryColor2;
            CursorControl.Prop_SecondaryColorGradient = Cursor.SecondaryColorGradient;
            CursorControl.Prop_SecondaryColorGradientMode = Cursor.SecondaryColorGradientMode;
            CursorControl.Prop_SecondaryNoise = Cursor.SecondaryColorNoise;
            CursorControl.Prop_SecondaryNoiseOpacity = Cursor.SecondaryColorNoiseOpacity;
            CursorControl.Prop_LoadingCircleBack1 = Cursor.LoadingCircleBack1;
            CursorControl.Prop_LoadingCircleBack2 = Cursor.LoadingCircleBack2;
            CursorControl.Prop_LoadingCircleBackGradient = Cursor.LoadingCircleBackGradient;
            CursorControl.Prop_LoadingCircleBackGradientMode = Cursor.LoadingCircleBackGradientMode;
            CursorControl.Prop_LoadingCircleBackNoise = Cursor.LoadingCircleBackNoise;
            CursorControl.Prop_LoadingCircleBackNoiseOpacity = Cursor.LoadingCircleBackNoiseOpacity;
            CursorControl.Prop_LoadingCircleHot1 = Cursor.LoadingCircleHot1;
            CursorControl.Prop_LoadingCircleHot2 = Cursor.LoadingCircleHot2;
            CursorControl.Prop_LoadingCircleHotGradient = Cursor.LoadingCircleHotGradient;
            CursorControl.Prop_LoadingCircleHotGradientMode = Cursor.LoadingCircleHotGradientMode;
            CursorControl.Prop_LoadingCircleHotNoise = Cursor.LoadingCircleHotNoise;
            CursorControl.Prop_LoadingCircleHotNoiseOpacity = Cursor.LoadingCircleHotNoiseOpacity;
            CursorControl.Prop_Shadow_Enabled = Cursor.Shadow_Enabled;
            CursorControl.Prop_Shadow_Color = Cursor.Shadow_Color;
            CursorControl.Prop_Shadow_Blur = Cursor.Shadow_Blur;
            CursorControl.Prop_Shadow_Opacity = Cursor.Shadow_Opacity;
            CursorControl.Prop_Shadow_OffsetX = Cursor.Shadow_OffsetX;
            CursorControl.Prop_Shadow_OffsetY = Cursor.Shadow_OffsetY;
        }

        #endregion

        private void button10_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, FileName = textBox2.Text, Title = Program.Lang.Strings.Extensions.OpenImages })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = dlg.FileName;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Program.WindowStyle == WindowStyle.WXP)
            {
                textBox1.Text = $@"{SysPaths.Windows}\Web\Wallpaper\Bliss.bmp";
            }
            else
            {
                textBox1.Text = $@"{SysPaths.Windows}\Web\Wallpaper\Windows\img0.jpg";
            }
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = ReadReg("HKEY_CURRENT_USER\\Control Panel\\Desktop", "Wallpaper", string.Empty);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, FileName = textBox2.Text, Title = Program.Lang.Strings.Extensions.OpenImages })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = dlg.FileName;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Program.WindowStyle == WindowStyle.WXP)
            {
                textBox2.Text = $@"{SysPaths.Windows}\Web\Wallpaper\Bliss.bmp";
            }
            else
            {
                textBox2.Text = $@"{SysPaths.Windows}\Web\Wallpaper\Windows\img0.jpg";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string defaultLockScreen = ReadReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\Personalization", "LockScreenImage", $"{SysPaths.Windows}\\Web\\Screen\\img100.jpg");
            textBox2.Text = defaultLockScreen;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = ReadReg("HKEY_CURRENT_USER\\Control Panel\\Desktop", "Wallpaper", string.Empty);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (Control c in ImgPaletteContainer.Controls)
            {
                c?.Dispose();
            }
            ImgPaletteContainer.Controls.Clear();

            using (Bitmap bmp_src = BitmapMgr.Load(textBox1.Text))
            using (Bitmap bmp = bmp_src.Resize(Program.PreviewSize))
            {
                Palette?.Clear();
                Palette = bmp.ToPalette(300, 10, false, 240, true, true);

                List<Color> strongColors = [.. Palette
                    .OrderByDescending(c => c.GetSaturation() * (1f - Math.Abs(c.GetBrightness() - 0.5f)))
                    .Take(10)];

                List<Color> variations = [];
                foreach (Color c in strongColors)
                {
                    variations.Add(c.CB(0.8f));
                    variations.Add(c.CB(-0.8f));
                }
                Palette.AddRange(variations);

                //// Add basic colors if not exist
                //if (!Palette.Contains(Color.White)) Palette.Add(Color.White);
                //if (!Palette.Contains(Color.Black)) Palette.Add(Color.Black);

                Palette = [.. Palette.AsParallel().OrderBy(c => c, new RGBColorComparer())];

                List<ColorItem> colorItems = [];
                colorItems?.Clear();

                foreach (Color c in Palette)
                {
                    ColorItem colorItem = new()
                    {
                        Size = ColorItem.GetMiniColorItemSize(),
                        AllowDrop = false,
                        PauseColorsHistory = true,
                        BackColor = Color.FromArgb(255, c),
                        DefaultBackColor = Color.FromArgb(255, c)
                    };

                    colorItems.Add(colorItem);
                }

                ImgPaletteContainer.Controls.AddRange([.. colorItems]);
            }
        }

        private void cur_anim_btn_Click(object sender, EventArgs e)
        {
            Angle = 180f;
            Cycles = 0;
            Cursor_Timer.Enabled = true;
            Cursor_Timer.Start();
        }

        private void Cursor_Timer_Tick(object sender, EventArgs e)
        {
            foreach (CursorControl i in AnimateList)
            {
                Increment = i.Prop_LoadingCircleHot_AnimationSpeed / 2f;

                i.Angle = Angle;
                i.Refresh();

                if (Angle + Increment >= 360f)
                    Angle = 0f;
                Angle += Increment;

                if (Angle == 180f & Cycles >= numericUpDown1.Value - 1)
                {
                    i.Angle = 180f;
                    Cursor_Timer.Enabled = false;
                    Cursor_Timer.Stop();
                }
                else if (Angle == 180f)
                {
                    Cycles += 1;
                }
            }
        }
    }
}
