using System;

namespace RateLimit.Core.Options
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/16 13:55:40
    /// </summary>
    public struct RateLimitCounter
    {
        public DateTime Timestamp { get; set; }

        public double Count { get; set; }
    }
}