using System.Collections.Generic;

namespace RateLimit.Core.Options
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/15 17:14:39
    /// </summary>
    public class RateLimitEndpointPolicy : BaseRateLimitPolicy
    {
        public string Endpoint { get; set; }

        public override IEnumerable<RateLimitRule> GetMatchedRules(ClientRequestIdentity clientRequestIdentity)
        {
            if (clientRequestIdentity.RequestEndpoint().IsEndpointMatched(Endpoint))
                return base.GetMatchedRules(clientRequestIdentity);
            return null;
        }
    }
}