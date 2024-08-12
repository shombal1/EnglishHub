using System.Diagnostics;
using EnglishHub.Domain.UseCases.GetForum;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnglishHub.Domain.monitoring;

public class MonitoringPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<MonitoringPipelineBehavior<TRequest, TResponse>> _logger;
    private readonly DomainMetrics _metrics;

    public MonitoringPipelineBehavior(
        ILogger<MonitoringPipelineBehavior<TRequest, TResponse>> logger,
        DomainMetrics metrics)
    {
        _logger = logger;
        _metrics = metrics;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not IMonitoredRequest monitoredRequest) return await next.Invoke();

        using Activity? activity = _metrics.ActivitySource.StartActivity("usecase");
        activity?.AddTag("command", request.GetType().Name);
        
        try
        {
            var result = await next.Invoke();
            monitoredRequest.Monitor(_metrics,new TagsBuilder().AddSuccess(true));
            activity?.AddTag("success", true);
            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception,"Unhandled error caught while handling command {Command}",request);            
            monitoredRequest.Monitor(_metrics,new TagsBuilder().AddSuccess(false));
            activity?.AddTag("success", false);
            throw;
        }
    }
}