using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whale.Api.Providers
{
    public class ConfigProvider : IConfigProvider
    {
        public String RedisHostname
        {
            get
            {
                return Environment.GetEnvironmentVariable("REDIS_HOSTNAME") ?? "redis";
            }
        }
    }

    public interface IConfigProvider
    {
        String RedisHostname { get; }
    }
}
