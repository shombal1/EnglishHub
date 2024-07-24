namespace EnglishHub.Api.Models;

public class Topic
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public DateTimeOffset PublicationAt { get; set; }
}