using System;
using System.Linq;

namespace WinPaletter.TypesExtensions
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Return string in the format of XXXXXXXX, useful for registry handling
        /// </summary>
        public static string DWORD(this int @int)
        {
            if (@int.ToString().Count() <= 8)
            {
                int i = 8 - @int.ToString().Count();
                string s = "";
                var loopTo = i;
                for (i = 1; i <= loopTo; i++)
                    s += "0";
                s += @int.ToString();
                return s.Replace("-", "");
            }
            else
            {
                return @int.ToString().Replace("-", "");
            }

        }

        /// <summary>
        /// Return string in the format of XXXXXXXXXXXXXXXX, useful for registry handling
        /// </summary>
        public static string QWORD(this int @int)
        {
            if (@int.ToString().Count() <= 16)
            {
                int i = 16 - @int.ToString().Count();
                string s = "";
                var loopTo = i;
                for (i = 1; i <= loopTo; i++)
                    s += "0";
                s += @int.ToString();
                return s.Replace("-", "");
            }
            else
            {
                return @int.ToString().Replace("-", "");
            }

        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
        /// </summary>
        public static string SizeString(this long length, bool ShowSecondUnit = false)
        {
            long KB = 1024L;
            long MB = KB * 1024L;
            long GB = MB * 1024L;
            long TB = GB * 1024L;
            double size = length;
            string suffix = Program.Lang.ByteSizeUnit;

            if (length >= TB)
            {
                size = Math.Round(length / (double)TB, 2);
                suffix = Program.Lang.TBSizeUnit;
            }

            else if (length >= GB)
            {
                size = Math.Round(length / (double)GB, 2);
                suffix = Program.Lang.GBSizeUnit;
            }

            else if (length >= MB)
            {
                size = Math.Round(length / (double)MB, 2);
                suffix = Program.Lang.MBSizeUnit;
            }

            else if (length >= KB)
            {
                size = Math.Round(length / (double)KB, 2);
                suffix = Program.Lang.KBSizeUnit;

            }

            if (ShowSecondUnit)
                suffix += Program.Lang.SecondUnit;

            return $"{size} {suffix}";
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
        /// </summary>
        public static string SizeString(this short length, bool ShowSecondUnit = false)
        {
            return ((long)length).SizeString(ShowSecondUnit);
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
        /// </summary>
        public static string SizeString(this float length, bool ShowSecondUnit = false)
        {
            return ((long)Math.Round(length)).SizeString(ShowSecondUnit);
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
        /// </summary>
        public static string SizeString(this double length, bool ShowSecondUnit = false)
        {
            return ((long)Math.Round(length)).SizeString(ShowSecondUnit);
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
        /// </summary>
        public static string SizeString(this decimal length, bool ShowSecondUnit = false)
        {
            return ((long)Math.Round(length)).SizeString(ShowSecondUnit);
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
        /// </summary>
        public static string SizeString(this int length, bool ShowSecondUnit = false)
        {
            return ((long)length).SizeString(ShowSecondUnit);
        }
    }

}
