using System.Reflection;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.monitoring;
using MediatR;

namespace EnglishHub.Domain.UseCases.CreateTopic;

public record CreateTopicCommand(Guid ForumId, string Title) : IRequest<Topic>, IMonitoredRequest
{
    private const string CounterName = "create.topic";

    public void Monitor(DomainMetrics metrics, TagsBuilder builder)
    {
        metrics.IncrementCount(CounterName, 1, builder.Build());
    }
}