using System.ComponentModel.DataAnnotations;
namespace EnglishHub.Storage.Models;

public class UserEntity
{
    public Guid Id { get; set; }

    [MaxLength(30)]
    public string Login { get; set; } = "";
    
    public ICollection<TopicEntity> Topics { get; set; }
    
    public ICollection<CommentEntity> Comments { get; set; }
}