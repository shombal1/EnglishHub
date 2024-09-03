using System.ComponentModel.DataAnnotations;

namespace EnglishHub.Storage.Models;

public class CommentEntity
{
    public Guid Id { get; set; }
    
    [MaxLength(1000)]
    public string Text { get; set; } = "";
    
    public DateTimeOffset PublicationAt { get; set; }
    
    public DateTimeOffset? UpdateAt { get; set; }
    public Guid TopicId { get; set; }
    public TopicEntity Topic { get; set; }
    public Guid AuthorId { get; set; }
    public UserEntity Author { get; set; }
}