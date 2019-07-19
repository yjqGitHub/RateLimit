namespace RateLimit.Core.Options
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/15 15:56:08
    /// </summary>
    public class RateLimitExceededResponse
    {
        public int StatusCode { get; set; }

        public string Content { get; set; }
    }
}