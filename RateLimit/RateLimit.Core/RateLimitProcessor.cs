using RateLimit.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace RateLimit.Core
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/15 18:27:30
    /// </summary>
    public class RateLimitProcessor
    {
        private readonly RateLimitOptions _rateLimitOptions;
        private readonly IRateLimitCounterHandler _rateLimitCounterHandler;
        private static readonly object o_lock = new object();

        public RateLimitProcessor(RateLimitOptions rateLimitOptions, IRateLimitCounterHandler rateLimitCounterHandler)
        {
            _rateLimitOptions = rateLimitOptions;
            _rateLimitCounterHandler = rateLimitCounterHandler;
        }

        public RateLimitResult Process(ClientRequestIdentity clientRequestIdentity, RateLimitPolicy rateLimitPolicy, CancellationToken cancellationToken)
        {
            if (clientRequestIdentity == null) throw new ArgumentNullException(nameof(clientRequestIdentity));
            if (rateLimitPolicy == null)
            {
                return RateLimitResult.NoLimit("no rate limit policy.");
            }
            if (rateLimitPolicy.ClientIsRequired && string.IsNullOrWhiteSpace(clientRequestIdentity.ClientId))
            {
                return RateLimitResult.Limited("client id is required.");
            }
            if (rateLimitPolicy.IsWhitelisted(clientRequestIdentity, out RateLimitNameListPolicyMatchedResult result))
            {
                return RateLimitResult.NoLimit(result.ToString());
            }
            var matchedRules = rateLimitPolicy.GetMatchedRules(clientRequestIdentity);

            if (matchedRules != null && matchedRules.Any())
            {
                lock (o_lock)
                {
                    var rulesDict = new Dictionary<string, Tuple<RateLimitRule, RateLimitCounter>>();
                    foreach (var rule in matchedRules)
                    {
                        if (rule.Limit > 0)
                        {
                            var counterKey = ComputeCounterKey(rule);
                            var counter = GetCounter(counterKey, rule);
                            // check if key expired
                            if (counter.Timestamp + rule.PeriodTimespan.Value < DateTime.UtcNow)
                            {
                                continue;
                            }
                            // check if limit is reached
                            if (counter.Count > rule.Limit)
                            {
                                return RateLimitResult.Limited(rule.ToString());
                            }
                            rulesDict.Add(counterKey, Tuple.Create(rule, counter));
                        }
                        else
                        {
                            return RateLimitResult.Limited(rule.ToString());
                        }
                    }
                    if (rulesDict.Any())
                    {
                        foreach (var item in rulesDict)
                        {
                            _rateLimitCounterHandler.Set(item.Key, item.Value.Item2, item.Value.Item1.PeriodTimespan.Value);
                        }
                    }
                }
            }

            return RateLimitResult.Default;
        }

        private RateLimitCounter GetCounter(string counterKey, RateLimitRule rule)
        {
            var counter = new RateLimitCounter
            {
                Timestamp = DateTime.UtcNow,
                Count = 1
            };
            var entry = _rateLimitCounterHandler.Get(counterKey);
            if (entry.HasValue)
            {
                // entry has not expired
                if (entry.Value.Timestamp + rule.PeriodTimespan.Value >= DateTime.UtcNow)
                {
                    // increment request count
                    var totalRequests = entry.Value.Count + 1;

                    // deep copy
                    counter = new RateLimitCounter
                    {
                        Timestamp = entry.Value.Timestamp,
                        Count = totalRequests
                    };
                }
            }
            return counter;
        }

        public string ComputeCounterKey(RateLimitRule rule)
        {
            var key = $"{_rateLimitOptions.CounterPrefix}_{rule.LimitType.ToString()}_{rule.Pattern}_{rule.Endpoint}_{rule.Period}";

            var idBytes = Encoding.UTF8.GetBytes(key);

            byte[] hashBytes;

            using (var algorithm = SHA1.Create())
            {
                hashBytes = algorithm.ComputeHash(idBytes);
            }

            return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }
    }
}