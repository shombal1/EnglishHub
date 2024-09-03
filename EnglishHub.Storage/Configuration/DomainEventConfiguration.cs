using EnglishHub.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishHub.Storage.Configuration;

public class DomainEventConfiguration : IEntityTypeConfiguration<DomainEventEntity>
{
    public void Configure(EntityTypeBuilder<DomainEventEntity> builder)
    {
        builder.HasKey(d => d.Id);
    }
}