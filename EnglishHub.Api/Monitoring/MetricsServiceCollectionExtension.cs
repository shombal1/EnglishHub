using OpenTelemetry.Metrics;

namespace EnglishHub.Api.Monitoring;

public static class MetricsServiceCollectionExtension
{
    public static IServiceCollection AddApiMetrics(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithMetrics(metrics => metrics
                .AddPrometheusExporter()
                .AddAspNetCoreInstrumentation()
                .AddView(
                    instrumentName: "http.server.request.duration",
                    new ExplicitBucketHistogramConfiguration() { Boundaries = new double[] {0,1,2,5, 10, 20 } })
            );
        
        return services;
    }
}