using System.Reflection;
using EnglishHub.Forums.Domain;
using EnglishHub.Forums.Domain.Authentication;
using EnglishHub.Forums.Domain.UseCases;
using EnglishHub.Forums.Domain.UseCases.CreateForum;
using EnglishHub.Forums.Domain.UseCases.CreateTopic;
using EnglishHub.Forums.Domain.UseCases.GetForum;
using EnglishHub.Forums.Domain.UseCases.GetTopic;
using EnglishHub.Forums.Domain.UseCases.SignIn;
using EnglishHub.Forums.Domain.UseCases.SignOn;
using EnglishHub.Forums.Domain.UseCases.SignOut;
using EnglishHub.Storage;
using EnglishHub.Storage.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishHub.Forums.Storage.DependencyInjection;

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
            .AddScoped<ISignOutStorage,SignOutStorage>()
            .AddScoped<IAuthenticationServiceStorage,AuthenticationServiceStorage>()
            .AddScoped<IDomainEventStorage,DomainEventStorage>();
        
        service.AddDbContextPool<EnglishHubDbContext>(
            options => { options.UseNpgsql(dbConnectionStringPostgres); });

        service.AddMemoryCache();

        service.AddAutoMapper(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(EnglishHubDbContext))));

        service.AddSingleton(TimeProvider.System);

        service.AddSingleton<IUnitOfWork, UnitOfWork>();
        
        return service;
    }
}