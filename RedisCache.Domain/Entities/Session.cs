namespace RedisCache.Domain.Entities
{
    public class Session
    {
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public Dictionary<string, string> Data { get; set; }
    }
}
