using EnglishHub.Domain.UseCases.SignOn;
using EnglishHub.Storage.Models;

namespace EnglishHub.Storage.Storage;

public class SignOnStorage : ISignOnStorage
{
    private readonly EnglishHubDbContext _dbContext;

    public SignOnStorage(EnglishHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> CreateUser(string login, byte[] salt, byte[] passwordHash, CancellationToken cancellationToken)
    {
        Guid userId = Guid.NewGuid();

        _dbContext.Users.Add(new UserEntity()
        {
            Id = userId,
            Login = login,
            PasswordHash = passwordHash,
            Salt = salt
        });

        await _dbContext.SaveChangesAsync(cancellationToken);

        return userId;
    }
}