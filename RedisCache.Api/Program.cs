using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using RedisCache.Application.Interfaces;
using RedisCache.Application.Services;
using RedisCache.Domain.Entities;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
var options = new ConfigurationOptions
{
    EndPoints =
    {
        { "localhost", 6379 }
    },
    ReconnectRetryPolicy = new LinearRetry(5000), // Retry every 5 seconds
    ConnectTimeout= 10000,
    AllowAdmin = true,
    AbortOnConnectFail = false
};
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(options));
builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();

var app = builder.Build();

app.MapPost("session", async ([FromServices] IRedisCacheService _redisCacheService, [FromBody] Session session) =>
    await _redisCacheService.SetSessionAsync(session));

app.MapGet("session", async ([FromServices] IRedisCacheService _redisCacheService, [FromQuery] string sessionId) =>
    await _redisCacheService.GetSessionAsync(sessionId));

app.MapDelete("session", async ([FromServices] IRedisCacheService _redisCacheService, [FromQuery] string sessionId) =>
    await _redisCacheService.DeleteSessionAsync(sessionId));

app.UseHttpsRedirection();

app.Run();