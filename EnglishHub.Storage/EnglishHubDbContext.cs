using EnglishHub.Storage.Configuration;
using EnglishHub.Storage.Module;
using Microsoft.EntityFrameworkCore;

namespace EnglishHub.Storage;

public class EnglishHubDbContext : DbContext
{
    public EnglishHubDbContext( DbContextOptions<EnglishHubDbContext> options ): base(options)
    {
                
    }
    
    public DbSet<ForumEntity> Forums { get; set; }
    public DbSet<TopicEntity> Topics { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ForumConfiguration());
        modelBuilder.ApplyConfiguration(new TopicConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}