namespace EnglishHub.Storage.Module;

public class ForumEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = "";

    public ICollection<TopicEntity> Topics { get; set; }
}