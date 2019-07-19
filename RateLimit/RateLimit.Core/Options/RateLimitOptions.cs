namespace RateLimit.Core.Options
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/16 13:47:45
    /// </summary>
    public class RateLimitOptions
    {
        public string ConfigFilePath { get; set; }

        public bool IsWatch { get; set; } = true;

        public bool IsOvewrite { get; set; } = false;

        public string CounterPrefix { get; set; } = "RateLimit";
    }
}