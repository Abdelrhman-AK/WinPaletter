using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
            bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed;

            try { RenderCursor(Paths.CursorType.Arrow, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Help, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.AppLoading, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Busy, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Pen, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.None, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Move, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Up, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.NS, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.EW, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.NESW, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.NWSE, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Link, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Pin, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Person, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.IBeam, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Cross, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
        }

        /// <summary>
        /// Renders a selected cursor from a WinPaletter theme
        /// </summary>
        /// <param name="Type">Cursor type</param>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="TreeView">TreeView used to show applying log</param>
        public void RenderCursor(Paths.CursorType Type, Manager TM, TreeView TreeView = null)
        {
            string CurName = "";

            switch (Type)
            {
                case Paths.CursorType.Arrow:
                    {
                        CurName = "Arrow";
                        break;
                    }

                case Paths.CursorType.Help:
                    {
                        CurName = "Help";
                        break;
                    }

                case Paths.CursorType.Busy:
                    {
                        CurName = "Busy";
                        break;
                    }

                case Paths.CursorType.AppLoading:
                    {
                        CurName = "AppLoading";
                        break;
                    }

                case Paths.CursorType.None:
                    {
                        CurName = "None";
                        break;
                    }

                case Paths.CursorType.Move:
                    {
                        CurName = "Move";
                        break;
                    }

                case Paths.CursorType.Up:
                    {
                        CurName = "Up";
                        break;
                    }

                case Paths.CursorType.NS:
                    {
                        CurName = "NS";
                        break;
                    }

                case Paths.CursorType.EW:
                    {
                        CurName = "EW";
                        break;
                    }

                case Paths.CursorType.NESW:
                    {
                        CurName = "NESW";
                        break;
                    }

                case Paths.CursorType.NWSE:
                    {
                        CurName = "NWSE";
                        break;
                    }

                case Paths.CursorType.Pen:
                    {
                        CurName = "Pen";
                        break;
                    }

                case Paths.CursorType.Link:
                    {
                        CurName = "Link";
                        break;
                    }

                case Paths.CursorType.Pin:
                    {
                        CurName = "Pin";
                        break;
                    }

                case Paths.CursorType.Person:
                    {
                        CurName = "Person";
                        break;
                    }

                case Paths.CursorType.IBeam:
                    {
                        CurName = "IBeam";
                        break;
                    }

                case Paths.CursorType.Cross:
                    {
                        CurName = "Cross";
                        break;
                    }

            }

            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_RenderingCursor, CurName), "pe_patch");

            if (!(Type == Paths.CursorType.Busy) & !(Type == Paths.CursorType.AppLoading))
            {

                if (!Directory.Exists(PathsExt.CursorsWP))
                    Directory.CreateDirectory(PathsExt.CursorsWP);

                string Path = string.Format(PathsExt.CursorsWP + @"\{0}.cur", CurName);

                var fs = new FileStream(Path, FileMode.Create);
                var EO = new EOIcoCurWriter(fs, 7, EOIcoCurWriter.IcoCurType.Cursor);

                for (float i = 1f; i <= 4f; i += 0.5f)
                {
                    var bmp = new Bitmap((int)Math.Round(32f * i), (int)Math.Round(32f * i), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                    var HotPoint = new Point(1, 1);

                    switch (Type)
                    {
                        case Paths.CursorType.Arrow:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_Arrow) { Cursor = Paths.CursorType.Arrow, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1, 1);
                                break;
                            }

                        case Paths.CursorType.Help:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_Help) { Cursor = Paths.CursorType.Help, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1, 1);
                                break;
                            }

                        case Paths.CursorType.None:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_None) { Cursor = Paths.CursorType.None, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                break;
                            }

                        case Paths.CursorType.Move:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_Move) { Cursor = Paths.CursorType.Move, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(11f * i), 1 + (int)Math.Round(11f * i));
                                break;
                            }

                        case Paths.CursorType.Up:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_Up) { Cursor = Paths.CursorType.Up, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(4f * i), 1);
                                break;
                            }

                        case Paths.CursorType.NS:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_NS) { Cursor = Paths.CursorType.NS, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(4f * i), 1 + (int)Math.Round(11f * i));
                                break;
                            }

                        case Paths.CursorType.EW:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_EW) { Cursor = Paths.CursorType.EW, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point((int)Math.Round(1f + 11f * i), (int)Math.Round(1f + 4f * i));
                                break;
                            }

                        case Paths.CursorType.NESW:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_NESW) { Cursor = Paths.CursorType.NESW, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                break;
                            }

                        case Paths.CursorType.NWSE:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_NWSE) { Cursor = Paths.CursorType.NWSE, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                break;
                            }

                        case Paths.CursorType.Pen:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_Pen) { Cursor = Paths.CursorType.Pen, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1, 1);
                                break;
                            }

                        case Paths.CursorType.Link:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_Link) { Cursor = Paths.CursorType.Link, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                break;
                            }

                        case Paths.CursorType.Pin:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_Pin) { Cursor = Paths.CursorType.Pin, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                break;
                            }

                        case Paths.CursorType.Person:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_Person) { Cursor = Paths.CursorType.Person, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                break;
                            }

                        case Paths.CursorType.IBeam:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_IBeam) { Cursor = Paths.CursorType.IBeam, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(4f * i), 1 + (int)Math.Round(9f * i));
                                break;
                            }

                        case Paths.CursorType.Cross:
                            {
                                var CurOptions = new CursorOptions(TM.Cursor_Cross) { Cursor = Paths.CursorType.Cross, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(9f * i), 1 + (int)Math.Round(9f * i));
                                break;
                            }

                    }

                    EO.WriteBitmap(bmp, null, HotPoint);

                }

                fs.Close();

                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_CursorRenderedInto, Path), "info");
            }

            else
            {
                var HotPoint = new Point(1, 1);

                for (float i = 1f; i <= 4f; i += 1f)
                {
                    List<Bitmap> BMPList = new();
                    BMPList.Clear();

                    #region Add angles bitmaps from 180 deg to 180 deg (Cycle)

                    for (int ang = 180; ang <= 360; ang += +10)
                    {
                        Bitmap bm;

                        if (Type == Paths.CursorType.AppLoading)
                        {
                            var CurOptions = new CursorOptions(TM.Cursor_AppLoading) { Cursor = Paths.CursorType.AppLoading, LineThickness = 1f, Scale = i, _Angle = ang };
                            bm = new Bitmap(Paths.Draw(CurOptions));
                            HotPoint = new Point(1, 1 + (int)Math.Round(8f * i));
                        }

                        else
                        {
                            var CurOptions = new CursorOptions(TM.Cursor_Busy) { Cursor = Paths.CursorType.Busy, LineThickness = 1f, Scale = i, _Angle = ang };
                            bm = new Bitmap(Paths.Draw(CurOptions));

                            HotPoint = new Point((CurOptions.CircleStyle != Paths.CircleStyle.Classic ? 1 : 2) + (int)Math.Round(11f * i), 1 + (int)Math.Round(11f * i));

                        }

                        BMPList.Add(bm);
                    }

                    for (int ang = 0; ang <= 180; ang += +10)
                    {
                        Bitmap bm;

                        if (Type == Paths.CursorType.AppLoading)
                        {
                            var CurOptions = new CursorOptions(TM.Cursor_AppLoading) { Cursor = Paths.CursorType.AppLoading, LineThickness = 1f, Scale = i, _Angle = ang };
                            bm = new Bitmap(Paths.Draw(CurOptions));
                            HotPoint = new Point(1, 1 + (int)Math.Round(8f * i));
                        }

                        else
                        {
                            var CurOptions = new CursorOptions(TM.Cursor_Busy) { Cursor = Paths.CursorType.Busy, LineThickness = 1f, Scale = i, _Angle = ang };
                            bm = new Bitmap(Paths.Draw(CurOptions));
                            HotPoint = new Point((CurOptions.CircleStyle != Paths.CircleStyle.Classic ? 1 : 2) + (int)Math.Round(11f * i), 1 + (int)Math.Round(11f * i));

                        }

                        BMPList.Add(bm);
                    }

                    #endregion

                    int Count = BMPList.Count;
                    uint[] frameRates = new uint[Count];
                    uint[] seqNums = new uint[Count];
                    int Speed = 2;

                    for (int ixx = 0, loopTo = Count - 1; ixx <= loopTo; ixx++)
                    {
                        frameRates[ixx] = Convert.ToUInt32(Speed);
                        seqNums[ixx] = (uint)ixx;
                    }

                    if (!Directory.Exists(PathsExt.CursorsWP))
                        Directory.CreateDirectory(PathsExt.CursorsWP);
                    var fs = new FileStream(string.Format(PathsExt.CursorsWP + @"\{0}_{1}x.ani", CurName, i), FileMode.Create);

                    var AN = new EOANIWriter(fs, (uint)Count, (uint)Speed, frameRates, seqNums, null, null, HotPoint);

                    for (int ix = 0, loopTo1 = Count - 1; ix <= loopTo1; ix++)
                        AN.WriteFrame32(BMPList[ix]);

                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_CursorRenderedInto, string.Format(PathsExt.CursorsWP + @"\{0}_{1}x.ani", CurName, i)), "info");

                    fs.Close();
                }
            }
        }

        /// <summary>
        /// Apply rendered WinPaletter cursors to registry and broadcast registry change to system
        /// </summary>
        /// <param name="scopeReg">It can be HKEY_CURRENT_USER or HKEY_USERS\...</param>
        /// <param name="TreeView">TreeView used to show applying log</param>
        public void ApplyCursorsToReg(string scopeReg = "HKEY_CURRENT_USER", TreeView TreeView = null)
        {
            string Path = PathsExt.CursorsWP;

            string RegValue;
            RegValue = string.Format(@"{0}\{1}", Path, "Arrow.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Help.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "AppLoading_1x.ani");
            RegValue += string.Format(@",{0}\{1}", Path, "Busy_1x.ani");
            RegValue += string.Format(@",{0}\{1}", Path, "Cross.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "IBeam.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Pen.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "None.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "NS.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "EW.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "NWSE.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "NESW.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Move.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Up.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Link.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Pin.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Person.cur");

            EditReg(scopeReg + @"\Control Panel\Cursors\Schemes", "WinPaletter", RegValue, RegistryValueKind.String);
            EditReg(scopeReg + @"\Control Panel\Cursors", "", "WinPaletter", RegistryValueKind.String);
            EditReg(scopeReg + @"\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord);
            EditReg(scopeReg + @"\Control Panel\Cursors", "Scheme Source", 1, RegistryValueKind.DWord);

            string x = string.Format(@"{0}\{1}", Path, "AppLoading_1x.ani");
            EditReg(scopeReg + @"\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING);

            x = string.Format(@"{0}\{1}", Path, "Arrow.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_NORMAL);

            x = string.Format(@"{0}\{1}", Path, "Cross.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_CROSS);

            x = string.Format(@"{0}\{1}", Path, "Link.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_HAND);

            x = string.Format(@"{0}\{1}", Path, "Help.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_HELP);

            x = string.Format(@"{0}\{1}", Path, "IBeam.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_IBEAM);

            x = string.Format(@"{0}\{1}", Path, "None.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "No", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_NO);

            x = string.Format(@"{0}\{1}", Path, "Pen.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "NWPen", x, RegistryValueKind.String);
            // SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_)

            x = string.Format(@"{0}\{1}", Path, "Person.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Person", x, RegistryValueKind.String);
            // SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_)

            x = string.Format(@"{0}\{1}", Path, "Pin.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Pin", x, RegistryValueKind.String);
            // SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_

            x = string.Format(@"{0}\{1}", Path, "Move.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEALL);

            x = string.Format(@"{0}\{1}", Path, "NESW.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENESW);

            x = string.Format(@"{0}\{1}", Path, "NS.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENS);

            x = string.Format(@"{0}\{1}", Path, "NWSE.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE);

            x = string.Format(@"{0}\{1}", Path, "EW.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEWE);

            x = string.Format(@"{0}\{1}", Path, "Up.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_UP);

            x = string.Format(@"{0}\{1}", Path, "Busy_1x.ani");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
            SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_WAIT);

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
                        var rx = Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors\Schemes", true);
                        rx.DeleteValue("WinPaletter", false);
                        rx.Close();
                    }
                }

                EditReg(scopeReg + @"\Control Panel\Cursors", "", "Windows Default", RegistryValueKind.String);
                EditReg(scopeReg + @"\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord);
                EditReg(scopeReg + @"\Control Panel\Cursors", "Scheme Source", 2, RegistryValueKind.DWord);

                string x = string.Format(@"{0}\{1}", path, "aero_working.ani");
                EditReg(scopeReg + @"\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING);
                }

                x = string.Format(@"{0}\{1}", path, "aero_arrow.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_NORMAL);
                }

                x = string.Format("");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_CROSS);
                }

                x = string.Format(@"{0}\{1}", path, "aero_link.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_HAND);
                }

                x = string.Format(@"{0}\{1}", path, "aero_helpsel.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_HELP);
                }

                x = string.Format("");
                EditReg(scopeReg + @"\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_IBEAM);
                }

                x = string.Format(@"{0}\{1}", path, "aero_unavail.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "No", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_NO);
                }

                x = string.Format(@"{0}\{1}", path, "aero_pen.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "NWPen", x, RegistryValueKind.String);
                // If IO.System.IO.File.Exists(X) then SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_)

                x = string.Format(@"{0}\{1}", path, "aero_person.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Person", x, RegistryValueKind.String);
                // If IO.System.IO.File.Exists(X) then SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

                x = string.Format(@"{0}\{1}", path, "aero_pin.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Pin", x, RegistryValueKind.String);
                // If IO.System.IO.File.Exists(X) then SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

                x = string.Format(@"{0}\{1}", path, "aero_move.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEALL);
                }

                x = string.Format(@"{0}\{1}", path, "aero_nesw.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENESW);
                }

                x = string.Format(@"{0}\{1}", path, "aero_ns.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENS);
                }

                x = string.Format(@"{0}\{1}", path, "aero_nwse.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE);
                }

                x = string.Format(@"{0}\{1}", path, "aero_ew.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEWE);
                }

                x = string.Format(@"{0}\{1}", path, "aero_up.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_UP);
                }

                x = string.Format(@"{0}\{1}", path, "aero_busy.ani");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_WAIT);
                }

                SystemParametersInfo(TreeView, SPI.SPI_SETCURSORS, 0, 0, SPIF.SPIF_UPDATEINIFILE);
            }

            catch (Exception ex)
            {
                if (MsgBox(Program.Lang.TM_RestoreCursorsError, MessageBoxButtons.OKCancel, MessageBoxIcon.Error, Program.Lang.TM_RestoreCursorsErrorPressOK, "", "", "", "", Program.Lang.TM_RestoreCursorsTip, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.OK)
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
                            var rx = Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors\Schemes", true);
                            rx.DeleteValue("WinPaletter", false);
                            rx.Close();
                        }
                    }
                    finally
                    {
                        Registry.CurrentUser.Close();
                    }
                }

                EditReg(scopeReg + @"\Control Panel\Cursors", "", "Windows Default", RegistryValueKind.String);
                EditReg(scopeReg + @"\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord);
                EditReg(scopeReg + @"\Control Panel\Cursors", "Scheme Source", 2, RegistryValueKind.DWord);

                string x = "";
                EditReg(scopeReg + @"\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_NORMAL);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_CROSS);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_HAND);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_HELP);

                EditReg(scopeReg + @"\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_IBEAM);

                EditReg(scopeReg + @"\Control Panel\Cursors", "No", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_NO);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEALL);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENESW);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENS);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_SIZEWE);

                EditReg(scopeReg + @"\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_UP);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
                SetSystemCursor(TreeView, x, OCR_SYSTEM_CURSORS.OCR_WAIT);

                SystemParametersInfo(TreeView, SPI.SPI_SETCURSORS, 0, 0, SPIF.SPIF_UPDATEINIFILE);
            }

            catch (Exception ex)
            {

                if (MsgBox(Program.Lang.TM_RestoreCursorsError, MessageBoxButtons.OKCancel, MessageBoxIcon.Error, Program.Lang.TM_RestoreCursorsErrorPressOK, "", "", "", "", Program.Lang.TM_RestoreCursorsTip, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.OK)
                    Forms.BugReport.ThrowError(ex);
            }
        }
    }
}