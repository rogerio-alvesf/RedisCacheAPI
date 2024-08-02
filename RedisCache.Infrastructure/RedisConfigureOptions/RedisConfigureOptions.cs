using StackExchange.Redis;

namespace RedisCache.Infrastructure
{
    public static class RedisConfigureOptions
    {
        public static ConfigurationOptions GetConfigurationOptions()
            => new ConfigurationOptions()
            {
                EndPoints =
                {
                    { "localhost", 6379 }
                },
                ReconnectRetryPolicy = new LinearRetry(5000),
                ConnectTimeout = 10000,
                AllowAdmin = true,
                AbortOnConnectFail = false
            };
    }
}
