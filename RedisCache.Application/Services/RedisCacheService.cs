using Newtonsoft.Json;
using RedisCache.Application.Interfaces;
using RedisCache.Domain.Entities;
using StackExchange.Redis;

namespace RedisCache.Application.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisCacheService(IConnectionMultiplexer redis)
            => _redis = redis;

        public async Task SetSessionAsync(Session session)
        {
            var db = _redis.GetDatabase();
            var serializedSession = JsonConvert.SerializeObject(session);
            //var x = DateTime.UtcNow;
            //x.AddHours(-3);
            DateTime getBrazilTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            var expiryTime = session.ExpiresAt - getBrazilTime;
            await db.StringSetAsync(session.SessionId, serializedSession, expiryTime);
        }

        public async Task<Session> GetSessionAsync(string sessionId)
        {
            var db = _redis.GetDatabase();
            var serializedSession = await db.StringGetAsync(sessionId);

            return serializedSession.IsNullOrEmpty
                ? null
                : JsonConvert.DeserializeObject<Session>(serializedSession);
        }

        public async Task UpdateSessionAsync(string sessionId, Session updatedSession)
        {
            var session = await GetSessionAsync(sessionId);
            if (session != null)
            {
                session.Data = updatedSession.Data;
                await SetSessionAsync(session);
            }
        }

        public async Task DeleteSessionAsync(string sessionId)
        {
            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync(sessionId);
        }
    }
}
