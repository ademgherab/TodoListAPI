using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todolist.Api.Database;
using Todolist.Api.Entities;

namespace Todolist.Api.Extensions;

public static class WebApplicationExtensions
{
    public static async Task<WebApplication> MigrateAndSeedDevAsync(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return app;
        }

        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await db.Database.MigrateAsync();

        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    public static WebApplication UseApiMiddleware(this WebApplication app)
    {
        app.UseExceptionHandler();

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}
