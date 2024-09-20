using System.Security.Cryptography;
using EnglishHub.Forums.Domain.Authentication;
using EnglishHub.Forums.Domain.UseCases.SignIn;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Time.Testing;
using Moq;
using Moq.Language.Flow;

namespace EnglishHub.Forums.Domain.Tests.SignIn;

public class SignInUseCaseShould
{
    private readonly SignInUseCase _sut;

    private readonly Mock<ISignInStorage> _storage;
    private readonly Mock<ISymmetricEncryptor> _symmetricEncryptor;
    private readonly ISetup<ISignInStorage, Task<RecognizeUser?>> _findUserSetup;
    private readonly ISetup<IPasswordManager, bool> _passwordManagerSetup;
    private readonly ISetup<ISymmetricEncryptor, Task<string>> _symmetricEncryptorSetup;
    private readonly ISetup<ISignInStorage, Task<Guid>> _createSessionSetup;
    
    public SignInUseCaseShould()
    {
        Mock<IValidator<SignInCommand>> validator = new Mock<IValidator<SignInCommand>>();
        _storage = new Mock<ISignInStorage>();
        Mock<IPasswordManager> passwordManager = new Mock<IPasswordManager>();
        _symmetricEncryptor = new Mock<ISymmetricEncryptor>();
        Mock<IOptions<AuthenticationConfiguration>> option = new Mock<IOptions<AuthenticationConfiguration>>();

        _findUserSetup = _storage.Setup(
            s => s.FindUser(It.IsAny<string>(), It.IsAny<CancellationToken>()));
        _passwordManagerSetup = passwordManager.Setup(p =>
            p.ComparePassword(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>()));
        _symmetricEncryptorSetup = _symmetricEncryptor.Setup(s =>
            s.Encrypt(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>()));
        _createSessionSetup = _storage.Setup(s =>
            s.CreateSession(It.IsAny<Guid>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()));
        option.Setup(o => o.Value).Returns(new AuthenticationConfiguration()
        {
            Base64Key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(24))
        });
        
        _sut = new SignInUseCase(validator.Object, _storage.Object, passwordManager.Object, _symmetricEncryptor.Object,
            option.Object,new FakeTimeProvider());
    }

    [Fact]
    public async Task ThrowValidationException_WhenUserNotFound()
    {
        var command = new SignInCommand("some login", "qwerty");

        _findUserSetup.ReturnsAsync(() => null);

        (await _sut.Invoking(s => s.Handle(command, CancellationToken.None))
                .Should().ThrowAsync<ValidationException>()).Which.Errors.Should()
            .Contain(e => e.PropertyName == nameof(command.Login));
    }

    [Fact]
    public async Task ThrowValidationException_WhenPasswordDoesntMatch()
    {
        var command = new SignInCommand("some login", "qwerty");

        _findUserSetup.ReturnsAsync(new RecognizeUser());
        _passwordManagerSetup.Returns(false);

        (await _sut.Invoking(s => s.Handle(command, CancellationToken.None))
                .Should().ThrowAsync<ValidationException>()).Which.Errors.Should()
            .Contain(e => e.PropertyName == nameof(command.Password));
    }

    [Fact]
    public async Task CreateSession_WhenPasswordMatches()
    {
        Guid userId = Guid.Parse("AD798DE5-8B5D-47C7-AD52-BE5C1C4CB6D6");
        Guid sessionId = Guid.Parse("DB96D4B9-C4CA-4397-9E80-C8CA3FFFE36A");

        _createSessionSetup.ReturnsAsync(sessionId);
        _findUserSetup.ReturnsAsync(new RecognizeUser() { UserId = userId });
        _passwordManagerSetup.Returns(true);

        var (identity,_) = await _sut.Handle(new SignInCommand("Login", "1111"), CancellationToken.None);
        
        _storage.Verify(s=>
            s.CreateSession(userId,It.IsAny<DateTimeOffset>(),CancellationToken.None),Times.Once);
    }
    
    [Fact]
    public async Task ReturnTokenAndIdentity()
    {
        var command = new SignInCommand("some login", "qwerty");
        Guid userId = Guid.Parse("8E2F99D6-DAA2-4DD2-BC87-9C51442C0AC0");
        Guid sessionId = Guid.Parse("5B045F2B-AA79-4857-B970-E82AA9F0A6C0");
        string expectedToken = "token"; 
        
        _findUserSetup.ReturnsAsync(new RecognizeUser()
        {
            Salt = [1],
            PasswordHash = [2],
            UserId = userId
        });
        _createSessionSetup.ReturnsAsync(sessionId);
        _passwordManagerSetup.Returns(true);
        _symmetricEncryptorSetup.ReturnsAsync(expectedToken);

        var (identity,token) = await _sut.Handle(command, CancellationToken.None);

        identity.Should().NotBeNull();
        identity.UserId.Should().Be(userId);
        identity.SessionId.Should().Be(sessionId);
        token.Should().Be(expectedToken);
    }

    [Fact]
    public async Task EncryptSessionIdIntoToken()
    {
        Guid userId = Guid.Parse("AD798DE5-8B5D-47C7-AD52-BE5C1C4CB6D6");
        Guid sessionId = Guid.Parse("db96d4b9-c4ca-4397-9e80-c8ca3fffe36a");

        _createSessionSetup.ReturnsAsync(sessionId);
        _findUserSetup.ReturnsAsync(new RecognizeUser() { UserId = userId });
        _passwordManagerSetup.Returns(true);

        await _sut.Handle(new SignInCommand("Login", "1111"), CancellationToken.None);
        
        _symmetricEncryptor.Verify(s=>
            s.Encrypt("db96d4b9-c4ca-4397-9e80-c8ca3fffe36a",It.IsAny<byte[]>(),It.IsAny<CancellationToken>()));

        
    }
}