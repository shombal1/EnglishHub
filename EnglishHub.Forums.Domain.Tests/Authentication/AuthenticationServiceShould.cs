using System.Security.Cryptography;
using EnglishHub.Forums.Domain.Authentication;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Language.Flow;

namespace EnglishHub.Forums.Domain.Tests.Authentication;

public class AuthenticationServiceShould
{
    private readonly IAuthenticationService _sut;
    private readonly ISetup<IAuthenticationServiceStorage, Task<Session?>> _findSessionSetup;
    private readonly ISetup<ISymmetricDecryptor, Task<string>> _symmetricDecryptorSetup;

    public AuthenticationServiceShould()
    {
        Mock<ISymmetricDecryptor> symmetricDecryptor = new Mock<ISymmetricDecryptor>();
        Mock<IOptions<AuthenticationConfiguration>> option = new Mock<IOptions<AuthenticationConfiguration>>();
        Mock<IAuthenticationServiceStorage> storage = new Mock<IAuthenticationServiceStorage>();

        _symmetricDecryptorSetup = symmetricDecryptor.Setup(s =>
            s.Decrypt(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>()));
        _findSessionSetup = storage.Setup(s
            => s.FindSession(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

        option.Setup(o => o.Value).Returns(new AuthenticationConfiguration()
        {
            Base64Key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(24))
        });

        _sut = new AuthenticationService(symmetricDecryptor.Object, option.Object, storage.Object,NullLogger<AuthenticationService>.Instance);
    }

    [Fact]
    public async Task ReturnGuestIdentity_WhenTokenCannotBeDecrypt()
    {
        _symmetricDecryptorSetup.Throws(new CryptographicException());

        var identity = await _sut.Authenticate("invalid token", CancellationToken.None);

        identity.Should().BeEquivalentTo(User.Guest);
    }

    [Fact]
    public async Task ReturnGuestIdentity_WhenDecryptedTokenIsNotGuid()
    {
        _symmetricDecryptorSetup.ReturnsAsync("some text, but not a guid");
        
        var identity = await _sut.Authenticate("invalid token", CancellationToken.None);

        identity.Should().BeEquivalentTo(User.Guest);
    }
    
    [Fact]
    public async Task ReturnGuestIdentity_WhenSessionNotFound()
    {
        _symmetricDecryptorSetup.ReturnsAsync("FC4BEC1C-6EF2-4884-A2C8-933DC0C94132");
        _findSessionSetup.ReturnsAsync(() => null);

        var identity = await _sut.Authenticate("valid token", CancellationToken.None);

        identity.Should().BeEquivalentTo(User.Guest);
    }

    [Fact]
    public async Task ReturnGuestIdentity_WhenSessionExpired()
    {
        _symmetricDecryptorSetup.ReturnsAsync("FC4BEC1C-6EF2-4884-A2C8-933DC0C94132");
        _findSessionSetup.ReturnsAsync(new Session() { Expires = DateTimeOffset.Now - TimeSpan.FromDays(1) });

        var identity = await _sut.Authenticate("valid token but expired", CancellationToken.None);

        identity.Should().BeEquivalentTo(User.Guest);
    }

    [Fact]
    public async Task ReturnIdentity_WhenSessionIsValid()
    {
        Guid userId = Guid.Parse("67C9BB83-CA6A-4A49-9011-9D987291845E");
        Guid sessionId = Guid.Parse("46B28B4F-2D53-43F7-818C-AEB9B14C5600");
        _findSessionSetup.ReturnsAsync(new Session()
        {
            Expires = DateTimeOffset.Now + TimeSpan.FromDays(1),
            UserId = userId
        });
        _symmetricDecryptorSetup.ReturnsAsync(sessionId.ToString());

        var identity = await _sut.Authenticate("some token", CancellationToken.None);

        identity.UserId.Should().Be(userId);
        identity.SessionId.Should().Be(sessionId);
    }
}