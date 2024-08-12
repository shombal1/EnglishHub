using EnglishHub.Domain.monitoring;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Sinks.Grafana.Loki;

namespace EnglishHub.Api.Monitoring;

public static class MonitoringServiceCollectionExtension
{
    public static IServiceCollection AddMonitoring(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var loggingLevelSwitch = new LoggingLevelSwitch
        {
            MinimumLevel = LogEventLevel.Information
        };
        services.AddSingleton(loggingLevelSwitch);

        services.AddLogging(a => a.AddSerilog(new LoggerConfiguration()
            .MinimumLevel.ControlledBy(loggingLevelSwitch)
            .Enrich.WithProperty("Application", "EnglishHub")
            .Enrich.WithProperty("Environment", environment.EnvironmentName)
            .WriteTo.Logger(lc => lc
                .Filter.ByExcluding(Matching.FromSource("Microsoft"))
                .WriteTo.GrafanaLoki(
                    configuration.GetConnectionString("Logs")!,
                    propertiesAsLabels: new[] { "Application", "Environment" }))
            .WriteTo.Logger(lc => lc
                .WriteTo.Console())
            .CreateLogger()));

        services.AddOpenTelemetry()
            .WithMetrics(metrics => metrics
                .AddMeter(DomainMetrics.DomainMeterName)
                .AddAspNetCoreInstrumentation()
                .AddPrometheusExporter())
            .WithTracing(tracer => tracer
                .AddSource(DomainMetrics.DomainActivityName)
                .AddAspNetCoreInstrumentation(configure =>
                {
                    configure.Filter += context =>
                        !context.Request.Path.Value!.Contains("metrics")
                        && !context.Request.Path.Value!.Contains("swagger");
                    configure.EnrichWithHttpResponse = (activity, response) =>
                        activity.AddTag("error", response.StatusCode >= 400);
                })
                .AddEntityFrameworkCoreInstrumentation(configure =>
                    configure.SetDbStatementForText = true)
                .ConfigureResource(r => r.AddService("EnglishHub"))
                .AddOtlpExporter(configure =>
                {
                    configure.Endpoint = new Uri(configuration.GetConnectionString("Tracing")!);
                    configure.Protocol = OtlpExportProtocol.HttpProtobuf;
                })
            );

        return services;
    }
}