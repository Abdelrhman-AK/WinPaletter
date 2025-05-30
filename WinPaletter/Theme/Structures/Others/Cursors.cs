using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows cursors, either rendered by WinPaletter or loaded from files.
    /// </summary>
    public struct Cursors : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled = false;

        /// <summary>
        /// Enable using custom shadow for cursors rendered by WinPaletter (not Windows).
        /// </summary>
        public bool Shadow = false;

        /// <summary>
        /// Enable sonar (gray circle) surrounding cursor while pressing <c>CTRL</c> key multiple times.
        /// </summary>
        public bool Sonar = false;

        /// <summary>
        /// Amount of cursor trails.
        /// </summary>
        public int Trails = 0;

        /// <summary>
        /// Size of cursors
        /// </summary>
        public int Size = 32;

        /// <summary>
        /// Structure instance that has data can be modified to customize 'Arrow' cursor.
        /// </summary>
        public Cursor Cursor_Arrow = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_arrow.cur" };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'AppLoading' cursor.
        /// </summary>
        public Cursor Cursor_AppLoading = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_working.ani" };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'Busy' cursor.
        /// </summary>
        public Cursor Cursor_Busy = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_busy.ani" };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'Help' cursor.
        /// </summary>
        public Cursor Cursor_Help = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_helpsel.cur" };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'Move' cursor.
        /// </summary>
        public Cursor Cursor_Move = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_move.cur" };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'NorthSouth' cursor.
        /// </summary>
        public Cursor Cursor_NS = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_ns.cur" };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'EastWest' cursor.
        /// </summary>
        public Cursor Cursor_EW = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_ew.cur" };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'NorthEastSouthWest' cursor.
        /// </summary>
        public Cursor Cursor_NESW = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_nesw.cur" };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'NorthWestSouthEast' cursor.
        /// </summary>
        public Cursor Cursor_NWSE = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_nwse.cur" };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'Up' cursor.
        /// </summary>
        public Cursor Cursor_Up = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_up.cur" };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'Pen' cursor.
        /// </summary>
        public Cursor Cursor_Pen = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_pen.cur" };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'None' cursor.
        /// </summary>
        public Cursor Cursor_None = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_unavail.cur", SecondaryColor1 = Color.FromArgb(255, 0, 0), SecondaryColor2 = Color.FromArgb(255, 0, 0) };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'Link\Hand' cursor.
        /// </summary>
        public Cursor Cursor_Link = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_link.cur" };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'Pin' cursor.
        /// </summary>
        public Cursor Cursor_Pin = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_pin.cur" };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'Person' cursor
        /// .</summary>
        public Cursor Cursor_Person = new() { File = $"{SysPaths.Windows}\\Cursors\\aero_person.cur" };

        /// <summary>
        /// Structure instance that has data can be modified to customize 'IBeam' cursor.
        /// </summary>
        public Cursor Cursor_IBeam = new();

        /// <summary>
        /// Structure instance that has data can be modified to customize 'Cross' cursor.
        /// </summary>
        public Cursor Cursor_Cross = new();

        /// <summary>
        /// Creates new Cursors structure with default values
        /// </summary>
        public Cursors() { }

        /// <summary>
        /// Loads cursors data from registry
        /// </summary>
        /// <param name="default">Default Cursors data structure</param>
        public void Load(Cursors @default)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Windows cursors settings from registry and User32.SystemParametersInfo");

            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors", string.Empty, @default.Enabled));

            if (!SystemParametersInfo(SPI.SPI_GETCURSORSHADOW, 0, ref Shadow, SPIF.SPIF_NONE))
                Shadow = @default.Shadow;

            if (!SystemParametersInfo(SPI.SPI_GETMOUSETRAILS, 0, ref Trails, SPIF.SPIF_NONE))
                Trails = @default.Trails;

            if (!SystemParametersInfo(SPI.SPI_GETMOUSESONAR, 0, ref Sonar, SPIF.SPIF_NONE))
                Sonar = @default.Sonar;

            Size = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Cursors", "CursorBaseSize", @default.Size));

            Cursor_Arrow.Load("Arrow");
            Cursor_Help.Load("Help");
            Cursor_AppLoading.Load("AppLoading");
            Cursor_Busy.Load("Busy");
            Cursor_Move.Load("Move");
            Cursor_NS.Load("NS");
            Cursor_EW.Load("EW");
            Cursor_NESW.Load("NESW");
            Cursor_NWSE.Load("NWSE");
            Cursor_Up.Load("Up");
            Cursor_Pen.Load("Pen");
            Cursor_None.Load("None");
            Cursor_Link.Load("Link");
            Cursor_Pin.Load("Pin");
            Cursor_Person.Load("Person");
            Cursor_IBeam.Load("IBeam");
            Cursor_Cross.Load("Cross");
        }

        /// <summary>
        /// Saves Cursors data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Saving Windows cursors settings into registry, rendering custom cursors and by using User32.SystemParametersInfo");

            SaveToggleState(treeView);

            // Determine if we should report progress
            bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None && treeView is not null;
            bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed;
            TreeView tv = ReportProgress_Detailed ? treeView : null;

            Stopwatch sw = new();
            ThemeLog.AddNode(tv, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.Strings.ThemeManager.Actions.SavingCursorsColors}", "info");
            sw.Reset();
            sw.Start();
            Cursor.Save_Cursors_To_Registry("Arrow", Cursor_Arrow, tv);
            Cursor.Save_Cursors_To_Registry("Help", Cursor_Help, tv);
            Cursor.Save_Cursors_To_Registry("AppLoading", Cursor_AppLoading, tv);
            Cursor.Save_Cursors_To_Registry("Busy", Cursor_Busy, tv);
            Cursor.Save_Cursors_To_Registry("Move", Cursor_Move, tv);
            Cursor.Save_Cursors_To_Registry("NS", Cursor_NS, tv);
            Cursor.Save_Cursors_To_Registry("EW", Cursor_EW, tv);
            Cursor.Save_Cursors_To_Registry("NESW", Cursor_NESW, tv);
            Cursor.Save_Cursors_To_Registry("NWSE", Cursor_NWSE, tv);
            Cursor.Save_Cursors_To_Registry("Up", Cursor_Up, tv);
            Cursor.Save_Cursors_To_Registry("Pen", Cursor_Pen, tv);
            Cursor.Save_Cursors_To_Registry("None", Cursor_None, tv);
            Cursor.Save_Cursors_To_Registry("Link", Cursor_Link, tv);
            Cursor.Save_Cursors_To_Registry("Pin", Cursor_Pin, tv);
            Cursor.Save_Cursors_To_Registry("Person", Cursor_Person, tv);
            Cursor.Save_Cursors_To_Registry("IBeam", Cursor_IBeam, tv);
            Cursor.Save_Cursors_To_Registry("Cross", Cursor_Cross, tv);
            ThemeLog.AddNode(tv, string.Format(Program.Lang.Strings.ThemeManager.Actions.Time, sw.ElapsedMilliseconds / 1000d), "time");
            sw.Stop();

            if (Enabled)
            {
                SystemParametersInfo(tv, SPI.SPI_SETCURSORSHADOW, 0, Shadow, SPIF.SPIF_UPDATEINIFILE);
                SystemParametersInfo(tv, SPI.SPI_SETMOUSESONAR, 0, Sonar, SPIF.SPIF_UPDATEINIFILE);
                SystemParametersInfo(tv, SPI.SPI_SETMOUSETRAILS, Trails, 0, SPIF.SPIF_UPDATEINIFILE);
                EditReg(tv, @"HKEY_CURRENT_USER\Control Panel\Cursors", "CursorBaseSize", Size);

                if (!Cursor_Arrow.UseFromFile || (Cursor_Arrow.UseFromFile && !File.Exists(Cursor_Arrow.File))) RenderCursor(Paths.CursorType.Arrow, this, tv);
                if (!Cursor_AppLoading.UseFromFile || (Cursor_AppLoading.UseFromFile && !File.Exists(Cursor_AppLoading.File))) RenderCursor(Paths.CursorType.AppLoading, this, tv);
                if (!Cursor_Busy.UseFromFile || (Cursor_Busy.UseFromFile && !File.Exists(Cursor_Busy.File))) RenderCursor(Paths.CursorType.Busy, this, tv);
                if (!Cursor_Help.UseFromFile || (Cursor_Help.UseFromFile && !File.Exists(Cursor_Help.File))) RenderCursor(Paths.CursorType.Help, this, tv);
                if (!Cursor_Pen.UseFromFile || (Cursor_Pen.UseFromFile && !File.Exists(Cursor_Pen.File))) RenderCursor(Paths.CursorType.Pen, this, tv);
                if (!Cursor_None.UseFromFile || (Cursor_None.UseFromFile && !File.Exists(Cursor_None.File))) RenderCursor(Paths.CursorType.None, this, tv);
                if (!Cursor_Move.UseFromFile || (Cursor_Move.UseFromFile && !File.Exists(Cursor_Move.File))) RenderCursor(Paths.CursorType.Move, this, tv);
                if (!Cursor_Up.UseFromFile || (Cursor_Up.UseFromFile && !File.Exists(Cursor_Up.File))) RenderCursor(Paths.CursorType.Up, this, tv);
                if (!Cursor_NS.UseFromFile || (Cursor_NS.UseFromFile && !File.Exists(Cursor_NS.File))) RenderCursor(Paths.CursorType.NS, this, tv);
                if (!Cursor_EW.UseFromFile || (Cursor_EW.UseFromFile && !File.Exists(Cursor_EW.File))) RenderCursor(Paths.CursorType.EW, this, tv);
                if (!Cursor_NESW.UseFromFile || (Cursor_NESW.UseFromFile && !File.Exists(Cursor_NESW.File))) RenderCursor(Paths.CursorType.NESW, this, tv);
                if (!Cursor_NWSE.UseFromFile || (Cursor_NWSE.UseFromFile && !File.Exists(Cursor_NWSE.File))) RenderCursor(Paths.CursorType.NWSE, this, tv);
                if (!Cursor_Link.UseFromFile || (Cursor_Link.UseFromFile && !File.Exists(Cursor_Link.File))) RenderCursor(Paths.CursorType.Link, this, tv);
                if (!Cursor_Pin.UseFromFile || (Cursor_Pin.UseFromFile && !File.Exists(Cursor_Pin.File))) RenderCursor(Paths.CursorType.Pin, this, tv);
                if (!Cursor_Person.UseFromFile || (Cursor_Person.UseFromFile && !File.Exists(Cursor_Person.File))) RenderCursor(Paths.CursorType.Person, this, tv);
                if (!Cursor_IBeam.UseFromFile || (Cursor_IBeam.UseFromFile && !File.Exists(Cursor_IBeam.File))) RenderCursor(Paths.CursorType.IBeam, this, tv);
                if (!Cursor_Cross.UseFromFile || (Cursor_Cross.UseFromFile && !File.Exists(Cursor_Cross.File))) RenderCursor(Paths.CursorType.Cross, this, tv);

                // Apply cursors to system
                SetCursorsToSystem(this, "HKEY_CURRENT_USER", ReportProgress_Detailed ? treeView : null);

                if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    EditReg(@"HKEY_USERS\.DEFAULT\Control Panel\Mouse", "MouseTrails", Trails);
                    SetCursorsToSystem(this, @"HKEY_USERS\.DEFAULT", ReportProgress_Detailed ? treeView : null);
                }
            }

            else if (Program.Settings.ThemeApplyingBehavior.ResetCursorsToAero)
            {
                if (!OS.WXP)
                {
                    ResetCursorsToAero("HKEY_CURRENT_USER", ReportProgress_Detailed ? treeView : null);
                    if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        ResetCursorsToAero(@"HKEY_USERS\.DEFAULT");
                }

                else
                {
                    ResetCursorsToNone_XP("HKEY_CURRENT_USER", ReportProgress_Detailed ? treeView : null);
                    if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        ResetCursorsToNone_XP(@"HKEY_USERS\.DEFAULT");
                }
            }
        }

        /// <summary>
        /// Saves Cursors toggle state into registry
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors", string.Empty, Enabled);
        }

        /// <summary>
        /// Renders a selected cursor from a WinPaletter theme
        /// </summary>
        /// <param name="Type">Cursor type</param>
        /// <param name="cursors">WinPaletter Cursors instance</param>
        /// <param name="treeView">TreeView used to show applying log</param>
        private void RenderCursor(Paths.CursorType Type, Cursors cursors, TreeView treeView = null)
        {
            Dictionary<Paths.CursorType, string> cursorTypeMap = new()
            {
                { Paths.CursorType.Arrow, "Arrow" },
                { Paths.CursorType.Help, "Help" },
                { Paths.CursorType.Busy, "Busy" },
                { Paths.CursorType.AppLoading, "AppLoading" },
                { Paths.CursorType.None, "None" },
                { Paths.CursorType.Move, "Move" },
                { Paths.CursorType.Up, "Up" },
                { Paths.CursorType.NS, "NS" },
                { Paths.CursorType.EW, "EW" },
                { Paths.CursorType.NESW, "NESW" },
                { Paths.CursorType.NWSE, "NWSE" },
                { Paths.CursorType.Pen, "Pen" },
                { Paths.CursorType.Link, "Link" },
                { Paths.CursorType.Pin, "Pin" },
                { Paths.CursorType.Person, "Person" },
                { Paths.CursorType.IBeam, "IBeam" },
                { Paths.CursorType.Cross, "Cross" }
            };

            string CurName = cursorTypeMap.TryGetValue(Type, out string name) ? name : string.Empty;

            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Rendering cursor `{CurName}`.");

            if (treeView is not null) ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.RenderingCursor, CurName), "pe_patch");

            if (!Directory.Exists(SysPaths.CursorsWP))
            {
                Directory.CreateDirectory(SysPaths.CursorsWP);
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Created directory for WinPaletter cursors: `{SysPaths.CursorsWP}`.");
            }

            if (Type != Paths.CursorType.Busy & Type != Paths.CursorType.AppLoading)
            {
                // Save cursor to file path inside WinPaletter cursors folder
                string Path = $"{SysPaths.CursorsWP}\\{CurName}.cur";

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Cursor `{CurName}` will be saved as `{Path}` with size `{Size}x{Size}`.");

                // Create cursor file stream
                using (FileStream FS = new(Path, FileMode.Create))
                {
                    // Use EOIcoCurWriter to write cursor file
                    EOIcoCurWriter EO = new(FS, 7, EOIcoCurWriter.IcoCurType.Cursor);

                    // Create scales array
                    int[] scales = [24, 32, 48, 64, 96];
                    if (!scales.Contains(Size)) scales = [.. scales.ToList().Append(Size).OrderBy(x => x)];

                    // Loop to create different cursors sizes (scales)
                    foreach (int scale in scales)
                    {
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Rendering a frame for `{CurName}` with size `{scale}x{scale}`.");

                        float i = scale / 32f;
                        Bitmap bmp = new(scale, scale, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                        Point HotPoint = new(1, 1);

                        // Switch to create different cursors. Each case is a different cursor type and has its own rendering options.
                        switch (Type)
                        {
                            case Paths.CursorType.Arrow:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_Arrow) { Cursor = Paths.CursorType.Arrow, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new(1, 1);
                                    break;
                                }

                            case Paths.CursorType.Help:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_Help) { Cursor = Paths.CursorType.Help, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new(1, 1);
                                    break;
                                }

                            case Paths.CursorType.None:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_None) { Cursor = Paths.CursorType.None, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                    break;
                                }

                            case Paths.CursorType.Move:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_Move) { Cursor = Paths.CursorType.Move, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new(1 + (int)Math.Round(11f * i), 1 + (int)Math.Round(11f * i));
                                    break;
                                }

                            case Paths.CursorType.Up:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_Up) { Cursor = Paths.CursorType.Up, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new(1 + (int)Math.Round(4f * i), 1);
                                    break;
                                }

                            case Paths.CursorType.NS:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_NS) { Cursor = Paths.CursorType.NS, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new(1 + (int)Math.Round(4f * i), 1 + (int)Math.Round(11f * i));
                                    break;
                                }

                            case Paths.CursorType.EW:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_EW) { Cursor = Paths.CursorType.EW, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new((int)Math.Round(1f + 11f * i), (int)Math.Round(1f + 4f * i));
                                    break;
                                }

                            case Paths.CursorType.NESW:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_NESW) { Cursor = Paths.CursorType.NESW, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                    break;
                                }

                            case Paths.CursorType.NWSE:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_NWSE) { Cursor = Paths.CursorType.NWSE, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                    break;
                                }

                            case Paths.CursorType.Pen:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_Pen) { Cursor = Paths.CursorType.Pen, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new(1, 1);
                                    break;
                                }

                            case Paths.CursorType.Link:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_Link) { Cursor = Paths.CursorType.Link, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                    break;
                                }

                            case Paths.CursorType.Pin:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_Pin) { Cursor = Paths.CursorType.Pin, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                    break;
                                }

                            case Paths.CursorType.Person:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_Person) { Cursor = Paths.CursorType.Person, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                    break;
                                }

                            case Paths.CursorType.IBeam:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_IBeam) { Cursor = Paths.CursorType.IBeam, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new(1 + (int)Math.Round(4f * i), 1 + (int)Math.Round(9f * i));
                                    break;
                                }

                            case Paths.CursorType.Cross:
                                {
                                    CursorOptions CurOptions = new(cursors.Cursor_Cross) { Cursor = Paths.CursorType.Cross, LineThickness = 1f, Scale = i, Angle = 0f };
                                    bmp = Paths.Draw(CurOptions);
                                    HotPoint = new(1 + (int)Math.Round(9f * i), 1 + (int)Math.Round(9f * i));
                                    break;
                                }

                        }

                        // Write bitmap to cursor file stream
                        EO.WriteBitmap(bmp, null, HotPoint);

                        // Log the rendering of the frame
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"This frame has been rendered`.");

                        bmp.Dispose();
                    }

                    // Close cursor file stream
                    FS.Close();
                }

                if (treeView is not null) ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.CursorRenderedInto, Path), "info");
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Cursor `{CurName}` has been rendered and saved to `{Path}`.");
            }

            // Render animated cursors
            else
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Rendering animated cursor `{CurName}`.");

                Point HotPoint = new(1, 1);
                int increment = 10;
                int steps = 360 / increment + 2; // +1 for first angle, +1 for last  angle
                int loopIndex = 0;
                int[] angles = new int[steps];
                int[] scales = [32, 64, 128];

                //Create array of angles
                for (int ang = 180; ang <= 360; ang += +increment) { angles[loopIndex] = ang; loopIndex++; }
                for (int ang = 0; ang <= 180; ang += +increment) { angles[loopIndex] = ang; loopIndex++; }

                string[] ProcessedFiles = [string.Empty];

                //Loop to create different cursors sizes (scales)
                foreach (int scale in scales)
                {
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Rendering animated cursor `{CurName}` with size `{scale}x{scale}`.");

                    List<Bitmap> BMPList = [];
                    BMPList.Clear();
                    float factor = scale / 32f;

                    foreach (int angle in angles)
                    {
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Rendering a frame for `{CurName}` with size `{scale}x{scale}` and angle `{angle}`.");

                        Bitmap bm = null;

                        if (Type == Paths.CursorType.AppLoading)
                        {
                            CursorOptions CurOptions = new(cursors.Cursor_AppLoading) { Cursor = Paths.CursorType.AppLoading, LineThickness = 1f, Scale = factor, Angle = angle };
                            bm = new(Paths.Draw(CurOptions));
                            HotPoint = new(1, 1 + (int)Math.Round(8f * factor));
                        }

                        else if (Type == Paths.CursorType.Busy)
                        {
                            CursorOptions CurOptions = new(cursors.Cursor_Busy) { Cursor = Paths.CursorType.Busy, LineThickness = 1f, Scale = factor, Angle = angle };
                            bm = new(Paths.Draw(CurOptions));
                            HotPoint = new((CurOptions.CircleStyle != Paths.CircleStyle.Classic ? 1 : 2) + (int)Math.Round(11f * factor), 1 + (int)Math.Round(11f * factor));
                        }

                        if (bm != null) { BMPList.Add(bm); }
                    }

                    uint count = (uint)BMPList.Count;

                    // Create frame rates and frame sequential numbers arrays
                    uint[] frameRates = new uint[count];
                    uint[] framesSequentialNumbers = new uint[count];
                    uint Speed = 2;

                    for (uint i1 = 0; i1 <= count - 1; i1++) { frameRates[i1] = Speed; framesSequentialNumbers[i1] = i1; }

                    string curFileNameModifier = string.Empty;
                    if (scale == 64) { curFileNameModifier = "_l"; }
                    if (scale == 128) { curFileNameModifier = "_xl"; }

                    // Save cursor to file path inside WinPaletter cursors folder
                    string OutputFile = $@"{SysPaths.CursorsWP}\{CurName}{curFileNameModifier}.ani";

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Cursor `{CurName}` will be saved as `{OutputFile}` with size `{scale}x{scale}` and speed `{Speed}`.");

                    // Create cursor file stream
                    using (FileStream fs = new(OutputFile, FileMode.Create))
                    {
                        // Use EOIcoCurWriter to write cursor file
                        EOANIWriter AN = new(fs, count, Speed, frameRates, framesSequentialNumbers, null, null, HotPoint);

                        for (uint i1 = 0; i1 <= count - 1; i1++) { AN.WriteFrame(BMPList[(int)i1]); }

                        ProcessedFiles = [.. ProcessedFiles, OutputFile];

                        for (uint i1 = 0; i1 <= count - 1; i1++) { BMPList[(int)i1].Dispose(); }
                        BMPList.Clear();

                        // Close cursor file stream
                        fs.Close();
                    }

                    if (treeView is not null) ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.CursorRenderedInto, OutputFile), "info");

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Cursor `{CurName}` has been rendered and saved to `{OutputFile}`.");
                }
            }
        }

        /// <summary>
        /// Apply rendered WinPaletter cursors to registry and broadcast registry change to system
        /// </summary>
        /// <param name="cursors">WinPaletter Cursors instance</param>
        /// <param name="scopeReg">It can be HKEY_CURRENT_USER or HKEY_USERS\...</param>
        /// <param name="treeView">TreeView used to show applying log</param>
        public void SetCursorsToSystem(Cursors cursors, string scopeReg = "HKEY_CURRENT_USER", TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Applying WinPaletter cursors to system from `{scopeReg}` registry scope.");

            // WinPaletter saves rendered cursors to the following path
            string Path = SysPaths.CursorsWP;

            double DPI = Program.GetWindowsScreenScalingFactor();
            string DPI_Fixer = string.Empty;

            if (Size <= 32)
            {
                if (DPI >= 150 && DPI < 175) { DPI_Fixer = "_l"; } else if (DPI >= 175) { DPI_Fixer = "_xl"; }
            }
            else if (Size > 32 && Size <= 48)
            {
                DPI_Fixer = "_l";
            }
            else if (Size > 48)
            {
                DPI_Fixer = "_xl";
            }

            string RegValue;
            RegValue = $@"{Path}\{"Arrow.cur"}";
            RegValue += $@",{Path}\{"Help.cur"}";
            RegValue += $@",{Path}\{$"AppLoading{DPI_Fixer}.ani"}";
            RegValue += $@",{Path}\{$"Busy{DPI_Fixer}.ani"}";
            RegValue += $@",{Path}\{"Cross.cur"}";
            RegValue += $@",{Path}\{"IBeam.cur"}";
            RegValue += $@",{Path}\{"Pen.cur"}";
            RegValue += $@",{Path}\{"None.cur"}";
            RegValue += $@",{Path}\{"NS.cur"}";
            RegValue += $@",{Path}\{"EW.cur"}";
            RegValue += $@",{Path}\{"NWSE.cur"}";
            RegValue += $@",{Path}\{"NESW.cur"}";
            RegValue += $@",{Path}\{"Move.cur"}";
            RegValue += $@",{Path}\{"Up.cur"}";
            RegValue += $@",{Path}\{"Link.cur"}";
            RegValue += $@",{Path}\{"Pin.cur"}";
            RegValue += $@",{Path}\{"Person.cur"}";

            EditReg($@"{scopeReg}\Control Panel\Cursors\Schemes", "WinPaletter", RegValue, RegistryValueKind.String);
            EditReg($@"{scopeReg}\Control Panel\Cursors", string.Empty, "WinPaletter", RegistryValueKind.String);
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Scheme Source", 1, RegistryValueKind.DWord);

            string x = System.IO.Path.Combine(Path, "Arrow.cur");
            if (cursors.Cursor_Arrow.UseFromFile && (File.Exists(cursors.Cursor_Arrow.File) || OS.WXP))
                x = cursors.Cursor_Arrow.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
            SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_NORMAL);

            x = System.IO.Path.Combine(Path, $"AppLoading{DPI_Fixer}.ani");
            if (cursors.Cursor_AppLoading.UseFromFile && (File.Exists(cursors.Cursor_AppLoading.File) || OS.WXP))
                x = cursors.Cursor_AppLoading.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);
            SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING);

            x = System.IO.Path.Combine(Path, $"Busy{DPI_Fixer}.ani");
            if (cursors.Cursor_Busy.UseFromFile && (File.Exists(cursors.Cursor_Busy.File) || OS.WXP))
                x = cursors.Cursor_Busy.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
            SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_WAIT);

            x = System.IO.Path.Combine(Path, "Cross.cur");
            if (cursors.Cursor_Cross.UseFromFile && (File.Exists(cursors.Cursor_Cross.File) || OS.WXP))
                x = cursors.Cursor_Cross.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
            SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_CROSS);

            x = System.IO.Path.Combine(Path, "Link.cur");
            if (cursors.Cursor_Link.UseFromFile && (File.Exists(cursors.Cursor_Link.File) || OS.WXP))
                x = cursors.Cursor_Link.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
            SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_HAND);

            x = System.IO.Path.Combine(Path, "Help.cur");
            if (cursors.Cursor_Help.UseFromFile && (File.Exists(cursors.Cursor_Help.File) || OS.WXP))
                x = cursors.Cursor_Help.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
            SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_HELP);

            x = System.IO.Path.Combine(Path, "IBeam.cur");
            if (cursors.Cursor_IBeam.UseFromFile && (File.Exists(cursors.Cursor_IBeam.File) || OS.WXP))
                x = cursors.Cursor_IBeam.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
            SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_IBEAM);

            x = System.IO.Path.Combine(Path, "None.cur");
            if (cursors.Cursor_None.UseFromFile && (File.Exists(cursors.Cursor_None.File) || OS.WXP))
                x = cursors.Cursor_None.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "No", x, RegistryValueKind.String);
            SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_NO);

            x = System.IO.Path.Combine(Path, "Pen.cur");
            if (cursors.Cursor_Pen.UseFromFile && (File.Exists(cursors.Cursor_Pen.File) || OS.WXP))
                x = cursors.Cursor_Pen.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "NWPen", x, RegistryValueKind.String);
            // SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_)

            x = System.IO.Path.Combine(Path, "Person.cur");
            if (cursors.Cursor_Person.UseFromFile && (File.Exists(cursors.Cursor_Person.File) || OS.WXP))
                x = cursors.Cursor_Person.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Person", x, RegistryValueKind.String);
            // SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_)

            x = System.IO.Path.Combine(Path, "Pin.cur");
            if (cursors.Cursor_Pin.UseFromFile && (File.Exists(cursors.Cursor_Pin.File) || OS.WXP))
                x = cursors.Cursor_Pin.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Pin", x, RegistryValueKind.String);
            // SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_

            x = System.IO.Path.Combine(Path, "Move.cur");
            if (cursors.Cursor_Move.UseFromFile && (File.Exists(cursors.Cursor_Move.File) || OS.WXP))
                x = cursors.Cursor_Move.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
            SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEALL);

            x = System.IO.Path.Combine(Path, "NESW.cur");
            if (cursors.Cursor_NESW.UseFromFile && (File.Exists(cursors.Cursor_NESW.File) || OS.WXP))
                x = cursors.Cursor_NESW.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
            SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENESW);

            x = System.IO.Path.Combine(Path, "NS.cur");
            if (cursors.Cursor_NS.UseFromFile && (File.Exists(cursors.Cursor_NS.File) || OS.WXP))
                x = cursors.Cursor_NS.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
            SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENS);

            x = System.IO.Path.Combine(Path, "NWSE.cur");
            if (cursors.Cursor_NWSE.UseFromFile && (File.Exists(cursors.Cursor_NWSE.File) || OS.WXP))
                x = cursors.Cursor_NWSE.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
            SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE);

            x = System.IO.Path.Combine(Path, "EW.cur");
            if (cursors.Cursor_EW.UseFromFile && (File.Exists(cursors.Cursor_EW.File) || OS.WXP))
                x = cursors.Cursor_EW.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
            SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEWE);

            x = System.IO.Path.Combine(Path, "Up.cur");
            if (cursors.Cursor_Up.UseFromFile && (File.Exists(cursors.Cursor_Up.File) || OS.WXP))
                x = cursors.Cursor_Up.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
            SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_UP);

            // Broadcast registry change to system
            SystemParametersInfo(treeView, SPI.SPI_SETCURSORS, 0, 0, SPIF.SPIF_UPDATEINIFILE);
            SystemParametersInfo(treeView, SPI.SPI_SETCURSORSSIZE, 0, Size, SPIF.SPIF_WRITEANDNOTIFY);
        }

        /// <summary>
        /// Reset cursors to default Windows scheme (Aero)
        /// </summary>
        /// <param name="scopeReg">It can be HKEY_CURRENT_USER or HKEY_USERS\...</param>
        /// <param name="treeView">TreeView used to show applying log</param>
        public static void ResetCursorsToAero(string scopeReg = "HKEY_CURRENT_USER", TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Resetting cursors to Aero scheme from `{scopeReg}` registry scope.");

            try
            {
                string path = @"%SystemRoot%\Cursors";

                if (scopeReg.ToUpper() == "HKEY_CURRENT_USER")
                {
                    DelValue(treeView, "HKEY_CURRENT_USER\\Control Panel\\Cursors\\Schemes", "WinPaletter");
                }

                EditReg($@"{scopeReg}\Control Panel\Cursors", string.Empty, "Windows Default", RegistryValueKind.String);
                EditReg($@"{scopeReg}\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord);
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Scheme Source", 2, RegistryValueKind.DWord);

                string x = $@"{path}\{"aero_working.ani"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING);
                }

                x = $@"{path}\{"aero_arrow.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_NORMAL);
                }

                x = string.Format(string.Empty);
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_CROSS);
                }

                x = $@"{path}\{"aero_link.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_HAND);
                }

                x = $@"{path}\{"aero_helpsel.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_HELP);
                }

                x = string.Format(string.Empty);
                EditReg($@"{scopeReg}\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_IBEAM);
                }

                x = $@"{path}\{"aero_unavail.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "No", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_NO);
                }

                x = $@"{path}\{"aero_pen.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "NWPen", x, RegistryValueKind.String);
                // If IO.System.IO.File.Exists(X) then SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_)

                x = $@"{path}\{"aero_person.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Person", x, RegistryValueKind.String);
                // If IO.System.IO.File.Exists(X) then SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

                x = $@"{path}\{"aero_pin.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Pin", x, RegistryValueKind.String);
                // If IO.System.IO.File.Exists(X) then SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

                x = $@"{path}\{"aero_move.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEALL);
                }

                x = $@"{path}\{"aero_nesw.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENESW);
                }

                x = $@"{path}\{"aero_ns.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENS);
                }

                x = $@"{path}\{"aero_nwse.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE);
                }

                x = $@"{path}\{"aero_ew.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEWE);
                }

                x = $@"{path}\{"aero_up.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_UP);
                }

                x = $@"{path}\{"aero_busy.ani"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_WAIT);
                }

                // Broadcast registry change to system
                SystemParametersInfo(treeView, SPI.SPI_SETCURSORS, 0, 0, SPIF.SPIF_UPDATEINIFILE);
                SystemParametersInfo(treeView, SPI.SPI_SETCURSORSSIZE, 0, 32, SPIF.SPIF_WRITEANDNOTIFY);
            }

            catch (Exception ex)
            {
                if (MsgBox(Program.Lang.Strings.ThemeManager.Errors.RestoreCursors, MessageBoxButtons.OKCancel, MessageBoxIcon.Error, Program.Lang.Strings.ThemeManager.Errors.RestoreCursorsErrorPressOK, string.Empty, string.Empty, string.Empty, string.Empty, Program.Lang.Strings.ThemeManager.Tips.RestoreCursorsTip, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.OK)
                    Forms.BugReport.ThrowError(ex);
            }

        }

        /// <summary>
        /// Reset cursors to default Windows NT 5.1 (WXP)
        /// </summary>
        /// <param name="scopeReg">It can be HKEY_CURRENT_USER or HKEY_USERS\...</param>
        /// <param name="treeView">TreeView used to show applying log</param>
        public static void ResetCursorsToNone_XP(string scopeReg = "HKEY_CURRENT_USER", TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Resetting cursors to Windows XP scheme from `{scopeReg}` registry scope.");

            try
            {
                if (scopeReg.ToUpper() == "HKEY_CURRENT_USER")
                {
                    DelValue(treeView, "HKEY_CURRENT_USER\\Control Panel\\Cursors\\Schemes", "WinPaletter");
                }

                EditReg($@"{scopeReg}\Control Panel\Cursors", string.Empty, "Windows Default", RegistryValueKind.String);
                EditReg($@"{scopeReg}\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord);
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Scheme Source", 2, RegistryValueKind.DWord);

                string x = string.Empty;
                EditReg($@"{scopeReg}\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);
                SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
                SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_NORMAL);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
                SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_CROSS);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
                SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_HAND);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
                SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_HELP);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
                SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_IBEAM);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "No", x, RegistryValueKind.String);
                SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_NO);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
                SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEALL);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
                SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENESW);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
                SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENS);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
                SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
                SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEWE);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
                SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_UP);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
                SetSystemCursor(treeView, x, OCR_SYSTEM_CURSORS.OCR_WAIT);

                // Broadcast registry change to system
                SystemParametersInfo(treeView, SPI.SPI_SETCURSORS, 0, 0, SPIF.SPIF_UPDATEINIFILE);
                SystemParametersInfo(treeView, SPI.SPI_SETCURSORSSIZE, 0, 32, SPIF.SPIF_WRITEANDNOTIFY);
            }

            catch (Exception ex)
            {
                if (MsgBox(Program.Lang.Strings.ThemeManager.Errors.RestoreCursors, MessageBoxButtons.OKCancel, MessageBoxIcon.Error, Program.Lang.Strings.ThemeManager.Errors.RestoreCursorsErrorPressOK, string.Empty, string.Empty, string.Empty, string.Empty, Program.Lang.Strings.ThemeManager.Tips.RestoreCursorsTip, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.OK)
                    Forms.BugReport.ThrowError(ex);
            }
        }

        /// <summary>Operator to check if two Cursors structures are equal</summary>
        public static bool operator ==(Cursors First, Cursors Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two Cursors structures are not equal</summary>
        public static bool operator !=(Cursors First, Cursors Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones Cursors structure</summary>
        public readonly object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two Cursors structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of Cursors structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
