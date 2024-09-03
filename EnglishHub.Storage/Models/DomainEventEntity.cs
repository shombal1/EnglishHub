namespace EnglishHub.Storage.Models;

public class DomainEventEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public string CalledEntity { get; set; } = "";
}