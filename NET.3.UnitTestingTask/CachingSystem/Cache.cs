using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CachingSystem
{
    /// <summary>
    /// Represents simple cache system
    /// </summary>
    /// <typeparam name="TKey">Type of key value to put in the cache</typeparam>
    /// <typeparam name="TValue">Type of value to put in the cache</typeparam>
    public class Cache<TKey, TValue>
    {
        private Timer cleanupTimer = new Timer()
        {
            AutoReset = true,
            Enabled = true,
            Interval = 10000
        };

        private Dictionary<TKey, CacheItem<TValue>> cache;

        /// <summary>
        /// Creates instance of <see cref="Cache{TKey, TValue}"/> class./>"
        /// </summary>
        /// <param name="timeOfLife">Time of life while data will be removed.</param>
        /// <param name="capasity">Capacity of the cache.</param>
        /// <param name="dictionary">Pair of elements such as key-value to initialize instance of the cache.</param>
        /// <param name="comparer">Equality Comparer for the chosen type</param>
        public Cache(TimeSpan timeOfLife, int capacity, IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer = null)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TKey>.Default;
            }

            cleanupTimer.Elapsed += Clear;
            cache = new Dictionary<TKey, CacheItem<TValue>>(capacity, comparer);
            Add(dictionary, timeOfLife);
        }

        /// <summary>
        /// Add elements into the cache.
        /// </summary>
        /// <param name="key">Key value to add into the cache.</param>
        /// <param name="value">Value to add into the cache.</param>
        /// <param name="timeOfLife">Time of life while data will be removed.</param>
        public void Add(TKey key, TValue value, TimeSpan timeOfLife)
        {
            cache.Add(key, new CacheItem<TValue>(value, DateTime.Now.Add(timeOfLife)));
        }

        /// <summary>
        /// Add elements into the cache.
        /// </summary>
        /// <param name="dictionary">Pair of elements such as key-value to initialize instance of the cache.</param>
        /// <param name="timeOfLife">Time of life while data will be removed.</param>
        public void Add(IDictionary<TKey, TValue> dictionary, TimeSpan timeOfLife)
        {
            foreach (var item in dictionary)
            {
                Add(item.Key, item.Value, timeOfLife);
            }
        }

        /// <summary>
        /// Returns a value by a key.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when key has null value</exception>
        /// <exception cref="ArgumentException">Thrown when key doesn't exist or its lifetime is over.</exception>
        /// <param name="key">Key element to find necessary value.</param>
        /// <returns>Value which relates by the key.</returns>
        public TValue Get(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException($"Key {nameof(key)} has null value.");
            }

            if (cache.ContainsKey(key) && cache[key].ExpiredTime > DateTime.Now)
            {
                return cache[key].Value;
            }

            throw new ArgumentException($"Such key {nameof(key)} doesn't exist.");
        }

        private void Remove(TKey key)
        {
            if (cache.ContainsKey(key))
            {
                cache.Remove(key);
            }
        }

        private void Clear(object sender, ElapsedEventArgs eventArgs)
        {
            cache.Keys.ToList().ForEach(x =>
            {
                if (cache[x].ExpiredTime <= eventArgs.SignalTime)
                {
                    this.Remove(x);
                }
            });
        }
    }
}
