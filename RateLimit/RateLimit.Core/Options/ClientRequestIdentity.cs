namespace RateLimit.Core.Options
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/15 17:51:36
    /// </summary>
    public class ClientRequestIdentity
    {
        public string ClientIp { get; set; }

        public string ClientId { get; set; }

        public string Path { get; set; }

        public string HttpVerb { get; set; }

        public string RequestEndpoint()
        {
            if (string.IsNullOrWhiteSpace(HttpVerb)) return Path;
            return $"{HttpVerb}:{Path}";
        }
    }
}