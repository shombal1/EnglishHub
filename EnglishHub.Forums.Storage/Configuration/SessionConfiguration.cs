using EnglishHub.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishHub.Storage.Configuration;

public class SessionConfiguration : IEntityTypeConfiguration<SessionEntity>
{
    public void Configure(EntityTypeBuilder<SessionEntity> builder)
    {
        builder.HasKey(key => key.Id);

        builder.HasOne(key => key.User)
            .WithMany(key => key.Sessions)
            .HasForeignKey(key => key.UserId);
    }
}