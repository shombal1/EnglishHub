using EnglishHub.Domain.Models;
using EnglishHub.Domain.monitoring;
using MediatR;

namespace EnglishHub.Domain.UseCases.GetForum;

public record class GetForumQuery():IRequest<IEnumerable<Forum>>,IMonitoredRequest
{
    private const string CounterName = "get.forum";
    
    public void Monitor(DomainMetrics metrics, TagsBuilder builder)
    {
        metrics.IncrementCount(CounterName,1,builder.Build());
    }
}