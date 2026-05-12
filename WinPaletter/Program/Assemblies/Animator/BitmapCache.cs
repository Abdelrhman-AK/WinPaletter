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
                    // Return existing if not disposed
                    if (cached is not null)
                        return cached;
                    else
                        _cache.Remove(cacheKey);
                }

                // Create new bitmap
                Bitmap bmp = new Bitmap(width, height);
                _cache[cacheKey] = bmp;
                return bmp;
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
                    kvp.Value?.Dispose();
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
                    cached?.Dispose();
                    _cache.Remove(cacheKey);
                }
            }
        }
    }
}
