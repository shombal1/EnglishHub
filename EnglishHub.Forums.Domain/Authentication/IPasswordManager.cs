namespace EnglishHub.Forums.Domain.Authentication;

public interface IPasswordManager
{
    public bool ComparePassword(string password, byte[] salt, byte[] hash);

    public (byte[] Salt, byte[] Hash) GeneratePassword(string password);
}