using RedisCache.Api.Endpoints;

namespace RedisCache.Api.Config
{
    public static class ConfigureEndpoints
    {
        public static void AddEndpoints(WebApplication app)
        {
            SessionEndpoints.AddSessionEndpoints(app);
        }
    }
}
