namespace EnglishHub.Forums.Domain.UseCases.SignIn;

public class RecognizeUser
{
    public Guid UserId { get; set; }
    public byte[] Salt { get; set; } 
    public byte[] PasswordHash { get; set; } 
}