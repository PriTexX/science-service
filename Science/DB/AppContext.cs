using Microsoft.EntityFrameworkCore;
using Npgsql;
using Science.Config;

namespace Science.DB;

public sealed class AppContext : DbContext
{
    public DbSet<Article> Articles { get; init; } = null!;

    public AppContext(DbContextOptions<AppContext> options)
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

        services.AddDbContext<AppContext>(options => options.UseNpgsql(dataSource));
    }
}
