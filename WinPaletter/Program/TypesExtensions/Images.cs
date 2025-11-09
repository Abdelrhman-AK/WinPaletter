using System;
using System.Collections.Generic;
using System.Drawing;
using static WinPaletter.TypesExtensions.BitmapExtensions;

namespace WinPaletter.TypesExtensions
{
    public static class ImageExtensions
    {
        /// <summary>
        /// Converts the specified <see cref="Bitmap"/> to a grayscale image.
        /// </summary>
        /// <remarks>The method processes the image pixel by pixel, applying a weighted formula to calculate the grayscale
        /// value based on the red, green, and blue components of each pixel. The alpha channel is preserved in the resulting
        /// image. The method ensures the image is processed in a 32bpp ARGB format for compatibility and safety.</remarks>
        /// <param name="original">The original <see cref="Bitmap"/> to be converted. Must not be <see langword="null"/>.</param>
        /// <returns>A new <see cref="Bitmap"/> instance representing the grayscale version of the original image. Returns <see
        /// langword="null"/> if the <paramref name="original"/> is <see langword="null"/>.</returns>
        public static Bitmap Grayscale(this Image image)
        {
            return image is Bitmap bitmap ? bitmap.Grayscale() : null;
        }

        /// <summary>
        /// Applies a noise effect to the specified <see cref="Bitmap"/> based on the given noise mode and opacity.
        /// </summary>
        /// <remarks>The method creates a clone of the input <see cref="Bitmap"/> and applies the
        /// specified noise effect to the clone. The <paramref name="noiseMode"/> determines the type of noise texture
        /// used, and the <paramref name="opacity"/> controls the transparency of the effect.</remarks>
        /// <param name="bitmap">The source <see cref="Bitmap"/> to which the noise effect will be applied. Cannot be <see langword="null"/>.</param>
        /// <param name="noiseMode">The type of noise effect to apply. Supported values are <see cref="NoiseMode.Acrylic"/> and <see
        /// cref="NoiseMode.Aero"/>.</param>
        /// <param name="opacity">The opacity level of the noise effect, specified as a float between 0.0 (completely transparent) and 1.0
        /// (completely opaque).</param>
        /// <returns>A new <see cref="Bitmap"/> instance with the applied noise effect, or <see langword="null"/> if the input
        /// <paramref name="bitmap"/> is <see langword="null"/>.</returns>
        public static Bitmap Noise(this Image image, NoiseMode noiseMode, float opacity)
        {
            return image is Bitmap bitmap ? bitmap.Noise(noiseMode, opacity) : null;
        }

        /// <summary>
        /// Creates a new <see cref="Bitmap"/> where the colors of the source image are inverted.
        /// </summary>
        /// <remarks>The method processes the image at the pixel level, inverting the red, green, and blue
        /// color channels while leaving the alpha channel unchanged. The returned image is always in the 32bpp ARGB
        /// pixel format.</remarks>
        /// <param name="source">The source <see cref="Bitmap"/> to invert. Must not be null.</param>
        /// <returns>A new <see cref="Bitmap"/> with inverted colors, or <see langword="null"/> if <paramref name="source"/> is
        /// <see langword="null"/>.</returns>
        public static Bitmap Invert(this Image image)
        {
            return image is Bitmap bitmap ? bitmap.Invert() : null;
        }

        /// <summary>
        /// Creates a tiled version of the source bitmap that fills the specified target size.
        /// </summary>
        /// <remarks>The method creates a new bitmap of the specified size and fills it by repeating the source bitmap
        /// both horizontally and vertically. The source bitmap is converted to 32bpp ARGB format if it is not already in that
        /// format. The resulting bitmap is always in 32bpp ARGB format.</remarks>
        /// <param name="source">The source <see cref="Bitmap"/> to tile. Cannot be null.</param>
        /// <param name="targetSize">The dimensions of the resulting tiled bitmap.</param>
        /// <returns>A new <see cref="Bitmap"/> of the specified size, filled by repeating the source bitmap. Returns <see
        /// langword="null"/> if <paramref name="source"/> is <see langword="null"/>.</returns>
        public static Bitmap Tile(this Image image, Size targetSize)
        {
            return image is Bitmap bitmap ? bitmap.Tile(targetSize) : null;
        }

        /// <summary>
        /// Adjusts the brightness of the specified bitmap and returns a new bitmap with the applied adjustment.
        /// </summary>
        /// <remarks>This method processes the bitmap pixel by pixel, adjusting the brightness of the red,
        /// green, and blue channels  independently. The alpha channel is preserved. The operation is performed in
        /// parallel for improved performance  on large images.</remarks>
        /// <param name="bmp">The source <see cref="Bitmap"/> to adjust. Cannot be <see langword="null"/>.</param>
        /// <param name="brightness">The brightness adjustment factor. A value of <c>0</c> applies no change. Positive values increase
        /// brightness,  while negative values decrease it. The value is clamped to the range [-1, 1].</param>
        /// <returns>A new <see cref="Bitmap"/> with the brightness adjustment applied. The original bitmap remains unchanged.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="bmp"/> is <see langword="null"/>.</exception>
        public static Bitmap Brighten(this Image image, float brightness = 0f)
        {
            return image is Bitmap bitmap ? bitmap.Brighten(brightness) : null;
        }

        /// <summary>
        /// Adjusts the contrast of the specified <see cref="Bitmap"/> image.
        /// </summary>
        /// <remarks>This method processes the image pixel by pixel, adjusting the contrast of the red,
        /// green, and blue channels while preserving the alpha channel. The operation is performed in parallel for
        /// improved performance on large images.</remarks>
        /// <param name="bmp">The source <see cref="Bitmap"/> to adjust. Cannot be <see langword="null"/>.</param>
        /// <param name="contrast">The contrast adjustment factor. A value of <c>0</c> applies no adjustment, positive values increase
        /// contrast, and negative values decrease contrast. The value is relative and does not have a fixed range.</param>
        /// <returns>A new <see cref="Bitmap"/> with the adjusted contrast. The original <paramref name="bmp"/> is not modified.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="bmp"/> is <see langword="null"/>.</exception>
        public static Bitmap Contrast(this Image image, float contrast = 0f)
        {
            return image is Bitmap bitmap ? bitmap.Contrast(contrast) : null;
        }

        /// <summary>
        /// Applies a tint to the specified bitmap using the provided color.
        /// </summary>
        /// <remarks>This method creates a new bitmap with the same dimensions as the original and applies the tint by
        /// multiplying each pixel's color components by the corresponding components of the <paramref name="tintColor"/>. The
        /// original bitmap is not modified. The method ensures thread safety by cloning the original bitmap before
        /// processing.</remarks>
        /// <param name="originalBitmap">The original <see cref="Bitmap"/> to which the tint will be applied. Cannot be null.</param>
        /// <param name="tintColor">The <see cref="Color"/> used to tint the bitmap. The red, green, blue, and alpha components of this color are used
        /// as multipliers for the corresponding components of each pixel in the bitmap.</param>
        /// <returns>A new <see cref="Bitmap"/> instance with the tint applied. Returns <see langword="null"/> if <paramref
        /// name="originalBitmap"/> is <see langword="null"/>.</returns>
        public static Bitmap Tint(this Image image, Color color)
        {
            return image is Bitmap bitmap ? bitmap.Tint(color) : null;
        }

        /// <summary>
        /// Fast opacity fade using unsafe LockBits (returns a new bitmap).
        /// - Normalizes source to 32bpp ARGB once if needed.
        /// - Uses integer math for alpha scaling (no floats in the inner loop).
        /// - Copies B,G,R as-is; scales A by opacity.
        /// </summary>
        /// <param name="source">Input bitmap (not modified)</param>
        /// <param name="opacity">0..1</param>
        /// <param name="useParallel">Parallelize across rows for large images</param>
        public static Bitmap Fade(this Image image, float opacity = 0.5f)
        {
            return image is Bitmap bitmap ? bitmap.Fade(opacity) : null;
        }

        /// <summary>
        /// Applies a Gaussian blur effect to the specified bitmap image, with cancellation support.
        /// </summary>
        /// <remarks>
        /// The method normalizes the input bitmap to a 32bpp ARGB format for processing and
        /// converts it back to the original pixel format if necessary. The Gaussian blur is applied using a separable
        /// kernel for improved performance. The opacity parameter allows blending the blurred result with the original
        /// image. Cancellation can be requested via the provided <paramref name="cancellationToken"/>.
        /// </remarks>
        /// <param name="bitmap">The source <see cref="Bitmap"/> to which the Gaussian blur will be applied. Cannot be <see langword="null"/>.</param>
        /// <param name="blurPower">The intensity of the blur effect. Must be greater than 0. Higher values result in a stronger blur. Defaults to 2.0f.</param>
        /// <param name="opacity">The opacity of the blur effect, where <see langword="0.0f"/> represents fully transparent and <see langword="1.0f"/> represents fully opaque. Values outside the range [0.0f, 1.0f] will be clamped. Defaults to 1.0f.</param>
        /// <param name="cancellationToken">The token that can be used to request cancellation of the operation.</param>
        /// <returns>A new <see cref="Bitmap"/> instance with the Gaussian blur applied. Returns <see langword="null"/> if the input <paramref name="bitmap"/> is <see langword="null"/> or has zero width or height.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via <paramref name="cancellationToken"/>.</exception>
        public static Bitmap Blur(this Image image, float radius = 1f)
        {
            return image is Bitmap bitmap ? bitmap.Blur(radius) : null;
        }

        /// <summary>
        /// Calculates the average color of the pixels in the specified bitmap.
        /// </summary>
        /// <remarks>This method locks the bitmap in memory for reading and processes its pixels directly.
        /// It uses a 32bpp ARGB pixel format and calculates the average color by iterating over the pixels with the
        /// specified step size.</remarks>
        /// <param name="bitmap">The bitmap from which to calculate the average color. Must not be null, and must have a non-zero width and
        /// height.</param>
        /// <param name="step">The step size for sampling pixels. A larger step reduces the number of pixels sampled, improving performance
        /// at the cost of accuracy. Defaults to 1, meaning every pixel is sampled.</param>
        /// <returns>A <see cref="Color"/> representing the average color of the sampled pixels in the bitmap. Returns <see
        /// cref="Color.Empty"/> if the bitmap is null, has zero width or height, or if an error occurs during
        /// processing.</returns>
        public static Color AverageColor(this Image image, int step = 1)
        {
            return image is Bitmap bitmap ? bitmap.AverageColor(step) : Color.Empty;
        }

        /// <summary>
        /// Resizes the specified image to the given width and height.
        /// </summary>
        /// <param name="image">The source image to resize. Must be a valid <see cref="Image"/> instance.</param>
        /// <param name="width">The desired width of the resized image, in pixels. Must be greater than 0.</param>
        /// <param name="height">The desired height of the resized image, in pixels. Must be greater than 0.</param>
        /// <returns>A <see cref="Bitmap"/> object representing the resized image if the source image is a <see cref="Bitmap"/>; 
        /// otherwise, <see langword="null"/>.</returns>
        public static Bitmap Resize(this Image image, int width, int height)
        {
            return image is Bitmap bitmap ? BitmapExtensions.Resize(bitmap, width, height) : null;
        }

        /// <summary>
        /// Resizes the specified image to fill the target size while maintaining the aspect ratio.
        /// </summary>
        /// <remarks>This method ensures that the resulting image completely covers the target size, which
        /// may involve cropping parts of the original image to maintain the aspect ratio.</remarks>
        /// <param name="image">The source image to be resized. Must be a valid <see cref="Image"/> instance.</param>
        /// <param name="targetSize">The desired dimensions for the output image.</param>
        /// <returns>A <see cref="Bitmap"/> that fills the target size while preserving the aspect ratio of the original image,
        /// or <see langword="null"/> if the input image is not a <see cref="Bitmap"/>.</returns>
        public static Bitmap FillInSize(this Image image, Size targetSize)
        {
            return image is Bitmap bitmap ? BitmapExtensions.FillInSize(bitmap, targetSize) : null;
        }

        /// <summary>
        /// Resizes the image to fit the specified dimensions while maintaining its aspect ratio.
        /// </summary>
        /// <remarks>The method ensures that the resized image fits within the specified width and height
        /// while preserving the original aspect ratio. If the source image is not a <see cref="Bitmap"/>, the method
        /// returns <see langword="null"/>.</remarks>
        /// <param name="image">The source image to resize. Must be a valid <see cref="Image"/> instance.</param>
        /// <param name="width">The target width of the resized image, in pixels.</param>
        /// <param name="height">The target height of the resized image, in pixels.</param>
        /// <returns>A <see cref="Bitmap"/> object representing the resized image with the specified dimensions,  or <see
        /// langword="null"/> if the source image is not a <see cref="Bitmap"/>.</returns>
        public static Bitmap FillInSize(this Image image, int width, int height)
        {
            return image is Bitmap bitmap ? BitmapExtensions.FillInSize(bitmap, new Size(width, height)) : null;
        }

        /// <summary>
        /// Replaces all occurrences of a specified color in the image with a new color.
        /// </summary>
        /// <remarks>This method is an extension method for the <see cref="Image"/> class. It delegates
        /// the color replacement operation to a helper method in <c>BitmapExtensions</c> if the input image is a <see
        /// cref="Bitmap"/>.</remarks>
        /// <param name="image">The source image in which the color replacement will occur. Must be a <see cref="Bitmap"/>.</param>
        /// <param name="oldColor">The color to be replaced in the image.</param>
        /// <param name="newColor">The color to replace <paramref name="oldColor"/> with.</param>
        /// <returns>A new <see cref="Bitmap"/> with the specified color replaced, or <see langword="null"/> if the input image
        /// is not a <see cref="Bitmap"/>.</returns>
        public static Bitmap ReplaceColor(this Image image, Color oldColor, Color newColor)
        {
            return image is Bitmap bitmap ? BitmapExtensions.ReplaceColor(bitmap, oldColor, newColor) : null;
        }

        /// <summary>
        /// Creates a tiled bitmap by repeating the specified image to fill the given dimensions.
        /// </summary>
        /// <remarks>This method extends the <see cref="Image"/> class to support creating tiled bitmaps. 
        /// If the input image is not a <see cref="Bitmap"/>, the method returns <see langword="null"/>.</remarks>
        /// <param name="image">The source image to be tiled. Must be a valid <see cref="Image"/> instance.</param>
        /// <param name="width">The width of the resulting tiled bitmap, in pixels. Must be greater than 0.</param>
        /// <param name="height">The height of the resulting tiled bitmap, in pixels. Must be greater than 0.</param>
        /// <returns>A <see cref="Bitmap"/> containing the tiled image with the specified dimensions,  or <see langword="null"/>
        /// if the input <paramref name="image"/> is not a <see cref="Bitmap"/>.</returns>
        public static Bitmap Tile(this Image image, int width, int height)
        {
            return image is Bitmap bitmap ? BitmapExtensions.Tile(bitmap, new Size(width, height)) : null;
        }
    }
}
