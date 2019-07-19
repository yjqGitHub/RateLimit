using System.Collections.Generic;
using System.Linq;

namespace RateLimit.Core.Options
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/15 14:23:54
    /// </summary>
    public class RateLimitPolicy : BaseRateLimitPolicy
    {
        public bool ClientIsRequired { get; set; } = false;

        public List<RateLimitIPPolicy> IPRules { get; set; } = new List<RateLimitIPPolicy>();

        public List<RateLimitClientPolicy> ClientRules { get; set; } = new List<RateLimitClientPolicy>();

        public List<RateLimitEndpointPolicy> EndpointRules { get; set; } = new List<RateLimitEndpointPolicy>();

        public override bool IsWhitelisted(ClientRequestIdentity clientRequestIdentity, out RateLimitNameListPolicyMatchedResult result)
        {
            if (!base.IsWhitelisted(clientRequestIdentity, out result))
            {
                foreach (var item in IPRules)
                {
                    if (item.IsWhitelisted(clientRequestIdentity, out result))
                    {
                        return true;
                    }
                }
                foreach (var item in ClientRules)
                {
                    if (item.IsWhitelisted(clientRequestIdentity, out result))
                    {
                        return true;
                    }
                }
                foreach (var item in EndpointRules)
                {
                    if (item.IsWhitelisted(clientRequestIdentity, out result))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public override IEnumerable<RateLimitRule> GetMatchedRules(ClientRequestIdentity clientRequestIdentity)
        {
            List<RateLimitRule> list = new List<RateLimitRule>();
            AddMatcherRules(list, base.GetMatchedRules(clientRequestIdentity));
            foreach (var item in IPRules)
            {
                AddMatcherRules(list, item.GetMatchedRules(clientRequestIdentity));
            }
            foreach (var item in ClientRules)
            {
                AddMatcherRules(list, item.GetMatchedRules(clientRequestIdentity));
            }
            foreach (var item in EndpointRules)
            {
                AddMatcherRules(list, item.GetMatchedRules(clientRequestIdentity));
            }
            return list.Distinct();
        }

        private void AddMatcherRules(List<RateLimitRule> list, IEnumerable<RateLimitRule> rateLimitRules)
        {
            if (rateLimitRules != null && rateLimitRules.Any())
            {
                list.AddRange(rateLimitRules);
            }
        }
    }
}