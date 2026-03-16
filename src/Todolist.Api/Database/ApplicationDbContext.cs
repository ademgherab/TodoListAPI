using Microsoft.EntityFrameworkCore;
using Todolist.Api.Entities;
using Todolist.Api.Enums;

namespace Todolist.Api.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Todo> TodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("todolist");

        modelBuilder.Entity<Todo>()
            .HasIndex(t => t.Id);

        modelBuilder.Entity<Todo>()
            .Property(t => t.Title)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Todo>()
            .HasIndex(t => t.Title)
            .IsUnique();

        modelBuilder.Entity<Todo>()
            .Property(t => t.Description)
            .HasMaxLength(1000);

        modelBuilder.Entity<Todo>()
            .Property(t => t.Priority)
            .HasConversion<string>(t => t.ToString(), v => Enum.Parse<TodoPriority>(v));
    }
}