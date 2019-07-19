using System;
using RateLimit.Core;

namespace RateLimit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("delete:/api/values".IsMatch("*:/api/values"));
            Console.WriteLine("delete:/api/values".IsMatch("*:/api/?"));
            Console.WriteLine("delete:/api/values".IsMatch("*:/api/*"));

            Console.WriteLine(IpParser.ContainsIp("127.0.0.0/10", "127.0.0.1"));
            Console.WriteLine(IpParser.ContainsIp("127.0.0.1/10", "127.0.0.1"));
            Console.WriteLine(IpParser.ContainsIp("127.0.1.0/10", "127.0.0.1"));
            Console.WriteLine(IpParser.ContainsIp("::1/10", "::9"));
            Console.WriteLine(IpParser.ContainsIp("::1/10", "127.0.0.1"));
            Console.ReadLine();
        }
    }
}
