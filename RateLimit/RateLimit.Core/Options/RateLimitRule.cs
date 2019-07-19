using System;

namespace RateLimit.Core.Options
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/15 14:49:23
    /// </summary>
    public class RateLimitRule
    {
        /// <summary>
        /// HTTP verb and path or methodname
        /// </summary>
        /// <example>
        /// get:/api/values
        /// *:/api/values
        /// *
        /// </example>
        public string Endpoint { get; set; }

        /// <summary>
        /// Rate limit period as in 1s, 1m, 1h
        /// </summary>
        public string Period { get; set; }

        public TimeSpan? PeriodTimespan { get; set; }

        /// <summary>
        /// Maximum number of requests that a client can make in a defined period
        /// </summary>
        public double Limit { get; set; }

        internal LimitType LimitType { get; set; }

        internal string Pattern { get; set; }

        internal void ChangeLimitPattern(string pattern, LimitType limitType)
        {
            LimitType = limitType;
            Pattern = pattern;
        }

        public override string ToString()
        {
            return $"the endpoint {Endpoint} with pattern {Pattern} of {LimitType} limit type limit {Limit} count in {Period}.";
        }
    }
}