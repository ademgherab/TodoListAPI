using Todolist.Api.Endpoints;
using Todolist.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

await app.MigrateAndSeedDevAsync();

app.UseApiMiddleware();

app.MapApiEndpoints();

app.Run();
