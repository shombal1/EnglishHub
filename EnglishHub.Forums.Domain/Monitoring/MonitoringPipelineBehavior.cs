using System.Diagnostics;
using EnglishHub.Forums.Domain.UseCases.GetForum;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnglishHub.Forums.Domain.monitoring;

public class MonitoringPipelineBehavior<TRequest, TResponse>(
    ILogger<MonitoringPipelineBehavior<TRequest, TResponse>> logger,
    DomainMetrics metrics)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not IMonitoredRequest monitoredRequest) return await next.Invoke();

        using Activity? activity = metrics.ActivitySource.StartActivity("usecase");
        activity?.AddTag("command", request.GetType().Name);
        
        try
        {
            var result = await next.Invoke();
            monitoredRequest.Monitor(metrics,new TagsBuilder().AddSuccess(true));
            activity?.AddTag("success", true);
            return result;
        }
        catch (Exception exception)
        {
            logger.LogError(exception,"Unhandled error caught while handling command {Command}",request);            
            monitoredRequest.Monitor(metrics,new TagsBuilder().AddSuccess(false));
            activity?.AddTag("success", false);
            throw;
        }
    }
}