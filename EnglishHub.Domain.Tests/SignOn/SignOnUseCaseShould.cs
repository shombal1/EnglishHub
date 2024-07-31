using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.UseCases.SignOn;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Moq.Language.Flow;

namespace EnglishHub.Domain.Tests.SignOn;

public class SignOnUseCaseShould
{
    private readonly SignOnUseCase _sut;
    private readonly ISetup<ISignOnStorage, Task<Guid>> _storageSetup;

    public SignOnUseCaseShould()
    {
        Mock<IValidator<SignOnCommand>> validator = new Mock<IValidator<SignOnCommand>>();
        Mock<ISignOnStorage> storage = new Mock<ISignOnStorage>();
        Mock<IPasswordManager> passwordManager = new Mock<IPasswordManager>();

        validator.Setup(v => v.ValidateAsync(It.IsAny<SignOnCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        passwordManager.Setup(p => p.GeneratePassword(It.IsAny<string>()))
            .Returns((new byte[] { 1 }, new byte[] { 2 }));

        _storageSetup = storage.Setup(s =>
            s.CreateUser(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>()));

        _sut = new SignOnUseCase(validator.Object, storage.Object, passwordManager.Object);
    }

    [Fact]
    public async Task ReturnIdentityNewlyCreatedUser()
    {
        Guid userId = Guid.Parse("5A65D276-D0D8-4A38-8566-3768CF612745");
        
        _storageSetup.ReturnsAsync(userId);

        IIdentity createdNewUser = await _sut.Handle(new SignOnCommand("some login", "some password"), CancellationToken.None);

        createdNewUser.UserId.Should().Be(userId);
    }
}