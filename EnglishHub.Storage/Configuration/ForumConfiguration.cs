using EnglishHub.Storage.Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishHub.Storage.Configuration;

public class ForumConfiguration : IEntityTypeConfiguration<ForumEntity>
{
    public void Configure(EntityTypeBuilder<ForumEntity> builder)
    {
        builder.HasKey(key => key.Id);

        builder.HasMany(key => key.Topics)
            .WithOne(key => key.Forum);
    }
}