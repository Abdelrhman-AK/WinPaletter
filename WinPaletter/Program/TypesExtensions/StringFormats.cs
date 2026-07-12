using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extensions for <see cref="StringFormat"/>
    /// </summary>
    public static class StringFormatExtensions
    {
        /// <summary>
        /// Convert ContentAlignment to StringFormat
        /// </summary>
        /// <param name="TextAlign"></param>
        /// <param name="RightToLeft"></param>
        /// <returns></returns>
        public static StringFormat ToStringFormat(this ContentAlignment TextAlign, bool RightToLeft = false)
        {
            StringFormat SF = new();
            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                    {
                        SF.LineAlignment = StringAlignment.Near;
                        SF.Alignment = StringAlignment.Near;
                        break;
                    }
                case ContentAlignment.TopCenter:
                    {
                        SF.LineAlignment = StringAlignment.Near;
                        SF.Alignment = StringAlignment.Center;
                        break;
                    }
                case ContentAlignment.TopRight:
                    {
                        SF.LineAlignment = StringAlignment.Near;
                        SF.Alignment = StringAlignment.Far;
                        break;
                    }
                case ContentAlignment.MiddleLeft:
                    {
                        SF.LineAlignment = StringAlignment.Center;
                        SF.Alignment = StringAlignment.Near;
                        break;
                    }
                case ContentAlignment.MiddleCenter:
                    {
                        SF.LineAlignment = StringAlignment.Center;
                        SF.Alignment = StringAlignment.Center;
                        break;
                    }
                case ContentAlignment.MiddleRight:
                    {
                        SF.LineAlignment = StringAlignment.Center;
                        SF.Alignment = StringAlignment.Far;
                        break;
                    }
                case ContentAlignment.BottomLeft:
                    {
                        SF.LineAlignment = StringAlignment.Far;
                        SF.Alignment = StringAlignment.Near;
                        break;
                    }
                case ContentAlignment.BottomCenter:
                    {
                        SF.LineAlignment = StringAlignment.Far;
                        SF.Alignment = StringAlignment.Center;
                        break;
                    }
                case ContentAlignment.BottomRight:
                    {
                        SF.LineAlignment = StringAlignment.Far;
                        SF.Alignment = StringAlignment.Far;
                        break;
                    }

                default:
                    {
                        SF.LineAlignment = StringAlignment.Center;
                        SF.Alignment = StringAlignment.Near;
                        break;
                    }

            }

            if (RightToLeft)
                SF.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            // Set character spacing (kerning) to remove increased spaces inbetween words
            // Adjust this value as needed
            SF.SetMeasurableCharacterRanges([new(0, 1)]);
            SF.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

            return SF;
        }

        /// <summary>
        /// Converts <see cref="StringFormat"/> to <see cref="TextFormatFlags"/>
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static TextFormatFlags ToTextFormatFlags(this StringFormat format)
        {
            TextFormatFlags flags = TextFormatFlags.Default;

            // Map Horizontal Alignment
            switch (format.Alignment)
            {
                case StringAlignment.Center: flags |= TextFormatFlags.HorizontalCenter; break;
                case StringAlignment.Far: flags |= TextFormatFlags.Right; break;
                case StringAlignment.Near: flags |= TextFormatFlags.Left; break;
            }

            // Map Vertical Alignment
            switch (format.LineAlignment)
            {
                case StringAlignment.Center: flags |= TextFormatFlags.VerticalCenter; break;
                case StringAlignment.Far: flags |= TextFormatFlags.Bottom; break;
                case StringAlignment.Near: flags |= TextFormatFlags.Top; break;
            }

            // Handle Trimming
            if (format.Trimming == StringTrimming.EllipsisCharacter)
                flags |= TextFormatFlags.EndEllipsis;
            else if (format.Trimming == StringTrimming.Word)
                flags |= TextFormatFlags.WordEllipsis;

            // Standard flags for clean rendering
            flags |= TextFormatFlags.NoPrefix; // Ensures '&' is not rendered as a mnemonic

            return flags;
        }
    }
}
