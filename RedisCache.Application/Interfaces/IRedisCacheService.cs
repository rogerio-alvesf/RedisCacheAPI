using RedisCache.Domain.Entities;

namespace RedisCache.Application.Interfaces
{
    public interface IRedisCacheService
    {
        Task SetSessionAsync(Session session);
        Task<Session> GetSessionAsync(string sessionId);
        Task DeleteSessionAsync(string sessionId);
    }
}
