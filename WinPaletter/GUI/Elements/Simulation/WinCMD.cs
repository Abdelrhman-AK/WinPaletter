using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.Simulation
{

    [Description("A simulated Windows Command Prompt/PS")]
    [DefaultEvent("Click")]
    public class WinCMD : ContainerControl
    {

        public WinCMD()
        {
            Text = "";
            DoubleBuffered = true;
        }

        #region Variables

        private readonly string S1 = "(c) Microsoft Corporation. All rights reserved.";
        private readonly string S2 = My.Env.PATH_System32 + ">";
        private readonly string CV = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";

        public enum Raster_Sizes
        {
            _4x6,
            _6x8,
            _8x8,
            _16x8,
            _5x12,
            _7x12,
            _8x12,
            _16x12,
            _12x16,
            _10x18
        }

        #endregion

        #region Properties

        public Color CMD_ColorTable00 { get; set; }
        public Color CMD_ColorTable01 { get; set; }
        public Color CMD_ColorTable02 { get; set; }
        public Color CMD_ColorTable03 { get; set; }
        public Color CMD_ColorTable04 { get; set; }
        public Color CMD_ColorTable05 { get; set; }
        public Color CMD_ColorTable06 { get; set; }
        public Color CMD_ColorTable07 { get; set; }
        public Color CMD_ColorTable08 { get; set; }
        public Color CMD_ColorTable09 { get; set; }
        public Color CMD_ColorTable10 { get; set; }
        public Color CMD_ColorTable11 { get; set; }
        public Color CMD_ColorTable12 { get; set; }
        public Color CMD_ColorTable13 { get; set; }
        public Color CMD_ColorTable14 { get; set; }
        public Color CMD_ColorTable15 { get; set; }
        public int CMD_ScreenColorsForeground { get; set; } = 7;
        public int CMD_ScreenColorsBackground { get; set; } = 0;
        public int CMD_PopupForeground { get; set; } = 15;
        public int CMD_PopupBackground { get; set; } = 5;
        public bool PowerShell { get; set; } = false;
        public bool Raster { get; set; } = true;
        public Raster_Sizes RasterSize { get; set; } = Raster_Sizes._8x12;
        public bool CustomTerminal { get; set; } = false;

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;
            DoubleBuffered = true;

            var Rect = new Rectangle(0, 0, Width - 1, Height - 1);
            var RectCMD = new Rectangle(Rect.X + 1, Rect.Y + 5, Rect.Width - 2, Rect.Height - 10);

            int pW0, pH0, pX0, pY0;
            pW0 = (int)Math.Round(240f * (Font.Size / 18f));
            pH0 = (int)Math.Round(54f * (Font.Size / 18f));
            pX0 = (int)Math.Round(5f * (Font.Size / 18f));
            pY0 = (int)Math.Round(10f * (Font.Size / 18f));

            var RectMiddle = new Rectangle((int)Math.Round(Rect.X + (Rect.Width - pW0) / 2d), (int)Math.Round(Rect.Y + (Rect.Height - pH0) / 2d), pW0, pH0);
            var RectMiddleBorder = new Rectangle(RectMiddle.X + pX0, RectMiddle.Y + pY0, RectMiddle.Width - pX0 * 2, RectMiddle.Height - pY0 * 2);

            Color FC = default, BK = default, PCF = default, PCB = default;
            string S;

            var F = Font;

            if (!Raster)
            {
                if (!PowerShell)
                {
                    F = new Font(Font.Name, (double)Font.SizeInPoints * 0.57d <= 0d ? 1f : (float)((double)Font.Size * 0.57d), Font.Style);
                }
                else
                {
                    F = new Font(Font.Name, (double)Font.SizeInPoints * 0.57d <= 0d ? 1f : (float)((double)Font.Size * 0.57d), Font.Style);
                }
            }

            switch (CMD_ScreenColorsForeground)
            {
                case 0:
                    {
                        if (CMD_ScreenColorsForeground == 0 & CMD_ScreenColorsBackground == 0)
                        {
                            FC = CMD_ColorTable07;
                        }
                        else
                        {
                            FC = CMD_ColorTable00;
                        }

                        break;
                    }
                case 1:
                    {
                        FC = CMD_ColorTable01;
                        break;
                    }
                case 2:
                    {
                        FC = CMD_ColorTable02;
                        break;
                    }
                case 3:
                    {
                        FC = CMD_ColorTable03;
                        break;
                    }
                case 4:
                    {
                        FC = CMD_ColorTable04;
                        break;
                    }
                case 5:
                    {
                        FC = CMD_ColorTable05;
                        break;
                    }
                case 6:
                    {
                        FC = CMD_ColorTable06;
                        break;
                    }
                case 7:
                    {
                        FC = CMD_ColorTable07;
                        break;
                    }
                case 8:
                    {
                        FC = CMD_ColorTable08;
                        break;
                    }
                case 9:
                    {
                        FC = CMD_ColorTable09;
                        break;
                    }
                case 10:
                    {
                        FC = CMD_ColorTable10;
                        break;
                    }
                case 11:
                    {
                        FC = CMD_ColorTable11;
                        break;
                    }
                case 12:
                    {
                        FC = CMD_ColorTable12;
                        break;
                    }
                case 13:
                    {
                        FC = CMD_ColorTable13;
                        break;
                    }
                case 14:
                    {
                        FC = CMD_ColorTable14;
                        break;
                    }
                case 15:
                    {
                        FC = CMD_ColorTable15;
                        break;
                    }
            }

            switch (CMD_ScreenColorsBackground)
            {
                case 0:
                    {
                        BK = CMD_ColorTable00;
                        break;
                    }
                case 1:
                    {
                        BK = CMD_ColorTable01;
                        break;
                    }
                case 2:
                    {
                        BK = CMD_ColorTable02;
                        break;
                    }
                case 3:
                    {
                        BK = CMD_ColorTable03;
                        break;
                    }
                case 4:
                    {
                        BK = CMD_ColorTable04;
                        break;
                    }
                case 5:
                    {
                        BK = CMD_ColorTable05;
                        break;
                    }
                case 6:
                    {
                        BK = CMD_ColorTable06;
                        break;
                    }
                case 7:
                    {
                        BK = CMD_ColorTable07;
                        break;
                    }
                case 8:
                    {
                        BK = CMD_ColorTable08;
                        break;
                    }
                case 9:
                    {
                        BK = CMD_ColorTable09;
                        break;
                    }
                case 10:
                    {
                        BK = CMD_ColorTable10;
                        break;
                    }
                case 11:
                    {
                        BK = CMD_ColorTable11;
                        break;
                    }
                case 12:
                    {
                        BK = CMD_ColorTable12;
                        break;
                    }
                case 13:
                    {
                        BK = CMD_ColorTable13;
                        break;
                    }
                case 14:
                    {
                        BK = CMD_ColorTable14;
                        break;
                    }
                case 15:
                    {
                        BK = CMD_ColorTable15;
                        break;
                    }
            }

            switch (CMD_PopupForeground)
            {
                case 0:
                    {
                        PCF = CMD_ColorTable00;
                        break;
                    }
                case 1:
                    {
                        PCF = CMD_ColorTable01;
                        break;
                    }
                case 2:
                    {
                        PCF = CMD_ColorTable02;
                        break;
                    }
                case 3:
                    {
                        PCF = CMD_ColorTable03;
                        break;
                    }
                case 4:
                    {
                        PCF = CMD_ColorTable04;
                        break;
                    }
                case 5:
                    {
                        PCF = CMD_ColorTable05;
                        break;
                    }
                case 6:
                    {
                        PCF = CMD_ColorTable06;
                        break;
                    }
                case 7:
                    {
                        PCF = CMD_ColorTable07;
                        break;
                    }
                case 8:
                    {
                        PCF = CMD_ColorTable08;
                        break;
                    }
                case 9:
                    {
                        PCF = CMD_ColorTable09;
                        break;
                    }
                case 10:
                    {
                        PCF = CMD_ColorTable10;
                        break;
                    }
                case 11:
                    {
                        PCF = CMD_ColorTable11;
                        break;
                    }
                case 12:
                    {
                        PCF = CMD_ColorTable12;
                        break;
                    }
                case 13:
                    {
                        PCF = CMD_ColorTable13;
                        break;
                    }
                case 14:
                    {
                        PCF = CMD_ColorTable14;
                        break;
                    }
                case 15:
                    {
                        PCF = CMD_ColorTable15;
                        break;
                    }
            }

            switch (CMD_PopupBackground)
            {
                case 0:
                    {
                        PCB = CMD_ColorTable00;
                        break;
                    }
                case 1:
                    {
                        PCB = CMD_ColorTable01;
                        break;
                    }
                case 2:
                    {
                        PCB = CMD_ColorTable02;
                        break;
                    }
                case 3:
                    {
                        PCB = CMD_ColorTable03;
                        break;
                    }
                case 4:
                    {
                        PCB = CMD_ColorTable04;
                        break;
                    }
                case 5:
                    {
                        PCB = CMD_ColorTable05;
                        break;
                    }
                case 6:
                    {
                        PCB = CMD_ColorTable06;
                        break;
                    }
                case 7:
                    {
                        PCB = CMD_ColorTable07;
                        break;
                    }
                case 8:
                    {
                        PCB = CMD_ColorTable08;
                        break;
                    }
                case 9:
                    {
                        PCB = CMD_ColorTable09;
                        break;
                    }
                case 10:
                    {
                        PCB = CMD_ColorTable10;
                        break;
                    }
                case 11:
                    {
                        PCB = CMD_ColorTable11;
                        break;
                    }
                case 12:
                    {
                        PCB = CMD_ColorTable12;
                        break;
                    }
                case 13:
                    {
                        PCB = CMD_ColorTable13;
                        break;
                    }
                case 14:
                    {
                        PCB = CMD_ColorTable14;
                        break;
                    }
                case 15:
                    {
                        PCB = CMD_ColorTable15;
                        break;
                    }
            }

            BackColor = BK;
            G.Clear(BK);

            if (!CustomTerminal)
            {
                if (!PowerShell)
                {
                    string sx = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Replace("Microsoft Windows ", "");
                    sx = sx.Replace("S", "").Trim();

                    string sy = "." + Microsoft.Win32.Registry.GetValue(CV, "UBR", 0).ToString();
                    if (sy == ".0")
                        sy = "";

                    S = string.Format("Microsoft Windows [Version {0}{1}]", sx, sy) + "\r\n" + S1 + "\r\n" + "\r\n" + S2;
                }

                else
                {
                    S = "Windows PowerShell" + "\r\n" + S1 + "\r\n" + "\r\n" + "Install the latest PowerShell for new features and improvements! https://aka.ms/PSWindows" + "\r\n" + "\r\n" + "PS " + S2;
                }
            }
            else
            {
                S = My.Env.Lang.CMDSimulator_Alert0 + "\r\n" + "\r\n" + S2;
            }


            if (Raster)
            {
                S += "\r\n" + "\r\n" + My.Env.Lang.CMDSimulator_Alert1;
            }

            if (!Raster)
            {
                using (var br = new SolidBrush(FC))
                {
                    G.DrawString(S, F, br, RectCMD.Location);
                }

                using (var br = new SolidBrush(PCB))
                {
                    G.FillRectangle(br, RectMiddle);
                }
                using (var P = new Pen(PCF))
                {
                    G.DrawRectangle(P, RectMiddleBorder);
                }

                using (var br = new SolidBrush(PCF))
                {
                    G.DrawString(My.Env.Lang.CMDSimulator_ThisIsAPopUp, F, br, RectMiddleBorder, ContentAlignment.MiddleCenter.ToStringFormat());
                }
            }

            else
            {
                Bitmap i0, i1;
                int pW, pH, pX, pY;

                switch (RasterSize)
                {
                    case Raster_Sizes._4x6:
                        {
                            if (!PowerShell)
                                i0 = My.Resources.CMD_4x6;
                            else
                                i0 = My.Resources.PS_4x6;
                            i1 = My.Resources.CMD_4x6_P;
                            pW = 120;
                            pH = 18;
                            pX = 2;
                            pY = 3;
                            break;
                        }

                    case Raster_Sizes._6x8:
                        {
                            if (!PowerShell)
                                i0 = My.Resources.CMD_6x8;
                            else
                                i0 = My.Resources.PS_6x8;
                            i1 = My.Resources.CMD_6x8_P;
                            pW = 180;
                            pH = 24;
                            pX = 3;
                            pY = 4;
                            break;
                        }

                    case Raster_Sizes._8x8:
                        {
                            if (!PowerShell)
                                i0 = My.Resources.CMD_8x8;
                            else
                                i0 = My.Resources.PS_8x8;
                            i1 = My.Resources.CMD_8x8_P;
                            pW = 240;
                            pH = 24;
                            pX = 4;
                            pY = 4;
                            break;
                        }

                    case Raster_Sizes._16x8:
                        {
                            if (!PowerShell)
                                i0 = My.Resources.CMD_16x8;
                            else
                                i0 = My.Resources.PS_16x8;
                            i1 = My.Resources.CMD_16x8_P;
                            pW = 480;
                            pH = 24;
                            pX = 8;
                            pY = 4;
                            break;
                        }

                    case Raster_Sizes._5x12:
                        {
                            if (!PowerShell)
                                i0 = My.Resources.CMD_5x12;
                            else
                                i0 = My.Resources.PS_5x12;
                            i1 = My.Resources.CMD_5x12_P;
                            pW = 150;
                            pH = 36;
                            pX = 3;
                            pY = 6;
                            break;
                        }

                    case Raster_Sizes._7x12:
                        {
                            if (!PowerShell)
                                i0 = My.Resources.CMD_7x12;
                            else
                                i0 = My.Resources.PS_7x12;
                            i1 = My.Resources.CMD_7x12_P;
                            pW = 210;
                            pH = 36;
                            pX = 4;
                            pY = 6;
                            break;
                        }

                    case Raster_Sizes._8x12:
                        {
                            if (!PowerShell)
                                i0 = My.Resources.CMD_8x12;
                            else
                                i0 = My.Resources.PS_8x12;
                            i1 = My.Resources.CMD_8x12_P;
                            pW = 240;
                            pH = 36;
                            pX = 4;
                            pY = 6;
                            break;
                        }

                    case Raster_Sizes._16x12:
                        {
                            if (!PowerShell)
                                i0 = My.Resources.CMD_16x12;
                            else
                                i0 = My.Resources.PS_16x12;
                            i1 = My.Resources.CMD_16x12_P;
                            pW = 480;
                            pH = 36;
                            pX = 8;
                            pY = 6;
                            break;
                        }

                    case Raster_Sizes._12x16:
                        {
                            if (!PowerShell)
                                i0 = My.Resources.CMD_12x16;
                            else
                                i0 = My.Resources.PS_12x16;
                            i1 = My.Resources.CMD_12x16_P;
                            pW = 360;
                            pH = 48;
                            pX = 6;
                            pY = 8;
                            break;
                        }

                    case Raster_Sizes._10x18:
                        {
                            if (!PowerShell)
                                i0 = My.Resources.CMD_10x18;
                            else
                                i0 = My.Resources.PS_10x18;
                            i1 = My.Resources.CMD_10x18_P;
                            pW = 300;
                            pH = 54;
                            pX = 8;
                            pY = 9;
                            break;
                        }

                    default:
                        {
                            if (!PowerShell)
                                i0 = My.Resources.CMD_8x12;
                            else
                                i0 = My.Resources.PS_8x12;
                            i1 = My.Resources.CMD_8x12_P;
                            pW = 240;
                            pH = 36;
                            pX = 4;
                            pY = 6;
                            break;
                        }

                }

                G.DrawImage(i0.ReplaceColor(Color.FromArgb(204, 204, 204), FC), new Point(0, 1));

                RectMiddle = new Rectangle((int)Math.Round(Rect.X + (Rect.Width - pW) / 2d), (int)Math.Round(Rect.Y + (Rect.Height - 36) / 2d), pW, pH);
                RectMiddleBorder = new Rectangle(RectMiddle.X + pX, RectMiddle.Y + pY, RectMiddle.Width - pX * 2, RectMiddle.Height - pY * 2);

                using (var br = new SolidBrush(PCB))
                {
                    G.FillRectangle(br, RectMiddle);
                }
                using (var P = new Pen(PCF))
                {
                    G.DrawRectangle(P, RectMiddleBorder);
                }


                G.DrawImage(i1.ReplaceColor(Color.FromArgb(204, 204, 204), PCF), new Point((int)Math.Round(RectMiddle.X + (RectMiddle.Width - i1.Width) / 2d), (int)Math.Round(RectMiddle.Y + (RectMiddle.Height - i1.Height) / 2d)));

            }
        }

    }

}