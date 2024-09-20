using System.ComponentModel.DataAnnotations;

namespace EnglishHub.Storage.Models;

public class TopicEntity
{
    public Guid Id { get; set; }
    [MaxLength(100)] 
    public string Title { get; set; } = "";
    public DateTimeOffset PublicationAt { get; set; }
    public DateTimeOffset? UpdateAt { get; set; }
    public Guid AuthorId { get; set; }
    public UserEntity Author { get; set; }
    public ForumEntity Forum { get; set; }
    public Guid ForumId { get; set; }
    public ICollection<CommentEntity> Comments { get; set; }
}