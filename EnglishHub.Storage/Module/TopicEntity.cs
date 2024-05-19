namespace EnglishHub.Storage.Module;

public class TopicEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public DateTimeOffset PublicationAt { get; set; }
    public DateTimeOffset? UpdateAt { get; set; }
    public Guid AuthorId { get; set; }
    public UserEntity Author { get; set; }
    public ForumEntity Forum { get; set; }
    public Guid ForumId { get; set; }
    public ICollection<CommentEntity> Comments { get; set; }
}