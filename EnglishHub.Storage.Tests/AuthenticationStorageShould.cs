using EnglishHub.Domain.Authentication;
using EnglishHub.Storage.Models;
using EnglishHub.Storage.Storage;
using FluentAssertions;

namespace EnglishHub.Storage.Tests;

public class AuthenticationStorageFixture : StorageTestFixture
{
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        await using var dbContext = GetDbContext();

        var user = new UserEntity()
        {
            Id = Guid.Parse("2DE9FD15-5A0C-43FB-B6AA-0ACCE08725EF"),
            Login = "Login",
            PasswordHash = new byte[] { 1 },
            Salt = new byte[] { 2 }
        };

        await dbContext.Users.AddAsync(user);

        await dbContext.SaveChangesAsync();
        
        await dbContext.Sessions.AddRangeAsync(new SessionEntity()
            {
                Id = Guid.Parse("2BD2EC61-AC70-4903-9F95-2C31C3BD2FC3"),
                Expires = new DateTimeOffset(2024, 10, 5, 3, 45, 2, TimeSpan.Zero),
                UserId = user.Id
            },
            new SessionEntity()
            {
                Id = Guid.Parse("63E41DF1-4FAC-44DE-8085-37722762F30C"),
                Expires = new DateTimeOffset(2024, 8, 2, 8, 33, 1, TimeSpan.Zero),
                UserId = user.Id
            });

        await dbContext.SaveChangesAsync();
    }
}

public class AuthenticationStorageShould : IClassFixture<AuthenticationStorageFixture>
{
    private readonly IAuthenticationServiceStorage _sut;

    private readonly AuthenticationStorageFixture _fixture;

    public AuthenticationStorageShould(AuthenticationStorageFixture fixture)
    {
        _fixture = fixture;
        _sut = new AuthenticationServiceStorage(fixture.GetDbContext(), fixture.GetMapper());
    }

    [Fact]
    public async Task ReturnSession_WhenSessionIsFound()
    {
        Guid sessionId = Guid.Parse("63E41DF1-4FAC-44DE-8085-37722762F30C");

        var actual = await _sut.FindSession(sessionId, CancellationToken.None);

        actual.Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnNull_WhenSessionIsNotFound()
    {
        Guid sessionId = Guid.Parse("0F255F43-3E50-4B7A-8B6F-F37ADE302A77");

        var actual = await _sut.FindSession(sessionId, CancellationToken.None);

        actual.Should().BeNull();
    }
}