using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingSystem
{
    public class CacheItem<T>
    {

        public CacheItem(T value, DateTime expiredTime)
        {
            Value = value;
            ExpiredTime = expiredTime;
        }
       
        public DateTime ExpiredTime { get; set; }
        public T Value { get; set; }

        public bool IsExpired()
        => ExpiredTime < DateTime.Now;
    }
}
