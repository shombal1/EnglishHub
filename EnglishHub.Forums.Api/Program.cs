using System.Reflection;
using Confluent.Kafka;
using EnglishHub.Forums.Api;
using EnglishHub.Forums.Api.Authentication;
using EnglishHub.Forums.Api.Mapping;
using EnglishHub.Forums.Api.Middleware;
using EnglishHub.Forums.Api.Monitoring;
using EnglishHub.Forums.Domain.Authentication;
using EnglishHub.Forums.Domain.DependencyInjection;
using EnglishHub.Forums.Storage.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddMonitoring(configuration,builder.Environment);

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

builder.Services.AddSingleton(new ConsumerBuilder<byte[], byte[]>(new ConsumerConfig()
{
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest,
    EnableAutoCommit = false,
    GroupId = "EnglishHub.test"
}).Build());
builder.Services.AddHostedService<TestReadKafka>();
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

app.UseRouting();
app.Run();

namespace EnglishHub.Forums.Api
{
    public partial class Program {}
}