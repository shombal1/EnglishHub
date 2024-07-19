using System.Reflection;
using EnglishHub.Authentication;
using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.DependencyInjection;
using EnglishHub.Middleware;
using EnglishHub.Storage.DependencyInjection;
using Serilog;
using Serilog.Filters;
using Serilog.Sinks.OpenSearch;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddLogging(a => a.AddSerilog(new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.WithProperty("Application", "EnglishHub")
    .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
    .WriteTo.Logger(lc => lc
        .Filter.ByExcluding(Matching.FromSource("Microsoft"))
        .WriteTo.OpenSearch(
            new OpenSearchSinkOptions(
                new Uri(configuration.GetConnectionString("Logs") ?? throw new ArgumentException()))
            {
                ModifyConnectionSettings = x => x.BasicAuthentication("admin", "OJxZer7nM1"),
                IndexFormat = "forum-logs-{0:yyyy.MM.dd}",
            }))
    .WriteTo.Logger(lc => lc
        .WriteTo.Console())
    .CreateLogger()));

builder.Services
    .AddScoped<IAuthenticationTokenStorage, AuthenticationTokenStorage>()
    .Configure<AuthenticationConfiguration>(builder.Configuration.GetSection("Authentication").Bind);

builder.Services
    .AddForumDomain()
    .AddForumStorage(configuration.GetConnectionString("EnglishHubDbContext") ?? throw new ArgumentException());

builder.Services.AddAutoMapper(config => config.AddMaps(Assembly.GetEntryAssembly()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandingMiddleware>();

app.Run();

public partial class Program {}