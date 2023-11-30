using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace WinPaletter
{

    public static class MemoryFonts
    {
        private static PrivateFontCollection Pfc { get; set; }

        static MemoryFonts()
        {
            if (Pfc is null)
                Pfc = new();
        }

        public static void AddMemoryFont(byte[] fontResource)
        {
            IntPtr p;
            uint c = 0U;
            p = Marshal.AllocCoTaskMem(fontResource.Length);
            Marshal.Copy(fontResource, 0, p, fontResource.Length);
            NativeMethods.GDI32.AddFontMemResourceEx(p, (uint)fontResource.Length, IntPtr.Zero, ref c);
            Pfc.AddMemoryFont(p, fontResource.Length);
            Marshal.FreeCoTaskMem(p);
        }

        public static Font GetFont(int fontIndex, float fontSize = 20f, FontStyle fontStyle = FontStyle.Regular)
        {
            return new Font(Pfc.Families[fontIndex], fontSize, fontStyle);
        }

    }
}