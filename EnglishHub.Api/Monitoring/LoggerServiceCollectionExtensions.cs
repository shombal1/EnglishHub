using Serilog;
using Serilog.Filters;
using Serilog.Sinks.OpenSearch;

namespace EnglishHub.Api.Monitoring;

public static class LoggerServiceCollectionExtensions
{
    public static IServiceCollection AddApiLogger(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddLogging(a => a.AddSerilog(new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.WithProperty("Application", "EnglishHub")
            .Enrich.WithProperty("Environment", environment.EnvironmentName)
            .WriteTo.Logger(lc => lc
                .Filter.ByExcluding(Matching.FromSource("Microsoft"))
                .WriteTo.OpenSearch(
                    new OpenSearchSinkOptions(
                        new Uri(configuration.GetSection("Logs")["Uri"] ?? throw new ArgumentException()))
                    {
                        ModifyConnectionSettings = x => 
                            x.BasicAuthentication(
                                configuration.GetSection("Logs")["Username"],
                                configuration.GetSection("Logs")["Password"]),
                        IndexFormat = "forum-logs-{0:yyyy.MM.dd}",
                    }))
            .WriteTo.Logger(lc => lc
                .WriteTo.Console())
            .CreateLogger()));
        
        return services;
    }
}