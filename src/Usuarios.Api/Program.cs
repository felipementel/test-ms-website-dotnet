using Usuarios.Adapters.Repositories;
using Usuarios.Api.Endpoints;
using Usuarios.Application.Services;
using Usuarios.Domain.Ports;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<UserService>();

var app = builder.Build();

var isOpenApiGeneration =
    Assembly.GetEntryAssembly()?.GetName().Name == "GetDocument.Insider";

if (!isOpenApiGeneration)
{
    // Coisas que não devem rodar durante a geração do OpenAPI
    // Ex: conexões externas, validações fortes de secrets, hosted services etc.
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapHealthEndpoints();
app.MapUserEndpoints();

await app.RunAsync();

public partial class Program
{
    protected Program()
    {
    }
}
