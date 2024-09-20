using EnglishHub.Forums.Domain.UseCases.SignIn;
using EnglishHub.Storage.Models;
using EnglishHub.Storage.Storage;
using FluentAssertions;

namespace EnglishHub.Forums.Storage.Tests;

public class SignInStorageShould : IClassFixture<StorageTestFixture>
{
    private readonly StorageTestFixture _fixture;
    private readonly ISignInStorage _sut;

    public SignInStorageShould(StorageTestFixture fixture)
    {
        _fixture = fixture;
        
        _sut = new SignInStorage(fixture.GetDbContext(), _fixture.GetMapper());
    }

    [Fact]
    public async Task FindUserByLogin()
    {
        await using var dbContext = _fixture.GetDbContext();
        Guid userId = Guid.Parse("9AD499D3-0F7B-4337-B37C-5F2B7275AD2E");
        string userTitle = "TestUser1";
        
        await dbContext.Users.AddRangeAsync(new UserEntity()
            {
                Login = userTitle,
                Id = userId,
                Salt = new byte[] { 1 },
                PasswordHash = new byte[] { 2 }

            },
            new UserEntity()
            {
                Login = "TestUser2",
                Id = Guid.Parse("09A74398-3188-4DA8-BE25-15E429494148"),
                Salt = new byte[] { 3 },
                PasswordHash = new byte[] { 4 }
            });

        await dbContext.SaveChangesAsync();

        var actual = await _sut.FindUser(userTitle, CancellationToken.None);

        actual.Should().NotBeNull();
        actual.UserId.Should().Be(userId);
    }
}