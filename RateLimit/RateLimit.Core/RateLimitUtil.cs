using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RateLimit.Core
{
    /// <summary>
    /// desc：
    /// author：yjq 2019/7/18 20:29:48
    /// </summary>
    public class RateLimitUtil
    {
        public void Test()
        {
            var fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles"));
            //https://www.cnblogs.com/artech/p/net-core-file-provider-01.html
            //ChangeToken.OnChange()
            //fileProvider.Watch()
            //分为两种文件一种是基本的配置文件 动态监听 
            //另一种为动态添加的规则 例如黑名单等等
        }
    }
}
