using EnglishHub.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishHub.Storage.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.HasKey(key => key.Id);

        builder.HasOne(key => key.Author)
            .WithMany(key => key.Comments)
            .HasForeignKey(key=>key.AuthorId);
        
        builder.HasOne(key => key.Topic)
            .WithMany(key => key.Comments)
            .HasForeignKey(key=>key.TopicId);
    }
}