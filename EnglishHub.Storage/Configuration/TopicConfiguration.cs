using EnglishHub.Storage.Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishHub.Storage.Configuration;

public class TopicConfiguration : IEntityTypeConfiguration<TopicEntity>
{
    public void Configure(EntityTypeBuilder<TopicEntity> builder)
    {
        builder.HasKey(key => key.Id);

        builder.HasOne(key => key.Forum)
            .WithMany(key => key.Topics)
            .HasForeignKey(key=>key.ForumId);

        builder.HasOne(key => key.Author)
            .WithMany(key => key.Topics)
            .HasForeignKey(key => key.AuthorId);

        builder.HasMany(key => key.Comments)
            .WithOne(key => key.Topic);
    }
}