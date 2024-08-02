using RedisCache.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RedisCache.Application.Interfaces
{
    public interface IRedisCacheService
    {
        Task SetSessionAsync(Session session);
        Task<Session> GetSessionAsync(string sessionId);
        Task UpdateSessionAsync(string sessionId, Session updatedSession);
        Task DeleteSessionAsync(string sessionId);
    }
}
