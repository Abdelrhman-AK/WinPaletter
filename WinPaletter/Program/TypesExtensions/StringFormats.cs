using System.Drawing;

namespace WinPaletter.TypesExtensions
{
    public static class StringFormatExtensions
    {
        public static StringFormat ToStringFormat(this ContentAlignment TextAlign, bool RightToLeft = false)
        {
            var SF = new StringFormat();
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

            return SF;
        }
    }

}
