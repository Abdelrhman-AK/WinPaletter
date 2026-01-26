using Microsoft.Win32;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.Assets;

namespace WinPaletter.UI.Simulation
{
    [Description("A simulated Windows Command Prompt/PS")]
    [DefaultEvent("Click")]
    public class WinCMD : Control
    {
        public WinCMD()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
            Text = string.Empty;
            DoubleBuffered = true;
            BackColor = Color.Transparent;
        }

        #region Variables

        private readonly string S1 = "(c) Microsoft Corporation. All rights reserved.";
        private readonly string S2 = $"{SysPaths.System32}>";
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

        private int alpha = 255;
        public int Alpha
        {
            get => alpha;
            set
            {
                if (alpha != value)
                {
                    if (value > 255) alpha = 255;
                    else if (value < 0) alpha = 0;
                    else alpha = value;
                    Invalidate();
                }
            }
        }

        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            Rectangle Rect = new(0, 0, Width - 1, Height - 1);
            Rectangle RectCMD = new(Rect.X + 1, Rect.Y + 5, Rect.Width - 2, Rect.Height - 10);

            float pW0, pH0, pX0, pY0;
            pW0 = 240f * (Font.Size / 18f);
            pH0 = 54f * (Font.Size / 18f);
            pX0 = 5f * (Font.Size / 18f);
            pY0 = 10f * (Font.Size / 18f);

            RectangleF RectMiddle = new(Rect.X + (Rect.Width - pW0) / 2f, Rect.Y + (Rect.Height - pH0) / 2f, pW0, pH0);
            RectangleF RectMiddleBorder = new(RectMiddle.X + pX0, RectMiddle.Y + pY0, RectMiddle.Width - pX0 * 2, RectMiddle.Height - pY0 * 2);

            Color FC = default, BK = default, PCF = default, PCB = default;
            string S;

            Font F = Font;
            if (!Raster)
            {
                F = new(Font.Name, Font.Size - 1.7f, Font.Style, GraphicsUnit.Pixel);
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

            using (SolidBrush br = new(Color.FromArgb(alpha, BK)))
            {
                G.FillRectangle(br, new Rectangle(-1, -1, Width + 2, Height + 2));
            }

            if (!CustomTerminal)
            {
                if (!PowerShell)
                {
                    string sx = RuntimeInformation.OSDescription.Replace("Microsoft Windows ", string.Empty);
                    sx = sx.Replace("S", string.Empty).Trim();

                    string sy = $".{Registry.GetValue(CV, "UBR", 0)}";
                    if (sy == ".0")
                        sy = string.Empty;

                    S = $"{$"Microsoft Windows [Version {sx}{sy}]"}\r\n{S1}\r\n\r\n{S2}";
                }

                else
                {
                    S = $"Windows PowerShell\r\n{S1}\r\n\r\nInstall the latest PowerShell for new features and improvements! https://aka.ms/PSWindows\r\n\r\nPS {S2}";
                }
            }
            else
            {
                S = $"{Program.Localization.Strings.Aspects.Consoles.CMDSimulator_Alert0}\r\n\r\n{S2}";
            }


            if (Raster)
            {
                S += $"\r\n\r\n{Program.Localization.Strings.Aspects.Consoles.CMDSimulator_Alert1}";
            }

            if (!Raster)
            {
                using (SolidBrush br = new(Color.FromArgb(alpha, FC)))
                {
                    G.DrawString(S, F, br, RectCMD.Location);
                }

                using (SolidBrush br = new(Color.FromArgb(alpha, PCB)))
                {
                    G.FillRectangle(br, RectMiddle);
                }
                using (Pen P = new(Color.FromArgb(alpha, PCF)))
                {
                    G.DrawRectangle(P, RectMiddleBorder.X, RectMiddleBorder.Y, RectMiddleBorder.Width, RectMiddleBorder.Height);
                }

                using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                {
                    using (SolidBrush br = new(Color.FromArgb(alpha, PCF)))
                    {
                        G.DrawString(Program.Localization.Strings.Aspects.Consoles.CMDSimulator_ThisIsAPopUp, F, br, RectMiddleBorder, sf);
                    }
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
                                i0 = CMD_Raster.CMD_4x6;
                            else
                                i0 = PowerShell_Raster.PS_4x6;
                            i1 = CMD_Raster.CMD_4x6_P;
                            pW = 120;
                            pH = 18;
                            pX = 2;
                            pY = 3;
                            break;
                        }

                    case Raster_Sizes._6x8:
                        {
                            if (!PowerShell)
                                i0 = CMD_Raster.CMD_6x8;
                            else
                                i0 = PowerShell_Raster.PS_6x8;
                            i1 = CMD_Raster.CMD_6x8_P;
                            pW = 180;
                            pH = 24;
                            pX = 3;
                            pY = 4;
                            break;
                        }

                    case Raster_Sizes._8x8:
                        {
                            if (!PowerShell)
                                i0 = CMD_Raster.CMD_8x8;
                            else
                                i0 = PowerShell_Raster.PS_8x8;
                            i1 = CMD_Raster.CMD_8x8_P;
                            pW = 240;
                            pH = 24;
                            pX = 4;
                            pY = 4;
                            break;
                        }

                    case Raster_Sizes._16x8:
                        {
                            if (!PowerShell)
                                i0 = CMD_Raster.CMD_16x8;
                            else
                                i0 = PowerShell_Raster.PS_16x8;
                            i1 = CMD_Raster.CMD_16x8_P;
                            pW = 480;
                            pH = 24;
                            pX = 8;
                            pY = 4;
                            break;
                        }

                    case Raster_Sizes._5x12:
                        {
                            if (!PowerShell)
                                i0 = CMD_Raster.CMD_5x12;
                            else
                                i0 = PowerShell_Raster.PS_5x12;
                            i1 = CMD_Raster.CMD_5x12_P;
                            pW = 150;
                            pH = 36;
                            pX = 3;
                            pY = 6;
                            break;
                        }

                    case Raster_Sizes._7x12:
                        {
                            if (!PowerShell)
                                i0 = CMD_Raster.CMD_7x12;
                            else
                                i0 = PowerShell_Raster.PS_7x12;
                            i1 = CMD_Raster.CMD_7x12_P;
                            pW = 210;
                            pH = 36;
                            pX = 4;
                            pY = 6;
                            break;
                        }

                    case Raster_Sizes._8x12:
                        {
                            if (!PowerShell)
                                i0 = CMD_Raster.CMD_8x12;
                            else
                                i0 = PowerShell_Raster.PS_8x12;
                            i1 = CMD_Raster.CMD_8x12_P;
                            pW = 240;
                            pH = 36;
                            pX = 4;
                            pY = 6;
                            break;
                        }

                    case Raster_Sizes._16x12:
                        {
                            if (!PowerShell)
                                i0 = CMD_Raster.CMD_16x12;
                            else
                                i0 = PowerShell_Raster.PS_16x12;
                            i1 = CMD_Raster.CMD_16x12_P;
                            pW = 480;
                            pH = 36;
                            pX = 8;
                            pY = 6;
                            break;
                        }

                    case Raster_Sizes._12x16:
                        {
                            if (!PowerShell)
                                i0 = CMD_Raster.CMD_12x16;
                            else
                                i0 = PowerShell_Raster.PS_12x16;
                            i1 = CMD_Raster.CMD_12x16_P;
                            pW = 360;
                            pH = 48;
                            pX = 6;
                            pY = 8;
                            break;
                        }

                    case Raster_Sizes._10x18:
                        {
                            if (!PowerShell)
                                i0 = CMD_Raster.CMD_10x18;
                            else
                                i0 = PowerShell_Raster.PS_10x18;
                            i1 = CMD_Raster.CMD_10x18_P;
                            pW = 300;
                            pH = 54;
                            pX = 8;
                            pY = 9;
                            break;
                        }

                    default:
                        {
                            if (!PowerShell)
                                i0 = CMD_Raster.CMD_8x12;
                            else
                                i0 = PowerShell_Raster.PS_8x12;
                            i1 = CMD_Raster.CMD_8x12_P;
                            pW = 240;
                            pH = 36;
                            pX = 4;
                            pY = 6;
                            break;
                        }

                }

                using (Bitmap b = i0.ReplaceColor(Color.FromArgb(204, 204, 204), Color.FromArgb(alpha, FC))) { G.DrawImage(b, new Point(0, 1)); }

                RectMiddle = new(Rect.X + (Rect.Width - pW) / 2f, Rect.Y + (Rect.Height - 36) / 2f, pW, pH);
                RectMiddleBorder = new(RectMiddle.X + pX, RectMiddle.Y + pY, RectMiddle.Width - pX * 2, RectMiddle.Height - pY * 2);

                using (SolidBrush br = new(Color.FromArgb(alpha, PCB)))
                {
                    G.FillRectangle(br, RectMiddle);
                }
                using (Pen P = new(Color.FromArgb(alpha, PCF)))
                {
                    G.DrawRectangle(P, RectMiddleBorder.X, RectMiddleBorder.Y, RectMiddleBorder.Width, RectMiddleBorder.Height);
                }

                using (Bitmap b = i1.ReplaceColor(Color.FromArgb(204, 204, 204), Color.FromArgb(alpha, PCF)))
                {
                    G.DrawImageUnscaled(b, new Point((int)(RectMiddle.X + (RectMiddle.Width - i1.Width) / 2f), (int)(RectMiddle.Y + (RectMiddle.Height - i1.Height) / 2f)));
                }
            }

            //base.OnPaint(e);
        }
    }
}