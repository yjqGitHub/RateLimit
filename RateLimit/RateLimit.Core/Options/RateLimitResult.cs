namespace RateLimit.Core.Options
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/16 14:02:31
    /// </summary>
    public class RateLimitResult
    {
        public static readonly RateLimitResult Default = new RateLimitResult(false, "check success.");

        public RateLimitResult(bool isLimited, string message)
        {
            IsLimited = isLimited;
            Message = message;
        }

        public bool IsLimited { get; set; }

        public string Message { get; set; }

        public static RateLimitResult Limited(string msg)
        {
            return new RateLimitResult(true, msg);
        }

        public static RateLimitResult NoLimit(string msg)
        {
            return new RateLimitResult(false, msg);
        }
    }

    public class RateLimitNameListPolicyMatchedResult
    {
        public RateLimitNameListPolicyMatchedResult(LimitType limitType, string value)
        {
            LimitType = limitType;
            Value = value;
        }

        public LimitType LimitType { get; }

        public string Value { get; }

        public override string ToString()
        {
            return $"Matched in {LimitType} {Value}";
        }
    }
}