namespace EnglishHub.Search.Storage.Models;

public class SearchEntity
{
    public Guid EntityId { get; set; }
    public int EntityType { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
}