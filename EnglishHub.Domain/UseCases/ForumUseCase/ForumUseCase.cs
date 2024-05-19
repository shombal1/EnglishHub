using EnglishHub.Domain.Module;
using EnglishHub.Storage;
using Microsoft.EntityFrameworkCore;

namespace EnglishHub.Domain.UseCases.ForumUseCase;

public class ForumUseCase : IForumUseCase
{
    private readonly EnglishHubDbContext _dbContext;
    
    public ForumUseCase(EnglishHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Forum>> Get()
    {
        return await _dbContext.Forums.AsNoTracking().Select(a => new Forum()
        {
            Id = a.Id,
            Title = a.Title
        }).ToArrayAsync();
    }
}