using System.Reflection;
using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.UseCases.CreateForum;
using EnglishHub.Domain.UseCases.CreateTopic;
using EnglishHub.Domain.UseCases.GetForum;
using EnglishHub.Domain.UseCases.GetTopic;
using EnglishHub.Domain.UseCases.SignIn;
using EnglishHub.Domain.UseCases.SignOn;
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
            .AddScoped<ISignInStorage, SignInStorage>()
            .AddScoped<IAuthenticationServiceStorage,AuthenticationServiceStorage>();

        service.AddDbContextPool<EnglishHubDbContext>(
            options => { options.UseNpgsql(dbConnectionStringPostgres); });

        service.AddMemoryCache();

        service.AddAutoMapper(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(EnglishHubDbContext))));

        return service;
    }
}