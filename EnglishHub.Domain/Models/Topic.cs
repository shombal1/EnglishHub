namespace EnglishHub.Domain.Models;

public class Topic
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public DateTimeOffset PublicationAt { get; set; }
    public Guid ForumId { get; set; }
    public Guid AuthorId { get; set; }
}