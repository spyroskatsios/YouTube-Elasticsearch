using Microsoft.EntityFrameworkCore;
using Songs.Api.Entities;

namespace Songs.Api.Persistence;

public interface IAppDbContext
{
    DbSet<Artist> Artists { get; set; }
    DbSet<Album> Albums { get; set; }
    DbSet<Song> Songs { get; set; }
    DbSet<Genre> Genres { get; set; }
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
}