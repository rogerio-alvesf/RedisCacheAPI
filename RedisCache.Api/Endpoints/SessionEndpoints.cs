using Microsoft.AspNetCore.Mvc;
using RedisCache.Application.Interfaces;
using RedisCache.Domain.Entities;

namespace RedisCache.Api.Endpoints
{
    public static class SessionEndpoints
    {
        public static void AddSessionEndpoints(WebApplication app)
        {
            var sessionEndpointApp = app.MapGroup("session");

            sessionEndpointApp.MapPost("/", async ([FromServices] IRedisCacheService _redisCacheService, [FromBody] Session session) =>
                await _redisCacheService.SetSessionAsync(session)).WithOpenApi(operation => new(operation)
                {
                    
                    Summary = "Cria uma nova sessão de usuário e armazena em Redis.",
                    Description = "Armazena a sessão com uma chave única (sessionId) e um tempo de expiração.",
                }).Accepts<Session>("application/json").WithName("SetSessionAsync");

            sessionEndpointApp.MapGet("/", async ([FromServices] IRedisCacheService _redisCacheService, [FromQuery] string sessionId) =>
                await _redisCacheService.GetSessionAsync(sessionId)).WithOpenApi(operation => new (operation)
                {
                    Summary = "Recupera os dados de uma sessão de usuário existente.",
                    Description = "Obtém os dados da sessão usando o sessionId como chave."
                }).WithName("GetSessionAsync");

            sessionEndpointApp.MapPut("/", async ([FromServices] IRedisCacheService _redisCacheService, string sessionId, [FromBody] Session updatedSession) =>
                await _redisCacheService.UpdateSessionAsync(sessionId, updatedSession)).WithOpenApi(operation => new(operation)
                {
                    Summary = "Atualiza os dados de uma sessão de usuário.",
                    Description = "Atualiza os dados da sessão existente em Redis."
                }).Accepts<Session>("application/json").WithName("UpdateSessionAsync");

            sessionEndpointApp.MapDelete("/", async ([FromServices] IRedisCacheService _redisCacheService, [FromQuery] string sessionId) =>
                await _redisCacheService.DeleteSessionAsync(sessionId)).WithOpenApi(operation => new(operation)
                {
                    Summary = "Exclui uma sessão de usuário.",
                    Description = "Remove a sessão de Redis usando o sessionId."
                }).WithName("DeleteSessionAsync");
        }
    }
}
