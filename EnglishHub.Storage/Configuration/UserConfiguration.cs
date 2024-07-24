using EnglishHub.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishHub.Storage.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(key => key.Id);

        builder.HasMany(key => key.Topics)
            .WithOne(key => key.Author);
        
        builder.HasMany(key => key.Comments)
            .WithOne(key => key.Author);

        builder.HasMany(key => key.Sessions)
            .WithOne(key => key.User);
    }
}