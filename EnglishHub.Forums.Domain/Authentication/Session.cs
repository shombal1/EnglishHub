namespace EnglishHub.Forums.Domain.Authentication;

public class Session
{
    public Guid UserId { get; set; }
    public DateTimeOffset Expires { get; set; }
}