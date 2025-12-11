using System;
using System.Collections.Generic;

namespace WinPaletter.GlobalVariables
{
    /// <summary>
    /// Lists of exceptions thrown during theme applying and loading
    /// </summary>
    public static class Exceptions
    {
        /// <summary>
        /// List of exceptions thrown during theme applying
        /// </summary>
        public static List<Tuple<string, Exception>> ThemeApply = [];

        /// <summary>
        /// List of exceptions thrown during theme loading
        /// </summary>
        public static List<Tuple<string, Exception>> ThemeLoad = [];
    }
}
