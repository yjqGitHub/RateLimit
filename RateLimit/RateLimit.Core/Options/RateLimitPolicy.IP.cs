using System.Collections.Generic;

namespace RateLimit.Core.Options
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/15 17:07:18
    /// </summary>
    public class RateLimitIPPolicy : BaseRateLimitPolicy
    {
        public string IP { get; set; }

        public override IEnumerable<RateLimitRule> GetMatchedRules(ClientRequestIdentity clientRequestIdentity)
        {
            if (clientRequestIdentity.ClientIp.IsIPMatched(IP))
                return base.GetMatchedRules(clientRequestIdentity);
            return null;
        }
    }
}