using System.Collections.Generic;

namespace RateLimit.Core.Options
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/15 16:29:05
    /// </summary>
    public class BaseRateLimitPolicy
    {
        public string RealIpHeader { get; set; }

        public string ClientIdHeader { get; set; }

        public RateLimitExceededResponse ExceededResponse { get; set; }

        public List<RateLimitWhitePolicy> WhiteList { get; set; }

        public List<RateLimitBlackPolicy> BlackList { get; set; }

        public List<RateLimitRule> Rules { get; set; } = new List<RateLimitRule>();

        public virtual bool IsWhitelisted(ClientRequestIdentity clientRequestIdentity, out RateLimitNameListPolicyMatchedResult result)
        {
            if (WhiteList != null)
            {
                string rule = null;
                if (!string.IsNullOrWhiteSpace(clientRequestIdentity.ClientId))
                {
                    if (WhiteList.Exists(m => m.LimitType == LimitType.Client && m.IsMatched(clientRequestIdentity.ClientId, out rule)))
                    {
                        result = new RateLimitNameListPolicyMatchedResult(LimitType.Client, rule);
                        return true;
                    }
                }
                string requestEndpoint = clientRequestIdentity.RequestEndpoint();
                if (WhiteList.Exists(m => m.LimitType == LimitType.Endpoint && m.IsMatched(requestEndpoint, out rule)))
                {
                    result = new RateLimitNameListPolicyMatchedResult(LimitType.Endpoint, rule);
                    return true;
                }
                if (WhiteList.Exists(m => m.LimitType == LimitType.IP && m.IsMatched(clientRequestIdentity.ClientIp, out rule)))
                {
                    result = new RateLimitNameListPolicyMatchedResult(LimitType.IP, rule);
                    return true;
                }
            }
            result = null;
            return false;
        }

        public virtual IEnumerable<RateLimitRule> GetMatchedRules(ClientRequestIdentity clientRequestIdentity)
        {
            foreach (var item in Rules)
            {
                if (clientRequestIdentity.RequestEndpoint().IsEndpointMatched(item.Endpoint))
                {
                    yield return item;
                }
            }
        }
    }
}