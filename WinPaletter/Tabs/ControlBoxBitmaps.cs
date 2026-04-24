using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace WinPaletter
{
    /// <summary>
    /// Loads and exposes caption-button bitmaps sliced from the current visual style's DWM atlas. Supports Vista through Windows 11 (DWM Composition and Basic/Classic where applicable).
    /// </summary>
    /// <remarks>
    /// Dispose this object when the loaded theme changes — all bitmaps will be
    /// released and the object becomes unusable until re-created.
    /// </remarks>
    public sealed class ControlBoxBitmaps : IDisposable
    {
        // ------------------------------------------------------------------
        // Internal atlas storage
        //
        // Layout:  _strips[partID]  -> List<Bitmap>[4]  (Normal/Hot/Pressed/Disabled)
        //          _margins[partID] -> NineSliceMargins  (sizing margins for that part)
        // ------------------------------------------------------------------

        private Bitmap _atlas;
        private readonly Dictionary<int, List<Bitmap>> _strips = [];
        private readonly Dictionary<int, NineSliceMargins> _margins = [];

        // Scalar single-frame bitmaps (Vista/7 glow overlays)
        private Bitmap _closeBkGlow;
        private Bitmap _minMaxBkGlow;

        private libmsstyle.Platform _platform;

        /// <summary>
        /// Which caption button to retrieve bitmaps for.
        /// </summary>
        public enum CaptionButton
        {
            Close,
            Minimize,
            Maximize,
            Restore,
            Help
        }

        /// <summary>
        /// Visual interaction state of a caption button.
        /// </summary>
        public enum ButtonState
        {
            Normal = 0,
            Hot = 1,
            Pressed = 2,
            Disabled = 3
        }

        /// <summary>
        /// Logical DPI scale used to pick the correct glyph atlas strip.
        /// The values match the Windows DPI suffixes used in visual style part names.
        /// </summary>
        public enum GlyphDpi
        {
            /// <summary>96 dpi — 100 %</summary>
            Dpi96 = 100,
            /// <summary>120 dpi — 125 %</summary>
            Dpi120 = 122,
            /// <summary>144 dpi — 150 %</summary>
            Dpi144 = 144,
            /// <summary>192 dpi — 200 %</summary>
            Dpi192 = 192
        }

        /// <summary>
        /// Whether the close button shares the title-bar with min/max buttons (Vista/7
        /// only — the background chrome changes shape depending on composition).
        /// On Win8+ this distinction does not exist in the atlas.
        /// </summary>
        public enum CloseButtonLayout
        {
            /// <summary>Close button sits next to min/max buttons.</summary>
            WithMinMax,
            /// <summary>Close button is the only caption button on the window.</summary>
            Alone
        }

        /// <summary>
        /// A composed caption-button bitmap: background chrome + centred glyph overlay.
        /// Caller owns both bitmaps and must dispose them.
        /// </summary>
        public sealed class ControlBoxButtonBitmap : IDisposable
        {
            /// <summary>
            /// Background bitmap rendered at the requested button size via nine-slice
            /// scaling. Fills the entire logical button rectangle.
            /// </summary>
            public Bitmap Background { get; }

            /// <summary>
            /// Glyph overlay at its natural atlas size for the chosen DPI scale.
            /// Centre this over <see cref="Background"/> when rendering.
            /// </summary>
            public Bitmap Overlay { get; }

            internal ControlBoxButtonBitmap(Bitmap background, Bitmap overlay)
            {
                Background = background;
                Overlay = overlay;
            }

            public void Dispose()
            {
                Background?.Dispose();
                Overlay?.Dispose();
            }
        }

        /// <summary>
        /// Sizing margins that define the nine-slice grid for a background bitmap.
        /// Matches the Windows MARGINS struct layout stored in the atlas part property:
        /// Left/Right are the horizontal fixed widths; Top/Bottom are the vertical
        /// fixed heights.
        /// </summary>
        private struct NineSliceMargins(int left, int right, int top, int bottom)
        {
            public int Left = left;
            public int Right = right;
            public int Top = top;
            public int Bottom = bottom;

            public readonly bool IsEmpty => Left == 0 && Right == 0 && Top == 0 && Bottom == 0;
        }

        private static class PartIds_Win10
        {
            public const int Button_Active_bk = 3;
            public const int Button_Inactive_bk = 4;
            public const int Button_End_Active_bk = 5;
            public const int Button_End_Inactive_bk = 6;
            public const int Close_Active_bk = 7;
            public const int Close_Inactive_bk = 8;
            public const int Close_Active_bk_Alone = 9;
            public const int Close_Inactive_bk_Alone = 10;
            public const int Close_Glyph = 11;
            public const int Close_Glyph_122 = 12;
            public const int Close_Glyph_144 = 13;
            public const int Close_Glyph_192 = 14;
            public const int Help_Glyph = 15;
            public const int Help_Glyph_122 = 16;
            public const int Help_Glyph_144 = 17;
            public const int Help_Glyph_192 = 18;
            public const int Max_Glyph = 19;
            public const int Max_Glyph_122 = 20;
            public const int Max_Glyph_144 = 21;
            public const int Max_Glyph_192 = 22;
            public const int Min_Glyph = 23;
            public const int Min_Glyph_122 = 24;
            public const int Min_Glyph_144 = 25;
            public const int Min_Glyph_192 = 26;
            public const int Restore_Glyph = 27;
            public const int Restore_Glyph_122 = 28;
            public const int Restore_Glyph_144 = 29;
            public const int Restore_Glyph_192 = 30;
            public const int Close_Glyph_Dark = 64;
            public const int Close_Glyph_122_Dark = 65;
            public const int Close_Glyph_144_Dark = 66;
            public const int Close_Glyph_192_Dark = 67;
            public const int Help_Glyph_Dark = 68;
            public const int Help_Glyph_122_Dark = 69;
            public const int Help_Glyph_144_Dark = 70;
            public const int Help_Glyph_192_Dark = 71;
            public const int Max_Glyph_Dark = 72;
            public const int Max_Glyph_122_Dark = 73;
            public const int Max_Glyph_144_Dark = 74;
            public const int Max_Glyph_192_Dark = 75;
            public const int Min_Glyph_Dark = 76;
            public const int Min_Glyph_122_Dark = 77;
            public const int Min_Glyph_144_Dark = 78;
            public const int Min_Glyph_192_Dark = 79;
            public const int Restore_Glyph_Dark = 80;
            public const int Restore_Glyph_122_Dark = 81;
            public const int Restore_Glyph_144_Dark = 82;
            public const int Restore_Glyph_192_Dark = 83;
            public const int Button_Active_bk_Dark = 88;
            public const int Button_Inactive_bk_Dark = 89;
            public const int Button_End_Active_bk_Dark = 90;
            public const int Button_End_Inactive_bk_Dark = 91;
        }

        private static class PartIds_Win8
        {
            public const int Button_Active_bk = 3;
            public const int Button_Inactive_bk = 4;
            public const int Button_End_Active_bk = 5;
            public const int Button_End_Inactive_bk = 6;
            public const int Close_Active_bk = 7;
            public const int Close_Inactive_bk = 8;
            public const int Close_Active_bk_Alone = 9;
            public const int Close_Inactive_bk_Alone = 10;
            public const int Close_Glyph = 12;
            public const int Close_Glyph_122 = 13;
            public const int Close_Glyph_144 = 14;
            public const int Close_Glyph_192 = 15;
            public const int Help_Glyph = 16;
            public const int Help_Glyph_122 = 17;
            public const int Help_Glyph_144 = 18;
            public const int Help_Glyph_192 = 19;
            public const int Max_Glyph = 20;
            public const int Max_Glyph_122 = 21;
            public const int Max_Glyph_144 = 22;
            public const int Max_Glyph_192 = 23;
            public const int Min_Glyph = 24;
            public const int Min_Glyph_122 = 25;
            public const int Min_Glyph_144 = 26;
            public const int Min_Glyph_192 = 27;
            public const int Restore_Glyph = 28;
            public const int Restore_Glyph_122 = 29;
            public const int Restore_Glyph_144 = 30;
            public const int Restore_Glyph_192 = 31;
        }

        private static class PartIds_Win7
        {
            public const int Button_Active_bk = 3;
            public const int Button_Inactive_bk = 4;
            public const int Button_End_Active_bk = 5;
            public const int Button_End_Inactive_bk = 6;
            public const int Close_Active_bk = 7;
            public const int Close_Inactive_bk = 8;
            public const int Close_Active_bk_Alone = 9;
            public const int Close_Inactive_bk_Alone = 10;
            public const int Close_bk_Glow = 11;
            public const int Close_Glyph = 12;
            public const int Close_Glyph_122 = 13;
            public const int Close_Glyph_144 = 14;
            public const int Close_Glyph_192 = 15;
            public const int MinMax_bk_Glow = 16;
            public const int Help_Glyph = 17;
            public const int Help_Glyph_122 = 18;
            public const int Help_Glyph_144 = 19;
            public const int Help_Glyph_192 = 20;
            public const int Max_Glyph = 21;
            public const int Max_Glyph_122 = 22;
            public const int Max_Glyph_144 = 23;
            public const int Max_Glyph_192 = 24;
            public const int Min_Glyph = 25;
            public const int Min_Glyph_122 = 26;
            public const int Min_Glyph_144 = 27;
            public const int Min_Glyph_192 = 28;
            public const int Restore_Glyph = 29;
            public const int Restore_Glyph_122 = 30;
            public const int Restore_Glyph_144 = 31;
            public const int Restore_Glyph_192 = 32;
        }

        /// <summary>
        /// Loads caption-button bitmaps from the currently active visual style.
        /// </summary>
        public ControlBoxBitmaps()
        {
            Load();
        }

        /// <summary>
        /// Returns a composed <see cref="ControlBoxButtonBitmap"/> whose background is
        /// nine-slice scaled to <paramref name="size"/>. The caller must dispose the result.
        /// </summary>
        /// <param name="button">Which caption button.</param>
        /// <param name="state">Visual interaction state.</param>
        /// <param name="size">
        /// Desired pixel size of the rendered button background. The glyph overlay is
        /// returned at its natural atlas size regardless of this value; centre it over
        /// the background when painting.
        /// </param>
        /// <param name="dpi">DPI scale used to pick the correct glyph strip.</param>
        /// <param name="darkGlyph">
        /// When <see langword="true"/>, returns the dark glyph variant (Win10/11 only).
        /// </param>
        /// <param name="closeLayout">
        /// Vista/7 only: whether the close button is alone or next to min/max.
        /// </param>
        /// <param name="activeWindow">
        /// <see langword="true"/> for the active-window chrome.
        /// </param>
        public ControlBoxButtonBitmap GetBitmap(CaptionButton button, ButtonState state, Size size, GlyphDpi dpi = GlyphDpi.Dpi96, bool darkGlyph = false, CloseButtonLayout closeLayout = CloseButtonLayout.WithMinMax, bool activeWindow = true)
        {
            Bitmap background = GetBackground(button, state, size, closeLayout, activeWindow);
            Bitmap overlay = GetOverlay(button, state, dpi, darkGlyph);
            return new ControlBoxButtonBitmap(background, overlay);
        }

        /// <summary>
        /// Convenience overload that uses the raw atlas frame dimensions as the
        /// background size — no scaling is applied.
        /// </summary>
        public ControlBoxButtonBitmap GetBitmap(CaptionButton button, ButtonState state, GlyphDpi dpi = GlyphDpi.Dpi96, bool darkGlyph = false, CloseButtonLayout closeLayout = CloseButtonLayout.WithMinMax, bool activeWindow = true)
        {
            int partID = ResolveBackgroundPartId(button, closeLayout, activeWindow);
            Size defaultSize = GetFrameSize(partID);
            return GetBitmap(button, state, defaultSize, dpi, darkGlyph, closeLayout, activeWindow);
        }

        /// <summary>
        /// Returns the raw glow overlay bitmap for the close button on Vista/7.
        /// Returns <see langword="null"/> on Win8+. Caller must dispose.
        /// </summary>
        public Bitmap GetCloseBkGlow() => _closeBkGlow is null ? null : CloneBitmap(_closeBkGlow);

        /// <summary>
        /// Returns the raw glow overlay bitmap for min/max/restore/help on Vista/7.
        /// Returns <see langword="null"/> on Win8+. Caller must dispose.
        /// </summary>
        public Bitmap GetMinMaxBkGlow() => _minMaxBkGlow is null ? null : CloneBitmap(_minMaxBkGlow);

        private Bitmap GetBackground(CaptionButton button, ButtonState state, Size targetSize, CloseButtonLayout closeLayout, bool activeWindow)
        {
            int partID = ResolveBackgroundPartId(button, closeLayout, activeWindow);
            return RenderBackground(partID, (int)state, targetSize);
        }

        private int ResolveBackgroundPartId(CaptionButton button, CloseButtonLayout closeLayout, bool activeWindow)
        {
            bool isWin10Plus = _platform == libmsstyle.Platform.Win10 || _platform == libmsstyle.Platform.Win11;
            bool isWin8x = _platform == libmsstyle.Platform.Win8 || _platform == libmsstyle.Platform.Win81;

            if (button == CaptionButton.Close)
            {
                if (isWin10Plus || isWin8x)
                {
                    return activeWindow
                        ? (closeLayout == CloseButtonLayout.Alone ? PartIds_Win8.Close_Active_bk_Alone : PartIds_Win8.Close_Active_bk)
                        : (closeLayout == CloseButtonLayout.Alone ? PartIds_Win8.Close_Inactive_bk_Alone : PartIds_Win8.Close_Inactive_bk);
                }
                else
                {
                    return activeWindow
                        ? (closeLayout == CloseButtonLayout.Alone ? PartIds_Win7.Close_Active_bk_Alone : PartIds_Win7.Close_Active_bk)
                        : (closeLayout == CloseButtonLayout.Alone ? PartIds_Win7.Close_Inactive_bk_Alone : PartIds_Win7.Close_Inactive_bk);
                }
            }

            bool isEnd = button == CaptionButton.Minimize;

            if (isWin10Plus)
            {
                return isEnd
                    ? (activeWindow ? PartIds_Win10.Button_End_Active_bk : PartIds_Win10.Button_End_Inactive_bk)
                    : (activeWindow ? PartIds_Win10.Button_Active_bk : PartIds_Win10.Button_Inactive_bk);
            }
            else if (isWin8x)
            {
                return isEnd
                    ? (activeWindow ? PartIds_Win8.Button_End_Active_bk : PartIds_Win8.Button_End_Inactive_bk)
                    : (activeWindow ? PartIds_Win8.Button_Active_bk : PartIds_Win8.Button_Inactive_bk);
            }
            else
            {
                return isEnd
                    ? (activeWindow ? PartIds_Win7.Button_End_Active_bk : PartIds_Win7.Button_End_Inactive_bk)
                    : (activeWindow ? PartIds_Win7.Button_Active_bk : PartIds_Win7.Button_Inactive_bk);
            }
        }

        /// <summary>
        /// Renders one state frame of a background part into a new bitmap of
        /// <paramref name="targetSize"/> using nine-slice scaling. Falls back to a
        /// plain scale if no sizing margins are stored for the part.
        /// </summary>
        private Bitmap RenderBackground(int partID, int stateIndex, Size targetSize)
        {
            if (!_strips.TryGetValue(partID, out List<Bitmap> frames) || frames is null || frames.Count == 0) return null;

            int index = Math.Max(0, Math.Min(stateIndex, frames.Count - 1));
            Bitmap source = frames[index];

            if (targetSize.Width <= 0 || targetSize.Height <= 0) return CloneBitmap(source);

            if (source.Width == targetSize.Width && source.Height == targetSize.Height) return CloneBitmap(source);

            bool hasMargins = _margins.TryGetValue(partID, out NineSliceMargins m);

            Bitmap result = new(targetSize.Width, targetSize.Height, PixelFormat.Format32bppArgb);

            using (Graphics G = Graphics.FromImage(result))
            {
                G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                G.CompositingMode = CompositingMode.SourceCopy;
                G.PixelOffsetMode = PixelOffsetMode.HighQuality;
                G.CompositingQuality = CompositingQuality.HighQuality;

                if (!hasMargins || m.IsEmpty) G.DrawImage(source, new Rectangle(Point.Empty, targetSize));
                else DrawNineSlice(G, source, targetSize, m);
            }

            return result;
        }

        /// <summary>
        /// Draws <paramref name="source"/> nine-slice scaled onto <paramref name="g"/>
        /// at the specified <paramref name="targetSize"/>.
        /// </summary>
        private static void DrawNineSlice(Graphics g, Bitmap source, Size targetSize, NineSliceMargins m)
        {
            int sw = source.Width;
            int sh = source.Height;
            int tw = targetSize.Width;
            int th = targetSize.Height;

            int srcCentreW = sw - m.Left - m.Right;
            int srcCentreH = sh - m.Top - m.Bottom;

            // Clamp destination centre to 0 so fixed margins never overlap
            int dstCentreW = Math.Max(0, tw - m.Left - m.Right);
            int dstCentreH = Math.Max(0, th - m.Top - m.Bottom);

            // Source rectangles  (column: left / centre / right,  row: top / mid / bottom)
            Rectangle srcTL = new(0, 0, m.Left, m.Top);
            Rectangle srcTM = new(m.Left, 0, srcCentreW, m.Top);
            Rectangle srcTR = new(sw - m.Right, 0, m.Right, m.Top);
            Rectangle srcML = new(0, m.Top, m.Left, srcCentreH);
            Rectangle srcMM = new(m.Left, m.Top, srcCentreW, srcCentreH);
            Rectangle srcMR = new(sw - m.Right, m.Top, m.Right, srcCentreH);
            Rectangle srcBL = new(0, sh - m.Bottom, m.Left, m.Bottom);
            Rectangle srcBM = new(m.Left, sh - m.Bottom, srcCentreW, m.Bottom);
            Rectangle srcBR = new(sw - m.Right, sh - m.Bottom, m.Right, m.Bottom);

            // Destination rectangles
            Rectangle dstTL = new(0, 0, m.Left, m.Top);
            Rectangle dstTM = new(m.Left, 0, dstCentreW, m.Top);
            Rectangle dstTR = new(tw - m.Right, 0, m.Right, m.Top);
            Rectangle dstML = new(0, m.Top, m.Left, dstCentreH);
            Rectangle dstMM = new(m.Left, m.Top, dstCentreW, dstCentreH);
            Rectangle dstMR = new(tw - m.Right, m.Top, m.Right, dstCentreH);
            Rectangle dstBL = new(0, th - m.Bottom, m.Left, m.Bottom);
            Rectangle dstBM = new(m.Left, th - m.Bottom, dstCentreW, m.Bottom);
            Rectangle dstBR = new(tw - m.Right, th - m.Bottom, m.Right, m.Bottom);

            DrawSlice(g, source, srcTL, dstTL);
            DrawSlice(g, source, srcTM, dstTM);
            DrawSlice(g, source, srcTR, dstTR);
            DrawSlice(g, source, srcML, dstML);
            DrawSlice(g, source, srcMM, dstMM);
            DrawSlice(g, source, srcMR, dstMR);
            DrawSlice(g, source, srcBL, dstBL);
            DrawSlice(g, source, srcBM, dstBM);
            DrawSlice(g, source, srcBR, dstBR);
        }

        private static void DrawSlice(Graphics g, Bitmap source, Rectangle src, Rectangle dst)
        {
            if (src.Width <= 0 || src.Height <= 0 || dst.Width <= 0 || dst.Height <= 0) return;

            g.DrawImage(source, dst, src, GraphicsUnit.Pixel);
        }

        private Bitmap GetOverlay(CaptionButton button, ButtonState state, GlyphDpi dpi, bool darkGlyph)
        {
            int partID = ResolveGlyphPartId(button, dpi, darkGlyph);
            return CloneStateFrame(partID, (int)state);
        }

        private int ResolveGlyphPartId(CaptionButton button, GlyphDpi dpi, bool darkGlyph)
        {
            bool isWin10Plus = _platform == libmsstyle.Platform.Win10 || _platform == libmsstyle.Platform.Win11;
            bool dark = darkGlyph && isWin10Plus;

            switch (button)
            {
                case CaptionButton.Close:
                    if (dark) return ResolveByDpi(dpi, PartIds_Win10.Close_Glyph_Dark, PartIds_Win10.Close_Glyph_122_Dark, PartIds_Win10.Close_Glyph_144_Dark, PartIds_Win10.Close_Glyph_192_Dark);
                    return ResolveGlyphByPlatformAndDpi(button, dpi);

                case CaptionButton.Help:
                    if (dark) return ResolveByDpi(dpi, PartIds_Win10.Help_Glyph_Dark, PartIds_Win10.Help_Glyph_122_Dark, PartIds_Win10.Help_Glyph_144_Dark, PartIds_Win10.Help_Glyph_192_Dark);
                    return ResolveGlyphByPlatformAndDpi(button, dpi);

                case CaptionButton.Maximize:
                    if (dark) return ResolveByDpi(dpi, PartIds_Win10.Max_Glyph_Dark, PartIds_Win10.Max_Glyph_122_Dark, PartIds_Win10.Max_Glyph_144_Dark, PartIds_Win10.Max_Glyph_192_Dark);
                    return ResolveGlyphByPlatformAndDpi(button, dpi);

                case CaptionButton.Minimize:
                    if (dark) return ResolveByDpi(dpi, PartIds_Win10.Min_Glyph_Dark, PartIds_Win10.Min_Glyph_122_Dark, PartIds_Win10.Min_Glyph_144_Dark, PartIds_Win10.Min_Glyph_192_Dark);
                    return ResolveGlyphByPlatformAndDpi(button, dpi);

                case CaptionButton.Restore:
                    if (dark) return ResolveByDpi(dpi, PartIds_Win10.Restore_Glyph_Dark, PartIds_Win10.Restore_Glyph_122_Dark, PartIds_Win10.Restore_Glyph_144_Dark, PartIds_Win10.Restore_Glyph_192_Dark);
                    return ResolveGlyphByPlatformAndDpi(button, dpi);

                default:
                    throw new ArgumentOutOfRangeException(nameof(button));
            }
        }

        private int ResolveGlyphByPlatformAndDpi(CaptionButton button, GlyphDpi dpi)
        {
            bool isWin10Plus = _platform == libmsstyle.Platform.Win10 || _platform == libmsstyle.Platform.Win11;
            bool isWin8x = _platform == libmsstyle.Platform.Win8 || _platform == libmsstyle.Platform.Win81;

            switch (button)
            {
                case CaptionButton.Close:
                    if (isWin10Plus) return ResolveByDpi(dpi, PartIds_Win10.Close_Glyph, PartIds_Win10.Close_Glyph_122, PartIds_Win10.Close_Glyph_144, PartIds_Win10.Close_Glyph_192);
                    if (isWin8x) return ResolveByDpi(dpi, PartIds_Win8.Close_Glyph, PartIds_Win8.Close_Glyph_122, PartIds_Win8.Close_Glyph_144, PartIds_Win8.Close_Glyph_192);
                    return ResolveByDpi(dpi, PartIds_Win7.Close_Glyph, PartIds_Win7.Close_Glyph_122, PartIds_Win7.Close_Glyph_144, PartIds_Win7.Close_Glyph_192);

                case CaptionButton.Help:
                    if (isWin10Plus) return ResolveByDpi(dpi, PartIds_Win10.Help_Glyph, PartIds_Win10.Help_Glyph_122, PartIds_Win10.Help_Glyph_144, PartIds_Win10.Help_Glyph_192);
                    if (isWin8x) return ResolveByDpi(dpi, PartIds_Win8.Help_Glyph, PartIds_Win8.Help_Glyph_122, PartIds_Win8.Help_Glyph_144, PartIds_Win8.Help_Glyph_192);
                    return ResolveByDpi(dpi, PartIds_Win7.Help_Glyph, PartIds_Win7.Help_Glyph_122, PartIds_Win7.Help_Glyph_144, PartIds_Win7.Help_Glyph_192);

                case CaptionButton.Maximize:
                    if (isWin10Plus) return ResolveByDpi(dpi, PartIds_Win10.Max_Glyph, PartIds_Win10.Max_Glyph_122, PartIds_Win10.Max_Glyph_144, PartIds_Win10.Max_Glyph_192);
                    if (isWin8x) return ResolveByDpi(dpi, PartIds_Win8.Max_Glyph, PartIds_Win8.Max_Glyph_122, PartIds_Win8.Max_Glyph_144, PartIds_Win8.Max_Glyph_192);
                    return ResolveByDpi(dpi, PartIds_Win7.Max_Glyph, PartIds_Win7.Max_Glyph_122, PartIds_Win7.Max_Glyph_144, PartIds_Win7.Max_Glyph_192);

                case CaptionButton.Minimize:
                    if (isWin10Plus) return ResolveByDpi(dpi, PartIds_Win10.Min_Glyph, PartIds_Win10.Min_Glyph_122, PartIds_Win10.Min_Glyph_144, PartIds_Win10.Min_Glyph_192);
                    if (isWin8x) return ResolveByDpi(dpi, PartIds_Win8.Min_Glyph, PartIds_Win8.Min_Glyph_122, PartIds_Win8.Min_Glyph_144, PartIds_Win8.Min_Glyph_192);
                    return ResolveByDpi(dpi, PartIds_Win7.Min_Glyph, PartIds_Win7.Min_Glyph_122, PartIds_Win7.Min_Glyph_144, PartIds_Win7.Min_Glyph_192);

                case CaptionButton.Restore:
                    if (isWin10Plus) return ResolveByDpi(dpi, PartIds_Win10.Restore_Glyph, PartIds_Win10.Restore_Glyph_122, PartIds_Win10.Restore_Glyph_144, PartIds_Win10.Restore_Glyph_192);
                    if (isWin8x) return ResolveByDpi(dpi, PartIds_Win8.Restore_Glyph, PartIds_Win8.Restore_Glyph_122, PartIds_Win8.Restore_Glyph_144, PartIds_Win8.Restore_Glyph_192);
                    return ResolveByDpi(dpi, PartIds_Win7.Restore_Glyph, PartIds_Win7.Restore_Glyph_122, PartIds_Win7.Restore_Glyph_144, PartIds_Win7.Restore_Glyph_192);

                default:
                    throw new ArgumentOutOfRangeException(nameof(button));
            }
        }

        private static int ResolveByDpi(GlyphDpi dpi, int id96, int id122, int id144, int id192)
        {
            switch (dpi)
            {
                case GlyphDpi.Dpi120: return id122;
                case GlyphDpi.Dpi144: return id144;
                case GlyphDpi.Dpi192: return id192;
                default: return id96;
            }
        }

        private void Load()
        {
            string themeName = NativeMethods.UxTheme.GetCurrentVS().Item1;
            libmsstyle.VisualStyle style = new(themeName);
            _platform = style.Platform;

            libmsstyle.StyleClass dwmClass = style.Class("DWMWindow").Value;

            libmsstyle.StylePart cp = dwmClass.Parts.FirstOrDefault(p => p.Value.PartId == libmsstyle.VisualStyleStates.STATES_COMMON_DEFAULT.FirstOrDefault().Value).Value;
            libmsstyle.StyleState commonState = cp.States.FirstOrDefault(s => s.Value.StateId == libmsstyle.VisualStyleStates.STATES_COMMON_DEFAULT.FirstOrDefault().Value).Value;
            libmsstyle.StyleProperty atlasProperty = commonState.Properties.FirstOrDefault(p => p.IsImageProperty());

            if (atlasProperty == null) return;

            using (MemoryStream ms = new(style.GetResourceFromProperty(atlasProperty).Data))
            {
                _atlas = Image.FromStream(ms) as Bitmap;
            }

            if (_platform == libmsstyle.Platform.Win10 || _platform == libmsstyle.Platform.Win11)
                LoadStrips_Win10(dwmClass);
            else if (_platform == libmsstyle.Platform.Win8 || _platform == libmsstyle.Platform.Win81)
                LoadStrips_Win8(dwmClass);
            else if (_platform == libmsstyle.Platform.Win7 || _platform == libmsstyle.Platform.Vista)
                LoadStrips_Win7(dwmClass);
        }

        private void LoadStrips_Win10(libmsstyle.StyleClass @class)
        {
            int[] partIds =
            [
                PartIds_Win10.Button_Active_bk,
                PartIds_Win10.Button_Inactive_bk,
                PartIds_Win10.Button_End_Active_bk,
                PartIds_Win10.Button_End_Inactive_bk,
                PartIds_Win10.Close_Active_bk,
                PartIds_Win10.Close_Inactive_bk,
                PartIds_Win10.Close_Active_bk_Alone,
                PartIds_Win10.Close_Inactive_bk_Alone,
                PartIds_Win10.Close_Glyph, PartIds_Win10.Close_Glyph_122, PartIds_Win10.Close_Glyph_144, PartIds_Win10.Close_Glyph_192,
                PartIds_Win10.Help_Glyph, PartIds_Win10.Help_Glyph_122, PartIds_Win10.Help_Glyph_144, PartIds_Win10.Help_Glyph_192,
                PartIds_Win10.Max_Glyph, PartIds_Win10.Max_Glyph_122, PartIds_Win10.Max_Glyph_144, PartIds_Win10.Max_Glyph_192,
                PartIds_Win10.Min_Glyph, PartIds_Win10.Min_Glyph_122, PartIds_Win10.Min_Glyph_144, PartIds_Win10.Min_Glyph_192,
                PartIds_Win10.Restore_Glyph, PartIds_Win10.Restore_Glyph_122, PartIds_Win10.Restore_Glyph_144, PartIds_Win10.Restore_Glyph_192,
                PartIds_Win10.Close_Glyph_Dark, PartIds_Win10.Close_Glyph_122_Dark, PartIds_Win10.Close_Glyph_144_Dark, PartIds_Win10.Close_Glyph_192_Dark,
                PartIds_Win10.Help_Glyph_Dark, PartIds_Win10.Help_Glyph_122_Dark, PartIds_Win10.Help_Glyph_144_Dark, PartIds_Win10.Help_Glyph_192_Dark,
                PartIds_Win10.Max_Glyph_Dark, PartIds_Win10.Max_Glyph_122_Dark, PartIds_Win10.Max_Glyph_144_Dark, PartIds_Win10.Max_Glyph_192_Dark,
                PartIds_Win10.Min_Glyph_Dark, PartIds_Win10.Min_Glyph_122_Dark, PartIds_Win10.Min_Glyph_144_Dark, PartIds_Win10.Min_Glyph_192_Dark,
                PartIds_Win10.Restore_Glyph_Dark, PartIds_Win10.Restore_Glyph_122_Dark, PartIds_Win10.Restore_Glyph_144_Dark, PartIds_Win10.Restore_Glyph_192_Dark,
                PartIds_Win10.Button_Active_bk_Dark,
                PartIds_Win10.Button_Inactive_bk_Dark,
                PartIds_Win10.Button_End_Active_bk_Dark,
                PartIds_Win10.Button_End_Inactive_bk_Dark
            ];

            foreach (int id in partIds) LoadStrip(@class, id);
        }

        private void LoadStrips_Win8(libmsstyle.StyleClass @class)
        {
            int[] partIds =
            [
                PartIds_Win8.Button_Active_bk,
                PartIds_Win8.Button_Inactive_bk,
                PartIds_Win8.Button_End_Active_bk,
                PartIds_Win8.Button_End_Inactive_bk,
                PartIds_Win8.Close_Active_bk,
                PartIds_Win8.Close_Inactive_bk,
                PartIds_Win8.Close_Active_bk_Alone,
                PartIds_Win8.Close_Inactive_bk_Alone,
                PartIds_Win8.Close_Glyph, PartIds_Win8.Close_Glyph_122, PartIds_Win8.Close_Glyph_144, PartIds_Win8.Close_Glyph_192,
                PartIds_Win8.Help_Glyph, PartIds_Win8.Help_Glyph_122, PartIds_Win8.Help_Glyph_144, PartIds_Win8.Help_Glyph_192,
                PartIds_Win8.Max_Glyph, PartIds_Win8.Max_Glyph_122, PartIds_Win8.Max_Glyph_144, PartIds_Win8.Max_Glyph_192,
                PartIds_Win8.Min_Glyph, PartIds_Win8.Min_Glyph_122, PartIds_Win8.Min_Glyph_144, PartIds_Win8.Min_Glyph_192,
                PartIds_Win8.Restore_Glyph, PartIds_Win8.Restore_Glyph_122, PartIds_Win8.Restore_Glyph_144, PartIds_Win8.Restore_Glyph_192
            ];

            foreach (int id in partIds)
                LoadStrip(@class, id);
        }

        private void LoadStrips_Win7(libmsstyle.StyleClass @class)
        {
            int[] stripIds =
            [
                PartIds_Win7.Button_Active_bk,
                PartIds_Win7.Button_Inactive_bk,
                PartIds_Win7.Button_End_Active_bk,
                PartIds_Win7.Button_End_Inactive_bk,
                PartIds_Win7.Close_Active_bk,
                PartIds_Win7.Close_Inactive_bk,
                PartIds_Win7.Close_Active_bk_Alone,
                PartIds_Win7.Close_Inactive_bk_Alone,
                PartIds_Win7.Close_Glyph, PartIds_Win7.Close_Glyph_122, PartIds_Win7.Close_Glyph_144, PartIds_Win7.Close_Glyph_192,
                PartIds_Win7.Help_Glyph, PartIds_Win7.Help_Glyph_122, PartIds_Win7.Help_Glyph_144, PartIds_Win7.Help_Glyph_192,
                PartIds_Win7.Max_Glyph, PartIds_Win7.Max_Glyph_122, PartIds_Win7.Max_Glyph_144, PartIds_Win7.Max_Glyph_192,
                PartIds_Win7.Min_Glyph, PartIds_Win7.Min_Glyph_122, PartIds_Win7.Min_Glyph_144, PartIds_Win7.Min_Glyph_192,
                PartIds_Win7.Restore_Glyph, PartIds_Win7.Restore_Glyph_122, PartIds_Win7.Restore_Glyph_144, PartIds_Win7.Restore_Glyph_192
            ];

            foreach (int id in stripIds) LoadStrip(@class, id);

            _closeBkGlow = LoadSingleFrame(@class, PartIds_Win7.Close_bk_Glow);
            _minMaxBkGlow = LoadSingleFrame(@class, PartIds_Win7.MinMax_bk_Glow);
        }

        private void LoadStrip(libmsstyle.StyleClass @class, int partID)
        {
            if (_atlas is null || @class is null) return;

            libmsstyle.StylePart partEntry = @class.Parts.FirstOrDefault(p => p.Value.PartId == partID).Value;
            if (partEntry is null) return;

            libmsstyle.StyleState commonState = partEntry.States.FirstOrDefault(s => s.Value.StateId == libmsstyle.VisualStyleStates.STATES_COMMON_DEFAULT.First().Value).Value;
            if (commonState is null) return;

            libmsstyle.StyleProperty rectProp = commonState.Properties.FirstOrDefault(p => p.Header.nameID == (int)libmsstyle.IDENTIFIER.ATLASRECT);
            if (rectProp is null) return;

            libmsstyle.StyleProperty countProp = commonState.Properties.FirstOrDefault(p => p.Header.nameID == (int)libmsstyle.IDENTIFIER.IMAGECOUNT);
            if (countProp is null) return;

            libmsstyle.Margins rect = rectProp.GetValue() as libmsstyle.Margins;
            int count = (int)countProp.GetValue();

            Bitmap strip = _atlas.Clone(Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom), PixelFormat.Format32bppArgb);

            _strips[partID] = strip.Split(count);
            strip.Dispose();

            // Read SIZINGMARGINS (TMT_SIZINGMARGINS = 3601) for this part.
            // The value is a libmsstyle.Margins with Left/Right as horizontal fixed widths and Top/Bottom as vertical fixed heights — exactly the nine-slice grid definition Windows uses internally.
            libmsstyle.StyleProperty sizingProp = commonState.Properties.FirstOrDefault(p => p.Header.nameID == (int)libmsstyle.IDENTIFIER.SIZINGMARGINS);

            if (sizingProp != null)
            {
                if (sizingProp.GetValue() is libmsstyle.Margins sm) _margins[partID] = new(sm.Left, sm.Right, sm.Top, sm.Bottom);
            }
        }

        private Bitmap LoadSingleFrame(libmsstyle.StyleClass @class, int partID)
        {
            if (_atlas is null || @class is null) return null;

            libmsstyle.StylePart partEntry = @class.Parts.FirstOrDefault(p => p.Value.PartId == partID).Value;
            if (partEntry is null) return null;

            libmsstyle.StyleState commonState = partEntry.States.FirstOrDefault(s => s.Value.StateId == libmsstyle.VisualStyleStates.STATES_COMMON_DEFAULT.First().Value).Value;
            if (commonState is null) return null;

            libmsstyle.StyleProperty rectProp = commonState.Properties.FirstOrDefault(p => p.Header.nameID == (int)libmsstyle.IDENTIFIER.ATLASRECT);
            if (rectProp is null) return null;

            libmsstyle.Margins rect = rectProp.GetValue() as libmsstyle.Margins;

            return _atlas.Clone(Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom), PixelFormat.Format32bppArgb);
        }

        private Bitmap CloneStateFrame(int partID, int stateIndex)
        {
            if (!_strips.TryGetValue(partID, out List<Bitmap> frames) || frames is null || frames.Count == 0) return null;

            int index = Math.Max(0, Math.Min(stateIndex, frames.Count - 1));
            return CloneBitmap(frames[index]);
        }

        private Size GetFrameSize(int partID)
        {
            if (!_strips.TryGetValue(partID, out List<Bitmap> frames) || frames is null || frames.Count == 0) return Size.Empty;

            return frames[0].Size;
        }

        private static Bitmap CloneBitmap(Bitmap source)
        {
            if (source is null) return null;
            return source.Clone(new Rectangle(0, 0, source.Width, source.Height), PixelFormat.Format32bppArgb);
        }

        public void Dispose()
        {
            foreach (List<Bitmap> strip in _strips.Values) strip?.ForEach(bmp => bmp?.Dispose());

            _strips.Clear();
            _margins.Clear();

            _atlas?.Dispose();
            _closeBkGlow?.Dispose();
            _minMaxBkGlow?.Dispose();

            _atlas = null;
            _closeBkGlow = null;
            _minMaxBkGlow = null;
        }
    }
}