using System;
using System.Collections.Generic;
using System.Linq;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extension methods for <see cref="int"/> types
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Return string in the format of XXXXXXXX, useful for registry handling
        /// </summary>
        public static string ToStringDWord(this int @int)
        {
            if (@int.ToString().Count() <= 8)
            {
                int i = 8 - @int.ToString().Count();
                string s = string.Empty;
                int loopTo = i;

                // If the number is less than 8 digits, add 0s to the left
                for (i = 1; i <= loopTo; i++) s += "0";

                s += @int.ToString();
                return s.Replace("-", string.Empty);
            }
            else
            {
                return @int.ToString().Replace("-", string.Empty);
            }
        }

        /// <summary>
        /// Return string in the format of XXXXXXXXXXXXXXXX, useful for registry handling
        /// </summary>
        public static string ToStringQWord(this int @int)
        {
            if (@int.ToString().Count() <= 16)
            {
                int i = 16 - @int.ToString().Count();
                string s = string.Empty;
                int loopTo = i;

                // If the number is less than 16 digits, add 0s to the left
                for (i = 1; i <= loopTo; i++) s += "0";

                s += @int.ToString();
                return s.Replace("-", string.Empty);
            }
            else
            {
                return @int.ToString().Replace("-", string.Empty);
            }
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a File, YY is the appropriate size unit
        /// </summary>
        public static string ToStringFileSize(this long length, bool ShowSecondUnit = false)
        {
            long KB = 1024L;
            long MB = KB * 1024L;
            long GB = MB * 1024L;
            long TB = GB * 1024L;
            float size = length;
            string suffix = Program.Localization.Strings.General.ByteSizeUnit;

            if (length >= TB)
            {
                size = (float)Math.Round(length / (float)TB, 2);
                suffix = Program.Localization.Strings.General.TBSizeUnit;
            }

            else if (length >= GB)
            {
                size = (float)Math.Round(length / (float)GB, 2);
                suffix = Program.Localization.Strings.General.GBSizeUnit;
            }

            else if (length >= MB)
            {
                size = (float)Math.Round(length / (float)MB, 2);
                suffix = Program.Localization.Strings.General.MBSizeUnit;
            }

            else if (length >= KB)
            {
                size = (float)Math.Round(length / (float)KB, 2);
                suffix = Program.Localization.Strings.General.KBSizeUnit;

            }

            if (ShowSecondUnit)
                suffix += Program.Localization.Strings.General.SecondUnit;

            return $"{size} {suffix}";
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a File, YY is the appropriate size unit
        /// </summary>
        public static string ToStringFileSize(this short length, bool ShowSecondUnit = false)
        {
            return ((long)length).ToStringFileSize(ShowSecondUnit);
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a File, YY is the appropriate size unit
        /// </summary>
        public static string ToStringFileSize(this float length, bool ShowSecondUnit = false)
        {
            return ((long)Math.Round(length)).ToStringFileSize(ShowSecondUnit);
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a File, YY is the appropriate size unit
        /// </summary>
        public static string ToStringFileSize(this double length, bool ShowSecondUnit = false)
        {
            return ((long)Math.Round(length)).ToStringFileSize(ShowSecondUnit);
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a File, YY is the appropriate size unit
        /// </summary>
        public static string ToStringFileSize(this decimal length, bool ShowSecondUnit = false)
        {
            return ((long)Math.Round(length)).ToStringFileSize(ShowSecondUnit);
        }

        /// <summary>
        /// Return string in the format of XX.XX YY, where XX.XX is the size of a File, YY is the appropriate size unit
        /// </summary>
        public static string ToStringFileSize(this int length, bool ShowSecondUnit = false)
        {
            return ((long)length).ToStringFileSize(ShowSecondUnit);
        }

        /// <summary>
        /// Random number generator.
        /// </summary>
        private static readonly Random StaticRandom = new();

        /// <summary>
        /// Generate a list of unique random numbers.
        /// </summary>
        /// <param name="int"></param>
        /// <param name="count">Count of the generated numbers</param>
        /// <returns></returns>
        public static List<int> GetUniqueRandomNumbers(this int @int, int count)
        {
            lock (StaticRandom) return [.. Enumerable.Range(@int, count).OrderBy(__ => StaticRandom.Next())];
        }
    }

}
