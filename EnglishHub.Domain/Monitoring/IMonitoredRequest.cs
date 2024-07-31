namespace EnglishHub.Domain.monitoring;

public interface IMonitoredRequest
{
    public void Monitor(DomainMetrics metrics, TagsBuilder builder);
}