﻿using RateLimit.Core.Options;
using System;

namespace RateLimit.Core
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/17 18:13:34
    /// </summary>
    public static class Extensions
    {
        public static string RetryAfterFrom(this DateTime timestamp, RateLimitRule rule)
        {
            var diff = timestamp + rule.PeriodTimespan.Value - DateTime.UtcNow;
            var seconds = Math.Max(diff.TotalSeconds, 1);

            return $"{seconds:F0}";
        }

        public static TimeSpan ToTimeSpan(this string timeSpan)
        {
            var l = timeSpan.Length - 1;
            var value = timeSpan.Substring(0, l);
            var type = timeSpan.Substring(l, 1);

            switch (type)
            {
                case "d": return TimeSpan.FromDays(double.Parse(value));
                case "h": return TimeSpan.FromHours(double.Parse(value));
                case "m": return TimeSpan.FromMinutes(double.Parse(value));
                case "s": return TimeSpan.FromSeconds(double.Parse(value));
                default: throw new FormatException($"{timeSpan} can't be converted to TimeSpan, unknown type {type}");
            }
        }
    }
}