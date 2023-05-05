using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Songs.Api.Entities;

namespace Songs.Api.Persistence.Configurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Albums)
            .WithOne(x => x.Artist)
            .HasForeignKey(x => x.ArtistId);
    }
}