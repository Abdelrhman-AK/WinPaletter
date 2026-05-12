using System;
using System.Collections.Generic;

namespace AnimatorNS
{
    /// <summary>
    /// Generic object pool for reducing GC pressure
    /// </summary>
    /// <typeparam name="T">Type of object to pool</typeparam>
    public class ObjectPool<T> where T : class, new()
    {
        private readonly Stack<T> _pool = new Stack<T>();
        private readonly Func<T> _factory;
        private readonly Action<T> _resetAction;

        public ObjectPool(Func<T> factory = null, Action<T> resetAction = null, int initialCapacity = 10)
        {
            _factory = factory ?? (() => new T());
            _resetAction = resetAction;
            
            // Pre-populate pool
            for (int i = 0; i < initialCapacity; i++)
            {
                _pool.Push(_factory());
            }
        }

        /// <summary>
        /// Get an object from the pool
        /// </summary>
        public T Get()
        {
            if (_pool.Count > 0)
            {
                return _pool.Pop();
            }
            return _factory();
        }

        /// <summary>
        /// Return an object to the pool
        /// </summary>
        public void Return(T item)
        {
            if (item == null) return;
            
            _resetAction?.Invoke(item);
            _pool.Push(item);
        }

        /// <summary>
        /// Clear the pool
        /// </summary>
        public void Clear()
        {
            _pool.Clear();
        }

        /// <summary>
        /// Get current pool size
        /// </summary>
        public int Count => _pool.Count;
    }

    /// <summary>
    /// Specialized pool for byte arrays
    /// </summary>
    public class ByteArrayPool
    {
        private readonly Dictionary<int, Stack<byte[]>> _pools = new Dictionary<int, Stack<byte[]>>();
        private readonly object _lock = new object();

        /// <summary>
        /// Get a byte array of the specified size
        /// </summary>
        public byte[] Get(int size)
        {
            lock (_lock)
            {
                if (_pools.TryGetValue(size, out var pool) && pool.Count > 0)
                {
                    return pool.Pop();
                }
            }
            return new byte[size];
        }

        /// <summary>
        /// Return a byte array to the pool
        /// </summary>
        public void Return(byte[] array)
        {
            if (array == null) return;
            
            lock (_lock)
            {
                if (!_pools.TryGetValue(array.Length, out var pool))
                {
                    pool = new Stack<byte[]>();
                    _pools[array.Length] = pool;
                }
                
                // Limit pool size to prevent memory bloat
                if (pool.Count < 50)
                {
                    pool.Push(array);
                }
            }
        }

        /// <summary>
        /// Clear all pools
        /// </summary>
        public void Clear()
        {
            lock (_lock)
            {
                _pools.Clear();
            }
        }
    }

    /// <summary>
    /// Global object pools for the animator system
    /// </summary>
    public static class AnimatorPools
    {
        private static readonly ByteArrayPool _byteArrayPool = new ByteArrayPool();

        /// <summary>
        /// Get a byte array from the pool
        /// </summary>
        public static byte[] GetByteArray(int size) => _byteArrayPool.Get(size);

        /// <summary>
        /// Return a byte array to the pool
        /// </summary>
        public static void ReturnByteArray(byte[] array) => _byteArrayPool.Return(array);

        /// <summary>
        /// Clear all pools (useful for testing or cleanup)
        /// </summary>
        public static void ClearAll()
        {
            _byteArrayPool.Clear();
        }
    }
}
