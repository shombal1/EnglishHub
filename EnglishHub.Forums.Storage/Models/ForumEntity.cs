using System.ComponentModel.DataAnnotations;

namespace EnglishHub.Storage.Models;

public class ForumEntity
{
    public Guid Id { get; set; }

    [MaxLength(200)]
    public string Title { get; set; } = "";

    public ICollection<TopicEntity> Topics { get; set; }
}