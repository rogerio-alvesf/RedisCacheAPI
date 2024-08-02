using RedisCache.Application.Interfaces;
using RedisCache.Application.Services;
using RedisCache.Infrastructure;
using StackExchange.Redis;

namespace RedisCache.Api.IoC
{
    public static class DependencyInjectionConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IRedisCacheService, RedisCacheService>();
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(RedisConfigureOptions.GetConfigurationOptions()));
        }
    }
}
