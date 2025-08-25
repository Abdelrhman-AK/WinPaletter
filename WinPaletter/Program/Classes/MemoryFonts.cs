using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    /// <summary>
    /// MemoryFonts class; used to load fonts from resources into memory
    /// </summary>
    public static class MemoryFonts
    {
        private static PrivateFontCollection Pfc { get; set; }

        static MemoryFonts()
        {
            Pfc ??= new();
        }

        /// <summary>
        /// Add a font from a resource byte array to the memory font collection
        /// </summary>
        /// <param name="fontResource"></param>
        public static void AddMemoryFont(byte[] fontResource)
        {
            IntPtr p;
            uint c = 0U;
            p = Marshal.AllocCoTaskMem(fontResource.Length);
            Marshal.Copy(fontResource, 0, p, fontResource.Length);
            GDI32.AddFontMemResourceEx(p, (uint)fontResource.Length, IntPtr.Zero, ref c);
            Pfc.AddMemoryFont(p, fontResource.Length);
            Marshal.FreeCoTaskMem(p);
        }

        /// <summary>
        /// Get a font from the memory font collection
        /// </summary>
        /// <param name="fontIndex"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontStyle"></param>
        /// <returns></returns>
        public static Font GetFont(int fontIndex, float fontSize = 20f, FontStyle fontStyle = FontStyle.Regular)
        {
            return new Font(Pfc.Families[fontIndex], fontSize, fontStyle);
        }

    }
}