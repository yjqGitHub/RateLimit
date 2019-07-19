using System.Collections.Generic;

namespace RateLimit.Core.Options
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/15 17:10:11
    /// </summary>
    public class RateLimitClientPolicy : BaseRateLimitPolicy
    {
        public string CleintId { get; set; }

        public override IEnumerable<RateLimitRule> GetMatchedRules(ClientRequestIdentity clientRequestIdentity)
        {
            if (clientRequestIdentity.ClientId.IsClientMatched(CleintId))
                return base.GetMatchedRules(clientRequestIdentity);
            return null;
        }
    }
}