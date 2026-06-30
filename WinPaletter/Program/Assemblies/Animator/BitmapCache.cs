using System;
using System.Collections.Generic;
using System.Drawing;

namespace AnimatorNS
{
    /// <summary>
    /// Simple bitmap cache to reduce GC pressure during animation startup
    /// </summary>
    public static class BitmapCache
    {
        private static readonly Dictionary<string, Bitmap> _cache = new Dictionary<string, Bitmap>();
        private static readonly object _lock = new object();

        /// <summary>
        /// Get or create a bitmap with caching
        /// </summary>
        public static Bitmap GetOrCreate(int width, int height, string key)
        {
            lock (_lock)
            {
                string cacheKey = $"{key}_{width}x{height}";

                if (_cache.TryGetValue(cacheKey, out Bitmap cached))
                {
                    // Validate the cached bitmap is still valid
                    if (cached != null && !IsDisposed(cached) && cached.Width == width && cached.Height == height)
                    {
                        // Return a clone to avoid sharing issues
                        return CloneBitmap(cached);
                    }
                    else
                    {
                        if (cached != null)
                        {
                            try { cached.Dispose(); } catch { }
                        }
                        _cache.Remove(cacheKey);
                    }
                }

                // Create new bitmap
                Bitmap bmp = new Bitmap(width, height);
                _cache[cacheKey] = bmp;
                // Return a clone to avoid sharing issues
                return CloneBitmap(bmp);
            }
        }

        /// <summary>
        /// Check if a bitmap is disposed
        /// </summary>
        private static bool IsDisposed(Bitmap bmp)
        {
            try
            {
                // This will throw if disposed
                int width = bmp.Width;
                return false;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// Clone a bitmap safely
        /// </summary>
        private static Bitmap CloneBitmap(Bitmap source)
        {
            if (source == null) return null;
            try
            {
                return new Bitmap(source);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Clear cache (call during app shutdown)
        /// </summary>
        public static void Clear()
        {
            lock (_lock)
            {
                foreach (var kvp in _cache)
                {
                    try { kvp.Value?.Dispose(); } catch { }
                }
                _cache.Clear();
            }
        }

        /// <summary>
        /// Remove specific bitmap from cache
        /// </summary>
        public static void Remove(string key, int width, int height)
        {
            lock (_lock)
            {
                string cacheKey = $"{key}_{width}x{height}";
                if (_cache.TryGetValue(cacheKey, out Bitmap cached))
                {
                    try { cached?.Dispose(); } catch { }
                    _cache.Remove(cacheKey);
                }
            }
        }
    }
}