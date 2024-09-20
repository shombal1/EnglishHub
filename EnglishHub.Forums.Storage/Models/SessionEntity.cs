namespace EnglishHub.Storage.Models;

public class SessionEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset Expires { get; set; } 
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
}