using System.Collections.Generic;

namespace RateLimit.Core.Options
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/15 15:06:42
    /// </summary>

    public abstract class BaseRateLimitNameListPolicy
    {
        public LimitType LimitType { get; set; }

        public List<string> Values { get; set; }

        public virtual bool IsMatched(string value, out string rule)
        {
            rule = null;
            if (Values == null) return false;
            switch (LimitType)
            {
                case LimitType.Endpoint:
                    return IsEndpointMatched(value, out rule);

                case LimitType.IP:
                    return IsIPMatched(value, out rule);

                case LimitType.Client:
                    return IsClientMatched(value, out rule);

                default:
                    return false;
            }
        }

        private bool IsEndpointMatched(string value, out string rule)
        {
            foreach (var item in Values)
            {
                if (value.IsEndpointMatched(item))
                {
                    rule = item;
                    return true;
                }
            }
            rule = null;
            return false;
        }

        private bool IsIPMatched(string value, out string rule)
        {
            foreach (var item in Values)
            {
                if (value.IsIPMatched(item))
                {
                    rule = item;
                    return true;
                }
            }
            rule = null;
            return false;
        }

        private bool IsClientMatched(string value, out string rule)
        {
            foreach (var item in Values)
            {
                if (value.IsClientMatched(item))
                {
                    rule = item;
                    return true;
                }
            }
            rule = null;
            return false;
        }
    }

    public class RateLimitWhitePolicy : BaseRateLimitNameListPolicy
    {
    }

    public class RateLimitWhitePolicys
    {
        public List<RateLimitWhitePolicy> WhiteList { get; set; } = new List<RateLimitWhitePolicy>();
    }

    public class RateLimitBlackPolicy : BaseRateLimitNameListPolicy
    {
    }

    public class RateLimitBlackPolicys
    {
        public List<RateLimitBlackPolicy> BlackList { get; set; } = new List<RateLimitBlackPolicy>();
    }

    public enum LimitType
    {
        IP = 1,
        Endpoint = 2,
        Client = 3,
        Public = 4
    }
}