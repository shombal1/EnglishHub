using System.Security.Cryptography;
using EnglishHub.Forums.Domain.Authentication;
using FluentAssertions;

namespace EnglishHub.Forums.Domain.Tests.Authentication;

public class TripleDesSymmetricEncryptorDecryptorShould
{
    private readonly byte[] _key=RandomNumberGenerator.GetBytes(24);
    private readonly TripleDesSymmetricEncryptorDecryptor _sut = new TripleDesSymmetricEncryptorDecryptor();

    [Theory]
    [InlineData("Hello world")]
    [InlineData("Big text has a number 123")]
    public async Task GenerateMeaningfulEncryptString(string plainText)
    {
        string encryptedString = await _sut.Encrypt(plainText, _key, CancellationToken.None);

        encryptedString.Should().NotBeEmpty();
    }

    [Theory]
    [InlineData("Hello world")]
    [InlineData("Big text has a number 123")]
    public async Task EncryptedDecryptedString_WhenKeyIsSame(string plainText)
    {
        string encryptedString = await _sut.Encrypt(plainText, _key, CancellationToken.None);

        string decryptedString = await _sut.Decrypt(encryptedString, _key, CancellationToken.None);

        decryptedString.Should().BeEquivalentTo(plainText);
    }

    [Theory]
    [InlineData("Hello world")]
    [InlineData("Big text has a number 123")]
    public async Task ThrowException_WhenDecryptEncryptStringWithDifferentKey(string plainText)
    {
        string encryptedString = await _sut.Encrypt(plainText, _key, CancellationToken.None);

        await _sut.Invoking(s =>
                s.Decrypt(encryptedString, RandomNumberGenerator.GetBytes(_key.Length), CancellationToken.None))
            .Should()
            .ThrowAsync<CryptographicException>();
    }
}