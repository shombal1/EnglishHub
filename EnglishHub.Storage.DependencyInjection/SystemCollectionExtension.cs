using System.Reflection;
using EnglishHub.Domain.UseCases.CreateTopicUseCase;
using EnglishHub.Domain.UseCases.GetForumUseCase;
using EnglishHub.Domain.UseCases.GetTopicUseCase;
using EnglishHub.Storage.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishHub.Storage.DependencyInjection;

public static class SystemCollectionExtension
{
    public static IServiceCollection AddForumStorage(this IServiceCollection service, string dbConnectionStringPostgres)
    {
        service
            .AddScoped<IGetForumStorage, GetForumStorage>()
            .AddScoped<ICreateTopicStorage, CreateCreateTopicStorage>()
            .AddScoped<IGetTopicStorage, GetTopicStorage>();

        service.AddDbContextPool<EnglishHubDbContext>(
            options => { options.UseNpgsql(dbConnectionStringPostgres); });

        service.AddMemoryCache();

        service.AddAutoMapper(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(EnglishHubDbContext))));

        return service;
    }
}