using Microsoft.EntityFrameworkCore;
using Npgsql;
using Science.Config;

namespace Science.DB;

public sealed class ApplicationContext : DbContext
{
    public DbSet<Article> Articles { get; init; } = null!;
    public DbSet<AdminUser> AdminUsers { get; init; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}

public static class DbExtensions
{
    public static void AddDB(this IServiceCollection services)
    {
        var dataSource = new NpgsqlDataSourceBuilder(Cfg.ConnectionString)
            .EnableDynamicJson()
            .Build();

        services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(dataSource));
    }
}
