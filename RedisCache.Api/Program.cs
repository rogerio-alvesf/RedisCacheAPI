using RedisCache.Api.IoC;
using RedisCache.Api.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DependencyInjectionConfig.Configure(builder.Services);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

ConfigureEndpoints.AddEndpoints(app);

app.UseHttpsRedirection();

app.Run();