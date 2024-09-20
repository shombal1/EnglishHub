
using EnglishHub.Forums.Domain.Authentication;
using FluentAssertions;

namespace EnglishHub.Forums.Domain.Tests.Authentication;

public class PasswordManagerShould
{
    private static readonly byte[] EmptySalt = new byte[100];
    private static readonly byte[] EmptyHash = new byte[32];
    
    private readonly IPasswordManager _sut = new PasswordManager();

    [Theory]
    [InlineData("BestPassword")]
    [InlineData("qwerty5679e")]
    public void GenerateMeaningFullSaltAndHash(string password)
    {
        var (salt,hash) =  _sut.GeneratePassword(password);

        salt.Should().HaveCount(100).And.NotBeEquivalentTo(EmptySalt);
        hash.Should().HaveCount(32).And.NotBeEquivalentTo(EmptyHash);
    }

    [Theory]
    [InlineData("qWeRty32")]
    [InlineData("йцук35ен")]
    public void ReturnSuccess_WhenPasswordMatch(string password)
    {
        var (salt,hash) =_sut.GeneratePassword(password);

        _sut.ComparePassword(password, salt, hash);
    }
    
    [Theory]
    [InlineData("qWeRty32")]
    [InlineData("йцук35ен")]
    public void ReturnFalse_WhenPasswordNotMatch(string password)
    {
        var (salt,hash) =_sut.GeneratePassword(password);

        _sut.ComparePassword("paSSword", salt, hash);
    }
}