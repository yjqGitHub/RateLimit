using RateLimit.Core.Options;
using System;

namespace RateLimit.Core
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/17 17:37:09
    /// </summary>
    public interface IRateLimitCounterHandler
    {
        RateLimitCounter? Get(string id);

        void Set(string id, RateLimitCounter counter, TimeSpan expirationTime);
    }
}