using System;
using System.Collections.Generic;

namespace WinPaletter.GlobalVariables
{
    public static class Exceptions
    {
        /// <summary>
        /// List of exceptions thrown during theme applying
        /// </summary>
        public static List<Tuple<string, Exception>> ThemeApply = new();

        /// <summary>
        /// List of exceptions thrown during theme loading
        /// </summary>
        public static List<Tuple<string, Exception>> ThemeLoad = new();
    }
}
