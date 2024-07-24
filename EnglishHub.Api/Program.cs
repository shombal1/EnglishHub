using System.Reflection;
using EnglishHub.Api.Authentication;
using EnglishHub.Api.Mapping;
using EnglishHub.Api.Middleware;
using EnglishHub.Api.Monitoring;
using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.DependencyInjection;
using EnglishHub.Storage.DependencyInjection;
using Serilog;
using Serilog.Filters;
using Serilog.Sinks.OpenSearch;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services
    .AddApiLogger(configuration,builder.Environment)
    .AddApiMetrics();

builder.Services
    .AddScoped<IAuthenticationTokenStorage, AuthenticationTokenStorage>()
    .Configure<AuthenticationConfiguration>(builder.Configuration.GetSection("Authentication").Bind);

builder.Services
    .AddForumDomain()
    .AddForumStorage(configuration.GetConnectionString("EnglishHubDbContext") ?? throw new ArgumentException());

builder.Services.AddAutoMapper(config => config.AddMaps(Assembly.GetAssembly(typeof(ApiProfile))));

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

app.UseMiddleware<ErrorHandingMiddleware>()
   .UseMiddleware<AuthenticationMiddleware>();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.Run();

namespace EnglishHub.Api
{
    public partial class Program {}
}