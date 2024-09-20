namespace EnglishHub.Forums.Domain.monitoring;

public interface IMonitoredRequest
{
    public void Monitor(DomainMetrics metrics, TagsBuilder builder);
}