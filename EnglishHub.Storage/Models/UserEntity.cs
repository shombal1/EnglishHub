using System.ComponentModel.DataAnnotations;
namespace EnglishHub.Storage.Models;

public class UserEntity
{
    public Guid Id { get; set; }

    [MaxLength(30)]
    public string Login { get; set; } = "";
    [MaxLength(120)]
    public byte[] Salt { get; set; }
    [MaxLength(32)]
    public byte[] PasswordHash { get; set; } 
    public ICollection<TopicEntity> Topics { get; set; }
    public ICollection<CommentEntity> Comments { get; set; }
    public ICollection<SessionEntity> Sessions { get; set; }
}