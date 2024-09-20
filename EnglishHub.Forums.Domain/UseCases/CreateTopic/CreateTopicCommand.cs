using System.Reflection;
using EnglishHub.Forums.Domain.Models;
using EnglishHub.Forums.Domain.monitoring;
using MediatR;

namespace EnglishHub.Forums.Domain.UseCases.CreateTopic;

public record CreateTopicCommand(Guid ForumId, string Title) : IRequest<Topic>, IMonitoredRequest
{
    private const string CounterName = "create.topic";

    public void Monitor(DomainMetrics metrics, TagsBuilder builder)
    {
        metrics.IncrementCount(CounterName, 1, builder.Build());
    }
}