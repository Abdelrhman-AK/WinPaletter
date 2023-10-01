using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.CP;

namespace WinPaletter
{

    public class Core
    {
        public static void RefreshDWM(CP CP)
        {

            try
            {
                var Com = default(bool);
                Dwmapi.DwmIsCompositionEnabled(ref Com);

                if (Com)
                {
                    var temp = new Dwmapi.DWM_COLORIZATION_PARAMS();

                    if (My.Env.W8 | My.Env.W81)
                    {
                        temp.clrColor = CP.Windows81.ColorizationColor.ToArgb();
                        temp.nIntensity = CP.Windows81.ColorizationColorBalance;
                    }

                    else if (My.Env.W7)
                    {
                        temp.clrColor = CP.Windows7.ColorizationColor.ToArgb();
                        temp.nIntensity = CP.Windows7.ColorizationColorBalance;

                        temp.clrAfterGlow = CP.Windows7.ColorizationAfterglow.ToArgb();
                        temp.clrAfterGlowBalance = CP.Windows7.ColorizationAfterglowBalance;
                        temp.clrBlurBalance = CP.Windows7.ColorizationBlurBalance;
                        temp.clrGlassReflectionIntensity = CP.Windows7.ColorizationGlassReflectionIntensity;
                        temp.fOpaque = CP.Windows7.Theme == Structures.Windows7.Themes.AeroOpaque;
                    }

                    else if (My.Env.WVista)
                    {
                        temp.clrColor = Color.FromArgb(CP.WindowsVista.Alpha, CP.WindowsVista.ColorizationColor).ToArgb();
                        temp.clrAfterGlow = Color.FromArgb(CP.WindowsVista.Alpha, CP.WindowsVista.ColorizationColor).ToArgb();

                        // temp.nIntensity = CP.WindowsVista.ColorizationColorBalance
                        // temp.clrAfterGlowBalance = CP.WindowsVista.ColorizationAfterglowBalance
                        // temp.clrBlurBalance = CP.WindowsVista.ColorizationBlurBalance
                        // temp.clrGlassReflectionIntensity = CP.WindowsVista.ColorizationGlassReflectionIntensity
                        temp.fOpaque = CP.WindowsVista.Theme == Structures.Windows7.Themes.AeroOpaque;
                    }

                    Dwmapi.DwmSetColorizationParameters(ref temp, false);
                }
            }
            catch
            {
            }
        }
        public static void RestartExplorer(TreeView TreeView = null)
        {
            {
                var temp = My.MyProject.Application;

                try
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.KillingExplorer), "info");
                    var sw = new Stopwatch();
                    sw.Reset();
                    sw.Start();
                    temp.processKiller.Start();
                    temp.processKiller.WaitForExit();
                    temp.processExplorer.Start();
                    sw.Stop();
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), string.Format(My.Env.Lang.ExplorerRestarted, sw.ElapsedMilliseconds / 1000d)), "time");
                    sw.Reset();
                }
                catch (Exception ex)
                {
                    if (TreeView is not null)
                    {
                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.ErrorExplorerRestart), "error");
                        My.Env.Saving_Exceptions.Add(new Tuple<string, Exception>(My.Env.Lang.ErrorExplorerRestart, ex));
                    }
                }

            }
        }

        /// <summary>
        /// Indicates whether any network connection is available
        /// </summary>
        /// <returns>
        ///    <c>true</c> if a network connection is available; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNetworkAvailable()
        {
            return Wininet.CheckNet();
        }
        public static bool Ping(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 60000;
                request.AllowAutoRedirect = false;
                request.Method = "HEAD";

                using (var response = request.GetResponse())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static double GetWindowsScreenScalingFactor(bool percentage = true)
        {
            var GraphicsObject = Graphics.FromHwnd(IntPtr.Zero);
            var DeviceContextHandle = GraphicsObject.GetHdc();
            int LogicalScreenHeight = GDI32.GetDeviceCaps(DeviceContextHandle, (int)GDI32.DeviceCap.VERTRES);
            int PhysicalScreenHeight = GDI32.GetDeviceCaps(DeviceContextHandle, (int)GDI32.DeviceCap.DESKTOPVERTRES);
            double ScreenScalingFactor = Math.Round(PhysicalScreenHeight / (double)LogicalScreenHeight, 2);

            if (percentage)
                ScreenScalingFactor *= 100.0d;

            GraphicsObject.ReleaseHdc(DeviceContextHandle);
            GraphicsObject.Dispose();
            return ScreenScalingFactor;
        }

        public enum TaskType
        {
            Shutdown,
            Logoff,
            Logon,
            Unlock,
            ChargerConnected
        }
        public enum Actions
        {
            Add,
            Delete
        }
        public static void TaskMgmt(TaskType TaskType, Actions Action, string File = "", TreeView TreeView = null)
        {

            DeleteTask(TaskType);

            if (Action == Actions.Add)
            {

                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = My.Env.PATH_System32 + @"\schtasks",
                        Verb = My.Env.WXP && My.Env.isElevated ? "" : "runas",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        UseShellExecute = true
                    }
                };

                string tmp = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), ".xml");
                if (System.IO.File.Exists(tmp))
                    FileSystem.Kill(tmp);

                switch (TaskType)
                {
                    case TaskType.Shutdown:
                        {
                            string XML_Scheme = string.Format(My.Resources.XML_Shutdown, File);
                            System.IO.File.WriteAllText(tmp, XML_Scheme);
                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_CreateTask, @"WinPaletter\Shutdown"), "task_add");
                            process.StartInfo.Arguments = @"/Create /TN WinPaletter\Shutdown /XML """ + tmp + "\"";
                            break;
                        }

                    case TaskType.Logoff:
                        {
                            string XML_Scheme = string.Format(My.Resources.XML_Logoff, File);
                            System.IO.File.WriteAllText(tmp, XML_Scheme);
                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_CreateTask, @"WinPaletter\Logoff"), "task_add");
                            process.StartInfo.Arguments = @"/Create /TN WinPaletter\Logoff /XML """ + tmp + "\"";
                            break;
                        }

                    case TaskType.Logon:
                        {
                            string XML_Scheme = string.Format(My.Resources.XML_Logon, File);
                            System.IO.File.WriteAllText(tmp, XML_Scheme);
                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_CreateTask, @"WinPaletter\Logon"), "task_add");
                            process.StartInfo.Arguments = @"/Create /TN WinPaletter\Logon /XML """ + tmp + "\"";
                            break;
                        }

                    case TaskType.Unlock:
                        {
                            string XML_Scheme = string.Format(My.Resources.XML_Unlock, File);
                            System.IO.File.WriteAllText(tmp, XML_Scheme);
                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_CreateTask, @"WinPaletter\Unlock"), "task_add");
                            process.StartInfo.Arguments = @"/Create /TN WinPaletter\Unlock /XML """ + tmp + "\"";
                            break;
                        }

                    case TaskType.ChargerConnected:
                        {
                            string XML_Scheme = string.Format(My.Resources.XML_ChargerConnected, File);
                            System.IO.File.WriteAllText(tmp, XML_Scheme);
                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_CreateTask, @"WinPaletter\ChargerConnected"), "task_add");
                            process.StartInfo.Arguments = @"/Create /TN WinPaletter\ChargerConnected /XML """ + tmp + "\"";
                            break;
                        }

                }

                process.Start();
                process.WaitForExit();

                if (System.IO.File.Exists(tmp))
                    FileSystem.Kill(tmp);
            }
        }
        private static void DeleteTask(TaskType TaskType, TreeView TreeView = null)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = My.Env.PATH_System32 + @"\schtasks",
                    Verb = My.Env.WXP && My.Env.isElevated ? "" : "runas",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = true
                }
            };

            switch (TaskType)
            {
                case TaskType.Shutdown:
                    {
                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_DeleteTask, @"WinPaletter\Shutdown"), "task_remove");
                        process.StartInfo.Arguments = @"/Delete /TN WinPaletter\Shutdown /F";
                        break;
                    }

                case TaskType.Logoff:
                    {
                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_DeleteTask, @"WinPaletter\Logoff"), "task_remove");
                        process.StartInfo.Arguments = @"/Delete /TN WinPaletter\Logoff /F";
                        break;
                    }

                case TaskType.Logon:
                    {
                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_DeleteTask, @"WinPaletter\Logon"), "task_remove");
                        process.StartInfo.Arguments = @"/Delete /TN WinPaletter\Logon /F";
                        break;
                    }

                case TaskType.Unlock:
                    {
                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_DeleteTask, @"WinPaletter\Unlock"), "task_remove");
                        process.StartInfo.Arguments = @"/Delete /TN WinPaletter\Unlock /F";
                        break;
                    }

                case TaskType.ChargerConnected:
                    {
                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_DeleteTask, @"WinPaletter\ChargerConnected"), "task_remove");
                        process.StartInfo.Arguments = @"/Delete /TN WinPaletter\ChargerConnected /F";
                        break;
                    }

            }

            process.Start();
            process.WaitForExit();
        }
    }

    /// <summary>
    /// Functions that help you draw\drop special DWM effects (Tabbed\Mica\Acrylic\Aero) on a form
    /// </summary>
    public static class FormDWMEffects
    {

        /// <summary>
        /// Draw effect on form depending on both user choice (Tabbed\Mica\Acrylic\Aero) and current OS
        /// </summary>
        public static void DrawDWMEffect(this Form Form, Padding Margins, bool Border = true, FormStyle FormStyle = FormStyle.Mica)
        {

            if (Margins == default)
                Margins = new Padding(-1, -1, -1, -1);

            var CompositionEnabled = default(bool);
            try
            {
                Dwmapi.DwmIsCompositionEnabled(ref CompositionEnabled);
            }
            catch
            {
                CompositionEnabled = false;
            }

            bool Transparency_W11_10;
            Transparency_W11_10 = (My.Env.W10 | My.Env.W11) && (bool)Reg_IO.GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", true);

            try
            {
                if (My.Env.W11 && Transparency_W11_10)
                {
                    if (FormStyle == FormStyle.Mica)
                    {
                        Form.DrawMica(Margins, MicaStyle.Mica);
                    }

                    else if (FormStyle == FormStyle.Tabbed)
                    {
                        Form.DrawMica(Margins, MicaStyle.Tabbed);
                    }

                    else if (FormStyle == FormStyle.Acrylic)
                    {
                        Form.DrawAcrylic(Border);
                    }

                    else
                    {
                        Form.DrawMica(Margins, MicaStyle.Mica);

                    }
                }

                else if (My.Env.W10 && Transparency_W11_10)
                {
                    Form.DrawAcrylic(Border);
                }

                else if (My.Env.W7 | My.Env.WVista && CompositionEnabled)
                {
                    Form.DrawAero(Margins);
                }

                else
                {
                    Form.DrawTransparentGray();

                }
            }
            catch
            {
                Form.DrawTransparentGray();

            }

        }

        public static void DrawAcrylic(this Form Form, bool Border = true)
        {
            var accent = new User32.AccentPolicy() { AccentState = User32.AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND };
            if (Border)
                accent.AccentFlags = 0x20 | 0x40 | 0x80 | 0x100;
            int accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var Data = new User32.WindowCompositionAttributeData()
            {
                Attribute = User32.WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };
            User32.SetWindowCompositionAttribute(Form.Handle, ref Data);
            Marshal.FreeHGlobal(accentPtr);
        }

        /// <summary>
        /// Draws Mica Style (Windows 11 and Higher - Tabbed Style is for Windows 11 Build 22523 and Higher, if not, Mica will be used instead)
        /// </summary>
        public static void DrawMica(this Form Form, Padding Margins, MicaStyle Style = MicaStyle.Mica)
        {
            var CompositionEnabled = default(bool);
            try
            {
                Dwmapi.DwmIsCompositionEnabled(ref CompositionEnabled);
            }
            catch
            {
                CompositionEnabled = false;
            }

            var FS = new FormStyle();
            if (Style == MicaStyle.Mica)
                FS = FormStyle.Mica;
            if (Style == MicaStyle.Tabbed & My.Env.W11_22523)
                FS = FormStyle.Tabbed;
            else
                FS = FormStyle.Mica;

            if (Margins == default)
                Margins = new Padding(-1, -1, -1, -1);
            var DWM_Margins = new Dwmapi.MARGINS() { leftWidth = Margins.Left, rightWidth = Margins.Right, topHeight = Margins.Top, bottomHeight = Margins.Bottom };

            DLLFunc.DarkTitlebar(Form.Handle, My.Env.Style.DarkMode);
            int argpvAttribute = (int)FS;
            Dwmapi.DwmSetWindowAttribute(Form.Handle, Dwmapi.DWMATTRIB.SYSTEMBACKDROP_TYPE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));
            Dwmapi.DwmExtendFrameIntoClientArea(Form.Handle, ref DWM_Margins);
        }

        public static void DrawAero(this Form Form, Padding Margins)
        {
            if (Margins == default)
                Margins = new Padding(-1, -1, -1, -1);
            var DWM_Margins = new Dwmapi.MARGINS() { leftWidth = Margins.Left, rightWidth = Margins.Right, topHeight = Margins.Top, bottomHeight = Margins.Bottom };
            Dwmapi.DwmExtendFrameIntoClientArea(Form.Handle, ref DWM_Margins);
        }

        public static void DrawTransparentGray(this Form Form, bool NoWindowBorders = true)
        {
            Form.BackColor = Color.FromArgb(5, 5, 5);
            Form.Opacity = 0.5d;
            if (NoWindowBorders)
                Form.FormBorderStyle = FormBorderStyle.None;
        }

        /// <summary>
        /// Sets Titlebar Backcolor, Forecolor and border color (Only for Windows 11 and Higher)
        /// </summary>
        public static void DrawCustomTitlebar(this Form Form, Color BackColor = default, Color BorderColor = default, Color ForeColor = default)
        {

            if (My.Env.W11)
            {
                try
                {
                    if (BackColor != default)
                    {
                        int argpvAttribute = ColorTranslator.ToWin32(BackColor);
                        Dwmapi.DwmSetWindowAttribute(Form.Handle, Dwmapi.DWMATTRIB.CAPTION_COLOR, ref argpvAttribute, Marshal.SizeOf(typeof(int)));
                    }
                }
                catch
                {
                }

                try
                {
                    if (ForeColor != default)
                    {
                        int argpvAttribute1 = ColorTranslator.ToWin32(ForeColor);
                        Dwmapi.DwmSetWindowAttribute(Form.Handle, Dwmapi.DWMATTRIB.TEXT_COLOR, ref argpvAttribute1, Marshal.SizeOf(typeof(int)));
                    }
                }
                catch
                {
                }

                try
                {
                    if (BorderColor != default)
                    {
                        int argpvAttribute2 = ColorTranslator.ToWin32(BorderColor);
                        Dwmapi.DwmSetWindowAttribute(Form.Handle, Dwmapi.DWMATTRIB.BORDER_COLOR, ref argpvAttribute2, Marshal.SizeOf(typeof(int)));
                    }
                }
                catch
                {
                }

            }

        }

        public enum FormStyle
        {
            Auto,
            Default,
            Mica,
            Acrylic,
            Tabbed
        }

        public enum MicaStyle
        {
            Mica,
            Tabbed
        }

    }

    public class Visual
    {
        // ' FILENAME:        Visual.vb
        // ' NAMESPACE:       PI.Common
        // ' CREATED BY:      Luke Berg
        // ' CREATED:         10-02-06
        // ' DESCRIPTION:     A common module of visual functions.

        private static readonly Dictionary<string, BackgroundWorker> colorsFading = new Dictionary<string, BackgroundWorker>(); // Keeps track of any backgroundworkers already fading colors
        private static readonly Dictionary<BackgroundWorker, ColorFaderInformation> backgroundWorkers = new Dictionary<BackgroundWorker, ColorFaderInformation>(); // Associate each background worker with information it needs
        private static bool PreviousColorItemPauseColorsHistory;

        // The delegate of a method that will be called when the color finishes fading
        public delegate void DoneFading(object container, string colorProperty);

        /// <summary>
        /// Fades a color property from one color to another
        /// </summary>
        /// <param name="container">The object that contains the color property</param>
        /// <param name="colorProperty">The name of the color property to change</param>
        /// <param name="startColor">The color to start the fade with</param>
        /// <param name="endColor">The color to end the fade with</param>
        /// <param name="steps">The number of steps to take to fade from the start color to the end color</param>
        /// <param name="delay">The delay in milliseconds between each step in the fade.</param>
        /// <param name="callback">A function to be called when the fade completes</param>
        /// <remarks></remarks>
        public static void FadeColor(object container, string colorProperty, Color startColor, Color endColor, int steps, int delay, DoneFading callback = null)
        {
            var colorSteps = new ColorStep[1];
            colorSteps[0] = new ColorStep(endColor, steps);
            try
            {
                FadeColor(container, colorProperty, startColor, colorSteps, delay, callback);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Fades a color property from one color to another, and then to yet another
        /// </summary>
        /// <param name="container">The object that contains the color property</param>
        /// <param name="colorProperty">The name of the color property to change</param>
        /// <param name="startColor">The color to start the fade with</param>
        /// <param name="middleColor">The color to fade to first</param>
        /// <param name="middleSteps">The number of steps to take in fading to the first color</param>
        /// <param name="endcolor">The last color to fade to</param>
        /// <param name="endSteps">The number of steps to take in fading to the last color</param>
        /// <param name="delay">The delay between each step in the fade</param>
        /// <param name="callback">A function that will be called after the fading has completed</param>
        /// <remarks></remarks>
        public static void FadeColor(object container, string colorProperty, Color startColor, Color middleColor, int middleSteps, Color endcolor, int endSteps, int delay, DoneFading callback = null)
        {
            var colorSteps = new ColorStep[2];
            colorSteps[0] = new ColorStep(middleColor, middleSteps);
            colorSteps[1] = new ColorStep(endcolor, endSteps);
            try
            {
                FadeColor(container, colorProperty, startColor, colorSteps, delay, callback);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Fades a color property to various colors
        /// </summary>
        /// <param name="container">The object that contains the color property</param>
        /// <param name="colorProperty">The name of the color property to change</param>
        /// <param name="startColor">The color to start the fade with</param>
        /// <param name="colorSteps">A list of steps in fading the color - an enumerable list of colors and the steps to get to that color</param>
        /// <param name="delay">The delay between each step in fading the color</param>
        /// <param name="callBack">A method to call when the fading has completed</param>
        /// <remarks></remarks>

        public static void FadeColor(object container, string colorProperty, Color startColor, IEnumerable<ColorStep> colorSteps, int delay, DoneFading callBack = null)
        {

            var colorFader = new BackgroundWorker();

            // Stores all the parameter information into a class that the background worker will access
            var colorFaderInfo = new ColorFaderInformation(container, colorProperty, startColor, colorSteps, delay, callBack);

            // Checks if the color is already in the process of fading.
            if (colorsFading.TryGetValue(GenerateHashCode(container, colorProperty), out colorFader))
            {
                // Cancels the backgroundWorkers process and sets a flag indicating that it should restart itself with
                // the new information.
                colorFader.CancelAsync();
                colorFaderInfo.Rerun = true;
                backgroundWorkers[colorFader] = colorFaderInfo;
            }
            else
            {

                // Creates a new backgroundWorker and adds handlers to all its events
                colorFader = new BackgroundWorker();
                colorFader.DoWork += BackgroundWorker_DoWork;
                colorFader.ProgressChanged += BackgroundWorker_ProgressChanged;
                colorFader.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
                colorFader.WorkerReportsProgress = true;
                colorFader.WorkerSupportsCancellation = true;
                if (container is UI.Controllers.ColorItem)
                {
                    PreviousColorItemPauseColorsHistory = ((UI.Controllers.ColorItem)container).PauseColorsHistory;
                    ((UI.Controllers.ColorItem)container).PauseColorsHistory = true;
                }

                backgroundWorkers.Add(colorFader, colorFaderInfo);
                colorsFading.Add(GenerateHashCode(container, colorProperty), colorFader);

            }

            // Starts the backgroundWorker beginning the fade
            if (!colorFader.IsBusy)
            {
                colorFader.RunWorkerAsync(colorFaderInfo);
            }
        }

        /// <summary>
        /// The work that the background worker does in fading the color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private static void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ColorFaderInformation info = (ColorFaderInformation)e.Argument;
            // These are calculated with each iteration (step) and used to set the color
            // when the background worker reports its progress.

            double curA;
            double curR;
            double curG;
            double curB;

            var startStepColor = info.StartColor;
            Color endStepColor;

            foreach (ColorStep colorStep in info.Colors)
            {

                endStepColor = colorStep.Color;

                // Gets the amount to change each color part per step

                double aStep = (endStepColor.A - (double)startStepColor.A) / colorStep.Steps;
                double rStep = (endStepColor.R - (double)startStepColor.R) / colorStep.Steps;
                double gStep = (endStepColor.G - (double)startStepColor.G) / colorStep.Steps;
                double bStep = (endStepColor.B - (double)startStepColor.B) / colorStep.Steps;

                // the red, green and blue parts of the current color
                curA = startStepColor.A;
                curR = startStepColor.R;
                curG = startStepColor.G;
                curB = startStepColor.B;

                // loop through, and fade
                for (int i = 1, loopTo = colorStep.Steps; i <= loopTo; i++)
                {
                    curA += aStep;
                    curR += rStep;
                    curB += bStep;
                    curG += gStep;

                    try
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, Color.FromArgb((int)Math.Round(curA), (int)Math.Round(curR), (int)Math.Round(curG), (int)Math.Round(curB)));
                    }
                    catch
                    {
                    }

                    System.Threading.Thread.Sleep(info.Delay);

                    if (((BackgroundWorker)sender).CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                }

                startStepColor = endStepColor;

            }

        }

        /// <summary>
        /// Calls to this method are marshalled back to the original thread, so here is where we actually change the color.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private static void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ColorFaderInformation info = null;
            if (backgroundWorkers.TryGetValue((BackgroundWorker)sender, out info))
            {
                Color currentColor = (Color)e.UserState;
                try
                {
                    Interaction.CallByName(info.Container, info.ColorProperty, CallType.Let, currentColor);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// This is raised when the background method completes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private static void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            ColorFaderInformation info = null;

            if (backgroundWorkers.TryGetValue((BackgroundWorker)sender, out info))
            {

                if (!e.Cancelled)
                {
                    info.CallBack?.Invoke(info.Container, info.ColorProperty);
                    backgroundWorkers.Remove((BackgroundWorker)sender);
                    colorsFading.Remove(GenerateHashCode(info.Container, info.ColorProperty));

                    if (info.Container is UI.Controllers.ColorItem)
                    {
                        {
                            var temp = (UI.Controllers.ColorItem)info.Container;
                            temp.PauseColorsHistory = PreviousColorItemPauseColorsHistory;
                            if (!PreviousColorItemPauseColorsHistory)
                                temp.ColorsHistory.Add(temp.BackColor);
                        }

                    } ((BackgroundWorker)sender).DoWork -= BackgroundWorker_DoWork;
                    ((BackgroundWorker)sender).ProgressChanged -= BackgroundWorker_ProgressChanged;
                    ((BackgroundWorker)sender).RunWorkerCompleted -= BackgroundWorker_RunWorkerCompleted;
                }

                else if (info.Rerun)
                {
                    info.Rerun = false;
                    ((BackgroundWorker)sender).RunWorkerAsync(info);

                }

            }

        }

        /// <summary>
        /// Generates a hashcode for an object and its color that are in the process of fading
        /// </summary>
        /// <param name="container">The object whose color property needs to be faded</param>
        /// <param name="colorProperty">The string name of the property to fade</param>
        /// <returns>A unique string representing the object and it's color property</returns>
        /// <remarks></remarks>
        private static string GenerateHashCode(object container, string colorProperty)
        {
            return container.GetHashCode() + colorProperty;
        }

        /// <summary>
        /// A simple class for storing information a backgroundWorker needs to perform the fading.
        /// </summary>
        /// <remarks></remarks>
        private class ColorFaderInformation
        {

            public DoneFading CallBack;
            public object Container;
            public string ColorProperty;
            public Color StartColor;
            public IEnumerable<ColorStep> Colors;
            public int Delay;
            public bool Rerun;

            public ColorFaderInformation(object container, string colorProperty, Color startColor, IEnumerable<ColorStep> colorSteps, int delay, DoneFading callBack = null)
            {
                Container = container;
                ColorProperty = colorProperty;
                StartColor = startColor;
                Colors = colorSteps;
                Delay = delay;
                CallBack = callBack;
                Rerun = false;
            }

        }

        /// <summary>
        /// A simple class needed to represent a single step in the fading process
        /// </summary>
        /// <remarks></remarks>
        public struct ColorStep
        {

            public Color Color;
            public int Steps;

            public ColorStep(Color color, int steps)
            {
                Color = color;
                Steps = steps;
            }

        }

    }

    public class RGBColorComparer : IComparer<Color>
    {
        public int Compare(Color a, Color b)
        {
            // Compare hue values
            int hueComparison = a.GetHue().CompareTo(b.GetHue());
            if (hueComparison != 0)
                return hueComparison;

            // Compare brightness values
            int brightnessComparison = a.GetBrightness().CompareTo(b.GetBrightness());
            if (brightnessComparison != 0)
                return brightnessComparison;

            // Compare saturation values
            int saturationComparison = a.GetSaturation().CompareTo(b.GetSaturation());
            return saturationComparison;
        }
    }
}