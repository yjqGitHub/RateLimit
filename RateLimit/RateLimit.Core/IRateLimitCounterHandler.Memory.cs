using Microsoft.Extensions.Caching.Memory;
using RateLimit.Core.Options;
using System;

namespace RateLimit.Core
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/18 20:16:56
    /// </summary>
    public class RateLimitCounterMmeoryHandler : IRateLimitCounterHandler
    {
        private readonly IMemoryCache _cache;

        public RateLimitCounterMmeoryHandler(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public RateLimitCounter? Get(string id)
        {
            if (_cache.TryGetValue(id, out RateLimitCounter stored))
            {
                return stored;
            }

            return null;
        }

        public void Set(string id, RateLimitCounter counter, TimeSpan expirationTime)
        {
            var options = new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove
            };

            options.SetAbsoluteExpiration(expirationTime);

            _cache.Set(id, counter, options);
        }
    }
}