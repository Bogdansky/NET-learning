using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlteringBehavior.Services
{
    public class CacheService
    {
        private readonly Dictionary<string, decimal> cache;

        public CacheService()
        {
            cache = new Dictionary<string, decimal>();
        }

        public bool AddToCache(string key, decimal value)
        {
            return cache.TryAdd(key, value);
        }

        public bool Get(string key, out decimal result)
        {
            return cache.TryGetValue(key, out result);
        }
    }
}
