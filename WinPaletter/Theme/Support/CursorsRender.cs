using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        /// <summary>
        /// Renders all cursor inside a WinPaletter theme
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="TreeView">TreeView used to show applying log</param>
        public void ExportCursors(Manager TM, TreeView TreeView = null)
        {
            bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
            bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed;

            if (!TM.Cursor_Arrow.UseFromFile || (TM.Cursor_Arrow.UseFromFile && !File.Exists(TM.Cursor_Arrow.File))) RenderCursor(Paths.CursorType.Arrow, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_AppLoading.UseFromFile || (TM.Cursor_AppLoading.UseFromFile && !File.Exists(TM.Cursor_AppLoading.File))) RenderCursor(Paths.CursorType.AppLoading, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_Busy.UseFromFile || (TM.Cursor_Busy.UseFromFile && !File.Exists(TM.Cursor_Busy.File))) RenderCursor(Paths.CursorType.Busy, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_Help.UseFromFile || (TM.Cursor_Help.UseFromFile && !File.Exists(TM.Cursor_Help.File))) RenderCursor(Paths.CursorType.Help, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_Pen.UseFromFile || (TM.Cursor_Pen.UseFromFile && !File.Exists(TM.Cursor_Pen.File))) RenderCursor(Paths.CursorType.Pen, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_None.UseFromFile || (TM.Cursor_None.UseFromFile && !File.Exists(TM.Cursor_None.File))) RenderCursor(Paths.CursorType.None, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_Move.UseFromFile || (TM.Cursor_Move.UseFromFile && !File.Exists(TM.Cursor_Move.File))) RenderCursor(Paths.CursorType.Move, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_Up.UseFromFile || (TM.Cursor_Up.UseFromFile && !File.Exists(TM.Cursor_Up.File))) RenderCursor(Paths.CursorType.Up, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_NS.UseFromFile || (TM.Cursor_NS.UseFromFile && !File.Exists(TM.Cursor_NS.File))) RenderCursor(Paths.CursorType.NS, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_EW.UseFromFile || (TM.Cursor_EW.UseFromFile && !File.Exists(TM.Cursor_EW.File))) RenderCursor(Paths.CursorType.EW, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_NESW.UseFromFile || (TM.Cursor_NESW.UseFromFile && !File.Exists(TM.Cursor_NESW.File))) RenderCursor(Paths.CursorType.NESW, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_NWSE.UseFromFile || (TM.Cursor_NWSE.UseFromFile && !File.Exists(TM.Cursor_NWSE.File))) RenderCursor(Paths.CursorType.NWSE, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_Link.UseFromFile || (TM.Cursor_Link.UseFromFile && !File.Exists(TM.Cursor_Link.File))) RenderCursor(Paths.CursorType.Link, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_Pin.UseFromFile || (TM.Cursor_Pin.UseFromFile && !File.Exists(TM.Cursor_Pin.File))) RenderCursor(Paths.CursorType.Pin, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_Person.UseFromFile || (TM.Cursor_Person.UseFromFile && !File.Exists(TM.Cursor_Person.File))) RenderCursor(Paths.CursorType.Person, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_IBeam.UseFromFile || (TM.Cursor_IBeam.UseFromFile && !File.Exists(TM.Cursor_IBeam.File))) RenderCursor(Paths.CursorType.IBeam, TM, ReportProgress_Detailed ? TreeView : null);
            if (!TM.Cursor_Cross.UseFromFile || (TM.Cursor_Cross.UseFromFile && !File.Exists(TM.Cursor_Cross.File))) RenderCursor(Paths.CursorType.Cross, TM, ReportProgress_Detailed ? TreeView : null);
        }

        /// <summary>
        /// Renders a selected cursor from a WinPaletter theme
        /// </summary>
        /// <param name="Type">Cursor type</param>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="TreeView">TreeView used to show applying log</param>
        public void RenderCursor(Paths.CursorType Type, Manager TM, TreeView TreeView = null)
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

            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_RenderingCursor, CurName), "pe_patch");

            if (!(Type == Paths.CursorType.Busy) & !(Type == Paths.CursorType.AppLoading))
            {
                if (!Directory.Exists(PathsExt.CursorsWP))
                    Directory.CreateDirectory(PathsExt.CursorsWP);

                string Path = string.Format($@"{PathsExt.CursorsWP}\{{0}}.cur", CurName);

                FileStream FS = new(Path, FileMode.Create);
                EOIcoCurWriter EO = new(FS, 7, EOIcoCurWriter.IcoCurType.Cursor);

                for (float i = 1f; i <= 4f; i += 0.5f)
                {
                    Bitmap bmp = new((int)Math.Round(32f * i), (int)Math.Round(32f * i), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                    Point HotPoint = new(1, 1);

                    switch (Type)
                    {
                        case Paths.CursorType.Arrow:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_Arrow) { Cursor = Paths.CursorType.Arrow, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new(1, 1);
                                break;
                            }

                        case Paths.CursorType.Help:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_Help) { Cursor = Paths.CursorType.Help, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new(1, 1);
                                break;
                            }

                        case Paths.CursorType.None:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_None) { Cursor = Paths.CursorType.None, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                break;
                            }

                        case Paths.CursorType.Move:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_Move) { Cursor = Paths.CursorType.Move, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new(1 + (int)Math.Round(11f * i), 1 + (int)Math.Round(11f * i));
                                break;
                            }

                        case Paths.CursorType.Up:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_Up) { Cursor = Paths.CursorType.Up, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new(1 + (int)Math.Round(4f * i), 1);
                                break;
                            }

                        case Paths.CursorType.NS:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_NS) { Cursor = Paths.CursorType.NS, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new(1 + (int)Math.Round(4f * i), 1 + (int)Math.Round(11f * i));
                                break;
                            }

                        case Paths.CursorType.EW:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_EW) { Cursor = Paths.CursorType.EW, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new((int)Math.Round(1f + 11f * i), (int)Math.Round(1f + 4f * i));
                                break;
                            }

                        case Paths.CursorType.NESW:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_NESW) { Cursor = Paths.CursorType.NESW, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                break;
                            }

                        case Paths.CursorType.NWSE:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_NWSE) { Cursor = Paths.CursorType.NWSE, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                break;
                            }

                        case Paths.CursorType.Pen:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_Pen) { Cursor = Paths.CursorType.Pen, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new(1, 1);
                                break;
                            }

                        case Paths.CursorType.Link:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_Link) { Cursor = Paths.CursorType.Link, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                break;
                            }

                        case Paths.CursorType.Pin:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_Pin) { Cursor = Paths.CursorType.Pin, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                break;
                            }

                        case Paths.CursorType.Person:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_Person) { Cursor = Paths.CursorType.Person, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                break;
                            }

                        case Paths.CursorType.IBeam:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_IBeam) { Cursor = Paths.CursorType.IBeam, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new(1 + (int)Math.Round(4f * i), 1 + (int)Math.Round(9f * i));
                                break;
                            }

                        case Paths.CursorType.Cross:
                            {
                                CursorOptions CurOptions = new(TM.Cursor_Cross) { Cursor = Paths.CursorType.Cross, LineThickness = 1f, Scale = i, Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new(1 + (int)Math.Round(9f * i), 1 + (int)Math.Round(9f * i));
                                break;
                            }

                    }

                    EO.WriteBitmap(bmp, null, HotPoint);
                }

                FS.Close();

                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_CursorRenderedInto, Path), "info");
            }

            else
            {
                Point HotPoint = new(1, 1);
                int increment = 10;
                int steps = 360 / increment + 2; // +1 for first angle, +1 for last  angle
                int[] angles = new int[steps];
                int loopIndex = 0;
                int[] scales = new int[] { 32, 64, 128 };

                //Create array of angles
                for (int ang = 180; ang <= 360; ang += +increment) { angles[loopIndex] = ang; loopIndex++; }
                for (int ang = 0; ang <= 180; ang += +increment) { angles[loopIndex] = ang; loopIndex++; }

                string[] ProcessedFiles = new string[] { string.Empty };

                //Loop to create different cursors sizes(scales)
                foreach (int scale in scales)
                {
                    List<Bitmap> BMPList = new();
                    BMPList.Clear();
                    float factor = (float)scale / 32;

                    foreach (int angle in angles)
                    {
                        Bitmap bm = null;

                        if (Type == Paths.CursorType.AppLoading)
                        {
                            CursorOptions CurOptions = new(TM.Cursor_AppLoading) { Cursor = Paths.CursorType.AppLoading, LineThickness = 1f, Scale = factor, Angle = angle };
                            bm = new(Paths.Draw(CurOptions));
                            HotPoint = new(1, 1 + (int)Math.Round(8f * factor));
                        }

                        else if (Type == Paths.CursorType.Busy)
                        {
                            CursorOptions CurOptions = new(TM.Cursor_Busy) { Cursor = Paths.CursorType.Busy, LineThickness = 1f, Scale = factor, Angle = angle };
                            bm = new(Paths.Draw(CurOptions));
                            HotPoint = new((CurOptions.CircleStyle != Paths.CircleStyle.Classic ? 1 : 2) + (int)Math.Round(11f * factor), 1 + (int)Math.Round(11f * factor));
                        }

                        if (bm != null) { BMPList.Add(bm); }
                    }

                    uint count = (uint)BMPList.Count;
                    uint[] frameRates = new uint[count];
                    uint[] framesSequentialNumbers = new uint[count];
                    uint Speed = 2;

                    for (uint i1 = 0; i1 <= count - 1; i1++) { frameRates[i1] = Speed; framesSequentialNumbers[i1] = i1; }

                    if (!Directory.Exists(PathsExt.CursorsWP)) { Directory.CreateDirectory(PathsExt.CursorsWP); }

                    string curFileNameModifier = string.Empty;
                    if (scale == 64) { curFileNameModifier = "_l"; }
                    if (scale == 128) { curFileNameModifier = "_xl"; }

                    string OutputFile = string.Format($@"{PathsExt.CursorsWP}\{{0}}{{1}}.ani", CurName, curFileNameModifier);

                    using (FileStream fs = new(OutputFile, FileMode.Create))
                    {
                        EOANIWriter AN = new(fs, count, Speed, frameRates, framesSequentialNumbers, null, null, HotPoint);

                        for (uint i1 = 0; i1 <= count - 1; i1++) { AN.WriteFrame(BMPList[(int)i1]); }

                        ProcessedFiles = ProcessedFiles.ToList().Append(string.Format($@"{PathsExt.CursorsWP}\{{0}}{{1}}.ani", CurName, curFileNameModifier)).ToArray();

                        fs.Close();
                    }

                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_CursorRenderedInto, string.Format($@"{PathsExt.CursorsWP}\{{0}}{{1}}.ani", CurName, curFileNameModifier)), "info");
                }
            }
        }

        /// <summary>
        /// Apply rendered WinPaletter cursors to registry and broadcast registry change to system
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="scopeReg">It can be HKEY_CURRENT_USER or HKEY_USERS\...</param>
        /// <param name="TreeView">TreeView used to show applying log</param>
        public void ApplyCursorsToReg(Manager TM, string scopeReg = "HKEY_CURRENT_USER", TreeView TreeView = null)
        {
            string Path = PathsExt.CursorsWP;

            double DPI = Program.GetWindowsScreenScalingFactor();
            string DPI_Fixer = string.Empty;

            if (DPI >= 150 && DPI < 175) { DPI_Fixer = "_l"; } else if (DPI >= 175) { DPI_Fixer = "_xl"; }

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
            EditReg($@"{scopeReg}\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord);
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Scheme Source", 1, RegistryValueKind.DWord);

            string x = System.IO.Path.Combine(Path, "Arrow.cur");
            if (TM.Cursor_Arrow.UseFromFile && (File.Exists(TM.Cursor_Arrow.File) || OS.WXP))
                x = TM.Cursor_Arrow.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_NORMAL);

            x = System.IO.Path.Combine(Path, $"AppLoading{DPI_Fixer}.ani");
            if (TM.Cursor_AppLoading.UseFromFile && (File.Exists(TM.Cursor_AppLoading.File) || OS.WXP))
                x = TM.Cursor_AppLoading.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING);

            x = System.IO.Path.Combine(Path, $"Busy{DPI_Fixer}.ani");
            if (TM.Cursor_Busy.UseFromFile && (File.Exists(TM.Cursor_Busy.File) || OS.WXP))
                x = TM.Cursor_Busy.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_WAIT);

            x = System.IO.Path.Combine(Path, "Cross.cur");
            if (TM.Cursor_Cross.UseFromFile && (File.Exists(TM.Cursor_Cross.File) || OS.WXP))
                x = TM.Cursor_Cross.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_CROSS);

            x = System.IO.Path.Combine(Path, "Link.cur");
            if (TM.Cursor_Link.UseFromFile && (File.Exists(TM.Cursor_Link.File) || OS.WXP))
                x = TM.Cursor_Link.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_HAND);

            x = System.IO.Path.Combine(Path, "Help.cur");
            if (TM.Cursor_Help.UseFromFile && (File.Exists(TM.Cursor_Help.File) || OS.WXP))
                x = TM.Cursor_Help.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_HELP);

            x = System.IO.Path.Combine(Path, "IBeam.cur");
            if (TM.Cursor_IBeam.UseFromFile && (File.Exists(TM.Cursor_IBeam.File) || OS.WXP))
                x = TM.Cursor_IBeam.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_IBEAM);

            x = System.IO.Path.Combine(Path, "None.cur");
            if (TM.Cursor_None.UseFromFile && (File.Exists(TM.Cursor_None.File) || OS.WXP))
                x = TM.Cursor_None.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "No", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_NO);

            x = System.IO.Path.Combine(Path, "Pen.cur");
            if (TM.Cursor_Pen.UseFromFile && (File.Exists(TM.Cursor_Pen.File) || OS.WXP))
                x = TM.Cursor_Pen.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "NWPen", x, RegistryValueKind.String);
            // SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_)

            x = System.IO.Path.Combine(Path, "Person.cur");
            if (TM.Cursor_Person.UseFromFile && (File.Exists(TM.Cursor_Person.File) || OS.WXP))
                x = TM.Cursor_Person.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Person", x, RegistryValueKind.String);
            // SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_)

            x = System.IO.Path.Combine(Path, "Pin.cur");
            if (TM.Cursor_Pin.UseFromFile && (File.Exists(TM.Cursor_Pin.File) || OS.WXP))
                x = TM.Cursor_Pin.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "Pin", x, RegistryValueKind.String);
            // SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_

            x = System.IO.Path.Combine(Path, "Move.cur");
            if (TM.Cursor_Move.UseFromFile && (File.Exists(TM.Cursor_Move.File) || OS.WXP))
                x = TM.Cursor_Move.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEALL);

            x = System.IO.Path.Combine(Path, "NESW.cur");
            if (TM.Cursor_NESW.UseFromFile && (File.Exists(TM.Cursor_NESW.File) || OS.WXP))
                x = TM.Cursor_NESW.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENESW);

            x = System.IO.Path.Combine(Path, "NS.cur");
            if (TM.Cursor_NS.UseFromFile && (File.Exists(TM.Cursor_NS.File) || OS.WXP))
                x = TM.Cursor_NS.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENS);

            x = System.IO.Path.Combine(Path, "NWSE.cur");
            if (TM.Cursor_NWSE.UseFromFile && (File.Exists(TM.Cursor_NWSE.File) || OS.WXP))
                x = TM.Cursor_NWSE.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE);

            x = System.IO.Path.Combine(Path, "EW.cur");
            if (TM.Cursor_EW.UseFromFile && (File.Exists(TM.Cursor_EW.File) || OS.WXP))
                x = TM.Cursor_EW.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEWE);

            x = System.IO.Path.Combine(Path, "Up.cur");
            if (TM.Cursor_Up.UseFromFile && (File.Exists(TM.Cursor_Up.File) || OS.WXP))
                x = TM.Cursor_Up.File;
            EditReg($@"{scopeReg}\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_UP);

            SystemParametersInfo(TreeView, SPI.SPI_SETCURSORS, 0, 0, SPIF.SPIF_UPDATEINIFILE);
        }

        /// <summary>
        /// Reset cursors to default Windows scheme (Aero)
        /// </summary>
        /// <param name="scopeReg">It can be HKEY_CURRENT_USER or HKEY_USERS\...</param>
        /// <param name="TreeView">TreeView used to show applying log</param>
        public static void ResetCursorsToAero(string scopeReg = "HKEY_CURRENT_USER", TreeView TreeView = null)
        {
            try
            {
                string path = @"%SystemRoot%\Cursors";

                if (scopeReg.ToUpper() == "HKEY_CURRENT_USER")
                {
                    if (Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors\Schemes", false) is not null)
                    {
                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(Program.Lang.Verbose_DelCursorWPFromReg, @"HKEY_CURRENT_USER\Control Panel\Cursors\Schemes"), "reg_delete");
                        RegistryKey rx = Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors\Schemes", true);
                        rx.DeleteValue("WinPaletter", false);
                        rx.Close();
                    }
                }

                EditReg($@"{scopeReg}\Control Panel\Cursors", string.Empty, "Windows Default", RegistryValueKind.String);
                EditReg($@"{scopeReg}\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord);
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Scheme Source", 2, RegistryValueKind.DWord);

                string x = $@"{path}\{"aero_working.ani"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING);
                }

                x = $@"{path}\{"aero_arrow.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_NORMAL);
                }

                x = string.Format(string.Empty);
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_CROSS);
                }

                x = $@"{path}\{"aero_link.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_HAND);
                }

                x = $@"{path}\{"aero_helpsel.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_HELP);
                }

                x = string.Format(string.Empty);
                EditReg($@"{scopeReg}\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_IBEAM);
                }

                x = $@"{path}\{"aero_unavail.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "No", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_NO);
                }

                x = $@"{path}\{"aero_pen.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "NWPen", x, RegistryValueKind.String);
                // If IO.System.IO.File.Exists(X) then SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_)

                x = $@"{path}\{"aero_person.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Person", x, RegistryValueKind.String);
                // If IO.System.IO.File.Exists(X) then SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

                x = $@"{path}\{"aero_pin.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Pin", x, RegistryValueKind.String);
                // If IO.System.IO.File.Exists(X) then SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

                x = $@"{path}\{"aero_move.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEALL);
                }

                x = $@"{path}\{"aero_nesw.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENESW);
                }

                x = $@"{path}\{"aero_ns.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENS);
                }

                x = $@"{path}\{"aero_nwse.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE);
                }

                x = $@"{path}\{"aero_ew.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEWE);
                }

                x = $@"{path}\{"aero_up.cur"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_UP);
                }

                x = $@"{path}\{"aero_busy.ani"}";
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_WAIT);
                }

                SystemParametersInfo(TreeView, SPI.SPI_SETCURSORS, 0, 0, SPIF.SPIF_UPDATEINIFILE);
            }

            catch (Exception ex)
            {
                if (MsgBox(Program.Lang.TM_RestoreCursorsError, MessageBoxButtons.OKCancel, MessageBoxIcon.Error, Program.Lang.TM_RestoreCursorsErrorPressOK, string.Empty, string.Empty, string.Empty, string.Empty, Program.Lang.TM_RestoreCursorsTip, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.OK)
                    Forms.BugReport.ThrowError(ex);
            }

        }

        /// <summary>
        /// Reset cursors to default Windows NT 5.1 (XP)
        /// </summary>
        /// <param name="scopeReg">It can be HKEY_CURRENT_USER or HKEY_USERS\...</param>
        /// <param name="TreeView">TreeView used to show applying log</param>
        public static void ResetCursorsToNone_XP(string scopeReg = "HKEY_CURRENT_USER", TreeView TreeView = null)
        {
            try
            {
                if (scopeReg.ToUpper() == "HKEY_CURRENT_USER")
                {
                    try
                    {
                        if (Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors\Schemes", false) is not null)
                        {
                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(Program.Lang.Verbose_DelCursorWPFromReg, @"HKEY_CURRENT_USER\Control Panel\Cursors\Schemes"), "reg_delete");
                            RegistryKey rx = Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors\Schemes", true);
                            rx.DeleteValue("WinPaletter", false);
                            rx.Close();
                        }
                    }
                    finally
                    {
                        Registry.CurrentUser.Close();
                    }
                }

                EditReg($@"{scopeReg}\Control Panel\Cursors", string.Empty, "Windows Default", RegistryValueKind.String);
                EditReg($@"{scopeReg}\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord);
                EditReg($@"{scopeReg}\Control Panel\Cursors", "Scheme Source", 2, RegistryValueKind.DWord);

                string x = string.Empty;
                EditReg($@"{scopeReg}\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_NORMAL);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_CROSS);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_HAND);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_HELP);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_IBEAM);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "No", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_NO);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEALL);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENESW);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENS);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEWE);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_UP);

                EditReg($@"{scopeReg}\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_WAIT);

                SystemParametersInfo(TreeView, SPI.SPI_SETCURSORS, 0, 0, SPIF.SPIF_UPDATEINIFILE);
            }

            catch (Exception ex)
            {

                if (MsgBox(Program.Lang.TM_RestoreCursorsError, MessageBoxButtons.OKCancel, MessageBoxIcon.Error, Program.Lang.TM_RestoreCursorsErrorPressOK, string.Empty, string.Empty, string.Empty, string.Empty, Program.Lang.TM_RestoreCursorsTip, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.OK)
                    Forms.BugReport.ThrowError(ex);
            }
        }
    }
}