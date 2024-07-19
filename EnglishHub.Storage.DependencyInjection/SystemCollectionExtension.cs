using System.Reflection;
using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.UseCases.CreateForumUseCase;
using EnglishHub.Domain.UseCases.CreateTopicUseCase;
using EnglishHub.Domain.UseCases.GetForumUseCase;
using EnglishHub.Domain.UseCases.GetTopicUseCase;
using EnglishHub.Domain.UseCases.SignInUseCase;
using EnglishHub.Domain.UseCases.SignOnUseCase;
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
            .AddScoped<IGetTopicStorage, GetTopicStorage>()
            .AddScoped<ICreateForumStorage, CreateForumStorage>()
            .AddScoped<ISignOnStorage, SignOnStorage>()
            .AddScoped<ISignInStorage, SignInStorage>();

        service.AddDbContextPool<EnglishHubDbContext>(
            options => { options.UseNpgsql(dbConnectionStringPostgres); });

        service.AddMemoryCache();

        service.AddAutoMapper(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(EnglishHubDbContext))));

        return service;
    }
}