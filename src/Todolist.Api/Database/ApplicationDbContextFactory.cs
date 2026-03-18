using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Todolist.Api.Database;

// Enables `dotnet ef` commands without relying on ASP.NET hosting/config being present.
public sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__Application")
            ?? "Host=localhost;Database=todo_db;Username=admin;Password=mbstdQrYo12A79Dkx0hI68lb;Port=54321;";

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
