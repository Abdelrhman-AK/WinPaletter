﻿using System.Drawing;

namespace WinPaletter.UI.Style
{
    /// <summary>
    /// Default colors for the dark and light themes.
    /// </summary>
    internal class DefaultColors
    {
        /// <summary>
        /// Default back color for the dark mode.
        /// </summary>
        public static readonly Color BackColor_Dark = Color.FromArgb(17, 17, 17);

        /// <summary>
        /// Default disabled back color for the dark mode.
        /// </summary>
        public static readonly Color DisabledBackColor_Dark = Color.FromArgb(60, 60, 60);

        /// <summary>
        /// Default primary color for the dark mode.
        /// </summary>
        public static readonly Color PrimaryColor_Dark = Color.FromArgb(0, 70, 175);

        /// <summary>
        /// Default secondary color for the dark mode.
        /// </summary>
        public static readonly Color SecondaryColor_Dark = Color.FromArgb(117, 0, 31);

        /// <summary>
        /// Default tertiary color for the dark mode.
        /// </summary>
        public static readonly Color TertiaryColor_Dark = Color.FromArgb(0, 51, 21);

        /// <summary>
        /// Default disabled color for the dark mode.
        /// </summary>
        public static readonly Color DisabledColor_Dark = Color.FromArgb(89, 89, 89);

        /// <summary>
        /// Default back color for the light mode.
        /// </summary>
        public static readonly Color BackColor_Light = Color.FromArgb(240, 240, 240);

        /// <summary>
        /// Default disabled back color for the light mode.
        /// </summary>
        public static readonly Color DisabledBackColor_Light = Color.FromArgb(210, 210, 210);

        /// <summary>
        /// Default primary color for the light mode.
        /// </summary>
        public static readonly Color PrimaryColor_Light = Color.FromArgb(0, 93, 234);

        /// <summary>
        /// Default secondary color for the light mode.
        /// </summary>
        public static readonly Color SecondaryColor_Light = Color.FromArgb(224, 0, 48);

        /// <summary>
        /// Default tertiary color for the light mode.
        /// </summary>
        public static readonly Color TertiaryColor_Light = Color.FromArgb(0, 173, 69);

        /// <summary>
        /// Default disabled color for the light mode.
        /// </summary>
        public static readonly Color DisabledColor_Light = Color.FromArgb(99, 99, 99);
    }
}