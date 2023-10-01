using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace WinPaletter
{

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class LogFont
    {
        public int lfHeight;
        public int lfWidth;
        public int lfEscapement;
        public int lfOrientation;
        public int lfWeight;
        public byte lfItalic;
        public byte lfUnderline;
        public byte lfStrikeOut;
        public byte lfCharSet;
        public byte lfOutPrecision;
        public byte lfClipPrecision;
        public byte lfQuality;
        public byte lfPitchAndFamily;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string lfFaceName;

        public LogFont(string lfFaceName = null)
        {
            this.lfFaceName = lfFaceName;
            lfHeight = 0;
            lfWidth = 0;
            lfEscapement = 0;
            lfOrientation = 0;
            lfWeight = 0;
            lfItalic = 0;
            lfUnderline = 0;
            lfStrikeOut = 0;
            lfCharSet = 0;
            lfOutPrecision = 0;
            lfClipPrecision = 0;
            lfQuality = 0;
            lfPitchAndFamily = 0;
        }

    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct LogFontStr
    {
        public int LfHeight;
        public int LfWidth;
        public int LfEscapement;
        public int LfOrientation;
        public int LfWeight;
        public byte LfItalic;
        public byte LfUnderline;
        public byte LfStrikeOut;
        public byte LfCharSet;
        public byte LfOutPrecision;
        public byte LfClipPrecision;
        public byte LfQuality;
        public byte LfPitchAndFamily;
        // <see cref="UnmanagedType.ByValTStr"/> means that the string should be marshalled as an array of TCHAR embedded in the structure.
        // This implies that the font names can be no larger than <see cref="LF_FACESIZE"/> including the terminating '\0'. That works out to 31 characters.
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string lfFaceName; // = String.Empty

    }


    public class LogFontHelper
    {
        public enum FontWeight : int
        {
            FW_DONTCARE = 0,
            FW_THIN = 100,
            FW_EXTRALIGHT = 200,
            FW_LIGHT = 300,
            FW_NORMAL = 400,
            FW_MEDIUM = 500,
            FW_SEMIBOLD = 600,
            FW_BOLD = 700,
            FW_EXTRABOLD = 800,
            FW_HEAVY = 900
        }

        public enum FontCharSet : byte
        {
            ANSI_CHARSET = 0,
            DEFAULT_CHARSET = 1,
            SYMBOL_CHARSET = 2,
            SHIFTJIS_CHARSET = 128,
            HANGEUL_CHARSET = 129,
            HANGUL_CHARSET = 129,
            GB2312_CHARSET = 134,
            CHINESEBIG5_CHARSET = 136,
            OEM_CHARSET = 255,
            JOHAB_CHARSET = 130,
            HEBREW_CHARSET = 177,
            ARABIC_CHARSET = 178,
            GREEK_CHARSET = 161,
            TURKISH_CHARSET = 162,
            VIETNAMESE_CHARSET = 163,
            THAI_CHARSET = 222,
            EASTEUROPE_CHARSET = 238,
            RUSSIAN_CHARSET = 204,
            MAC_CHARSET = 77,
            BALTIC_CHARSET = 186
        }

        public enum FontPrecision : byte
        {
            OUT_DEFAULT_PRECIS = 0,
            OUT_STRING_PRECIS = 1,
            OUT_CHARACTER_PRECIS = 2,
            OUT_STROKE_PRECIS = 3,
            OUT_TT_PRECIS = 4,
            OUT_DEVICE_PRECIS = 5,
            OUT_RASTER_PRECIS = 6,
            OUT_TT_ONLY_PRECIS = 7,
            OUT_OUTLINE_PRECIS = 8,
            OUT_SCREEN_OUTLINE_PRECIS = 9,
            OUT_PS_ONLY_PRECIS = 10
        }

        public enum FontClipPrecision : byte
        {
            CLIP_DEFAULT_PRECIS = 0,
            CLIP_CHARACTER_PRECIS = 1,
            CLIP_STROKE_PRECIS = 2,
            CLIP_MASK = 0xF,
            CLIP_LH_ANGLES = 1 << 4,
            CLIP_TT_ALWAYS = 2 << 4,
            CLIP_DFA_DISABLE = 4 << 4,
            CLIP_EMBEDDED = 8 << 4
        }

        public enum FontQuality : byte
        {
            DEFAULT_QUALITY = 0,
            DRAFT_QUALITY = 1,
            PROOF_QUALITY = 2,
            NONANTIALIASED_QUALITY = 3,
            ANTIALIASED_QUALITY = 4,
            CLEARTYPE_QUALITY = 5,
            CLEARTYPE_NATURAL_QUALITY = 6
        }

        public enum FontPitchAndFamily : byte
        {
            DEFAULT_PITCH = 0,
            FIXED_PITCH = 1,
            VARIABLE_PITCH = 2,
            FF_DONTCARE = 0 << 4,
            FF_ROMAN = 1 << 4,
            FF_SWISS = 2 << 4,
            FF_MODERN = 3 << 4,
            FF_SCRIPT = 4 << 4,
            FF_DECORATIVE = 5 << 4
        }

    }

    static class LogFontHelpers
    {

        public static LogFont ToLogFont(this byte[] fontBytes)
        {
            var lOGFONT = new LogFont()
            {
                lfHeight = BitConverter.ToInt32(fontBytes, 0),
                lfWidth = 0,
                lfEscapement = 0,
                lfOrientation = 0,
                lfWeight = BitConverter.ToInt32(fontBytes, 16),
                lfItalic = fontBytes[20],
                lfUnderline = fontBytes[21],
                lfStrikeOut = fontBytes[22],
                lfCharSet = fontBytes[23],
                lfOutPrecision = fontBytes[24],
                lfClipPrecision = fontBytes[25],
                lfQuality = fontBytes[26]
            };
            lOGFONT.lfClipPrecision = fontBytes[27];
            byte[] array = new byte[64];

            for (int i = 0; i <= 64 - 1; i++)
                array[i] = fontBytes[i + 28];

            lOGFONT.lfFaceName = Encoding.Unicode.GetString(array).TrimEnd(null);
            return lOGFONT;
        }

        public static Font ToFont(this byte[] fontBytes)
        {
            return Font.FromLogFont(fontBytes.ToLogFont());
        }

        public static byte[] ToByte(this LogFont lOGFONT)
        {
            byte[] b = new byte[92];

            for (int x = 0; x <= 3; x += +1)
                b[x] = BitConverter.GetBytes(lOGFONT.lfHeight)[x];

            for (int x = 4; x <= 15; x += +1)
                b[x] = 0;

            for (int x = 16; x <= 19; x += +1)
                b[x] = BitConverter.GetBytes(lOGFONT.lfWeight)[x - 16];

            b[20] = lOGFONT.lfItalic;
            b[21] = lOGFONT.lfUnderline;
            b[22] = lOGFONT.lfStrikeOut;
            b[23] = lOGFONT.lfCharSet;
            b[24] = lOGFONT.lfOutPrecision;
            b[25] = lOGFONT.lfClipPrecision;
            b[26] = lOGFONT.lfQuality;
            b[27] = lOGFONT.lfClipPrecision;

            int i = 0;

            foreach (byte x in Encoding.Unicode.GetBytes(lOGFONT.lfFaceName))
            {
                b[28 + i] = x;
                i += 1;
            }

            return b;
        }

        public static byte[] ToByte(this Font Font)
        {
            var LF = new LogFont();
            Font.ToLogFont(LF);
            return LF.ToByte();
        }

    }
}