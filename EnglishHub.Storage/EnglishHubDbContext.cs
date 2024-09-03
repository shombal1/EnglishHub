using System.Reflection;
using EnglishHub.Storage.Configuration;
using EnglishHub.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishHub.Storage;

public class EnglishHubDbContext(DbContextOptions<EnglishHubDbContext> options) : DbContext(options)
{
    public DbSet<ForumEntity> Forums { get; set; }
    public DbSet<TopicEntity> Topics { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<SessionEntity> Sessions { get; set; }
    public DbSet<DomainEventEntity> DomainEvents { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(EnglishHubDbContext))!);
        
        base.OnModelCreating(modelBuilder);
    }
}