using Microsoft.EntityFrameworkCore;
using CapstoneApp.Integrations.Models;

namespace CapstoneApp.Integrations.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
}