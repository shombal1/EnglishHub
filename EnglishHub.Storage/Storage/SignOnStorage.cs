using EnglishHub.Domain.UseCases.SignOn;
using EnglishHub.Storage.Models;

namespace EnglishHub.Storage.Storage;

public class SignOnStorage(EnglishHubDbContext dbContext) : ISignOnStorage
{
    public async Task<Guid> CreateUser(string login, byte[] salt, byte[] passwordHash, CancellationToken cancellationToken)
    {
        Guid userId = Guid.NewGuid();

        dbContext.Users.Add(new UserEntity()
        {
            Id = userId,
            Login = login,
            PasswordHash = passwordHash,
            Salt = salt
        });

        await dbContext.SaveChangesAsync(cancellationToken);

        return userId;
    }
}