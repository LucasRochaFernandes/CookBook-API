using CookBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Infra;
public class AppDbContext : DbContext
{

    private readonly IConfiguration _configuration;
    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<CodeToPerformAction> CodeToPerformActions { get; set; }

    public AppDbContext(IServiceProvider serviceProvider, DbContextOptions<AppDbContext> options)
        : base(options)
    {
        _configuration = serviceProvider.GetRequiredService<IConfiguration>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }


}
