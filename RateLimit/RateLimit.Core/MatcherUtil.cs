namespace RateLimit.Core
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/16 14:58:55
    /// </summary>
    public static class MatcherUtil
    {
        public static bool IsClientMatched(this string client, string pattern)
        {
            return client.IsMatch(pattern);
        }

        public static bool IsEndpointMatched(this string endpoint, string pattern)
        {
            return endpoint.IsMatch(pattern);
        }

        public static bool IsIPMatched(this string ip, string pattern)
        {
            return IpAddressUtil.ContainsIp(pattern, ip);
        }
    }
}