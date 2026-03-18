using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Todolist.Api.Entities;
using Todolist.Api.Enums;

namespace Todolist.Api.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Todo> TodoItems { get; set; } = default!;
    public DbSet<UserOtp> UserOtps { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("todolist");

        modelBuilder.Entity<Todo>().HasIndex(t => t.Id);

        modelBuilder.Entity<Todo>().Property(t => t.Title).HasMaxLength(255).IsRequired();

        modelBuilder.Entity<Todo>().HasIndex(t => new { t.OwnerId, t.Title }).IsUnique();

        modelBuilder.Entity<Todo>().Property(t => t.Description).HasMaxLength(1000);

        modelBuilder
            .Entity<Todo>()
            .Property(t => t.Priority)
            .HasConversion<string>(t => t.ToString(), v => Enum.Parse<TodoPriority>(v));

        modelBuilder.Entity<Todo>().Property(t => t.OwnerId).IsRequired();

        modelBuilder
            .Entity<Todo>()
            .HasOne(t => t.Owner)
            .WithMany()
            .HasForeignKey(t => t.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserOtp>().HasIndex(o => new { o.UserId, o.Purpose, o.ExpiresAt });
        modelBuilder.Entity<UserOtp>().Property(o => o.Purpose).HasMaxLength(64).IsRequired();
        modelBuilder.Entity<UserOtp>().Property(o => o.CodeSalt).HasMaxLength(256).IsRequired();
        modelBuilder.Entity<UserOtp>().Property(o => o.CodeHash).HasMaxLength(256).IsRequired();
        modelBuilder.Entity<UserOtp>().Property(o => o.TokenProtected).IsRequired();

        modelBuilder
            .Entity<UserOtp>()
            .HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Apply a global snake_case naming policy for tables/columns without manually specifying names.
        ApplySnakeCaseNames(modelBuilder);
    }

    private static void ApplySnakeCaseNames(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (string.IsNullOrWhiteSpace(tableName))
            {
                continue;
            }

            var schema = entityType.GetSchema();

            var snakeTableName = ToSnakeCase(tableName);
            entityType.SetTableName(snakeTableName);

            var storeObject = StoreObjectIdentifier.Table(snakeTableName, schema);
            foreach (var property in entityType.GetProperties())
            {
                var columnName = property.GetColumnName(storeObject);
                if (string.IsNullOrWhiteSpace(columnName))
                {
                    continue;
                }

                property.SetColumnName(ToSnakeCase(columnName));
            }
        }
    }

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        var span = input.AsSpan();
        var sb = new System.Text.StringBuilder(input.Length + Math.Min(16, input.Length / 2));

        for (var i = 0; i < span.Length; i++)
        {
            var c = span[i];

            if (char.IsUpper(c))
            {
                var hasPrev = i > 0;
                var hasNext = i + 1 < span.Length;

                if (hasPrev)
                {
                    var prev = span[i - 1];
                    var prevIsUnderscore = prev == '_';
                    var prevIsUpper = char.IsUpper(prev);
                    var nextIsLower = hasNext && char.IsLower(span[i + 1]);

                    if (!prevIsUnderscore && (!prevIsUpper || nextIsLower))
                    {
                        sb.Append('_');
                    }
                }

                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }
}
