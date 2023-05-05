using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Songs.Api.Entities;

namespace Songs.Api.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Songs");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    
    public async Task<int> SaveAsync(CancellationToken cancellationToken = default) 
        => await SaveChangesAsync(cancellationToken);
}